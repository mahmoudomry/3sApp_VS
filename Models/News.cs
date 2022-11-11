using _3sApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Cover Image")]
        public string CoverImage { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        [Required, Display(Name = "Description")]
        public string Description { get; set; }
        [Required, Display(Name = "Scope")]
        public string Scope { get; set; }
        [Required, Display(Name = "Industrial solution")]
        public string Industrialsolution { get; set; }

        [Display(Name = "KeyWords")]
        public string? KeyWords { get; set; }

        [Display(Name = "IsActive")]
        public bool? IsActive { get; set; }
        [Display(Name = "Created on")]
        public DateTime? Createdon { get; set; }

        public List<Media>? media { get; set; } 

    }
}
