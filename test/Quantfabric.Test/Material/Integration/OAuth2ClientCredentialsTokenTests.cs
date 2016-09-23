﻿using Material.Contracts;
using Material.Infrastructure.Credentials;
using Material.Infrastructure.OAuth;
using Material.Infrastructure.ProtectedResources;
using Quantfabric.Test.Helpers;
using Quantfabric.Test.TestHelpers;
using Xunit;

namespace Quantfabric.Test.Material.Integration
{
    public class OAuth2ClientCredentialsTokenTests
    {

        private readonly AppCredentialRepository _appRepository;
        private readonly TokenCredentialRepository _tokenRepository;

        public OAuth2ClientCredentialsTokenTests()
        {
            _appRepository = new AppCredentialRepository(CallbackTypeEnum.Localhost);
            _tokenRepository = new TokenCredentialRepository(true);
        }


        //TODO: put in (exception) tests for other OAuth2 providers

        [Fact]
        public async void CanGetValidAccessTokenFromOmnitureClientCredentialsGrant()
        {
            var clientId = _appRepository.GetClientId<Omniture>();
            var clientSecret = _appRepository.GetClientSecret<Omniture>();

            var token = await new OAuth2Client<Omniture>(
                    clientId, 
                    clientSecret)
                .GetCredentialsAsync()
                .ConfigureAwait(false);

            Assert.True(IsValidToken(token));

            _tokenRepository.PutToken<Omniture, OAuth2Credentials>(token);
        }

        private bool IsValidToken(OAuth2Credentials token)
        {
            return token != null &&
                   token.AccessToken != string.Empty &&
                   token.TokenName != string.Empty;
        }
    }
}
