using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectJune.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(8), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool StayConnected { get; set; }
    }
}
