﻿using System;
using System.Collections.Generic;
using Material.Domain.Core;
using Material.Framework.Enums;

namespace Quantfabric.Test.Material.Mocks
{
    public class OAuth2ResourceProviderMock : OAuth2ResourceProvider
    {
        private readonly OAuth2ResourceProvider _provider;

        public int Port { get; } = TestUtilities.GetAvailablePort(30000);

        public OAuth2ResourceProviderMock(
            OAuth2ResourceProvider provider)
        {
            _provider = provider;
        }

        public override List<string> AvailableScopes => _provider.AvailableScopes;
        public override List<OAuth2FlowType> AllowedFlows => _provider.AllowedFlows;
        public override List<GrantType> AllowedGrantTypes => _provider.AllowedGrantTypes;
        public override List<OAuth2ResponseType> AllowedResponseTypes => _provider.AllowedResponseTypes;

        public override Uri AuthorizationUrl => _provider.AuthorizationUrl != null ? 
            new Uri($"http://localhost:{Port}{_provider.AuthorizationUrl.AbsolutePath}") : 
            null;

        public override Uri TokenUrl => _provider.TokenUrl != null ?
            new Uri($"http://localhost:{Port}{_provider.TokenUrl.AbsolutePath}") :
            null;
    }
}
