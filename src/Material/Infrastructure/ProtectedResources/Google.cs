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
    /// Google API 1
    /// </summary>
    [CredentialType(typeof(OAuth2Credentials))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class Google : OpenIdResourceProvider              
	{
        public override List<String> AvailableScopes => new List<String> { "https://www.googleapis.com/auth/gmail.readonly", "https://www.googleapis.com/auth/userinfo.email", "https://www.googleapis.com/auth/analytics.readonly", "openid", "openid email", "openid profile", "openid email profile" };
        public override List<OAuth2FlowType> AllowedFlows => new List<OAuth2FlowType> { OAuth2FlowType.AccessCode, OAuth2FlowType.Implicit };
        public override List<GrantType> AllowedGrantTypes => new List<GrantType> { GrantType.AuthCode, GrantType.RefreshToken };
        public override List<OAuth2ResponseType> AllowedResponseTypes => new List<OAuth2ResponseType> { OAuth2ResponseType.Code, OAuth2ResponseType.Token, OAuth2ResponseType.IdToken, OAuth2ResponseType.IdTokenToken };
        public override String TokenName => "Bearer";
        public override Uri AuthorizationUrl => new Uri("https://accounts.google.com/o/oauth2/auth");
        public override Uri TokenUrl => new Uri("https://accounts.google.com/o/oauth2/token");
        public override Boolean SupportsPkce => true;
        public override Boolean SupportsCustomUrlScheme => true;
        public override Uri OpenIdDiscoveryUrl => new Uri("https://accounts.google.com/.well-known/openid-configuration");
        public override List<String> ValidIssuers => new List<String> { "https://accounts.google.com", "accounts.google.com" };
	}
}
