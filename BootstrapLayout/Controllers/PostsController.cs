using BootstrapLayout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BootstrapLayout.Controllers
{

    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize(Policy = "WebPolicy")]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://dummyjson.com/posts");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Root root = JsonConvert.DeserializeObject<Root>(json);
            return View(root.posts);
        }
    }
}
