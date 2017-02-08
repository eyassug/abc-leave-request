using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace Livit.Common.Google
{
    public class GoogleCalendarApi : IGoogleCalendarApi
    {
        public async Task<Event> CreateEvent(CalendarService calendarService, Event @event, string calendarId)
        {
            var request = calendarService.Events.Insert(@event, calendarId);
            return await request.ExecuteAsync();
        }
    }
}
