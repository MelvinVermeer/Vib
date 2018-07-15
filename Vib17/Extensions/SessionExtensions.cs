using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Vib17
{
    public static class SessionExtensions
    {
        public static string ClassList(this Session session, ClaimsPrincipal user)
        {
            var classList = new List<string>{ "session" };

            if(session.IsFull)
                classList.Add("full");

            if (!session.IsOptional)
                classList.Add("group-session");

            if (session.HasAttendee(user.GetId()))
                classList.Add("subscribed");

            return string.Join(" ", classList);
        } 
    }
}
