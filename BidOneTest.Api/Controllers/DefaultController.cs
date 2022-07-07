using System.Net;
using BidOneTest.Api.Interfaces;
using BidOneTest.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BidOneTest.Api.Controllers
{
    /// <summary>
    /// Default Controller
    /// </summary>
    [Route("")]
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        // FileHandler interface
        private readonly IFileHandler _fileHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileHandler">FileHandler interface</param>
        public DefaultController(IFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        /// <summary>
        /// Post action
        /// </summary>
        /// <param name="model">Model model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Index([FromBody][Bind("FirstName, LastName")] Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await this._fileHandler.WriteFileAsync($"{DateTime.Now:yyyyMMddhhMMss}.txt",
                    JsonConvert.SerializeObject(model, Formatting.Indented));

                return Ok();
            }
            catch (Exception exception)
            {
                // log exception
                return Problem(exception.InnerException?.Message);
            }
        }
    }
}