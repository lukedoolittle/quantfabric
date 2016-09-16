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
using Material.Infrastructure.Requests;
using Material.Enums;
using Material.Infrastructure;
using Foundations.Attributes;

namespace Material.Infrastructure.Requests
{     
    /// <summary>
    /// A user represents a person on Facebook
    /// </summary>
    [ServiceType(typeof(Facebook))]
	public partial class FacebookUser : OAuthRequest              
	{
        public override String Host => "https://graph.facebook.com";
        public override String Path => "/v2.7/me";
        public override String HttpMethod => "GET";
        public override List<String> RequiredScopes => new List<String> { "email" };
        /// <summary>
        /// Optional fields to request
        /// </summary>
        [Name("fields")]
        [ParameterType(RequestParameterTypeEnum.Query)]
        public  FacebookUserFieldsEnum Fields { get; set; } = FacebookUserFieldsEnum.Email;
	}
    public enum FacebookUserFieldsEnum
    {
        [Description("email")] Email,
    }
}
