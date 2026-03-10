using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        ICommandRepository<T> Command { get; }
        IQueryRepository<T> Query { get; }
    }
}
