using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectJune.Models
{
    public class Service:BaseEntity
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(200)]
        public string Text { get; set; }
        [Required]
        public string Icon{ get; set; }
    }
}
