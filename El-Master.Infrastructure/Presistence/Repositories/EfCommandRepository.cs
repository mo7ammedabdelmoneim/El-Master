using El_Master.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class EfCommandRepository<T> : ICommandRepository<T> where T : class
    {
        public readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;

        public EfCommandRepository(ApplicationDbContext context)
        {
            this.context = context;
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
