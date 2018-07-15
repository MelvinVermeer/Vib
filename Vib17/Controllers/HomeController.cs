using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vib17.Models;
using Vib17.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Vib17.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _db.Sessions
                .Include(s => s.Attendees)
                .Include(s => s.TimeSlot)
                .OrderBy(s => s.TimeSlot.Start)
                .ToListAsync();

            return View(sessions);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
