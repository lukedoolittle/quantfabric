﻿using System;
using Foundations.HttpClient.Authenticators;

namespace Foundations.HttpClient.Extensions
{
    public static class HttpRequestOAuth2Extensions
    {
        public static HttpRequest ForOAuth2AccessToken(
            this HttpRequest instance,
            string clientId,
            string clientSecret,
            string redirectUrl,
            string code,
            string scope)
        {
            if (instance == null)
            {
                throw new NullReferenceException();
            }

            var authenticator = new OAuth2AccessToken(
                clientId, 
                clientSecret, 
                redirectUrl, 
                code, 
                scope);

            return instance.Authenticator(authenticator);
        }

        public static HttpRequest ForOAuth2ClientAccessToken(
            this HttpRequest instance,
            string clientId,
            string clientSecret)
        {
            if (instance == null)
            {
                throw new NullReferenceException();
            }

            var authenticator = new OAuth2ClientAccessToken(
                clientId,
                clientSecret);

            return instance.Authenticator(authenticator);
        }

        public static HttpRequest ForOAuth2RefreshToken(
            this HttpRequest instance,
            string clientId,
            string clientSecret,
            string refreshToken)
        {
            if (instance == null)
            {
                throw new NullReferenceException();
            }

            var authenticator = new OAuth2RefreshToken(
                clientId,
                clientSecret, 
                refreshToken);

            return instance.Authenticator(authenticator);
        }

        public static HttpRequest ForOAuth2JsonWebToken(
            this HttpRequest instance, 
            JsonWebToken token, 
            IJWTSigningFactory signingFactory,
            string privateKey,
            string clientId)
        {
            if (instance == null)
            {
                throw new NullReferenceException();
            }

            var authenticator = new OAuth2JsonWebToken(
                token,
                signingFactory,
                privateKey,
                clientId);

            return instance.Authenticator(authenticator);
        }

        public static HttpRequest ForOAuth2ProtectedResource(
            this HttpRequest instance,
            string accessToken,
            string accessTokenName)
        {
            if (instance == null)
            {
                throw new NullReferenceException();
            }

            var authenticator = new OAuth2ProtectedResource(
                accessToken,
                accessTokenName);

            return instance.Authenticator(authenticator);
        }
    }
}
