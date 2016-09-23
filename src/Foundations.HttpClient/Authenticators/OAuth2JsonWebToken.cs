﻿using Foundations.Extensions;
using Foundations.HttpClient.Enums;

namespace Foundations.HttpClient.Authenticators
{
    public class OAuth2JsonWebToken : IAuthenticator
    {
        private readonly OAuth2JWTSigningTemplate _template;
        private readonly string _privateKey;
        private readonly string _clientId;

        public OAuth2JsonWebToken(
            JsonWebToken token,
            string privateKey,
            string clientId)
        {
            _clientId = clientId;
            _privateKey = privateKey;
            _template = new OAuth2JWTSigningTemplate(token);
        }

        public void Authenticate(HttpRequest request)
        {
            var signatureBase = _template.CreateSignatureBase();
            var signature = _template.CreateSignature(signatureBase, _privateKey);
            var assertion = _template.CreateJsonWebToken(signature);

            request
                .Parameter(
                    OAuth2ParameterEnum.Assertion.EnumToString(),
                    assertion)
                 .Parameter(
                    OAuth2ParameterEnum.GrantType.EnumToString(),
                    GrantTypeEnum.JsonWebToken.EnumToString());

            if (_clientId != null)
            {
                request.Parameter(
                    OAuth2ParameterEnum.ClientId.EnumToString(),
                    _clientId);
            }
        }
    }
}
