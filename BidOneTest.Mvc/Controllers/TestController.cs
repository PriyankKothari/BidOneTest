using BidOneTest.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BidOneTest.Mvc.Controllers
{
    /// <summary>
    /// Test Controller
    /// </summary>
    public class TestController : Controller
    {
        // AppSettings interface
        private readonly AppSettings.AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settingsOptions">AppSettings Options</param>
        public TestController(IOptions<AppSettings.AppSettings> appSettingsOptions)
        {
            this._appSettings = appSettingsOptions.Value;
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>Index view</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <param name="viewModel">View Model</param>
        /// <returns>Redirects to Index action or renders Error view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ViewModel viewModel)
        {
            try
            {
                using (var httpClient = new HttpClient() { BaseAddress = new Uri(this._appSettings.ApiRootUrl) })
                {
                    var postJsonAsyncTask = await httpClient.PostAsJsonAsync<ViewModel>("", viewModel);

                    return postJsonAsyncTask.IsSuccessStatusCode
                        ? RedirectToAction("Index")
                        : View("Error", new ErrorViewModel {ErrorMessage = postJsonAsyncTask.ReasonPhrase ?? "Something went wrong"});
                }
            }
            catch (Exception exception)
            {
                // log exception
                return Problem($"Something went wrong: {exception.InnerException?.Message}");
            }
        }
    }
}