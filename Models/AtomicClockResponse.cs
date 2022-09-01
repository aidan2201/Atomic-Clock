using System;

namespace Atomic_Clock.Models
{
    public class AtomicClockResponse
    {
        public string abbreviation { get; set; }

        public string Ip { get; set; }

        public DateTime DateTime { get; set; }

        public int dayOfWeek { get; set; }

        public int dayOfYear { get; set; }

        public int weekNumber { get; set; }

        public string timezone { get; set; }

        public string utc_offset { get; set; }

        public DateTime utc_datetime { get; set; }

    }
}