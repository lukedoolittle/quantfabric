﻿using System;
using Material.Contracts;
using Material.Enums;
using Material.Infrastructure.Credentials;
using Material.View.WebAuthorization;
#if __WINDOWS__
using Foundations.Http;
#endif

namespace Material.Infrastructure.OAuth
{
    public class OAuthAuthorizerUIFactory : IOAuthAuthorizerUIFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public IOAuthAuthorizerUI<TCredentials> GetAuthorizer<TResourceProvider, TCredentials>(
            AuthorizationInterface browserType,
            IOAuthCallbackHandler<TCredentials> handler,
            Uri callbackUri)
            where TResourceProvider : ResourceProvider
            where TCredentials : TokenCredentials
        {
#if __ANDROID__
            switch (browserType)
            {
                case AuthorizationInterface.Dedicated:
                    return new ProtocolAuthorizerUI<TCredentials>(
                        handler,
                        callbackUri,
                        browserType,
                        Framework.Platform.Current.RunOnMainThread);
                case AuthorizationInterface.Embedded:
                    return new WebViewAuthorizerUI<TCredentials>(
                        handler,
                        callbackUri,
                        browserType,
                        Framework.Platform.Current.RunOnMainThread);
                default:
                    throw new NotSupportedException();
            }
#elif __IOS__
            switch (browserType)
            {
                case AuthorizationInterface.Dedicated:
                    return new ProtocolAuthorizerUI<TCredentials>(
                        handler,
                        callbackUri,
                        browserType,
                        Framework.Platform.Current.RunOnMainThread);
                case AuthorizationInterface.Embedded:
                    return new UIWebViewAuthorizerUI<TCredentials>(
                        handler,
                        callbackUri,
                        browserType,
                        Framework.Platform.Current.RunOnMainThread);
                default:
                    throw new NotSupportedException();
            }
#elif WINDOWS_UWP
            switch (browserType)
            {
                case AuthorizationInterface.Dedicated:
                    return new ProtocolAuthorizerUI<TCredentials>(
                        handler, 
                        callbackUri,
                        browserType,
                        Framework.Platform.Current.RunOnMainThread);
                case AuthorizationInterface.Embedded:
                    return new WebViewAuthorizerUI<TCredentials>(
                        handler,
                        callbackUri,
                        browserType,
                        Framework.Platform.Current.RunOnMainThread);
                default:
                    throw new NotSupportedException();
            }
#elif __WINDOWS__
            return new BrowserAuthorizerUI<TCredentials>(
                new HttpServer(),
                handler, 
                callbackUri,
                AuthorizationInterface.Dedicated,
                action => { });
#else
            throw new NotSupportedException();
#endif
        }
    }
}
