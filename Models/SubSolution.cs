using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("SubSolution")]
    public class SubSolution
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }


        [Required, Display(Name = "Describtion")]
        public string Describtion { get; set; }


        [Required, Display(Name = "Image")]
        public string CoverImage { get; set;}

        [Required, Display(Name = "Icon")]
        public string Icon { get; set; }

        [Required, Display(Name = "Order")]
        public int Order { get; set; }

        [Required, Display(Name = "Solution") ]
        
        public int SolutionId { get; set; }
        [Required, Display(Name = "Solution")]
        [ForeignKey("SolutionId")]
        public Solution Solution { get; set; }
    }
}
