using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("Industry")]
    public class Industry
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }


        [Required, Display(Name = "Describtion")]
        public string Describtion { get; set; }


        [ Display(Name = "Image")]
        public string? CoverImage { get; set; }
        [ Display(Name = "Icon")]
        public string? Icon { get; set; }

    }
}
