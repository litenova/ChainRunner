using System.Threading.Tasks;
using ChainRunner.WebApiSample.Chain;
using Microsoft.AspNetCore.Mvc;

namespace ChainRunner.WebApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnDemandChainController : ControllerBase
    {
        private readonly IChainBuilder _chainBuilder;

        public OnDemandChainController(IChainBuilder chainBuilder)
        {
            _chainBuilder = chainBuilder;
        }

        [HttpGet("run-chain-built-on-demand")]
        public async Task<IActionResult> RunChain2()
        {
            var chain = ChainBuilder.For<ChainRequest>()
                                    .WithHandler<ResponsibilityHandler1>()
                                    .WithHandler<ResponsibilityHandler2>()
                                    .WithHandler<ResponsibilityHandler3>()
                                    .Build();

            await chain.RunAsync(new ChainRequest());

            return Ok();
        }

        [HttpGet("run-chain-built-on-demand-with-di-support")]
        public async Task<IActionResult> RunChain3()
        {
            var chain = _chainBuilder.For<ChainRequest>()
                                     .WithHandler<ResponsibilityHandler1>()
                                     .WithHandler<ResponsibilityHandler2>()
                                     .WithHandler<ResponsibilityHandler3>()
                                     .Build();

            await chain.RunAsync(new ChainRequest());

            return Ok();
        }
    }
}