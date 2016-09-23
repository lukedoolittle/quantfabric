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
using Material.Infrastructure;

namespace Material.Infrastructure.Requests
{     
    /// <summary>
    /// Get a person's profile
    /// </summary>
    [ServiceType(typeof(Google))]
	public partial class GoogleProfile : OAuthRequest              
	{
        public override String Host => "https://www.googleapis.com";
        public override String Path => "/plus/v1/people/me";
        public override String HttpMethod => "GET";
        public override List<String> RequiredScopes => new List<String> { "https://www.googleapis.com/auth/userinfo.email" };
	}
}
