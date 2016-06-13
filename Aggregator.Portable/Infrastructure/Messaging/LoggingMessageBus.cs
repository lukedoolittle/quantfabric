﻿using System;
using Autofac;
using BatmansBelt.Extensions;
using Aggregator.Framework.Contracts;
using SimpleCQRS.Autofac;
using SimpleCQRS.Framework.Contracts;

namespace Aggregator.Infrastructure.Messaging
{
    using System.Threading.Tasks;

    public class LoggingMessageBus : DynamicSubscriptionMessageBus
    {
        private readonly ILogger _logger;

        public LoggingMessageBus(
            ILogger logger,
            IComponentContext context) :
            base(context)
        {
            _logger = logger;
        }

        public override Task Publish<T>(T @event)
        {
            _logger.Info($"Publishing event {typeof(T).GetGenericName()}");

            return base.Publish(@event);
        }

        public override Task Send<T>(T command)
        {
            _logger.Info($"Sending command {typeof(T).GetGenericName()}");

            return base.Send(command);
        }

        public override ISubscription Subscribe<TMessage>(Action<TMessage> action) 
        {
            _logger.Info($"Subscribing to message {typeof(TMessage).GetGenericName()}");

            return base.Subscribe(action);
        }

        public override void UnSubscribe(ISubscription subscription)
        {
            _logger.Info($"Unsubscribing from an event {subscription.ActionType.GetGenericName()}");

            base.UnSubscribe(subscription);
        }
    }
}
