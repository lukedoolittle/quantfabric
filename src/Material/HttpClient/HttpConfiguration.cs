﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using Material.Framework.Collections;
using Material.Framework.Enums;
using Material.Framework.Extensions;
using Material.Framework.Serialization;
using Material.HttpClient.Content;

namespace Material.HttpClient
{
    public static class HttpConfiguration
    {
        /// <summary>
        /// Creates the default HttpClient instance internal to HttpRequestBuilder
        /// </summary>
        public static Func<RequestParameters, HttpClientSet> HttpClientFactory { get; set; } = //-V3070
            (request) => ClientPool.GetClient(request, MessageHandlerFactory, ClientHashFactory);

        /// <summary>
        /// Determines the hash value for the HttpClient pooler
        /// </summary>
        public static Func<RequestParameters, string> ClientHashFactory { get; set; } =
            parameters => parameters.Address.NonPath();

        /// <summary>
        /// Creates the default HttpClientHandler for the HttpClient instance internal to HttpRequestBuilder
        /// </summary>
        public static Func<RequestParameters, HttpClientHandler> MessageHandlerFactory { get; set; } =
            (parameters) => new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                AllowAutoRedirect = parameters.AllowHttpRedirect,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

        /// <summary>
        /// Defines any default configurations applied to all HttpRequestBuilder instances
        /// </summary>
        public static Action<HttpRequestBuilder> DefaultBuilderSetup { get; set; } = 
            builder =>
            {
                builder.AcceptsDecompressionEncoding(DecompressionMethods.GZip);
                builder.AcceptsDecompressionEncoding(DecompressionMethods.Deflate);
            };

        /// <summary>
        /// Maps all request and response media types to their corresponding serializers
        /// </summary>
        public static IDictionary<MediaType, ISerializer> ContentSerializers { get; } =
            new DefaultingDictionary<MediaType, ISerializer>(type =>
            {
                throw new SerializationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        StringResources.UnknownMediaType,
                        type));
            })
            {
                {MediaType.Json, new JsonSerializer()},
                {MediaType.TextJson, new JsonSerializer()},
                {MediaType.TextXJson, new JsonSerializer()},
                {MediaType.Xml, new XmlSerializer()},
                {MediaType.TextXml, new XmlSerializer()},
                {MediaType.Html, new HtmlSerializer()},
                {MediaType.Text, new HtmlSerializer()},
                {MediaType.Form, new HtmlSerializer()},
                {MediaType.Javascript, new JsonSerializer()},
                {MediaType.RunkeeperFitnessActivity, new JsonSerializer()}
            };

        /// <summary>
        /// Default media type if none is given and response does not contain a Content-Type header
        /// </summary>
        public static MediaType DefaultResponseMediaType { get; set; } = MediaType.Json;

        /// <summary>
        /// Timeout for HTTP requests
        /// </summary>
        public static int RequestTimeoutInSeconds { get; set; } = 30;
    }


    public static class ClientPool
    {
        private static readonly object _syncLock = new object();
        private static readonly ConcurrentDictionary<string, Tuple<System.Net.Http.HttpClient, HttpClientHandler>> _clients =
            new ConcurrentDictionary<string, Tuple<System.Net.Http.HttpClient, HttpClientHandler>>();

        //Per discussions http://byterot.blogspot.com/2016/07/singleton-httpclient-dns.html IDisposable does not need implementing
        //with HttpClient
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static HttpClientSet GetClient(
            RequestParameters request,
            Func<RequestParameters, HttpClientHandler> clientHandlerFactory,
            Func<RequestParameters, string> clientHashFactory)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (clientHandlerFactory == null) throw new ArgumentNullException(nameof(clientHandlerFactory));
            if (clientHashFactory == null) throw new ArgumentNullException(nameof(clientHashFactory));

            var key = clientHashFactory(request);

            lock (_syncLock)
            {
                if (!_clients.ContainsKey(key))
                {
                    var handler = clientHandlerFactory(request);
                    var client = new System.Net.Http.HttpClient(handler)
                    {
                        Timeout = TimeSpan.FromSeconds(
                            HttpConfiguration.RequestTimeoutInSeconds)
                    };
                    _clients.AddOrUpdate(
                        key,
                        new Tuple<System.Net.Http.HttpClient, HttpClientHandler>(client, handler),
                        (s, pair) => { throw new NotSupportedException(); });
                }

                return new HttpClientSet(
                    _clients[key].Item1, 
                    _clients[key].Item2,
                    new HttpRequestMessage
                    {
                        Method = request.Method,
                        RequestUri = request.Address,
                        Content = request.Content,
                    });
            }
        }
    }
}
