﻿/*
 * WARNING: This is generated code. Any changes you make will
 * be overwritten. If you wish to modify this class create a
 * partial definition in a seperate file.
 *
 */
using System;
using System.Collections.Generic;
using Aggregator.Domain.Write;
using Newtonsoft.Json.Linq;

namespace Aggregator.Infrastructure.Requests
{
	[Aggregator.Framework.Metadata.ClientType(typeof(Aggregator.Infrastructure.Clients.OAuthClient<FacebookEvent>))]        
	[Aggregator.Framework.Metadata.ServiceType(typeof(Aggregator.Infrastructure.Services.Facebook))]        
	public partial class FacebookEvent : OAuthRequest
	{
			public override String Host => "https://graph.facebook.com";
		public override String SinglePath => "/me/events";
		public override String HttpMethod => "GET";
		public override String RequestFilterKey => "since";
		public override String ResponseFilterKey => "created_time";
		public override String PayloadProperty => "data";
		public override TimestampOptions ResponseTimestamp => new TimestampOptions("start_time", "yyyy-MM-ddTHH:mm:sszzz", null, null);
		public override PollingInterval PollingInterval => new PollingInterval(1000, 5000, 10000);
	}
}
