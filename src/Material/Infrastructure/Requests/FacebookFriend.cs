﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Material.Metadata;
using Material.Infrastructure.ProtectedResources;
using System;
using System.Collections.Generic;
using Foundations.Enums;
using Material.Enums;
using Material.Metadata.Formatters;
using Material.Infrastructure;

namespace Material.Infrastructure.Requests
{     
    /// <summary>
    /// The person's friends
    /// </summary>
    [ServiceType(typeof(Facebook))]
	public partial class FacebookFriend : OAuthRequest              
	{
        public override String Host => "https://graph.facebook.com";
        public override String Path => "/v2.7/me/friends";
        public override String HttpMethod => "GET";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.Json };
        public override List<String> RequiredScopes => new List<String> { "user_friends" };
        /// <summary>
        ///  A Unix timestamp or strtotime data value that points to the start of the range of time-based data
        /// </summary>
        [Name("since")]
        [ParameterType(RequestParameterType.Query)]
        [DateTimeFormatter("yyyy-MM-ddTHH:mm:sszzz")]
        public  Nullable<DateTime> Since { get; set; }
        /// <summary>
        ///  A Unix timestamp or strtotime data value that points to the end of the range of time-based data
        /// </summary>
        [Name("until")]
        [ParameterType(RequestParameterType.Query)]
        [DateTimeFormatter("yyyy-MM-ddTHH:mm:sszzz")]
        public  Nullable<DateTime> Until { get; set; }
        /// <summary>
        /// This is the number of individual objects that are returned in each page
        /// </summary>
        [Name("limit")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Int32> Limit { get; set; }
        /// <summary>
        /// Aggregated information about the edge, such as counts
        /// </summary>
        [Name("summary")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  String Summary { get; set; } = "total_count";
	}
}
