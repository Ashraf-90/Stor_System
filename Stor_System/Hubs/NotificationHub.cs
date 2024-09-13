using Microsoft.AspNetCore.SignalR;
using Stor_System.Models;
using Microsoft.AspNetCore.Http; // Make sure to include this for IHttpContextAccessor

namespace SignalRSample.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly TawerStorContext _context;
        public static int notificationCounter = 0;
        public static int REQ_notificationCounter = 0;
        public static List<string> messages = new();

        public NotificationHub(TawerStorContext context)
        {
            _context = context;

            notificationCounter = _context.Projects.Where(PJ => PJ.Active == "1")
                    .Where(PJ => PJ.ApproveStatus == "0")
                                    .OrderByDescending(m => m.ProjectId).Count();


            REQ_notificationCounter = _context.Projects.Where(PJ => PJ.Active == "1")
                                        .Where(PJ => PJ.ApproveStatus != "0")
                                        .Where(PJ => PJ.NotifivationSeen != "1")
                                    .OrderByDescending(m => m.ProjectId).Count();



        }

        public async Task SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                notificationCounter = notificationCounter + 1;
                REQ_notificationCounter = REQ_notificationCounter + 1;
                messages.Add(message);
                await LoadMessages();
            }
        }

        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification", messages, notificationCounter , REQ_notificationCounter);
        }








        public async Task SendMessage2(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                notificationCounter = notificationCounter + 1;
                REQ_notificationCounter = REQ_notificationCounter + 1;
                messages.Add(message);
                await LoadMessages2();
            }
        }

        public async Task LoadMessages2()
        {
            await Clients.All.SendAsync("LoadNotification", messages, notificationCounter, REQ_notificationCounter);
        }
    }
}

