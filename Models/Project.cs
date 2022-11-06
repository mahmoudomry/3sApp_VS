using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        [Required, Display(Name = "Client")]
        public string Client { get; set; }
        [Required, Display(Name = "Client Logo")]
        public string ClientLogo { get; set; }

        [Required, Display(Name = "Scope")]
        public string Scope { get; set; }
        [Required, Display(Name = "Industrial Solution")]
        public string IndustrialSolution { get; set; }

        [Required, Display(Name = "Describtion")]
        public string Describtion { get; set; }


        [ Display(Name = "Image")]
        public string CoverImage { get; set; }
        [ForeignKey("PostId")]
        public List<Media>? Images { get; set; }

    }
}
