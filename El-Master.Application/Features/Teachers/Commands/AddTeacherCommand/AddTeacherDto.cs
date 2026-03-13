using El_Master.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.AddTeacherCommand
{
    public class AddTeacherDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public IFormFile? Image { get; set; }

    }
}
