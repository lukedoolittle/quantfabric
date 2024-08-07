﻿using System;
using System.Collections.Generic;
using Material.Contracts;
using Material.Infrastructure;
using Material.Infrastructure.Credentials;
using Material.Infrastructure.ProtectedResources;

namespace Application.Configuration
{
    using Twitter = Material.Infrastructure.ProtectedResources.Twitter;

    public class CredentialApplicationSettings : IClientCredentials
    {
        private const string REDIRECT_URI_FORMAT = 
            "http://localhost:33533/oauth/{0}";

		public readonly Dictionary<Type, AccountKeyCredentials> _accountKeyCredentials =
            new Dictionary<Type, AccountKeyCredentials>
            {
                {
                    typeof(AzureTableStorage), new AccountKeyCredentials()
                        .AddAccountInformation(
                            "",
                            "",
                            new AzureTableStorage().KeyName)
                }
            };

        public readonly Dictionary<Type, UsernameAndPassword> _passwordCredentials = 
            new Dictionary<Type, UsernameAndPassword>
        {
            { typeof(XamarinInsights), new UsernameAndPassword("", "") }
        };	
			
        public readonly Dictionary<Type, ApiKeyCredentials> _apiKeyCredentials = 
            new Dictionary<Type, ApiKeyCredentials>
        {
                { typeof(MicrosoftLuis), ApiKeyCredentials.FromProvider<MicrosoftLuis>(
                    "") },
                {typeof(MicrosoftBing), ApiKeyCredentials.FromProvider<MicrosoftBing>(
                    "") }
        };

        private readonly Dictionary<Type, OAuth2Credentials> _jwtCredentials =
            new Dictionary<Type, OAuth2Credentials>
        {
                { typeof(Google), new OAuth2Credentials()
                                    .SetClientProperties(
                                        null,
                    @"-----BEGIN PRIVATE KEY-----

                    -----END PRIVATE KEY-----"
                    , "")
                }
        };

        private readonly Dictionary<Type, TokenCredentials> _protocolCredentials = 
            new Dictionary<Type, TokenCredentials>
        {
                { typeof(Google), new OAuth2Credentials()
                .SetClientProperties("", null)
                .SetCallbackUrl("quantfabric.material:/google")},

                { typeof(Fitbit), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl("quantfabric.material://fitbit")},

                { typeof(Foursquare), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl("quantfabric.material://foursquare")},
				
				{ typeof(Withings), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl("quantfabric://withings")},

                { typeof(Twitter), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl("quantfabric://twitter")},

                { typeof(Fatsecret), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl("quantfabric://fatsecret")},

                { typeof(Tumblr), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl("quantfabric://tumblr")},

                { typeof(Spotify), new OAuth2Credentials()
                .SetClientId(")
                .SetCallbackUrl("quantfabric://spotify")},

                { typeof(Facebook), new OAuth2Credentials()
                .SetClientId("")
                .SetCallbackUrl("quantfabric://facebook")},

                { typeof(Amazon), new OAuth2Credentials()
                .SetClientId("")
                .SetCallbackUrl("amzn-quantfabric.material://?methodName=signin")},
        };

        private readonly Dictionary<Type, TokenCredentials> _localhostCredentials = 
            new Dictionary<Type, TokenCredentials>
        {
            { typeof(Facebook), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Facebook).Name.ToLower()))},

