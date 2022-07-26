using System.Linq;
using ChainRunner.UnitTests.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ChainRunner.UnitTests;

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

        var chainType = typeof(IChain<FakeChainRequest>);
        var responsibilityHandlerType = typeof(IResponsibilityHandler<FakeChainRequest>);

        // Assert
        services.Should()
                .ContainSingle(d => d.ServiceType == chainType)
                .Which.Lifetime.Should()
                .Be(ServiceLifetime.Singleton);

        services.Where(d => d.ServiceType.IsAssignableTo(responsibilityHandlerType))
                .Should()
                .HaveCount(3)
                .And
                .OnlyContain(descriptor => descriptor.Lifetime == ServiceLifetime.Transient);
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