using System;
using System.ComponentModel.DataAnnotations;
using EfCore.Common.Enums;

namespace CoreDemo.Models
{
    public class StudentAddModel
    {
        [Display(Name = "名"), Required]
        public string FirstName { get; set; }

        [Display(Name = "姓"), Required, MaxLength(10)]
        public string LastName { get; set; }

        [Display(Name = "出生日期")]
        public DateTime Birthday { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }
    }
}