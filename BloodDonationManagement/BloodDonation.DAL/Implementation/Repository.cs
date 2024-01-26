using BloodDonation.DAL.AppContext;
using BloodDonation.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            _dbSet = this.context.Set<T>();
        }

        public async Task<bool> CreateAsync(T entity)
        {
            _dbSet.AddAsync(entity);

            var res = context.SaveChanges();

            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            var res = context.SaveChanges();

            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var res = _dbSet.ToList();

            return res;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T res = _dbSet.FindAsync(id).Result;

            return res;
        }
    }
}
