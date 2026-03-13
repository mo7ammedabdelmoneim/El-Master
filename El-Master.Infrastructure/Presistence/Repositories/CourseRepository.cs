using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {
        }
    }
}
