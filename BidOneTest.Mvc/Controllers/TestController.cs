using BidOneTest.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BidOneTest.Mvc.Controllers
{
    public class TestController : Controller
    {
        private readonly AppSettings.AppSettings _settings;

        public TestController(IOptions<AppSettings.AppSettings> settingsOptions)
        {
            this._settings = settingsOptions.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ViewModel viewModel)
        {
            try
            {
                using (var httpClient = new HttpClient() { BaseAddress = new Uri(this._settings.ApiRootUrl) })
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