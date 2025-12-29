using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.AnalyticsViewModel
{
    public class AnalyticsViewModel
    {
        public int TotalMembers {  get; set; }
        public int ActiveMemebers { get; set; }
        public int TotalTrainer { get; set; }
        public int UpcomingSessions { get; set; }
        public int OngoingSessions { get; set; }
        public int CompeleteSessions { get; set; }

    }
}
