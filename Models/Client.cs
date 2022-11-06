using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Link")]
        public string? Link { get; set; }



        [Display(Name = "Image")]
        public string? Image { get; set; }
    }
}
