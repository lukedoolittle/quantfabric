﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Material.Metadata;
using Material.Infrastructure.ProtectedResources;
using System;
using System.Collections.Generic;
using Foundations.Enums;
using System.Net;
using Material.Enums;
using Material.Metadata.Formatters;
using Material.Infrastructure;
using System.CodeDom.Compiler;

namespace Material.Infrastructure.Requests
{     
    /// <summary>
    /// 
    /// </summary>
    [ServiceType(typeof(AzureTableStorage))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class AzureTableStorageBatch : OAuthRequest              
	{
        public override String Host => "https://ACCOUNTNAME.table.core.windows.net";
        public override String Path => "/$batch";
        public override String HttpMethod => "POST";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.Json };
        public override List<HttpStatusCode> ExpectedStatusCodes => new List<HttpStatusCode> { HttpStatusCode.NoContent };
        /// <summary>
        /// Schema date
        /// </summary>
        [Name("x-ms-version")]
        [ParameterType(RequestParameterType.Header)]
        [Required()]
        [DefaultFormatter()]
        public  String XMsVersion { get; set; } = "2016-05-31";
        /// <summary>
        /// Timeout parameter in seconds.
        /// </summary>
        [Name("timeout")]
        [ParameterType(RequestParameterType.Query)]
        [DefaultFormatter()]
        public  Nullable<Int32> Timeout { get; set; }
	}
}