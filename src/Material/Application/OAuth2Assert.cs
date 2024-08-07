﻿using System;
using Material.Domain.Credentials;
using System.Threading.Tasks;
using Material.Authorization;
using Material.Domain.Core;
using Material.HttpClient.Cryptography.Enums;
using Material.HttpClient.Cryptography.Keys;
using Material.Workflow.Facade;

namespace Material.Application
{
    /// <summary>
    /// Authenticates a resource owner with the given resource provider using OAuth2
    /// </summary>
    /// <typeparam name="TResourceProvider">Resource provider to authenticate with</typeparam>
    public class OAuth2Assert<TResourceProvider>
        where TResourceProvider : OAuth2ResourceProvider, new()
    {
        private readonly TResourceProvider _resourceProvider;
        private readonly OAuthClientFacade<TResourceProvider> _facade;
        private readonly CryptoKey _privateKey;
        private readonly string _issuer;
        private readonly string _clientId;

        public OAuth2Assert(
            CryptoKey privateKey,
            string issuer,
            string clientId) : 
                this(
                    privateKey,
                    issuer,
                    clientId,
                    new TResourceProvider())
        { }

        public OAuth2Assert(
            CryptoKey privateKey,
            string issuer) :
                this(
                    privateKey,
                    issuer,
                    null)
        { }

        public OAuth2Assert(
            CryptoKey privateKey,
            string issuer,
            string clientId,
            TResourceProvider resourceProvider)
        {
            _privateKey = privateKey;
            _issuer = issuer;
            _clientId = clientId;
            _resourceProvider = resourceProvider;
            _facade = new OAuthClientFacade<TResourceProvider>(
                new OAuthAuthorizationAdapter(),
                resourceProvider);
        }

        /// <summary>
        /// Authenticates a resource owner using the OAuth2 Json Web Token workflow
        /// </summary>
        /// <returns>OAuth2Credentials with access token</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<OAuth2Credentials> GetCredentialsAsync(
            JsonWebTokenAlgorithm algorithm)
        {
            var time = DateTime.Now.ToUniversalTime();

            var token = new JsonWebToken(
                new JsonWebTokenHeader
                {
                    Algorithm = algorithm
                },
                new JsonWebTokenClaims
                {
                    Issuer = _issuer,
                    Scope = _resourceProvider.Scope,
                    Audience = _resourceProvider.TokenUrl.ToString(),
                    ExpirationTime = time.Add(TimeSpan.FromMinutes(59)),
                    IssuedAt = time
                });

            return _facade
                    .GetJsonWebTokenTokenCredentials(
                        token, 
                        _privateKey,
                        _clientId);
        }

        /// <summary>
        /// Adds scope to be requested with OAuth2 authentication
        /// </summary>
        /// <typeparam name="TRequest">The request type scope is needed for</typeparam>
        /// <returns>The current instance</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public OAuth2Assert<TResourceProvider> AddScope<TRequest>()
            where TRequest : OAuthRequest, new()
        {
            _resourceProvider.AddRequestScope<TRequest>();

            return this;
        }
    }
}
