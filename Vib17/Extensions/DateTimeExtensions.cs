using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Vib17
{
    public static class DateTimeExtensions
    {
        public static string ToTimeStringNL(this DateTimeOffset time)
        {
            var format = "H:mm";
            var NL = new CultureInfo("NL-nl");

            return time.ToString(format, NL);
        }
    }
}
