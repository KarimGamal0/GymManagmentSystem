using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ISessionRepository : IGenericRepository<Session>
    {
        #region old-way
        //IEnumerable<Session> GetAll();
        //Session? GetById(int id);
        //int Add(Session session);
        //int Update(Session session);
        //int Delete(Session session);
        #endregion

    }
}
