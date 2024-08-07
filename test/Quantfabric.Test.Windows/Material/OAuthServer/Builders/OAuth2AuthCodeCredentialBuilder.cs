﻿using System;
using Material.Domain.Credentials;
using System.Collections.Generic;
using System.Linq;
using Material.Framework.Enums;
using Material.HttpClient.Cryptography;
using Material.HttpClient.Cryptography.Enums;
using Quantfabric.Test.Material.OAuthServer.Requests;
using Quantfabric.Test.Material.OAuthServer.Tokens;

namespace Quantfabric.Test.Material.OAuthServer.Builders
{
    public class OAuth2AuthCodeCredentialBuilder : 
        ICredentialBuilder<OAuth2Credentials, OAuth2TokenRequest>
    {
        private readonly string _clientSecret;
        private readonly Uri _redirectUri;
        private readonly IDictionary<string, List<OAuth2Token>> _tokens;
        private readonly int _msUntilExpiry;

        public OAuth2AuthCodeCredentialBuilder(
            string clientSecret, 
            Uri redirectUri,
            IDictionary<string, List<OAuth2Token>> tokens, 
            int msUntilExpiry = 0)
        {
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;
            _tokens = tokens;
            _msUntilExpiry = msUntilExpiry;
        }

        public OAuth2Credentials BuildCredentials(OAuth2TokenRequest request)
        {
            if (request.GrantType != GrantType.AuthCode)
            {
                throw new Exception();
            }
            if (request.ClientSecret != _clientSecret)
            {
                throw new Exception();
            }
            if (request.RedirectUri != _redirectUri.ToString())
            {
                throw new Exception();
            }

            var token = _tokens[request.ClientId]
                .SingleOrDefault(t => t.Code == request.Code);

            if (token == null)
            {
                throw new Exception();
            }

            token.RemoveCode();

            var accessToken = new CryptoStringGenerator()
                .CreateRandomString(
                    32,
                    CryptoStringType.Base64Alphanumeric);
            token.SetToken(accessToken);

            var credentials = new OAuth2Credentials();
            credentials.SetMemberValue("_accessToken", accessToken);
            if (_msUntilExpiry > 0)
            {
                credentials.SetMemberValue("_expiresIn", _msUntilExpiry.ToString());
            }

            return credentials;
        }
    }
}
