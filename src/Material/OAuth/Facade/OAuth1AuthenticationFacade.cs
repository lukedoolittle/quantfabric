﻿using System;
using System.Threading.Tasks;
using Foundations.Extensions;
using Foundations.HttpClient.Enums;
using Material.Contracts;
using Material.Infrastructure.Credentials;

namespace Material.Infrastructure.OAuth
{
    public class OAuth1AuthenticationFacade : 
        IOAuthFacade<OAuth1Credentials>
    {
        private readonly OAuth1ResourceProvider _resourceProvider;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly IOAuth1Authentication _oauth;
        private readonly IOAuthCallbackHandler<OAuth1Credentials> _handler;
        private readonly string _callbackUri;
        protected readonly IOAuthSecurityStrategy _securityStrategy;

        public OAuth1AuthenticationFacade(
            OAuth1ResourceProvider resourceProvider,
            string consumerKey,
            string consumerSecret,
            string callbackUrl,
            IOAuth1Authentication oauth, 
            IOAuthSecurityStrategy securityStrategy,
            IOAuthCallbackHandler<OAuth1Credentials> handler)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _resourceProvider = resourceProvider;
            _oauth = oauth;
            _securityStrategy = securityStrategy;
            _handler = handler;
            _callbackUri = callbackUrl;
        }

        /// <summary>
        /// Gets the authorization uri for the Resource Owner to enter his/her credentials
        /// </summary>
        /// <param name="userId">Resource owner's Id</param>
        /// <returns>Authorization uri</returns>
        public async Task<Uri> GetAuthorizationUriAsync(string userId)
        {
            var credentials =
                await _oauth
                    .GetRequestToken(
                        _resourceProvider.RequestUrl,
                        _consumerKey,
                        _consumerSecret,
                        _resourceProvider.ParameterType,
                        new Uri(_callbackUri))
                    .ConfigureAwait(false);

            var authorizationPath =
                _oauth.GetAuthorizationUri(
                    credentials.OAuthToken,
                    _resourceProvider.AuthorizationUrl);

            _securityStrategy.SetSecureParameter(
                userId, 
                OAuth1ParameterEnum.OAuthToken.EnumToString(), 
                credentials.OAuthToken);
            _securityStrategy.SetSecureParameter(
                userId,
                OAuth1ParameterEnum.OAuthTokenSecret.EnumToString(),
                credentials.OAuthSecret);

            return authorizationPath;
        }

        /// <summary>
        /// Convert a callback uri into intermediate OAuth1Credentials
        /// </summary>
        /// <param name="responseUri">The received callback uri</param>
        /// <param name="userId">Resource owner's Id</param>
        /// <returns>Intermediate OAuth1 credentials</returns>
        public OAuth1Credentials ParseAndValidateCallback(
            Uri responseUri,
            string userId)
        {
            return _handler.ParseAndValidateCallback(
                responseUri,
                userId);
        }

        /// <summary>
        /// Exchanges intermediate credentials for access token credentials
        /// </summary>
        /// <param name="intermediateResult">Intermediate credentials received from OAuth1 callback</param>
        /// <param name="oauthSecret">The oauth secret provided during the token request</param>
        /// <returns>Access token credentials</returns>
        public async Task<OAuth1Credentials> GetAccessTokenAsync(
            OAuth1Credentials intermediateResult,
            string oauthSecret)
        {
            var token = await _oauth
                .GetAccessToken(
                    _resourceProvider.TokenUrl,
                    _consumerKey,
                    _consumerSecret,
                    intermediateResult.OAuthToken,
                    oauthSecret,
                    intermediateResult.Verifier,
                    _resourceProvider.ParameterType,
                    intermediateResult.AdditionalParameters)
                .ConfigureAwait(false);

            return token
                .SetConsumerProperties(
                    _consumerKey,
                    _consumerSecret)
                .SetParameterHandling(
                    _resourceProvider.ParameterType);
        }
    }
}
