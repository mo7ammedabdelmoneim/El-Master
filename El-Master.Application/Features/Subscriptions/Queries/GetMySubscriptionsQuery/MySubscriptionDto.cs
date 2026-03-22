using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Subscriptions.Queries.GetMySubscriptionsQuery
{
    public class MySubscriptionDto
    {
        public Guid SubscriptionId { get; set; }

        public Guid PackageId { get; set; }
        public string PackageName { get; set; }

        public decimal Price { get; set; }

        public Guid CourseId { get; set; }
        public string CourseName { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }
    }
}
