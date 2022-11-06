using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Job Title")]
        public string? JobTitle { get; set; }

        [Display(Name = "Image")]
        public string? Image { get; set; }

        [Required, Display(Name = "Order")]
        public int Order { get; set; }
    }
}
