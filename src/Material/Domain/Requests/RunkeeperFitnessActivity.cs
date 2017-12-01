﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Material.Framework.Metadata;
using Material.Domain.ResourceProviders;
using System;
using System.Collections.Generic;
using Material.Framework.Enums;
using System.Net;
using Material.Framework.Metadata.Formatters;
using Material.Domain.Core;
using System.CodeDom.Compiler;

namespace Material.Domain.Requests
{     
    /// <summary>
    /// Fitness activities appear in a user’s fitness feed on the Runkeeper website
    /// </summary>
    [ServiceType(typeof(Runkeeper))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class RunkeeperFitnessActivity : OAuthRequest              
	{
        public override String Host => "https://api.runkeeper.com";
        public override String Path => "/fitnessActivities";
        public override String HttpMethod => "GET";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.RunkeeperFitnessActivity };
        public override List<HttpStatusCode> ExpectedStatusCodes => new List<HttpStatusCode> { HttpStatusCode.OK };
        public override List<String> RequiredScopes => new List<String>();
        /// <summary>
        /// Starting ime scope for the request
        /// </summary>
        [Name("noEarlierThan")]
        [ParameterType(RequestParameterType.Query)]
        [DateTimeFormatter("yyyy-MM-dd")]
        public  Nullable<DateTime> NoEarlierThan { get; set; }
        /// <summary>
        /// Ending time scope for the request
        /// </summary>
        [Name("noLaterThan")]
        [ParameterType(RequestParameterType.Query)]
        [DateTimeFormatter("yyyy-MM-dd")]
        public  Nullable<DateTime> NoLaterThan { get; set; }
        /// <summary>
        /// The pageSize query parameter controls how many entries are returned per page.
        /// </summary>
        [Name("pageSize")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Int32> PageSize { get; set; }
        /// <summary>
        /// The page number to request
        /// </summary>
        [Name("page")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Int32> Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Name("If-Modified-Since")]
        [ParameterType(RequestParameterType.Header)]
        [DateTimeFormatter("yyyy-MM-ddTHH:mm:ss")]
        public  Nullable<DateTime> IfModifiedSince { get; set; }
	}
}