using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainRunner.Abstractions;
using ChainRunner.WebApiSample.Chain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    }
}