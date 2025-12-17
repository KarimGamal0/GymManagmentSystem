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
    internal class TrainerRepository : GenericRepository<Trainer>
    {
        private readonly GymDBContext _dbContext;

        public TrainerRepository(GymDBContext dbContext) : base(dbContext) 
        {
           
        }

        #region old-way
        //public int Add(Trainer trainer)
        //{
        //    _dbContext.Trainers.Add(trainer);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Trainer trainer)
        //{
        //    _dbContext.Trainers.Remove(trainer);
        //    return _dbContext.SaveChanges();
        //}

        //public IEnumerable<Trainer> GetAll() => _dbContext.Trainers.ToList();

        //public Trainer? GetById(int id)
        //{
        //    return _dbContext.Trainers.Find(id);
        //}

        //public int Update(Trainer trainer)
        //{
        //     _dbContext.Trainers.Update(trainer);
        //    return _dbContext.SaveChanges();
        //}
        #endregion
    }
}
