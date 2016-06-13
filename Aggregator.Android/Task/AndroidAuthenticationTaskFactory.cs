using System;
using Android.Content;
using Aggregator.Domain.Write;
using Aggregator.Framework.Contracts;
using Aggregator.Framework.Enums;
using Aggregator.Infrastructure;
using Aggregator.Task.Authentication;
using Aggregator.Task.Factories;
using SimpleCQRS.Framework.Contracts;

namespace Aggregator.Task
{
    public class AndroidAuthenticationTaskFactory : AuthenticationTaskFactory
    {
        public AndroidAuthenticationTaskFactory(
            IBluetoothAuthorizerFactory bluetoothAuthorizerFactory,
            IWebAuthorizerFactory webAuthorizerFactory,
            IClientCredentials clientCredentials,
            ICommandSender sender,
            IOAuthFactory oauthFactory) : 
                base(
                bluetoothAuthorizerFactory,
                webAuthorizerFactory, 
                clientCredentials, 
                sender, 
                oauthFactory)
        {
        }

        public OAuth1AuthenticationTask<TService> GenerateOAuth1Task<TService>(
            Context context,
            AuthenticationInterfaceEnum browserType = AuthenticationInterfaceEnum.Embedded)
            where TService : OAuth1Service, new()
        {
            return (OAuth1AuthenticationTask<TService>)GenerateTask<TService>(
                Guid.Empty,
                -1,
                context,
                browserType);
        }

        public OAuth2AuthenticationTask<TService> GenerateOAuth2Task<TService>(
            Context context,
            AuthenticationInterfaceEnum browserType = AuthenticationInterfaceEnum.Embedded)
            where TService : OAuth2Service, new()
        {
            return (OAuth2AuthenticationTask<TService>)GenerateTask<TService>(
                Guid.Empty,
                -1,
                context,
                browserType);
        }

        public ITask GenerateTask<TService>(
            Guid aggregateId,
            int originalVersion,
            Context context,
            AuthenticationInterfaceEnum browserType = AuthenticationInterfaceEnum.Embedded,
            bool tokenAlreadyExists = false)
            where TService : Service, new()
        {
            return base.GenerateTask<TService>(
                aggregateId,
                originalVersion,
                context,
                browserType,
                tokenAlreadyExists);
        }
    }
}