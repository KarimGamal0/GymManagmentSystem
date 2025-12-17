using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository : IGenericRepository<Trainer>
    {
        #region old-way
        //IEnumerable<Trainer> GetAll();

        //Trainer? GetById(int id);
        //int Add(Trainer trainer);

        //int Update(Trainer trainer);

        //int Delete(Trainer trainer);
        #endregion
    }
}
