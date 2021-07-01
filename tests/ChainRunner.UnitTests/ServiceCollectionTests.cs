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
        public void required_should_be_added_to_service_collection()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();

            // Act
            services.AddChain<FakeChainRequest>()
                    .WithHandler<FirstFakeChainHandler>()
                    .WithHandler<SecondFakeChainHandler>()
                    .WithHandler<ThirdFakeChainHandler>();

            // Assert
            var chainServiceDescriptor = services
                .SingleOrDefault(sd => sd.ServiceType == typeof(IChain<FakeChainRequest>));
            chainServiceDescriptor.Lifetime.Should().Be(ServiceLifetime.Transient);

            var handlerDescriptors = 
                services.Where(sd => sd.ServiceType.IsAssignableTo(typeof(IResponsibilityHandler<FakeChainRequest>)));

            handlerDescriptors.Should().HaveCount(3);
            
            foreach (var handlerDescriptor in handlerDescriptors)
            {
                handlerDescriptor.Lifetime.Should().Be(ServiceLifetime.Transient);
            }
        }
    }
}