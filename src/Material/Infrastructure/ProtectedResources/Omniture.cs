﻿using Material.Metadata;
using System;
using Material.Infrastructure.Credentials;
using System.Collections.Generic;
using Foundations.HttpClient.Enums;
using Material.Infrastructure;

namespace Material.Infrastructure.ProtectedResources
{     
    /// <summary>
    /// Omniture / Adobe Web API 1.4
    /// </summary>
    [CredentialType(typeof(OAuth2Credentials))]
	public partial class Omniture : OAuth2ResourceProvider              
	{
        public override Uri AuthorizationUrl => new Uri("https://marketing.adobe.com/authorize");
        public override List<String> AvailableScopes => new List<String> { "Bookmark", "Company", "DataFeed", "DataWarehouse", "Permissions", "ReportSuite", "Saint", "Survey", "Dashboards", "DataSource", "Social", "Report", "Livestream" };
        public override List<ResponseTypeEnum> Flows => new List<ResponseTypeEnum> { ResponseTypeEnum.Code, ResponseTypeEnum.Token };
        public override List<GrantTypeEnum> GrantTypes => new List<GrantTypeEnum> { GrantTypeEnum.AuthCode, GrantTypeEnum.ClientCredentials };
        public override String TokenName => "Bearer";
        public override Uri TokenUrl => new Uri("https://api.omniture.com/token");
	}
}