using BidOneTest.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BidOneTest.Mvc.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ViewModel viewModel)
        {
            try
            {
                using (var httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7000") })
                {
                    var postJsonAsyncTask = httpClient.PostAsJsonAsync<ViewModel>("", viewModel);

                    return postJsonAsyncTask.Result.IsSuccessStatusCode
                        ? RedirectToAction("Index")
                        : View("Error",
                            new ErrorViewModel
                                {ErrorMessage = postJsonAsyncTask.Result.ReasonPhrase ?? "Something went wrong"});
                }
            }
            catch (Exception exception)
            {
                return Problem($"Something went wrong: {exception.InnerException?.Message}");
            }
        }
    }
}