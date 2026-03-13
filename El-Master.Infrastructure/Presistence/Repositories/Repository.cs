using El_Master.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ApplicationDbContext context;
        public readonly QueryFactory db;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext context, QueryFactory db)
        {
            this.context = context;
            this.db = db;
            dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
            => await dbSet.AddAsync(entity);

        public void Update(T entity)
            => dbSet.Update(entity);

        public void Delete(T entity)
            => dbSet.Remove(entity);

        public async Task SaveChangesAsync()
            => await context.SaveChangesAsync();
    }
}
