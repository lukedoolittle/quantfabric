﻿using System;
using Foundations.HttpClient.Enums;

namespace Material.Infrastructure
{
    public abstract class OAuth1ResourceProvider : ResourceProvider
    {
        public abstract Uri RequestUrl { get; }
        public abstract Uri AuthorizationUrl { get; }
        public abstract Uri TokenUrl { get; }
        public abstract HttpParameterType ParameterType { get; }

        public virtual bool SupportsCustomUrlScheme { get; } = false;
    }
}
