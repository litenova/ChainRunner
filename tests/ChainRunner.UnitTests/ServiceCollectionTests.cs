using System;
using System.Linq;
using ChainRunner.UnitTests.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace ChainRunner.UnitTests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void AddChain_should_add_required_services_to_service_collection()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();

            // Act
            services.AddChain<FakeChainRequest>()
                    .WithHandler<FirstFakeResponsibilityHandler>()
                    .WithHandler<SecondFakeResponsibilityHandler>()
                    .WithHandler<ThirdFakeResponsibilityHandler>();

            // Assert
            var chainServiceDescriptor = services
                .SingleOrDefault(sd => sd.ServiceType == typeof(IChain<FakeChainRequest>));
            chainServiceDescriptor.Lifetime.Should().Be(ServiceLifetime.Singleton);

            var handlerDescriptors = 
                services.Where(sd => sd.ServiceType.IsAssignableTo(typeof(IResponsibilityHandler<FakeChainRequest>)));

            handlerDescriptors.Should().HaveCount(3);
            
            foreach (var handlerDescriptor in handlerDescriptors)
            {
                handlerDescriptor.Lifetime.Should().Be(ServiceLifetime.Transient);
            }
        }
        
        [Fact]
        public void AddChainRunner_should_add_handlers_to_service_collection()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();

            // Act
            services.AddChainRunner(typeof(FirstFakeResponsibilityHandler).Assembly);

            // Assert
            services.Should().ContainSingle(d => d.ImplementationType == typeof(FirstFakeResponsibilityHandler));
            services.Should().ContainSingle(d => d.ImplementationType == typeof(SecondFakeResponsibilityHandler));
            services.Should().ContainSingle(d => d.ImplementationType == typeof(ThirdFakeResponsibilityHandler));
        }
    }
}