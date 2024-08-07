﻿#if !__FOUNDATIONS__
using System;
using Material.Domain.Credentials;
using System.IO;
using System.Reflection;
using Material.Domain.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Quantfabric.Test.TestHelpers
{
    public class TokenCredentialRepository
    {
        private static Assembly _assembly = typeof(TokenCredentialRepository).GetTypeInfo().Assembly;
        private static string SETTINGS_FILE_NAME = string.Format(
            "{0}.testCredentials.json",
            _assembly.FullName.Split(',')[0]);
        private const string _pathToTestCredentials = "../../../Quantfabric.Test.Helpers/testCredentials.json";

        private JObject CredentialSettings
        {
            get
            {
                using (var stream = _assembly.GetManifestResourceStream(SETTINGS_FILE_NAME))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var document = reader.ReadToEnd();
                        return JObject.Parse(document);
                    }
                }
            }
        } 

        private readonly bool _persistCredentials;

        public TokenCredentialRepository(bool persistCredentials)
        {
            _persistCredentials = persistCredentials;
        }

        public TToken GetToken<TService, TToken>()
            where TService : ResourceProvider
            where TToken : TokenCredentials
        {
            var token = CredentialSettings[typeof(TService).Name]
                ?.ToString();

            if (token == null)
            {
                throw new Exception();
            }

            var result = (TToken)JsonConvert.DeserializeObject(
                token,
                typeof(TToken));

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

#if __WINDOWS__
        public void PutToken<TService, TToken>(TToken token)
            where TService : ResourceProvider
            where TToken : TokenCredentials
        {
            if (!_persistCredentials)
            {
                return;
            }

            var serviceName = typeof(TService).Name;
            var credentials2 = JObject.Parse(File.ReadAllText(_pathToTestCredentials));
            credentials2[serviceName] = (JObject)JToken.FromObject(
                token,
                JsonSerializer.CreateDefault());
            var json = JsonConvert.SerializeObject(credentials2, Formatting.Indented);
            File.WriteAllText(_pathToTestCredentials, json);
        }
#endif
    }
}
#endif