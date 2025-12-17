using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    // 
    internal class MemberRepository : GenericRepository<Member>
    {
        private readonly GymDBContext _dbContext;

        //private readonly GymDBContext _dbContext = new GymDBContext();

        //Ask Clr to inject dbcontext
        public MemberRepository(GymDBContext dBContext) : base(dBContext)
        {

        }

        #region Old-way

        //public int Add(Member member)
        //{
        //    _dbContext.Members.Add(member);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Member member)
        //{
        //    _dbContext.Members.Remove(member);
        //    return _dbContext.SaveChanges();

        //}

        //public IEnumerable<Member> GetAll()
        //{
        //    return _dbContext.Members.ToList();
        //}

        //public Member? GetById(int id)
        //{
        //    return _dbContext.Members.Find(id);
        //}

        //public int Update(Member member)
        //{
        //    _dbContext.Members.Update(member);
        //    return _dbContext.SaveChanges();
        //}
        #endregion
    }
}
