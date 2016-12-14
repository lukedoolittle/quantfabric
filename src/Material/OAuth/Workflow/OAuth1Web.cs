﻿using System;
using System.Threading.Tasks;
using Foundations.Extensions;
using Foundations.HttpClient.Enums;
using Material.Contracts;
using Material.Infrastructure;
using Material.Infrastructure.Credentials;
using Material.OAuth.Authorization;
using Material.OAuth.Callback;
using Material.OAuth.Facade;
using Material.OAuth.Security;

namespace Material.OAuth.Workflow
{
    /// <summary>
    /// Authorize a resource owner with the given resource provider using OAuth1a
    /// </summary>
    /// <typeparam name="TResourceProvider">Resource provider to authenticate with</typeparam>
    public class OAuth1Web<TResourceProvider>
        where TResourceProvider: OAuth1ResourceProvider, new()
    {
        private readonly IOAuthFacade<OAuth1Credentials> _authFacade;
        private readonly IOAuthSecurityStrategy _securityStrategy;

        /// <summary>
        /// Authorize a resource owner using the OAuth1a workflow
        /// </summary>
        /// <param name="consumerKey">The application's consumer key</param>
        /// <param name="consumerSecret">The application's consumer secret</param>
        /// <param name="callbackUrl">The application's registered callback url</param>
        /// <param name="securityStrategy">The security strategy to use for token and secret handling</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OAuth1Web(
            string consumerKey,
            string consumerSecret,
            string callbackUrl,
            IOAuthSecurityStrategy securityStrategy)
        {
            _securityStrategy = securityStrategy;

            _authFacade = new OAuth1AuthorizationFacade(
                new TResourceProvider(), 
                consumerKey, 
                consumerSecret,
                new Uri(callbackUrl),
                new OAuth1AuthorizationAdapter(),
                securityStrategy);
        }

        /// <summary>
        /// Authorize a resource owner using the OAuth1a workflow with default security strategy
        /// </summary>
        /// <param name="consumerKey">The application's consumer key</param>
        /// <param name="consumerSecret">The application's consumer secret</param>
        /// <param name="callbackUrl">The application's registered callback url</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OAuth1Web(
            string consumerKey,
            string consumerSecret,
            string callbackUrl) : 
                this (
                    consumerKey, 
                    consumerSecret,
                    callbackUrl, 
                    new OAuthSecurityStrategy(
                        new InMemoryCryptographicParameterRepository(),
                        TimeSpan.FromMinutes(
                            OAuthConfiguration.SecurityParameterTimeoutInMinutes)))
        { }

        /// <summary>
        /// Gets the authorization uri for the Resource Owner to enter his/her credentials
        /// </summary>
        /// <param name="userId">The users Id within the application</param>
        /// <returns>Authorization uri</returns>
        public Task<Uri> GetAuthorizationUriAsync(string userId)
        {
            return _authFacade.GetAuthorizationUriAsync(userId);
        }

        /// <summary>
        /// Exchanges callback uri credentials for access token credentials
        /// </summary>
        /// <param name="responseUri">The received callback uri</param>
        /// <param name="userId">The users Id within the application</param>
        /// <returns>Access token credentials</returns>
        public Task<OAuth1Credentials> GetAccessTokenAsync(
            Uri responseUri,
            string userId)
        {
            var result = new OAuth1CallbackHandler(
                            _securityStrategy,
                            OAuth1Parameter.OAuthToken.EnumToString())
                        .ParseAndValidateCallback(responseUri, userId);

            var oauthSecret = _securityStrategy.CreateOrGetSecureParameter(
                userId,
                OAuth1Parameter.OAuthTokenSecret.EnumToString());

            return _authFacade.GetAccessTokenAsync(result, oauthSecret);
        }
    }
}