using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.DTOs
{
    public class RegisterDto
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(30)]
        public string Grade { get; set; }
        
        [Required, StringLength(14)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(128)]
        [PasswordPropertyText]
        
        public string Password { get; set; }
    }
}
