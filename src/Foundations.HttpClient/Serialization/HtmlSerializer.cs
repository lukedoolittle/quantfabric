﻿using System;
using System.Linq;

namespace Foundations.HttpClient.Serialization
{
    public class HtmlSerializer : ISerializer
    {
        public string Serialize(object item)
        {
            throw new NotImplementedException();
        }

        public string Serialize<T>(T item)
        {
            return Serialize((object)item);
        }

        public T Deserialize<T>(
            string item, 
            string datetimeFormat = null)
        {
            var jsonArray = HttpUtility
                .ParseQueryString(item)
                .Select(q => $"\"{q.Key}\" : \"{q.Value}\"");

            var json = "{ " + string.Join(",", jsonArray) + " }";

            var dotnet = new JsonSerializer().Deserialize<T>(
                json, 
                datetimeFormat);

            return dotnet;
        }
    }
}
