using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerViewModel GetTrainerDetails(int trainerId);
        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        bool UpdateMember(int trainerId, TrainerToUpdateViewModel trainerToUpdate);
        bool DeleteMember(int trainerId);
    }
}
