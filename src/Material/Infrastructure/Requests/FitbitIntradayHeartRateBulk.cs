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
    /// The Get Heart Rate Time Series endpoint returns time series data for a specific date
    /// </summary>
    [ServiceType(typeof(Fitbit))]
	public partial class FitbitIntradayHeartRateBulk : OAuthRequest              
	{
        public override String Host => "https://api.fitbit.com";
        public override String Path => "/1/user/-/activities/heart/date/{date}/1d/1min.json";
        public override String HttpMethod => "GET";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.Json };
        public override List<String> RequiredScopes => new List<String> { "heartrate" };
        /// <summary>
        /// The date, in the format yyyy-MM-dd or today
        /// </summary>
        [Name("date")]
        [ParameterType(RequestParameterType.Path)]
        [Required()]
        [DateTimeFormatter("yyyy-MM-dd")]
        public  Nullable<DateTime> Date { get; set; }
	}
}
