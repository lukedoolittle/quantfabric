﻿using System;
using System.Threading.Tasks;
using Material.Enums;
using Material.Infrastructure.Credentials;

namespace Material.Contracts
{
    public interface IOAuthAuthorizerUI<TCredentials>
        where TCredentials : TokenCredentials
    {
        AuthenticationInterfaceEnum BrowserType { get; }

        Task<TCredentials> Authorize(
            Uri callbackUri,
            Uri authorizationUri,
            string userId);
    }
}
