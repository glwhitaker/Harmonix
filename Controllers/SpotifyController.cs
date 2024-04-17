using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace Harmonix.Controllers
{
    public class SpotifyController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public SpotifyController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AuthUserAsync()
        {
            var spotify = new SpotifyClient("{Spotify.ClientSecret}");

            var me = await spotify.UserProfile.Current();
            Console.WriteLine($"Hello there {me.DisplayName}");

            await foreach (
              var playlist in spotify.Paginate(await spotify.Playlists.CurrentUsers())
            )
            {
                Console.WriteLine(playlist.Name);
            }

            return View();
        }
    }
}
