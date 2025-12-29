using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int sessionId);

        bool CreateSession(CreateSessionViewModel createSession);

        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);

        bool UpdateSession(int sessionId, UpdateSessionViewModel updateSession);

        bool RemoveSession(int sessionId);

        IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown();
        IEnumerable<CategorySelectViewModel> GetCategoryForDropDown();
    }
}
