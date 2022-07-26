using System.Threading.Tasks;
using ChainRunner.WebApiSample.Chain;
using Microsoft.AspNetCore.Mvc;

namespace ChainRunner.WebApiSample.Controllers;

[ApiController]
[Route("[controller]")]
public class ChainController : ControllerBase
{
    private readonly IChain<ChainRequest> _chain;

    public ChainController(IChain<ChainRequest> chain)
    {
        _chain = chain;
    }

    [HttpGet("run-predefined-chain")]
    public async Task<IActionResult> RunChain()
    {
        await _chain.RunAsync(new ChainRequest());

        return Ok();
    }
}