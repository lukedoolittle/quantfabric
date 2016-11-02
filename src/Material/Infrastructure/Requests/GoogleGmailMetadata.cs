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
    /// Lists the messages in the user's mailbox
    /// </summary>
    [ServiceType(typeof(Google))]
	public partial class GoogleGmailMetadata : OAuthRequest              
	{
        public override String Host => "https://www.googleapis.com";
        public override String Path => "/gmail/v1/users/me/messages";
        public override String HttpMethod => "GET";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.Json };
        public override List<String> RequiredScopes => new List<String> { "https://www.googleapis.com/auth/gmail.readonly" };
        /// <summary>
        /// Only return messages matching the specified query
        /// </summary>
        [Name("q")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  String Q { get; set; }
        /// <summary>
        /// Maximum number of messages to return
        /// </summary>
        [Name("maxResults")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Int32> MaxResults { get; set; }
        /// <summary>
        /// Page token to retrieve a specific page of results in the list
        /// </summary>
        [Name("pageToken")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  String PageToken { get; set; }
        /// <summary>
        /// Include messages from SPAM and TRASH in the results
        /// </summary>
        [Name("includeSpamTrash")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Boolean> IncludeSpamTrash { get; set; }
        /// <summary>
        /// Only return messages with labels that match all of the specified label IDs
        /// </summary>
        [Name("labelIds")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  String LabelIds { get; set; }
	}
}
