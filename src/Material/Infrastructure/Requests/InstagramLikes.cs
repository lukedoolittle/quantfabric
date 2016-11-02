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
    /// Get the list of recent media liked by the owner of the access_token
    /// </summary>
    [ServiceType(typeof(Instagram))]
	public partial class InstagramLikes : OAuthRequest              
	{
        public override String Host => "https://api.instagram.com";
        public override String Path => "/v1/users/self/media/liked";
        public override String HttpMethod => "GET";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.Json };
        public override List<String> RequiredScopes => new List<String> { "public_content" };
        /// <summary>
        /// Count of media to return
        /// </summary>
        [Name("count")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Int32> Count { get; set; }
        /// <summary>
        /// Return media liked before this id
        /// </summary>
        [Name("max_like_id")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  String MaxLikeId { get; set; }
	}
}
