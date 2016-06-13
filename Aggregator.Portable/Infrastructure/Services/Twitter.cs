﻿/*
 * WARNING: This is generated code. Any changes you make will
 * be overwritten. If you wish to modify this class create a
 * partial definition in a seperate file.
 *
 */
using System;
using System.Collections.Generic;
using Aggregator.Framework.Enums;

namespace Aggregator.Infrastructure.Services
{
	[Aggregator.Framework.Metadata.CredentialType(typeof(Aggregator.Infrastructure.Credentials.OAuth1Credentials))]        
	public partial class Twitter : OAuth1Service
	{
		public override Uri RequestUrl => new Uri("https://api.twitter.com/oauth/request_token");
		public override Uri AuthorizeUrl => new Uri("https://api.twitter.com/oauth/authenticate");
		public override Uri AccessUrl => new Uri("https://api.twitter.com/oauth/access_token");
		public override Uri CallbackUrl => new Uri("http://localhost:33633/twitter");
	}
}
