﻿using Material.Infrastructure.ProtectedResources;

namespace Quantfabric.Test.Integration
{
    public class GoogleMock : OAuth2ResourceProviderMock
    {
        public GoogleMock() : base(new Google()) { }
    }

    public class FacebookMock : OAuth2ResourceProviderMock
    {
        public FacebookMock() : base(new Facebook()) { }
    }

    public class FitbitMock : OAuth2ResourceProviderMock
    {
        public FitbitMock() : base(new Fitbit()) { }
    }

    public class RescuetimeMock : OAuth2ResourceProviderMock
    {
        public RescuetimeMock() : base(new Rescuetime()) { }
    }

    public class RunkeeperMock : OAuth2ResourceProviderMock
    {
        public RunkeeperMock() : base(new Runkeeper()) { }
    }

    public class FoursquareMock : OAuth2ResourceProviderMock
    {
        public FoursquareMock() : base(new Foursquare()) { }
    }

    public class TwentyThreeAndMeMock : OAuth2ResourceProviderMock
    {
        public TwentyThreeAndMeMock() : base(new TwentyThreeAndMe()) { }
    }

    public class LinkedInMock : OAuth2ResourceProviderMock
    {
        public LinkedInMock() : base(new LinkedIn()) { }
    }

    public class InstagramMock : OAuth2ResourceProviderMock
    {
        public InstagramMock() : base(new Instagram()) { }
    }

    public class PinterestMock : OAuth2ResourceProviderMock
    {
        public PinterestMock() : base(new Pinterest()) { }
    }

    public class SpotifyMock : OAuth2ResourceProviderMock
    {
        public SpotifyMock() : base(new Spotify()) { }
    }
}