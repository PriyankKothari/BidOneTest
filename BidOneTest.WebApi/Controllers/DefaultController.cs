using BidOneTest.WebApi.Interfaces;
using BidOneTest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BidOneTest.WebApi.Controllers
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
        public async Task<IActionResult> Index([FromBody] Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await this._fileHandler.WriteFileAsync($"{DateTime.Now:yyyyMMddhhMMss}.txt",
                    Newtonsoft.Json.JsonConvert.SerializeObject(model));

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