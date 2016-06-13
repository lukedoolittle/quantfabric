﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using BatmansBelt.Serialization;
using Newtonsoft.Json.Linq;
using Aggregator.Domain.Events;
using Aggregator.Domain.Read;
using Aggregator.Domain.Write;
using Aggregator.Domain.Write.Samples;
using Aggregator.Framework.Enums;
using Aggregator.Framework.Serialization;
using Aggregator.Infrastructure.Credentials;
using Aggregator.Infrastructure.Requests;
using Aggregator.Infrastructure.Services;
using Aggregator.Test.Helpers;
using SimpleCQRS.Domain;
using SimpleCQRS.Infrastructure;
using Xunit;

namespace Aggregator.Test.Unit.JsonHandling
{
    
    public class JsonSerializationFixture : IClassFixture<GlobalizationFixture>
    {
        [Fact]
        public void JsonSerializeAndDeserialize()
        {
            var id = Guid.NewGuid();
            var expected = new EventDescriptors {Id = id};
            var events = new List<Event>
            {
                new PersonCreated(Guid.NewGuid()),
                new TokenCreated<Facebook>(new JObject()),
                new SensorCreated<FacebookFeed>(Guid.NewGuid(), PollingIntervalEnum.Fast, new JObject()),
                new FilterChanged<FacebookFeed>("someNewFilter")
            };

            expected.Add(new EventDescriptor {Id = Guid.NewGuid(), EventData = events[0], Version = 0});
            expected.Add(new EventDescriptor {Id = Guid.NewGuid(), EventData = events[1], Version = -5});
            expected.Add(new EventDescriptor { Id = Guid.NewGuid(), EventData = events[2], Version = 1 });
            expected.Add(new EventDescriptor { Id = Guid.NewGuid(), EventData = events[3], Version = 134 });
            
            var intermediate = expected.AsJson();
            var actual = intermediate.AsEntity<EventDescriptors>();

            Assert.Equal(id, actual.Id);
            Assert.Equal(expected.Count(), actual.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
                Assert.Equal(expected[i].EventData.GetType(), actual[i].EventData.GetType());
                Assert.Equal(expected[i].Version, actual[i].Version);
            }
        }

        [Fact]
        public void ConvertNameValueCollectionToEntity()
        {
            var expected = new NameValueCollection
            {
                {"ConsumerKey", "someValue"},
                {"ConsumerSecret", "anotherValue"}
            };

            var actual = expected.AsEntity<OAuth1Credentials>();

            Assert.Equal(expected["ConsumerKey"].ToString(), actual.ConsumerKey);
            Assert.Equal(expected["ConsumerSecret"].ToString(), actual.ConsumerSecret);
        }

        [Fact]
        public void ConvertObjectToDictionaryAndThenBackToObject()
        {
            var id = Guid.NewGuid();
            var expected = new EventDescriptors { Id = id };
            var events = new List<Event>
            {
                new PersonCreated(Guid.NewGuid()),
                new TokenCreated<Facebook>(new JObject()),
                new SensorCreated<FacebookFeed>(Guid.NewGuid(), PollingIntervalEnum.Fast, new JObject()),
                new FilterChanged<FacebookFeed>("someNewFilter")
            };

            expected.Add(new EventDescriptor { Id = Guid.NewGuid(), EventData = events[0], Version = 0 });
            expected.Add(new EventDescriptor { Id = Guid.NewGuid(), EventData = events[1], Version = -5 });
            expected.Add(new EventDescriptor { Id = Guid.NewGuid(), EventData = events[2], Version = 1 });
            expected.Add(new EventDescriptor { Id = Guid.NewGuid(), EventData = events[3], Version = 134 });

            var intermediate = expected.AsDictionary();

            Assert.False(intermediate.Any(a => a.Key.StartsWith("_") || a.Key.StartsWith("$")));
            Assert.True(intermediate.Keys.Contains("Type"));

            var actual = intermediate.AsEntity<EventDescriptors>();

            Assert.Equal(id, actual.Id);
            Assert.Equal(expected.Count(), actual.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
                Assert.Equal(expected[i].EventData.GetType(), actual[i].EventData.GetType());
                Assert.Equal(expected[i].Version, actual[i].Version);
            }
        }

        [Fact]
        public void JsonSerializeAndDeserializeSensorDto()
        {
            var expected = new SensorDto(
                Guid.NewGuid(), 
                Guid.NewGuid(), 
                PollingIntervalEnum.Fast, 
                new JObject(), 
                typeof(Service), 
                typeof(Request), "", 0);

            var actual = SerializeThenDeserializeJson(expected);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.PollingInterval, actual.PollingInterval);
            Assert.Equal(expected.AggregateId, actual.AggregateId);
            Assert.Equal(expected.LastSample, actual.LastSample);

        }

        [Fact]
        public void JsonSerializeAndDeserializeSampleDto()
        {
            var expected = new SampleDto(DateTimeOffset.Now, new JObject(), Guid.NewGuid());

            var actual = SerializeThenDeserializeJson(expected);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Timestamp, actual.Timestamp);
        }

        [Fact]
        public void DictionarySerializeAndDeserializeSensorDto()
        {
            var expected = new SensorDto(
                Guid.NewGuid(),
                Guid.NewGuid(),
                PollingIntervalEnum.Fast,
                new JObject(),
                typeof(Service),
                typeof(Request), "", 0);

            var actual = SerializeThenDeserializeDictionary(expected);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.PollingInterval, actual.PollingInterval);
            Assert.Equal(expected.AggregateId, actual.AggregateId);
            Assert.Equal(expected.LastSample, actual.LastSample);

        }

        [Fact]
        public void DictionarySerializeAndDeserializeSampleDto()
        {
            var expected = new SampleDto(DateTimeOffset.Now, new JObject(), Guid.NewGuid());

            var actual = SerializeThenDeserializeDictionary(expected);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Timestamp, actual.Timestamp);
        }

        private T SerializeThenDeserializeJson<T>(T item)
        {
            var intermediate = item.AsJson();
            var actual = intermediate.AsEntity<T>();
            return actual;
        }

        private T SerializeThenDeserializeDictionary<T>(T item)
        {
            var intermediate = item.AsDictionary();

            Assert.False(intermediate.Any(a => a.Key.StartsWith("_") || a.Key.StartsWith("$")));
            Assert.True(intermediate.Keys.Contains("Type"));

            var actual = intermediate.AsEntity<T>();

            return actual;
        }
    }
}
