using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vib17
{
    public class UserSession
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
