using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vib17.Data;

namespace Vib17.Controllers
{
    [Produces("application/json")]
    [Route("api/sessions")]
    [Authorize]
    public class SessionsController : Controller
    {
        private readonly UserManager<ApplicationUser> _users;

        private readonly ApplicationDbContext _db;

        public SessionsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _users = userManager;
            _db = db;
        }

        [HttpPost]
        [Route("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] Session input)
        {
            var session = await _db.Sessions
                .Include(s => s.Attendees)
                .SingleAsync(s => s.Id == input.Id);

            var user = await _users.GetUserAsync(User);

            if (!session.IsFull)
            {
                await UnsubscribeSessionsInTimeSlot(session.TimeSlotId, user);
                SubscribeToSession(session, user);

                await _db.SaveChangesAsync();
            }

            var sessionIds = await _db.UserSessions.Where(s => s.UserId == user.Id).Select(s => s.SessionId).ToArrayAsync();
            return Json(sessionIds);
        }

        private void SubscribeToSession(Session session, ApplicationUser user)
        {
            var userSession = new UserSession { UserId = user.Id, SessionId = session.Id };
            _db.UserSessions.Add(userSession);
        }

        private async Task UnsubscribeSessionsInTimeSlot(int timeslotId, ApplicationUser user)
        {
            var alreadySubscribedSessions = await _db.UserSessions
                                .Where(us => us.UserId == user.Id)
                                .Where(us => us.Session.TimeSlotId == timeslotId)
                                .ToListAsync();

            _db.UserSessions.RemoveRange(alreadySubscribedSessions);
        }

        [HttpPost]
        [Route("unsubscribe")]
        public async Task<IActionResult> Unsbscribe([FromBody] Session session)
        {
            var user = await _users.GetUserAsync(User);

            var userSession = new UserSession { UserId = user.Id, SessionId = session.Id };
            _db.UserSessions.Remove(userSession);

            await _db.SaveChangesAsync();

            var sessionIds = await _db.UserSessions.Where(s => s.UserId == user.Id).Select(s => s.SessionId).ToArrayAsync();
            return Json(sessionIds);
        }

        [HttpGet]
        [Route("subscribed")]
        public async Task<IActionResult> Subscribed()
        {
            var user = await _users.GetUserAsync(User);
            var sessionIds = await _db.UserSessions.Where(s => s.UserId == user.Id).Select(s => s.SessionId).ToArrayAsync();
            return Json(sessionIds);
        }

        [HttpGet]
        [Route("full")]
        public async Task<IActionResult> Full()
        {
            var optioneleSessies = await _db.Sessions
                .Where(s => s.IsOptional && s.Limit > 0)
                .Include(s =>s.Attendees).ToListAsync();

            return Json(optioneleSessies.Where(s => s.IsFull).Select(s=>s.Id).ToArray());
        }
    }
}