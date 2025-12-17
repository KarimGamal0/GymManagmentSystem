using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IMemberRepository : IGenericRepository<Member>
    {

        #region Old-Way
        ////Get All
        //IEnumerable<Member> GetAll();
        ////GetById
        //Member? GetById(int id);
        ////Add
        //int Add(Member member);
        ////Update
        //int Update(Member member);
        ////Delete
        //int Delete(int Id);
        #endregion
    }
}
