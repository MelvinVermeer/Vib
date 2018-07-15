using System;
using System.Collections.Generic;

namespace Vib17
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public string Title => $"{Start.ToTimeStringNL()} - {End.ToTimeStringNL()}";

        public string Name { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public List<Session> Sessions { get; set; }
    }
}