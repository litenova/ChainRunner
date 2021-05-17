using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Buildy.Chain;
using Microsoft.Extensions.DependencyInjection;

namespace Buildy.Extensions.MicrosoftDependencyInjection
{
    public interface IChainConfiguration
    {
        IChainConfiguration Register(Assembly assembly);
        IChainConfiguration Register<TRequest>(Func<IChain<TRequest>> chainConfig);
    }

    internal class ChainConfiguration : IChainConfiguration
    {
        public Dictionary<Type, Type> Handlers { get; set; } = new();
        public Dictionary<Type, object> Chains { get; set; } = new();

        public IChainConfiguration Register(Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(IResponsibilityHandler<>))
                    {
                        Handlers.Add(@interface, type);
                    }
                }
            }

            return this;
        }

        public IChainConfiguration Register<TRequest>(Func<IChain<TRequest>> chainConfig)
        {
            var chain = chainConfig();

            Chains.Add(typeof(IChain<TRequest>), chain);

            return this;
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddChain(this IServiceCollection services, Action<IChainConfiguration> config)
        {
            ChainConfiguration chainConfiguration = new ChainConfiguration();
            config(chainConfiguration);

            foreach (var pair in chainConfiguration.Handlers)
            {
                services.AddTransient(pair.Key, pair.Value);
            }

            foreach (var pair in chainConfiguration.Chains)
            {
                services.AddTransient(pair.Key, provider => pair.Value);
            }

            return services;
        }
    }
}