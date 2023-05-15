using Microsoft.AspNetCore.Mvc;
using Invoice_Gen.WebApi.Models;

namespace Invoice_Gen.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class HelloController : ControllerBase
{
    private readonly ILogger<HelloController> _logger;

    public HelloController(ILogger<HelloController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns a new instance of <see cref="Invoice_Gen.WebApi.Models.HelloResponse"/>
    /// </summary>
    /// <returns>
    /// A  new instance of <see cref="Invoice_Gen.WebApi.Models.HelloResponse"/> with some default data
    /// </returns>
    [HttpGet(Name = "GetHello")]
    [ProducesResponseType(typeof(HelloResponse), StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new HelloResponse
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            Message = "Howdy!"
        }
    );
}
