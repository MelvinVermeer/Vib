using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vib17.Data;

namespace Vib17.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("admin")]
        public async Task<ActionResult> Sessions()
        {
            var sessions = await _db.Sessions
                .Include(s => s.TimeSlot)
                .Include(s => s.Attendees)
                .OrderBy(s => s.TimeSlot.Start)
                .ToListAsync();
            return View(sessions);
        }

        public async Task<ActionResult> EditSession(int id)
        {
            var session = await _db.Sessions
                .Include(s => s.TimeSlot)
                .SingleAsync(s => s.Id == id);

            session.Attendees = await _db.UserSessions
                .Where(x => x.SessionId == id)
                .Include(x => x.User)
                .ToListAsync();

            var timeslots = await _db.Timeslots.ToListAsync();


            ViewBag.TimeSlots = timeslots.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Title,
                Selected = session.TimeSlot.Id == t.Id
            });
            return View(session);
        }

        public async Task<ActionResult> NewSession()
        {
            var timeslots = await _db.Timeslots.ToListAsync();
            ViewBag.TimeSlots = timeslots.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Title });
            return View(new Session());
        }

        public async Task<ActionResult> SaveSession(Session input)
        {
            Session session;

            if (input.Id > 0)
            {
                session = await _db.Sessions.SingleAsync(s => s.Id == input.Id);
                session.Title = input.Title;
                session.IsOptional = input.IsOptional;
                session.Limit = input.Limit;
                session.TimeSlotId = input.TimeSlotId;
                session.Speaker = input.Speaker;
                session.Location = input.Location;
                session.Description = input.Description;
            }
            else
            {
                _db.Sessions.Add(new Session
                {
                    Title = input.Title,
                    Limit = input.Limit,
                    IsOptional = input.IsOptional,
                    TimeSlotId = input.TimeSlotId,
                    Speaker = input.Speaker,
                    Location = input.Location,
                    Description = input.Description
                });
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Sessions));
        }

        public async Task<ActionResult> Timeslots()
        {
            var slots = await _db.Timeslots.ToListAsync();
            return View(slots);
        }

        public async Task<ActionResult> EditTimeslot(int id)
        {
            var slot = await _db.Timeslots.SingleAsync(s => s.Id == id);
            return View();
        }

        public ActionResult NewTimeslot() => View(new TimeSlot());

        public async Task<ActionResult> SaveTimeslot(TimeSlot input)
        {
            TimeSlot slot;

            if (input.Id > 0)
            {
                slot = await _db.Timeslots.SingleAsync(s => s.Id == input.Id);
                slot.Start = new DateTimeOffset(2017, 11, 8, input.Start.Hour, input.Start.Minute, 0, input.Start.Offset);
                slot.End = new DateTimeOffset(2017, 11, 8, input.End.Hour, input.End.Minute, 0, input.End.Offset);
            }
            else
            {
                _db.Timeslots.Add(new TimeSlot
                {
                    Start = new DateTimeOffset(2017, 11, 8, input.Start.Hour, input.Start.Minute,0, input.Start.Offset),
                    End = new DateTimeOffset(2017, 11, 8, input.End.Hour, input.End.Minute, 0, input.End.Offset)
                });
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Sessions));
        }
    }
}