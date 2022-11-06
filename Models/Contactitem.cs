﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3sApp.Models
{
    [Table("Contactitem")]
    public class Contactitem
    {   [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Value")]
        public string Value { get; set; }
        [Display(Name = "Icon")]
        public string Icon { get; set; }
        [Display(Name = "Order")]
        public int Order { get; set; }

    }
}
