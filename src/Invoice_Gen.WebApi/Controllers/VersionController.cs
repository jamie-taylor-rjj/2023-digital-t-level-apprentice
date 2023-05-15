using Microsoft.AspNetCore.Mvc;
using Invoice_Gen.WebApi.Models;

namespace Invoice_Gen.WebApi.Controllers;

// This controller currently responds to /
// This is a quick way to find out if the app is running correctly.
[ApiController]
[Route("/")]
[Produces("application/json")]
public class VersionController : ControllerBase
{
    /// <summary>
    /// Gets basic information about this app, wrapped in an instance of <see cref="Invoice_Gen.WebApi.Models.VersionResponse" />
    /// </summary>
    /// <returns>
    /// An instance of <see cref="Invoice_Gen.WebApi.Models.VersionResponse" /> which represents the name and version number
    /// for the running application
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(VersionResponse), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new VersionResponse
        {
            VersionNumber = Helpers.CommonHelpers.GetVersionNumber(),
            AppName = Helpers.CommonHelpers.GetAppName()
        });
    }
}
