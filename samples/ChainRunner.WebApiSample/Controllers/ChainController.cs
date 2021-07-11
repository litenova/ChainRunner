using System.Threading.Tasks;
using ChainRunner.WebApiSample.Chain;
using Microsoft.AspNetCore.Mvc;

namespace ChainRunner.WebApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChainController : ControllerBase
    {
        private readonly IChain<ChainRequest> _chain;
        private readonly IChainBuilder _chainBuilder;

        public ChainController(IChain<ChainRequest> chain, IChainBuilder chainBuilder)
        {
            _chain = chain;
            _chainBuilder = chainBuilder;
        }

        [HttpGet("run-predefined-chain")]
        public async Task<IActionResult> RunChain()
        {
            await _chain.RunAsync(new ChainRequest());

            return Ok();
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