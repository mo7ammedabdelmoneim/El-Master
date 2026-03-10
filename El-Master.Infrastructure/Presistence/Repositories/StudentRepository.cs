using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ICommandRepository<Student> command, IQueryRepository<Student> query) : base(command, query)
        {
        }
    }
}
