﻿using Material.Metadata;
using Material.Infrastructure.ProtectedResources;
using System;
using System.Collections.Generic;
using Material.Enums;
using Material.Infrastructure;

namespace Material.Infrastructure.Requests
{     
    /// <summary>
    /// The Get Heart Rate Time Series endpoint endpoint returns time series data for a specific time range. NOTE THAT THIS TIME RANGE CANNOT EXCEED 24 HOURS
    /// </summary>
    [ServiceType(typeof(Fitbit))]
	public partial class FitbitIntradayHeartRate : OAuthRequest              
	{
        public override String Host => "https://api.fitbit.com";
        public override String Path => "/1/user/-/activities/heart/date/{startdate}/{enddate}/1min/time/{starttime}/{endtime}.json";
        public override String HttpMethod => "GET";
        public override List<String> RequiredScopes => new List<String> { "heartrate" };
        /// <summary>
        /// The start date, in the format yyyy-MM-dd or today
        /// </summary>
        [Name("startdate")]
        [ParameterType(RequestParameterTypeEnum.Path)]
        [Format("yyyy-MM-dd")]
        public  Nullable<DateTime> Startdate { get; set; }
        /// <summary>
        /// The end date, in the format yyyy-MM-dd or today
        /// </summary>
        [Name("enddate")]
        [ParameterType(RequestParameterTypeEnum.Path)]
        [Format("yyyy-MM-dd")]
        public  Nullable<DateTime> Enddate { get; set; }
        /// <summary>
        /// The start of the period, in the format HH:mm
        /// </summary>
        [Name("starttime")]
        [ParameterType(RequestParameterTypeEnum.Path)]
        [Format("HH:mm")]
        public  Nullable<DateTime> Starttime { get; set; }
        /// <summary>
        /// The end of the period, in the format HH:mm
        /// </summary>
        [Name("endtime")]
        [ParameterType(RequestParameterTypeEnum.Path)]
        [Format("HH:mm")]
        public  Nullable<DateTime> Endtime { get; set; }
	}
}
