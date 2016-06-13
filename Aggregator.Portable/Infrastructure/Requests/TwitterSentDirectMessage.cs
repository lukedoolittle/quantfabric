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
	[Aggregator.Framework.Metadata.ClientType(typeof(Aggregator.Infrastructure.Clients.OAuthClient<TwitterSentDirectMessage>))]        
	[Aggregator.Framework.Metadata.ServiceType(typeof(Aggregator.Infrastructure.Services.Twitter))]        
	public partial class TwitterSentDirectMessage : OAuthRequest
	{
			public override String Host => "https://api.twitter.com/";
		public override String SinglePath => "/1.1/direct_messages/sent.json";
		public override String HttpMethod => "GET";
		public override String RequestFilterKey => "since_id";
		public override String ResponseFilterKey => "id";
		public override String PayloadProperty => "";
		public override TimestampOptions ResponseTimestamp => new TimestampOptions("created_at", "ddd MMM dd HH:mm:ss zzz yyyy", null, null);
		public override PollingInterval PollingInterval => new PollingInterval(1000, 5000, 10000);
	}
}
