using Invoice_Gen.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
    /// Returns a new instance of <see cref="HelloResponse"/>
    /// </summary>
    /// <returns>
    /// A  new instance of <see cref="HelloResponse"/> with some default data
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
