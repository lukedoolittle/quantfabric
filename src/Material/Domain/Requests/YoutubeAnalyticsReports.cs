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
using Material.Domain.Requests;
using Material.Domain.Core;
using System.CodeDom.Compiler;

namespace Material.Domain.Requests
{     
    /// <summary>
    /// Retrieves a YouTube Analytics report
    /// </summary>
    [ServiceType(typeof(Youtube))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class YoutubeAnalyticsReports : OAuthRequest              
	{
        public override String Host => "https://www.googleapis.com";
        public override String Path => "/youtube/analytics/v1/reports";
        public override String HttpMethod => "GET";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.Json };
        public override List<HttpStatusCode> ExpectedStatusCodes => new List<HttpStatusCode> { HttpStatusCode.OK };
        public override List<String> RequiredScopes => new List<String> { "https://www.googleapis.com/auth/yt-analytics.readonly" };
        /// <summary>
        /// The maximum number of rows to include in the response
        /// </summary>
        [Name("max-results")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Int32> MaxResults { get; set; }
        /// <summary>
        /// The end date for fetching YouTube Analytics data. The value should be in YYYY-MM-DD format.
        /// </summary>
        [Name("end-date")]
        [ParameterType(RequestParameterType.Query)]
        [Required()]
        [DateTimeFormatter("yyyy-MM-dd")]
        public  Nullable<DateTime> EndDate { get; set; }
        /// <summary>
        /// Identifies the YouTube channel or content owner for which you are retrieving YouTube Analytics data. channel==MINE, channel==CHANNEL_ID,contentOwner==OWNER_NAME
        /// </summary>
        [Name("ids")]
        [ParameterType(RequestParameterType.Query)]
        [Required()]
        [DefaultFormatter()]
        public  String Ids { get; set; }
        /// <summary>
        /// A comma-separated list of YouTube Analytics metrics, such as views or likes,dislikes
        /// </summary>
        [Name("metrics")]
        [ParameterType(RequestParameterType.Query)]
        [Required()]
        [DefaultFormatter()]
        public  String Metrics { get; set; }
        /// <summary>
        /// The start date for fetching YouTube Analytics data. The value should be in YYYY-MM-DD format.
        /// </summary>
        [Name("start-date")]
        [ParameterType(RequestParameterType.Query)]
        [Required()]
        [DateTimeFormatter("yyyy-MM-dd")]
        public  Nullable<DateTime> StartDate { get; set; }
        /// <summary>
        /// A comma-separated list of YouTube Analytics dimensions, such as video or ageGroup,gender
        /// </summary>
        [Name("dimensions")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  String Dimensions { get; set; }
        /// <summary>
        /// A list of filters that should be applied when retrieving YouTube Analytics data
        /// </summary>
        [Name("filters")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  String Filters { get; set; }
        /// <summary>
        /// The data format for the API response
        /// </summary>
        [Name("alt")]
        [ParameterType(RequestParameterType.Query)]
        [EnumFormatter()]
        public  Nullable<YoutubeAnalyticsReportsAlt> Alt { get; set; } = YoutubeAnalyticsReportsAlt.Json;
        /// <summary>
        /// Returns response with indentations and line breaks
        /// </summary>
        [Name("prettyPrint")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Boolean> PrettyPrint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Name("quotaUser")]
        [ParameterType(RequestParameterType.Query)]
        [Required()]
        [DefaultFormatter()]
        public  String QuotaUser { get; set; } = "undefined";
	}
	
	[GeneratedCode("T4Toolbox", "14.0")]
    public enum YoutubeAnalyticsReportsAlt
    {
        [Description("json")] Json,
        [Description("csv")] Csv,
    }
}
