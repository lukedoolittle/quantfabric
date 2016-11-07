﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Foundations.Collections;
using Foundations.HttpClient.Request;

namespace Foundations.HttpClient.ParameterHandlers
{
    public class BodyParameterHandler : IParameterHandler
    {
        public void AddParameters(
            RequestParameters message,
            HttpValueCollection parameters)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (parameters == null || !parameters.Any())
            {
                return;
            }

            message.Content = new FormUrlEncodedContent(
                parameters.Select(item =>
                    new KeyValuePair<string, string>(
                            item.Key,
                            item.Value)));
        }
    }
}
