﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundations.Extensions;

namespace Foundations.HttpClient.Extensions
{
    public static class ParameterExtensions
    {
        public static HttpRequestBuilder Parameter(
            this HttpRequestBuilder instance,
            Enum key,
            string value)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return instance.Parameter(
                key.EnumToString(),
                value);
        }

        public static HttpRequestBuilder Parameters(
            this HttpRequestBuilder instance,
            params KeyValuePair<string, string>[] parameters)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return instance.Parameters(
                parameters.ToDictionary(
                    x => x.Key, 
                    x=> x.Value));
        }

        public static HttpRequestBuilder ParameterFromObject(
            this HttpRequestBuilder instance, 
            object item)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var parameters = item
                .GetType()
                .GetTypeInfo()
                .DeclaredProperties
                .Select(
                    x => new KeyValuePair<string, string>(
                        x.Name,
                        x.GetValue(item, null).ToString()));

            return instance.Parameters(parameters.ToArray());
        }
    }
}
