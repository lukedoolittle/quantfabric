﻿using System;
using System.Threading.Tasks;
using Foundations.Extensions;
using Foundations.HttpClient.Enums;
using Material.Contracts;
using Material.Infrastructure;
using Material.Infrastructure.Credentials;
using Material.OAuth.Authorization;
using Material.OAuth.Callback;
using Material.OAuth.Facade;
using Material.OAuth.Security;

namespace Material.OAuth.Workflow
{
    public class OAuth2Web<TResourceProvider>
        where TResourceProvider : OAuth2ResourceProvider, new()
    {
        private readonly string _clientSecret;
        private readonly IOAuthFacade<OAuth2Credentials> _authFacade;
        private readonly IOAuthCallbackHandler<OAuth2Credentials> _callbackHandler;
        private readonly TResourceProvider _resourceProvider;

        /// <summary>
        /// Authorize a resource owner using the OAuth2 workflow with default security strategy
        /// </summary>
        /// <param name="clientSecret">The application's client secret</param>
        /// <param name="resourceProvider">Endpoint information for the resource provider</param>
        /// <param name="callbackHandler">Handles the authorization uris callback response</param>
        /// <param name="facade">Creates the authorization uri and exchanges code for an access token</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OAuth2Web(
            string clientSecret,
            TResourceProvider resourceProvider,
            IOAuthCallbackHandler<OAuth2Credentials> callbackHandler,
            IOAuthFacade<OAuth2Credentials> facade)
        {
            _clientSecret = clientSecret;
            _resourceProvider = resourceProvider;
            _callbackHandler = callbackHandler;
            _authFacade = facade;
        }

        /// <summary>
        /// Authorize a resource owner using the OAuth2 workflow with default security strategy
        /// </summary>
        /// <param name="clientId">The application's client Id</param>
        /// <param name="clientSecret">The application's client secret</param>
        /// <param name="callbackUrl">The application's registered callback url</param>
        /// <param name="strategy">The security strategy to use for "state" handling</param>
        /// <param name="resourceProvider">Endpoint information for the resource provider</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OAuth2Web(
            string clientId,
            string clientSecret,
            string callbackUrl,
            IOAuthSecurityStrategy strategy,
            TResourceProvider resourceProvider) : 
                this(
                    clientSecret, 
                    resourceProvider, 
                    new OAuth2CallbackHandler(
                        strategy,
                        OAuth2Parameter.State.EnumToString()), 
                    new OAuth2CodeAuthorizationFacade(
                        resourceProvider,
                        clientId,
                        clientSecret,
                        new Uri(callbackUrl),
                        new OAuth2AuthorizationAdapter(),
                        strategy))
        { }

        /// <summary>
        /// Authorize a resource owner using the OAuth2 workflow with default security strategy
        /// </summary>
        /// <param name="clientId">The application's client Id</param>
        /// <param name="clientSecret">The application's client secret</param>
        /// <param name="callbackUri">The application's registered callback url</param>
        /// <param name="resourceProvider">Endpoint information for the resource provider</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OAuth2Web(
            string clientId,
            string clientSecret,
            string callbackUri,
            TResourceProvider resourceProvider) : 
                this(
                    clientId, 
                    clientSecret,
                    callbackUri,
                    new OAuthSecurityStrategy(
                        new InMemoryCryptographicParameterRepository(),
                        TimeSpan.FromMinutes(OAuthConfiguration.SecurityParameterTimeoutInMinutes)), 
                    resourceProvider)
        {}

        /// <summary>
        /// Authorize a resource owner using the OAuth2 workflow with default security strategy
        /// </summary>
        /// <param name="clientId">The application's client Id</param>
        /// <param name="clientSecret">The application's client secret</param>
        /// <param name="callbackUrl">The application's registered callback url</param>
        /// <param name="strategy">The security strategy to use for "state" handling</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OAuth2Web(
            string clientId,
            string clientSecret,
            string callbackUrl,
            IOAuthSecurityStrategy strategy) :
                this(
                    clientId,
                    clientSecret, 
                    callbackUrl, 
                    strategy, 
                    new TResourceProvider())
        {}

        /// <summary>
        /// Authorize a resource owner using the OAuth2 workflow with default security strategy
        /// </summary>
        /// <param name="clientId">The application's client Id</param>
        /// <param name="clientSecret">The application's client secret</param>
        /// <param name="callbackUri">The application's registered callback url</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OAuth2Web(
            string clientId,
            string clientSecret,
            string callbackUri) :
                this(
                    clientId,
                    clientSecret,
                    callbackUri,
                    new OAuthSecurityStrategy(
                        new InMemoryCryptographicParameterRepository(),
                        TimeSpan.FromMinutes(
                            OAuthConfiguration.SecurityParameterTimeoutInMinutes)),
                    new TResourceProvider())
        {}



        /// <summary>
        /// Gets the authorization uri for the Resource Owner to enter his/her credentials
        /// </summary>
        /// <param name="userId">Resource owner's Id</param>
        /// <returns>Authorization uri</returns>
        public Task<Uri> GetAuthorizationUriAsync(string userId)
        {
            return _authFacade.GetAuthorizationUriAsync(userId);
        }

        /// <summary>
        /// Exchanges callback uri for access token credentials
        /// </summary>
        /// <param name="userId">Resource owner's Id</param>
        /// <param name="responseUri">The received callback uri</param>
        /// <returns>Access token credentials</returns>
        public Task<OAuth2Credentials> GetAccessTokenAsync(
            Uri responseUri,
            string userId)
        {
            var result = _callbackHandler
                        .ParseAndValidateCallback(
                            responseUri, 
                            userId);

            return _authFacade.GetAccessTokenAsync(
                result, 
                _clientSecret);
        }

        /// <summary>
        /// Adds scope to be requested with OAuth2 Authorize
        /// </summary>
        /// <typeparam name="TRequest">The request type scope is needed for</typeparam>
        /// <returns>The current instance</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public OAuth2Web<TResourceProvider> AddScope<TRequest>()
            where TRequest : OAuthRequest, new()
        {
            _resourceProvider.AddRequestScope<TRequest>();

            return this;
        }

        /// <summary>
        /// Adds scope to be requested with OAuth2 authorization
        /// </summary>
        /// <param name="scope">The scope to request</param>
        /// <returns>The current instance</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public OAuth2Web<TResourceProvider> AddScope(string scope)
        {
            _resourceProvider.AddRequestScope(scope);

            return this;
        }
    }
}