            { typeof(Foursquare), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Foursquare).Name.ToLower()))},

            { typeof(Spotify), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Spotify).Name.ToLower()))},

            { typeof(Google), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Google).Name.ToLower()))},

            { typeof(LinkedIn), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(LinkedIn).Name.ToLower()))},

            { typeof(Rescuetime), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl("https://localhost:33533/oauth/rescuetime")},

            { typeof(Runkeeper), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Runkeeper).Name.ToLower()))},

            { typeof(Fitbit), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Fitbit).Name.ToLower()))},

            { typeof(Withings), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Withings).Name.ToLower()))},

            { typeof(Twitter), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Twitter).Name.ToLower()))},

            { typeof(Fatsecret), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Fatsecret).Name.ToLower()))},

            { typeof(TwentyThreeAndMe), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(TwentyThreeAndMe).Name.ToLower()))},

            { typeof(Omniture), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Omniture).Name.ToLower()))},

            { typeof(Pinterest), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl("https://localhost:33533/oauth/pinterest")},

            { typeof(Instagram), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Instagram).Name.ToLower()))},

             { typeof(Tumblr), new OAuth1Credentials()
                .SetConsumerProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Tumblr).Name.ToLower()))},

            { typeof(Amazon), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl(string.Format(REDIRECT_URI_FORMAT,typeof(Amazon).Name.ToLower()))},
				
			{ typeof(Yahoo), new OAuth2Credentials()
                .SetClientProperties("", "")
                .SetCallbackUrl("http://quantfabric.com")}
        };

        public TCredentials GetClientCredentials<TService, TCredentials>(
            CallbackType callbackType)
            where TService : ResourceProvider
            where TCredentials : TokenCredentials
        {
            if (callbackType == CallbackType.Localhost)
            {
                TokenCredentials credentials = null;
                if (_localhostCredentials.TryGetValue(typeof(TService), out credentials))
                {
                    return (credentials as TCredentials);
                }
                else
                {
                    throw new Exception($"{typeof(TService).Name} doesn't have populated credentials");
                }
            }
            else if (callbackType == CallbackType.Protocol)
            {
                TokenCredentials credentials = null;
                if (_protocolCredentials.TryGetValue(typeof(TService), out credentials))
                {
                    return (credentials as TCredentials);
                }
                else
                {
                    throw new Exception($"{typeof(TService).Name} doesn't have populated credentials");
                }
            }
            else
            {
                TokenCredentials credentials = null;
                if (_protocolCredentials.TryGetValue(typeof(TService), out credentials))
                {
                    return (credentials as TCredentials);
                }
                else
                {
                    if (_localhostCredentials.TryGetValue(typeof(TService), out credentials))
                    {
                        return (credentials as TCredentials);
                    }
                    else
                    {
                        throw new Exception($"{typeof(TService).Name} doesn't have populated credentials");
                    }
                }
            }
        }

        public TCredentials GetClientCredentials<TService, TCredentials>()
            where TService : ResourceProvider
            where TCredentials : TokenCredentials
        {
            return GetClientCredentials<TService, TCredentials>(CallbackType.NotSpecified);
        }

        public OAuth2Credentials GetJsonWebTokenCredentials<TService>()
            where TService : ResourceProvider
        {
            OAuth2Credentials credentials = null;
            if (_jwtCredentials.TryGetValue(typeof(TService), out credentials))
            {
                return credentials;
            }
            else
            {
                throw new Exception($"{typeof(TService).Name} doesn't have populated credentials");
            }
        }

        public ApiKeyCredentials GetApiKeyCredentials<TService>()
            where TService : ApiKeyResourceProvider
        {
            ApiKeyCredentials credentials = null;
            if (_apiKeyCredentials.TryGetValue(typeof(TService), out credentials))
            {
                return credentials;
            }
            else
            {
                throw new Exception($"{typeof(TService).Name} doesn't have populated credentials");
            }
        }

        public UsernameAndPassword GetPasswordCredentials<TService>()
            where TService : PasswordResourceProvider
        {
            UsernameAndPassword credentials = null;
            if (_passwordCredentials.TryGetValue(typeof(TService), out credentials))
            {
                return credentials;
            }
            else
            {
                throw new Exception($"{typeof(TService).Name} doesn't have populated credentials");
            }
        }

        public AccountKeyCredentials GetAccountKeyCredentials<TService>()
            where TService : ApiKeyResourceProvider
        {
            AccountKeyCredentials credentials = null;
            if (_accountKeyCredentials.TryGetValue(typeof(TService), out credentials))
            {
                return credentials;
            }
            else
            {
                throw new Exception($"{typeof(TService).Name} doesn't have populated credentials");
            }
        }
    }
}
