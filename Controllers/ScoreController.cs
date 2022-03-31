using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wordle.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Wordle.Areas.Identity.Data;

namespace Wordle.Controllers
{

    public class ScoreController : Controller
    {
        private readonly AuthDbContext _context;
        private UserManager<User> _userManager;

        public ScoreController(AuthDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Word word = new Word();
            return View("index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JsonElement value)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User); // Get user id:

            var score = new Score();

            score.ScorePoints = 1;
            score.Tries = 3;
            score.User = user;

            _context.Scores.Add(score);
            _context.SaveChanges();

            return new StatusCodeResult(200);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
