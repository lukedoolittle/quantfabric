﻿using System;
using System.Globalization;
using System.Linq;
using Material.Framework.Collections;

namespace Material.Framework.Serialization
{
    public class HtmlSerializer : ISerializer
    {
        public string Serialize(object entity)
        {
            throw new NotImplementedException();
        }

        public string Serialize(
            object entity, 
            string dateTimeFormat)
        {
            throw new NotImplementedException();
        }


        public TEntity Deserialize<TEntity>(
            string entity, 
            string dateTimeFormat)
        {
            var jsonArray = HttpUtility
                .ParseQueryString(entity)
                .Select(q => string.Format(
                    CultureInfo.CurrentCulture, 
                    "\"{0}\" : \"{1}\"", 
                    q.Key, 
                    q.Value));

            var json = "{ " + string.Join(",", jsonArray) + " }";

            var dotnet = new JsonSerializer().Deserialize<TEntity>(
                json, 
                dateTimeFormat);

            return dotnet;
        }

        public TEntity Deserialize<TEntity>(string entity)
        {
            return Deserialize<TEntity>(entity, null);
        }
    }
}
