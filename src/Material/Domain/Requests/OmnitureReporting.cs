﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Material.Framework.Metadata;
using Material.Domain.ResourceProviders;
using System;
using System.Collections.Generic;
using Material.Framework.Enums;
using System.Net;
using Material.Domain.Requests;
using Material.Framework.Metadata.Formatters;
using Material.Domain.Core;
using System.CodeDom.Compiler;

namespace Material.Domain.Requests
{     
    /// <summary>
    /// https://marketing.adobe.com/developer/documentation/analytics-reporting-1-4/get-started
    /// </summary>
    [ServiceType(typeof(Omniture))]
	[GeneratedCode("T4Toolbox", "14.0")]
	public partial class OmnitureReporting : OAuthRequest              
	{
        public override String Host => "https://api2.omniture.com";
        public override String Path => "/admin/1.4/rest/";
        public override String HttpMethod => "POST";
        public override List<MediaType> Produces => new List<MediaType> { MediaType.Json };
        public override List<MediaType> Consumes => new List<MediaType> { MediaType.Json };
        public override List<HttpStatusCode> ExpectedStatusCodes => new List<HttpStatusCode> { HttpStatusCode.OK };
        public override List<String> RequiredScopes => new List<String>();
        /// <summary>
        /// The name of the method to call
        /// </summary>
        [Name("method")]
        [ParameterType(RequestParameterType.Query)]
        [Required()]
        [EnumFormatter()]
        public  Nullable<OmnitureReportingMethod> Method { get; set; }
	}
	
	[GeneratedCode("T4Toolbox", "14.0")]
    public enum OmnitureReportingMethod
    {
        [Description("Report.Cancel")] ReportCancel,
        [Description("Report.Get")] ReportGet,
        [Description("Report.GetElements")] ReportGetElements,
        [Description("Report.GetMetrics")] ReportGetMetrics,
        [Description("Report.GetQueue")] ReportGetQueue,
        [Description("Report.Run")] ReportRun,
        [Description("Report.Queue")] ReportQueue,
        [Description("Report.Validate")] ReportValidate,
    }
}