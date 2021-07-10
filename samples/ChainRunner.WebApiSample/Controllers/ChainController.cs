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

        public ChainController(IChain<ChainRequest> chain)
        {
            _chain = chain;
        }

        [HttpGet("run-sample-chain")]
        public async Task<IActionResult> RunChain()
        {
            await _chain.RunAsync(new ChainRequest());

            return Ok();
        }
        
        [HttpGet("run-sample-chain-2")]
        public async Task<IActionResult> RunChain2()
        {
            var chain = new ChainBuilder<ChainRequest>()
                        .WithHandler<ResponsibilityHandler1>()
                        .WithHandler<ResponsibilityHandler2>()
                        .WithHandler<ResponsibilityHandler3>()
                        .Build();
            
            await chain.RunAsync(new ChainRequest());

            return Ok();
        }
    }
}