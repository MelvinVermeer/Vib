using System.Collections.Generic;
using System.Linq;

namespace Vib17
{
    public class Session
    {
        public Session()
        {
            Attendees = new List<UserSession>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Speaker { get; set; }

        public string Location { get; set; }

        public bool HasAttendee(string attendeeId) => IsOptional && Attendees.Any(u => u.UserId == attendeeId);

        public bool IsOptional { get; set; }
        
        public int TimeSlotId { get; set; }

        public TimeSlot TimeSlot { get; set; }

        /// <summary>
        /// Maximum number of attendees for this session
        /// </summary>
        public int Limit{ get; set; }

        public bool HasLimit => Limit > 0;

        /// <summary>
        /// A Session is full when it has a limit and the limit is reached
        /// </summary>
        public bool IsFull => HasLimit && Attendees.Count >= Limit;

        /// <summary>
        /// Describes whether a session is open for registration. This holds true for sessions that are optional and not yet full.
        /// </summary>
        public bool Open => IsOptional && !IsFull;

        public List<UserSession> Attendees { get; set; }
    }
}
