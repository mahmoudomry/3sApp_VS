using _3sApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3sApp.Models
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        [Display(Name ="Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Title"), Column(TypeName = "varchar(500)")]
        public string Title { get; set; }
        [Required, 
         Display(Name = "Description"),
         Column(TypeName ="varchar(500)"),
         MaxLength(500,ErrorMessage ="Allowd Max Length is 500")]
        public string Description { get; set; }

        [Display(Name = "Cover Image")]
        public string? CoverImage { get; set; }
        [Display(Name = "IsActive")]

        public bool? IsActive { get; set; } = true;
        [Display(Name = "Order")]
        [Required]
        public int? Order { get; set; }
        [Display(Name = "AddBy")]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }


    }
}
