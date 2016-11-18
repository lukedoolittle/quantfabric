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
    /// Yahoo Developer Network API 1
    /// </summary>
    [CredentialType(typeof(OAuth2Credentials))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class Yahoo : OpenIdResourceProvider              
	{
        public override List<String> AvailableScopes => new List<String> { "openid" };
        public override List<OAuth2FlowType> AllowedFlows => new List<OAuth2FlowType> { OAuth2FlowType.AccessCode, OAuth2FlowType.Implicit };
        public override List<GrantType> AllowedGrantTypes => new List<GrantType> { GrantType.AuthCode, GrantType.RefreshToken };
        public override List<OAuth2ResponseType> AllowedResponseTypes => new List<OAuth2ResponseType> { OAuth2ResponseType.Code, OAuth2ResponseType.Token, OAuth2ResponseType.IdToken, OAuth2ResponseType.IdTokenToken };
        public override String TokenName => "Bearer";
        public override Uri AuthorizationUrl => new Uri("https://api.login.yahoo.com/oauth2/request_auth");
        public override Uri TokenUrl => new Uri("https://api.login.yahoo.com/oauth2/get_token");
        public override Uri OpenIdDiscoveryUrl => new Uri("https://accounts.google.com/.well-known/openid-configuration");
	}
}
