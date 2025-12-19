using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Interfaces
{
    internal interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
    }
}
