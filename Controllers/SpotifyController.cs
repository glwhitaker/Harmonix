using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyAPI.Web;

namespace Harmonix.Controllers
{
    public class SpotifyController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration _configuration;
        public SpotifyController(IConfiguration configuration, IHttpContextAccessor context)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserLogin()
        {
            // Generates a secure random verifier of length 100 and its challenge
            var (verifier, challenge) = PKCEUtil.GenerateCodes();

            // add verifier to session
            _context.HttpContext.Session.SetString("SpotifyVerifier", verifier);

            var loginRequest = new LoginRequest(
              new Uri("https://localhost:7092/Spotify"),
              _configuration["Spotify:ClientID"],
              LoginRequest.ResponseType.Code
            )
            {
                CodeChallengeMethod = "S256",
                CodeChallenge = challenge,
                Scope = [Scopes.PlaylistModifyPublic, Scopes.UserLibraryModify, Scopes.UserTopRead]
            };
            var uri = loginRequest.ToUri();


            return Redirect(uri.ToString());
        }

        [HttpGet("/Spotify")]
        // This method should be called from your web-server when the user visits "https://localhost:7092/Spotify"
        public async Task<RedirectToActionResult> GetCallback(string code)
        {
            // catch user cancels
            if (code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var uri = new Uri("https://localhost:7092/Spotify");

            var initialResponse = await new OAuthClient().RequestToken(
            new PKCETokenRequest(_configuration["Spotify:ClientID"], code, uri, _context.HttpContext.Session.GetString("SpotifyVerifier")));

            var authenticator = new PKCEAuthenticator(_configuration["Spotify:ClientID"], initialResponse);

            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            // get user profile
            var user = await spotify.UserProfile.Current();

            // add user to session
            _context.HttpContext.Session.SetString("SpotifyUser", JsonConvert.SerializeObject(user));

            return RedirectToAction("Index", "Generate");
        }

    }
}
