using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wordle.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Wordle.Areas.Identity.Data;
using System.Text;

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
            List<Score> scores = _context.Scores.Where(s => s.User.Id == _userManager.GetUserId(User)).OrderBy(s => s.Date).ToList();
            if(scores == null)
            {
                return NotFound();
            }
            return View("index", scores);
        }

        public IActionResult HighScores()
        {
            List<Score> scores = _context.Scores.Where(s => s.User.Id == _userManager.GetUserId(User)).OrderByDescending(s => s.ScorePoints).Take(3).ToList();
            if (scores == null)
            {
                return NotFound();
            }
            return View("index", scores);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Score score)
        {

            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User); // Get user id:g

            score.User = user;
            score.Date = DateTime.Now;

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
