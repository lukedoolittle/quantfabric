﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Material.Metadata;
using System;
using Material.Infrastructure.Credentials;
using System.Collections.Generic;
using Foundations.HttpClient.Enums;
using Material.Infrastructure;
using System.CodeDom.Compiler;

namespace Material.Infrastructure.ProtectedResources
{     
    /// <summary>
    /// Foursquare and Swarm API 2
    /// </summary>
    [CredentialType(typeof(OAuth2Credentials))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class Foursquare : OAuth2ResourceProvider              
	{
        public override List<String> AvailableScopes => new List<String>();
        public override List<OAuth2FlowType> AllowedFlows => new List<OAuth2FlowType> { OAuth2FlowType.AccessCode, OAuth2FlowType.Implicit };
        public override List<GrantType> AllowedGrantTypes => new List<GrantType> { GrantType.AuthCode };
        public override List<OAuth2ResponseType> AllowedResponseTypes => new List<OAuth2ResponseType> { OAuth2ResponseType.Code, OAuth2ResponseType.Token };
        public override String TokenName => "oauth_token";
        public override Uri AuthorizationUrl => new Uri("https://foursquare.com/oauth2/authorize");
        public override Uri TokenUrl => new Uri("https://foursquare.com/oauth2/access_token");
        public override Boolean SupportsCustomUrlScheme => true;
	}
}
