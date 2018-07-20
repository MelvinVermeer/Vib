using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Vib17.Data;
using Vib17.Models;

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

        public IActionResult Contact() => View();

        public IActionResult Error()
        {
            var error = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(error);
        }
    }
}
