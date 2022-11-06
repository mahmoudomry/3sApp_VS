using _3sApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Name { get; set; }

        [Required, Display(Name = "Describtion")]
        public string Describtion { get; set; }
        [ Display(Name = "Image")]
        public string? Image { get; set; }
        [ Display(Name = "Icon")]
        public string ?Icon { get; set; }
    }
}
