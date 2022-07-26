# ChainRunner
[![Build](https://github.com/litenova/ChainRunner/actions/workflows/build.yml/badge.svg)](https://github.com/litenova/ChainRunner/actions/workflows/build.yml)
[![Coverage Status](https://coveralls.io/repos/github/litenova/ChainRunner/badge.svg?branch=main)](https://coveralls.io/github/litenova/ChainRunner?branch=main)
[![NuGet](https://img.shields.io/nuget/vpre/ChainRunner.svg)](https://www.nuget.org/packages/ChainRunner)


A simple and innovative library to implement chain of responsibilities. To learn more about chain of responsibilities pattern, read the following article https://refactoring.guru/design-patterns/chain-of-responsibility

* Written in .NET 5
* No Dependencies
* No Reflection
* Easy to Use

## Installation

Depending on your usage, follow one of the guidelines below.

### ASP.NET Core

Install with NuGet:

```
Install-Package ChainRunner
Install-Package ChainRunner.Extensions.MicrosoftDependencyInjection
```

or with .NET CLI:

```
dotnet add package ChainRunner
dotnet add package ChainRunner.Extensions.MicrosoftDependencyInjection
```

and configure your desired as below in the `ConfigureServices` method of `Startup.cs`:

```c#
    services.AddChain<ChainRequest>()
            .WithHandler<ResponsibilityHandler1>()
            .WithHandler<ResponsibilityHandler2>()
            .WithHandler<ResponsibilityHandler3>();
```

## How to Use

Consider the following example and follow the sections in order:

You intend to execute a series of actions (i.e., PersistUserData, SubscribeUserToNewsletter, SendWelcomeEmail) when a new user signs up into system.
First, you create a class the contains all the necessary information to executes these actions. 

```c#
    public class CompleteRegistrationRequest
    {
        public string UserId { get; set; }
    }
```

Then, for each action you create class that implements `IResponsibilityHandler` interface.

```c#
    public class PersistUserDataHandler : IResponsibilityHandler<CompleteRegistrationRequest>
    {
        public Task HandleAsync(SendNotificationRequest request, CancellationToken cancellationToken = default)
        {
            // implementation goes here ...
        }
    }
    
    public class SubscribeUserToNewsletterHandler : IResponsibilityHandler<CompleteRegistrationRequest>
    {
        public Task HandleAsync(SendNotificationRequest request, CancellationToken cancellationToken = default)
        {
            // implementation goes here ...
        }
    }
    
    public class SendWelcomeEmailHandler : IResponsibilityHandler<CompleteRegistrationRequest>
    {
        public Task HandleAsync(SendNotificationRequest request, CancellationToken cancellationToken = default)
        {
            // implementation goes here ...
        }
    }
```

### Setup Pre-Defined Chain with Dependency Injection Support

Setup your chain in the `ConfigureServices` method of `Startup.cs`

```c#
    services.AddChain<CompleteRegistrationRequest>()
            .WithHandler<PersistUserDataHandler>()
            .WithHandler<SubscribeUserToNewsletterHandler>()
            .WithHandler<SendWelcomeEmailHandler>();
```

Inject your chain to your desired class and run it

```c#
    [ApiController]
    [Route("[controller]")]
    public class Controller
    {
        private readonly IChain<CompleteRegistrationRequest> _chain;

        public Controller(IChain<CompleteRegistrationRequest> chain)
        {
            _chain = chain;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CompleteRegistrationRequest request)
        {
            await _chain.RunAsync(request);

            return Ok();
        }
    }
```

### Setup Chain on Demand with Dependency Injection Support

Inject `IChainBuilder` to your desired class and setup you chain.

```c#
    [ApiController]
    [Route("[controller]")]
    public class Controller
    {
        private readonly IChainBuilder _chainBuilder;

        public Controller(IChainBuilder chainBuilder)
        {
            _chainBuilder = chainBuilder;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(CompleteRegistrationRequest request)
        {
            var chain = _chainBuilder.For<CompleteRegistrationRequest>()
                                     .WithHandler<PersistUserDataHandler>()
                                     .WithHandler<SubscribeUserToNewsletterHandler>()
                                     .WithHandler<SendWelcomeEmailHandler>()
                                     .Build();

            await chain.RunAsync(request);                 
        }
    }    
```
Before using `IChainBuilder`, make sure to call `AddChainRunner(assemblies)` method in `ConfigureServices()` of `Startup.cs` class to register `IChainBuilder` and handlers.

```c#
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddChainRunner(typeof(PersistUserDataHandler).Assembly);
    }
```

### Setup Chain on Demand Without Dependency Injection Support

You can use the `ChainBuilder<T>` class to build chains without the need of DI. The `WithHandler()` method either accepts a handler with empty constructor or a pre-initialized instance of a handler.

```c#
    [ApiController]
    [Route("[controller]")]
    public class Controller
    {
        [HttpPost]
        public async Task<IActionResult> Register(CompleteRegistrationRequest request)
        {
        
            var chain = ChainBuilder.For<CompleteRegistrationRequest>()
                                    .WithHandler<PersistUserDataHandler>() // pass the handler with empty constructor
                                    .WithHandler<SubscribeUserToNewsletterHandler>() // pass the handler with empty constructor
                                    .WithHandler(new SendWelcomeEmailHandler()) // pass the handler instance 
                                    .Build();              
            
            await chain.RunAsync(request);                    
        }
    }
```

