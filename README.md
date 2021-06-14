# ChainRunner
[![.NET 5 CI](https://github.com/litenova/ChainRunner/actions/workflows/dotnet.yml/badge.svg)](https://github.com/litenova/ChainRunner/actions/workflows/dotnet.yml)
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

## Usages

Using the class below you intend to send notifications to your desired user in the form of email, SMS and Telegram message.

```c#
    public class SendNotificationRequest
    {
        public string UserId { get; set; }
    }
```

For each form of notification, you create a responsibility handler.

```c#
    public class SendEmailHandler : IResponsibilityHandler<SendNotificationRequest>
    {
        public Task HandleAsync(SendNotificationRequest request, CancellationToken cancellationToken = default)
        {
            // send notification using email
        }
    }
    
    public class SendSmsHandler : IResponsibilityHandler<SendNotificationRequest>
    {
        public Task HandleAsync(SendNotificationRequest request, CancellationToken cancellationToken = default)
        {
            // send notification using sms
        }
    }
    
    public class SendTelegramMessageHandler : IResponsibilityHandler<SendNotificationRequest>
    {
        public Task HandleAsync(SendNotificationRequest request, CancellationToken cancellationToken = default)
        {
            // send notification using Telegram message
        }
    }
```

### Use with DI
Setup your chain in DI container

```c#
    services.AddChain<SendNotificationRequest>()
            .WithHandler<SendEmailHandler>()
            .WithHandler<SendSmsHandler>()
            .WithHandler<SendTelegramMessageHandler>();
```

Inject your chain to your class and run it

```c#
    [ApiController]
    [Route("[controller]")]
    public class NotificationController
    {
        private readonly IChain<ChainRequest> _chain;

        public NotificationController(IChain<ChainRequest> chain)
        {
            _chain = chain;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(SendNotificationRequest request)
        {
            await _chain.RunAsync(request);

            return Ok();
        }
    }
```

### Use Without DI

Inject your chain to your class and run it. You can either pass instance of a handler to `WithHandler` method or the handler should have a public empty constructor  

```c#
    [ApiController]
    [Route("[controller]")]
    public class NotificationController
    {
        [HttpPost]
        public async Task<IActionResult> SendNotification(SendNotificationRequest request)
        {
            var chain = new ChainBuilder<ChainRequest>()
                        .WithHandler<SendEmailHandler>() // pass the handler with empty constructor
                        .WithHandler(new SendSmsHandler()) // pass the handler instance 
                        .WithHandler(new SendTelegramMessageHandler()) // pass the handler instance
                        .Build();
            
            await chain.RunAsync(new ChainRequest());                    
        }
    }
```