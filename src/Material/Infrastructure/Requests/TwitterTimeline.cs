﻿using Material.Metadata;
using Material.Infrastructure.ProtectedResources;
using System;
using Material.Enums;
using Material.Infrastructure;

namespace Material.Infrastructure.Requests
{     
    /// <summary>
    /// Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow
    /// </summary>
    [ServiceType(typeof(Twitter))]
	public partial class TwitterTimeline : OAuthRequest              
	{
        public override String Host => "https://api.twitter.com";
        public override String Path => "/1.1/statuses/home_timeline.json";
        public override String HttpMethod => "GET";
        /// <summary>
        /// Returns results with an ID greater than (that is, more recent than) the specified ID
        /// </summary>
        [Name("since_id")]
        [ParameterType(RequestParameterTypeEnum.Query)]
        public  String SinceId { get; set; }
        /// <summary>
        /// Returns results with an ID less than (that is, older than) or equal to the specified ID
        /// </summary>
        [Name("max_id")]
        [ParameterType(RequestParameterTypeEnum.Query)]
        public  String MaxId { get; set; }
        /// <summary>
        /// Specifies the number of records to retrieve. Must be less than or equal to 100
        /// </summary>
        [Name("count")]
        [ParameterType(RequestParameterTypeEnum.Query)]
        public  Nullable<Int32> Count { get; set; }
        /// <summary>
        /// When true, each tweet returned in a timeline will include a user object including only the status authors numerical ID
        /// </summary>
        [Name("trim_user")]
        [ParameterType(RequestParameterTypeEnum.Query)]
        public  Nullable<Boolean> TrimUser { get; set; }
        /// <summary>
        /// The tweet entities node will not be included when set to false
        /// </summary>
        [Name("include_entities")]
        [ParameterType(RequestParameterTypeEnum.Query)]
        public  Nullable<Boolean> IncludeEntities { get; set; }
        /// <summary>
        /// This parameter will prevent replies from appearing in the returned timeline
        /// </summary>
        [Name("exclude_replies")]
        [ParameterType(RequestParameterTypeEnum.Query)]
        public  Nullable<Boolean> ExcludeReplies { get; set; }
	}
}
