﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CodeGen.Class;
using CodeGen.Metadata;
using CodeGen.PropertyValues;
using Foundations.Enums;
using Foundations.Extensions;
using Foundations.HttpClient.Enums;
using Material.Enums;
using Material.Metadata;
using Material.Infrastructure;
using Material.Infrastructure.Credentials;
using Material.Metadata.Formatters;
using Newtonsoft.Json.Linq;

namespace CodeGen
{
    public class SwaggerToClass
    {
        private readonly JObject _swagger;
        private readonly DummyOAuth2ResourceProvider _oauth2Provider = 
            new DummyOAuth2ResourceProvider();
        private readonly DummyOAuth1ResourceProvider _oauth1Provider = 
            new DummyOAuth1ResourceProvider();
        private readonly DummyOpenIdResourceProvider _openidProvider = 
            new DummyOpenIdResourceProvider();

        public SwaggerToClass(string pathToSwaggerFile)
        {
            _swagger = JObject.Parse(File.ReadAllText(pathToSwaggerFile));
        }

        public ClassRepresentation GenerateServiceClass(string @namespace)
        {
            var @class = new ClassRepresentation(_swagger["info"]["title"].ToString(), @namespace)
            {
                Comments = _swagger["info"]["description"].ToString() + " " + _swagger["info"]["version"].ToString()
            };

            var securityDefinitions = _swagger["securityDefinitions"];

            foreach (var securityDefinition in securityDefinitions.Values())
            {
                if (securityDefinition["type"]?.ToString() == "oauth2")
                {
                    @class.BaseType = securityDefinition["scopes"]["openid"] != null ? 
                        new BaseTypeRepresentation(typeof(OpenIdResourceProvider)) : 
                        new BaseTypeRepresentation(typeof(OAuth2ResourceProvider));
                    
                    if (@class.Properties.Count == 0)
                    {
                        @class.Properties.Add(
                            new PropertyRepresentation(typeof(List<string>), 
                            nameof(_oauth2Provider.AvailableScopes))
                        {
                            IsOverride = true,
                            PropertyValue = new ConcreteValueRepresentation(securityDefinition["scopes"]
                                .ToObject<JObject>()
                                .Properties()
                                .Select(p => p.Name)
                                .ToList())
                        });
                        @class.Properties.Add(new PropertyRepresentation(
                            typeof(List<OAuth2FlowType>), 
                            nameof(_oauth2Provider.AllowedFlows))
                        {
                            IsOverride = true,
                            PropertyValue = new ConcreteValueRepresentation(new List<OAuth2FlowType>())
                        });
                        @class.Properties.Add(new PropertyRepresentation(
                            typeof(List<GrantType>),
                            nameof(_oauth2Provider.AllowedGrantTypes))
                        {
                            IsOverride = true,
                            PropertyValue = new ConcreteValueRepresentation(new List<GrantType>())
                        });
                        @class.Properties.Add(new PropertyRepresentation(
                            typeof(List<OAuth2ResponseType>),
                            nameof(_oauth2Provider.AllowedResponseTypes))
                        {
                            IsOverride = true,
                            PropertyValue = new ConcreteValueRepresentation(new List<OAuth2ResponseType>())
                        });
                        if (securityDefinition["x-token-name"] != null)
                        {
                            @class.Properties.Add(new PropertyRepresentation(
                                typeof(string), 
                                nameof(_oauth2Provider.TokenName))
                            {
                                IsOverride = true,
                                PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-token-name"].ToString())
                            });
                        }
                        if (securityDefinition["x-scope-delimiter"] != null)
                        {
                            @class.Properties.Add(new PropertyRepresentation(
                                typeof(char), 
                                nameof(_oauth2Provider.ScopeDelimiter))
                            {
                                IsOverride = true,
                                PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-scope-delimiter"].ToString().ToCharArray()[0])
                            });
                        }

                        @class.Metadatas.Add(new ConcreteMetadataRepresentation(typeof(CredentialTypeAttribute))
                        {
                            ConstructorParameters = new List<object> { typeof(OAuth2Credentials) }
                        });

                    }

                    var flow = securityDefinition["flow"]?.ToString();
                    if (flow != null)
                    {
                        var flows = @class.Properties.Single(p => p.Name == nameof(_oauth2Provider.AllowedFlows));
                        ((List<OAuth2FlowType>)((ConcreteValueRepresentation)flows.PropertyValue).PropertyValue)
                            .Add(flow.StringToEnum<OAuth2FlowType>());
                    }

                    var grants = securityDefinition["x-grant-types"]?.ToObject<List<string>>();
                    if (grants != null)
                    {
                        var grantTypes = @class.Properties.SingleOrDefault(p => p.Name == nameof(_oauth2Provider.AllowedGrantTypes));
                        foreach (var grantType in grants)
                        {
                            ((List<GrantType>)
                                ((ConcreteValueRepresentation)grantTypes.PropertyValue).PropertyValue).Add(
                                    grantType.StringToEnum<GrantType>());
                        }
                    }

                    var responses = securityDefinition["x-response-types"]?.ToObject<List<string>>();
                    if (responses != null)
                    {
                        var responseTypes = @class.Properties.SingleOrDefault(p => p.Name == nameof(_oauth2Provider.AllowedResponseTypes));
                        foreach (var responseType in responses)
                        {
                            ((List<OAuth2ResponseType>)
                                ((ConcreteValueRepresentation)responseTypes.PropertyValue).PropertyValue).Add(
                                    responseType.StringToEnum<OAuth2ResponseType>());
                        }
                    }

                    var authorizationUrl = securityDefinition["authorizationUrl"]?.ToString();
                    if (authorizationUrl != null)
                    {
                        if (@class.Properties.All(p => p.Name != nameof(_oauth2Provider.AuthorizationUrl)))
                        {
                            @class.Properties.Add(new PropertyRepresentation(typeof(Uri), nameof(_oauth2Provider.AuthorizationUrl))
                            {
                                IsOverride = true,
                                PropertyValue = new ConcreteValueRepresentation(new Uri(authorizationUrl))
                            });
                        }
                    }

                    var tokenUrl = securityDefinition["tokenUrl"]?.ToString();
                    if (tokenUrl != null)
                    {
                        if (@class.Properties.All(p => p.Name != nameof(_oauth2Provider.TokenUrl)))
                        {
                            @class.Properties.Add(new PropertyRepresentation(typeof(Uri), nameof(_oauth2Provider.TokenUrl))
                            {
                                IsOverride = true,
                                PropertyValue = new ConcreteValueRepresentation(new Uri(tokenUrl))
                            });
                        }
                    }

                    var openIdDiscoveryUrl = securityDefinition["x-openid-discovery-url"]?.ToString();
                    if (openIdDiscoveryUrl != null)
                    {
                        if (@class.Properties.All(p => p.Name != nameof(_openidProvider.OpenIdDiscoveryUrl)))
                        {
                            @class.Properties.Add(new PropertyRepresentation(typeof(Uri), nameof(_openidProvider.OpenIdDiscoveryUrl))
                            {
                                IsOverride = true,
                                PropertyValue = new ConcreteValueRepresentation(new Uri(openIdDiscoveryUrl))
                            });
                        }
                    }

                    var openIdissuers = securityDefinition["x-openid-issuers"]?.ToObject<List<string>>();
                    if (openIdissuers != null)
                    {
                        if (@class.Properties.All(p => p.Name != nameof(_openidProvider.ValidIssuers)))
                        {
                            @class.Properties.Add(new PropertyRepresentation(typeof(List<string>), nameof(_openidProvider.ValidIssuers))
                            {
                                IsOverride = true,
                                PropertyValue = new ConcreteValueRepresentation(openIdissuers)
                            });
                        }
                    }
                }
                else if (securityDefinition["type"]?.ToString() == "oauth1")
                {
                    @class.BaseType = new BaseTypeRepresentation(typeof(OAuth1ResourceProvider));

                    @class.Properties.Add(new PropertyRepresentation(
                        typeof(Uri), 
                        nameof(_oauth1Provider.RequestUrl))
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(new Uri(securityDefinition["requestUrl"].ToString()))
                    });
                    @class.Properties.Add(new PropertyRepresentation(
                        typeof(Uri), 
                        nameof(_oauth1Provider.AuthorizationUrl))
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(new Uri(securityDefinition["authorizationUrl"].ToString()))
                    });
                    @class.Properties.Add(new PropertyRepresentation(
                        typeof(Uri), 
                        nameof(_oauth1Provider.TokenUrl))
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(new Uri(securityDefinition["tokenUrl"].ToString()))
                    });
                    @class.Properties.Add(
                        new PropertyRepresentation(typeof(HttpParameterType), 
                        nameof(_oauth1Provider.ParameterType))
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-parameter-type"].ToString().StringToEnum<HttpParameterType>())
                    });
                    @class.Metadatas.Add(new ConcreteMetadataRepresentation(typeof(CredentialTypeAttribute))
                    {
                        ConstructorParameters = new List<object> { typeof(OAuth1Credentials) }
                    });
                }
                else if (securityDefinition["type"]?.ToString() == "apiKey")
                {
                    @class.BaseType = new BaseTypeRepresentation(typeof(ApiKeyResourceProvider));

                    @class.Properties.Add(new PropertyRepresentation(typeof(string), "KeyName")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-key-name"].ToString())
                    });
                    @class.Properties.Add(new PropertyRepresentation(typeof(HttpParameterType), "KeyType")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-key-location"].ToString().StringToEnum<HttpParameterType>())
                    });

                    @class.Metadatas.Add(new ConcreteMetadataRepresentation(typeof(CredentialTypeAttribute))
                    {
                        ConstructorParameters = new List<object> { typeof(ApiKeyCredentials) }
                    });
                }
                else if (securityDefinition["type"]?.ToString() == "keyJwtExchange")
                {
                    @class.BaseType = new BaseTypeRepresentation(typeof(ApiKeyExchangeResourceProvider));

                    @class.Properties.Add(new PropertyRepresentation(typeof(string), "KeyName")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-key-name"].ToString())
                    });
                    @class.Properties.Add(new PropertyRepresentation(typeof(string), "TokenName")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-token-name"].ToString())
                    });
                    @class.Properties.Add(new PropertyRepresentation(typeof(HttpParameterType), "KeyType")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(securityDefinition["x-key-location"].ToString().StringToEnum<HttpParameterType>())
                    });
                    @class.Properties.Add(new PropertyRepresentation(typeof(Uri), "TokenUrl")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(new Uri(securityDefinition["tokenUrl"].ToString()))
                    });
                    @class.Metadatas.Add(new ConcreteMetadataRepresentation(typeof(CredentialTypeAttribute))
                    {
                        ConstructorParameters = new List<object> { typeof(ApiKeyCredentials) }
                    });
                }
                else
                {
                    throw new Exception();
                }
            }

            return @class;
        }

        public List<ClassRepresentation> GenerateRequestClasses(
            string @namespace,
            string serviceTypeName,
            string serviceTypeNamespace)
        {
            var classes = new List<ClassRepresentation>();

            var scheme = _swagger["schemes"][0].ToString();
            var domain = _swagger["host"].ToString();
            var pathRoot = _swagger["basePath"].ToString();

            var produces = _swagger["produces"]
                .Select(item => item.ToString().StringToEnum<MediaType>())
                .ToList();
            var consumes = _swagger["consumes"]
                .Select(item => item.ToString().StringToEnum<MediaType>())
                .ToList();

            var paths = _swagger["paths"];

            foreach (JProperty path in paths)
            {
                var pathResidual = path.Name;

                foreach (JProperty request in path.Value)
                {
                    var details = request.Value.ToObject<JObject>();

                    var @class = new ClassRepresentation(
                        details["operationId"].ToString(),
                        @namespace)
                    {
                        Comments = details["summary"].ToString()
                    };

                    @class.Metadatas.Add(new AbstractMetadataRepresentation(typeof(ServiceTypeAttribute), serviceTypeNamespace, serviceTypeName));

                    @class.BaseType = new BaseTypeRepresentation(typeof(OAuthRequest));

                    @class.Properties.Add(new PropertyRepresentation(typeof(string), "Host")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation($"{scheme}://{domain}")
                    });
                    @class.Properties.Add(new PropertyRepresentation(typeof(string), "Path")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation((pathRoot + pathResidual).Replace("//", "/"))
                    });
                    @class.Properties.Add(new PropertyRepresentation(typeof(string), "HttpMethod")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(request.Name.ToUpper())
                    });

                    var requestProduces = produces;

                    if (details["produces"] != null)
                    {
                        requestProduces = details["produces"]
                            .ToObject<JArray>()
                            .Select(t => t.ToString().StringToEnum<MediaType>())
                            .ToList();
                    }

                    @class.Properties.Add(new PropertyRepresentation(typeof(List<MediaType>), "Produces")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(requestProduces)
                    });

                    var requestConsumes = consumes;

                    if (details["consumes"] != null)
                    {
                        requestConsumes = details["consumes"]
                            .ToObject<JArray>()
                            .Select(t => t.ToString().StringToEnum<MediaType>())
                            .ToList();
                    }

                    @class.Properties.Add(new PropertyRepresentation(typeof(List<MediaType>), "Consumes")
                    {
                        IsOverride = true,
                        PropertyValue = new ConcreteValueRepresentation(requestConsumes)
                    });

                    if (details["responses"] != null)
                    {
                        var acceptableResponseCodes = new List<HttpStatusCode>();
                        foreach (var response in details["responses"])
                        {
                            var statusCode = response.ToObject<JProperty>().Name;
                            acceptableResponseCodes.Add((HttpStatusCode) Convert.ToInt32(statusCode));
                        }

                        @class.Properties.Add(new PropertyRepresentation(typeof(List<HttpStatusCode>), "ExpectedStatusCodes")
                        {
                            IsOverride = true,
                            PropertyValue = new ConcreteValueRepresentation(acceptableResponseCodes)
                        });
                    }

                    if (details["x-request-filter-property"] != null)
                    {
                        @class.Properties.Add(new PropertyRepresentation(typeof(string), "RequestFilterKey")
                        {
                            IsOverride = true,
                            PropertyValue = new ConcreteValueRepresentation(details["x-request-filter-property"].ToString())
                        });
                    }

                    //this is pretty ridgid and makes assumptions about the structure that may not be true
                    if (details["security"]?.ToObject<JArray>()?.Count > 0 &&
                        details["security"]?.ToObject<JArray>()[0]?.ToObject<JObject>()?["oauth2"] != null)
                    {
                        var scopes =
                            details["security"]
                            .ToObject<JArray>()[0]
                            .ToObject<JObject>()["oauth2"]
                            .ToObject<JArray>()
                            .Select(s => s.ToString())
                            .ToList();

                        @class.Properties.Add(new PropertyRepresentation(typeof(List<string>), "RequiredScopes")
                        {
                            IsOverride = true,
                            PropertyValue = new ConcreteValueRepresentation(scopes)
                        });
                    }

                    string requestFilterProperty = null;

                    if (details["parameters"] != null)
                    {
                        foreach (JObject parameter in details["parameters"])
                        {
                            if (parameter["x-request-filter-property"] != null)
                            {
                                requestFilterProperty = parameter["x-request-filter-property"].ToString();
                            }

                            var name = CreateCSharpPropertyName(parameter["name"].ToString());

                            PropertyRepresentation property = null;

                            if (parameter["enum"] != null)
                            {
                                var enumName = @class.Name + name;
                                var enumNamespace = "Foundations.Attributes";
                                var @enum = new EnumRepresentation(
                                    enumName, 
                                    enumNamespace);

                                foreach (var item in parameter["enum"])
                                {
                                    @enum.Values.Add(
                                        CreateCSharpPropertyName(item.ToString()), 
                                        item.ToString());
                                }

                                @class.Enums.Add(@enum);

                                ValueRepresentation propertyValue = null;
                                if (parameter["default"] != null)
                                {
                                    propertyValue = new NewEnumValueRepresentation(
                                        @enum.Name,
                                        CreateCSharpPropertyName(parameter["default"].ToString()));
                                }

                                property = new PropertyRepresentation(
                                    @enum.Name,
                                    @namespace,
                                    name)
                                {
                                    IsAutoProperty = true,
                                    PropertyValue = propertyValue,
                                    Comments = parameter["description"].ToString()
                                };
                            }
                            else
                            {
                                var type = GetTypeFromParameterType(
                                    parameter["type"]?.ToString(),
                                    parameter["format"]?.ToString(),
                                    parameter["required"]?.ToString());


                                ConcreteValueRepresentation propertyValue = null;

                                if (parameter["default"] != null)
                                {
                                    propertyValue = new ConcreteValueRepresentation(ConvertType(
                                        parameter["default"].ToString(),
                                        type));
                                }

                                property = new PropertyRepresentation(type, name)
                                {
                                    IsAutoProperty = true,
                                    PropertyValue = propertyValue,
                                    Comments = parameter["description"].ToString()
                                };
                            }

                            var nameMetadata = new ConcreteMetadataRepresentation(typeof(NameAttribute));
                            nameMetadata.ConstructorParameters = new List<object> { parameter["name"].ToString()};
                            property.Metadatas.Add(nameMetadata);


                            if (parameter["in"].ToString() == "query")
                            {
                                var propertyTypeMetadata = new ConcreteMetadataRepresentation(typeof(ParameterTypeAttribute));
                                propertyTypeMetadata.ConstructorParameters = new List<object> { RequestParameterType.Query };
                                property.Metadatas.Add(propertyTypeMetadata);

                            }
                            else if (parameter["in"].ToString() == "path")
                            {
                                var propertyTypeMetadata = new ConcreteMetadataRepresentation(typeof(ParameterTypeAttribute));
                                propertyTypeMetadata.ConstructorParameters = new List<object> { RequestParameterType.Path };
                                property.Metadatas.Add(propertyTypeMetadata);
                            }
                            else if (parameter["in"].ToString() == "header")
                            {
                                var propertyTypeMetadata = new ConcreteMetadataRepresentation(typeof(ParameterTypeAttribute));
                                propertyTypeMetadata.ConstructorParameters = new List<object> { RequestParameterType.Header };
                                property.Metadatas.Add(propertyTypeMetadata);
                            }
                            else
                            {
                                //cover "body" here
                                throw new NotImplementedException();
                            }

                            if (parameter["required"]?.ToString().ToLower() == "true")
                            {
                                property.Metadatas.Add(new ConcreteMetadataRepresentation(typeof(RequiredAttribute)));
                            }

                            var formatMetadata = GetParameterMetadata(
                                parameter["type"]?.ToString(),
                                parameter["format"]?.ToString(),
                                parameter["pattern"]?.ToString(),
                                parameter["enum"] != null);
                            property.Metadatas.Add(formatMetadata);

                            @class.Properties.Add(property);
                        }
                    }


                    //handle the response
                    //if the response contains x-timeseries-information then append the ITimeseries interface
                    //if the response contains the x-response-filter property AND there is a parameter with the x-request-filter property, then implement the IFilterable (and implicitly the ITimeseries) interface

                    classes.Add(@class);
                }
            }

            return classes;
        }

        private ConcreteMetadataRepresentation GetParameterMetadata(
            string type,
            string format, 
            string pattern,
            bool isEnum)
        {
            if (isEnum)
            {
                return new ConcreteMetadataRepresentation(typeof(EnumFormatterAttribute));
            }
            else if (pattern == null)
            {
                return new ConcreteMetadataRepresentation(typeof(DefaultFormatterAttribute));
            }
            else if (format == "byte" ||
                     format == "binary" ||
                     format == "uuid" ||
                     format == "double" ||
                     format == "float" ||
                     format == "int32" ||
                     format == "int64")
            {
                return new ConcreteMetadataRepresentation(typeof(DefaultFormatterAttribute));
            }
            else if (type == "boolean")
            {
                return new ConcreteMetadataRepresentation(typeof(BooleanFormatterAttribute));
            }
            else if (pattern == "ddd")
            {
                if (format == "date-time-offset")
                {
                    return new ConcreteMetadataRepresentation(typeof(UnixTimeSecondsDateTimeOffsetFormatterAttribute));
                }
                else
                {
                    return new ConcreteMetadataRepresentation(typeof(UnixTimeSecondsDateTimeFormatterAttribute));
                }
            }
            else if (pattern == "d")
            {
                if (format == "date-time-offset")
                {
                    return new ConcreteMetadataRepresentation(typeof(UnixTimeDaysDateTimeOffsetFormatterAttribute));
                }
                else
                {
                    return new ConcreteMetadataRepresentation(typeof(UnixTimeDaysDateTimeFormatterAttribute));
                }
            }
            else if (format == "date" || format == "date-time" || format == "date-time-offset")
            {
                if (format == "date-time-offset")
                {
                    return new ConcreteMetadataRepresentation(typeof(DateTimeOffsetFormatterAttribute))
                    {
                        ConstructorParameters = new List<object> {pattern}
                    };
                }
                else
                {
                    return new ConcreteMetadataRepresentation(typeof(DateTimeFormatterAttribute))
                    {
                        ConstructorParameters = new List<object> { pattern }
                    };
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private object ConvertType(
            string item, 
            Type type)
        {
            if (type == typeof(string))
            {
                return item;
            }
            else if (type == typeof(int) || type == typeof(Nullable<int>))
            {
                return Convert.ToInt32(item);
            }
            else if (type == typeof(long) || type == typeof(Nullable<long>))
            {
                return Convert.ToInt64(item);
            }
            else if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                DateTime result;

                if (DateTime.TryParse(item, out result))
                {
                    return result;
                }
                else
                {
                    throw new Exception("Couldn't parse datetime string");
                }
            }
            else if (type == typeof(DateTimeOffset) || type == typeof(Nullable<DateTimeOffset>))
            {
                DateTimeOffset result;

                if (DateTimeOffset.TryParse(item, out result))
                {
                    return result;
                }
                else
                {
                    throw new Exception("Couldn't parse datetime string");
                }
            }
            else if (type == typeof(bool?) || type == typeof(bool))
            {
                if (item.ToLower() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                return Guid.Parse(item);
            }
            else
            {
                throw new Exception("Unhandled object type: " + type.Name);
            }
        }

        private Type GetTypeFromParameterType(
            string type, 
            string format,
            string required)
        {
            var isRequired = required == "true";

            if (type == "string")
            {
                if (format == null)
                {
                    return typeof(string);
                }
                if (format == "binary")
                {
                    return isRequired ? typeof(int) : typeof(int?);
                }
                if (format == "byte")
                {
                    return isRequired ? typeof(byte) : typeof(byte?);
                }
                if (format == "date" || format == "date-time")
                {
                    return isRequired ? typeof(DateTime) : typeof(DateTime?);
                }
                if (format == "date-time-offset")
                {
                    return isRequired ? typeof(DateTimeOffset) : typeof(DateTimeOffset?);
                }
                if (format == "uuid")
                {
                    return isRequired ? typeof(Guid) : typeof(Guid?);
                }
            }
            else if (type == "number")
            {
                if (format == null || format == "double")
                {
                    return isRequired ? typeof(double) : typeof(double?);
                }
                if (format == "float")
                {
                    return isRequired ? typeof(float) : typeof(float?);
                }
            }
            else if (type == "integer")
            {
                if (format == null || format == "int32")
                {
                    return isRequired ? typeof(int) : typeof(int?);
                }
                if (format == "int64")
                {
                    return isRequired ? typeof(long) : typeof(long?);
                }
            }
            else if (type == "array")
            {
                return typeof(object[]);
            }
            else if (type == "boolean")
            {
                return isRequired ? typeof(bool) : typeof(bool?);
            }
            else if (type == "file")
            {
                throw new Exception();
            }

            throw new Exception();
        }

        private string CreateCSharpPropertyName(string jsonName)
        {
            if (string.IsNullOrEmpty(jsonName))
            {
                return string.Empty;
            }

            var result = jsonName.Split(new[] { "_", "-", " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                .Aggregate(string.Empty, (s1, s2) => s1 + s2);

            var upperResult = char.ToUpper(result[0]) + result.Substring(1);

            return upperResult.Replace(".", "");
        }
    }
}
