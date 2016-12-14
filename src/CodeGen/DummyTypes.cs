﻿using System;
using System.Collections.Generic;
using Foundations.HttpClient.Enums;
using Material.Infrastructure;

namespace CodeGen
{
    public class DummyOAuth2ResourceProvider : OAuth2ResourceProvider
    {
        public override Uri TokenUrl { get; }
        public override List<string> AvailableScopes { get; }
        public override List<OAuth2FlowType> AllowedFlows { get; }
        public override List<GrantType> AllowedGrantTypes { get; }
        public override bool SupportsPkce { get; }
        public override bool SupportsCustomUrlScheme { get; }
    }

    public class DummyOAuth1ResourceProvider : OAuth1ResourceProvider
    {
        public override Uri RequestUrl { get; }
        public override Uri AuthorizationUrl { get; }
        public override Uri TokenUrl { get; }
        public override HttpParameterType ParameterType { get; }
        public override bool SupportsCustomUrlScheme { get; }
    }

    public class DummyOpenIdResourceProvider : OpenIdResourceProvider
    {
        public override Uri TokenUrl { get; }
        public override List<string> AvailableScopes { get; }
        public override List<OAuth2FlowType> AllowedFlows { get; }
        public override List<GrantType> AllowedGrantTypes { get; }
        public override Uri OpenIdDiscoveryUrl { get; }
        public override List<string> ValidIssuers { get; }
    }
}
