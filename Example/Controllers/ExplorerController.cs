using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Example.Controllers;
/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExplorerController : ControllerBase
{
    readonly ILogger<ExplorerController> _logger;
    readonly IHostEnvironment _environment;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environement"></param>
    public ExplorerController(ILogger<ExplorerController> logger, IHostEnvironment environement)
    {
        _logger = logger;
        _environment = environement;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Microsoft.Extensions.FileProviders.IDirectoryContents))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Microsoft.Extensions.FileProviders.IFileInfo))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public IActionResult Root() => Get(string.Empty);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpGet("{*path}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Microsoft.Extensions.FileProviders.IDirectoryContents))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Microsoft.Extensions.FileProviders.IFileInfo))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public IActionResult Get(string path = "")
    {
        path = (path ?? "").TrimStart().Replace("%2f", "/", StringComparison.InvariantCultureIgnoreCase);
        _logger.LogTrace("Get (path:{path})", path);
        try
        {
            var results = _environment.ContentRootFileProvider.GetDirectoryContents(path);
            if (results.Exists)
                return Ok(results);
        }
        catch
        {
            throw;
        }
        try
        {
            var result = _environment.ContentRootFileProvider.GetFileInfo(path);
            if (result.Exists)
                return Ok(result);
        }
        catch
        {
            throw;
        }
        return NotFound();
    }
}
