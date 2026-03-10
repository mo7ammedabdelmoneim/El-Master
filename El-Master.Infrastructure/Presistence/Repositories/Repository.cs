using El_Master.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public ICommandRepository<T> Command { get; }
        public IQueryRepository<T> Query { get; }

        public Repository(
            ICommandRepository<T> command,
            IQueryRepository<T> query)
        {
            Command = command;
            Query = query;
        }
    }
}
