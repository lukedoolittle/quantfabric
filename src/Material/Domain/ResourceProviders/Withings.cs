﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Material.Domain.Credentials;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Material.Framework.Enums;
using Material.Framework.Metadata;
using Material.Domain.Core;

namespace Material.Domain.ResourceProviders
{     
    /// <summary>
    /// Withings API 1.0.1
    /// </summary>
    [CredentialType(typeof(OAuth1Credentials))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class Withings  : OAuth1ResourceProvider 
    {
        public override Uri RequestUrl { get; } = new Uri("https://oauth.withings.com/account/request_token");
        public override Uri AuthorizationUrl { get; } = new Uri("https://oauth.withings.com/account/authorize");
        public override Uri TokenUrl { get; } = new Uri("https://oauth.withings.com/account/access_token");
        public override HttpParameterType ParameterType { get; } = HttpParameterType.Querystring;
        public override bool SupportsCustomUrlScheme { get; } = true;
    }
}
