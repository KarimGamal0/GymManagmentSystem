using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Trainer : GymUser
    {
        //hiredate ==> createdAt of base entity

        public Speciality Speciality { get; set; }

        #region Trainer-Session
        public ICollection<Session> TrainerSession { get; set; } = null!;
        #endregion
    }
}
