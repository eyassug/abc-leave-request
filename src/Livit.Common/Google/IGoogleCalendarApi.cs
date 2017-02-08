using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Common.Google
{
    public interface IGoogleCalendarApi
    {
        Task<Event> CreateEvent(CalendarService calendarService, Event @event, string calendarId);
    }
}
