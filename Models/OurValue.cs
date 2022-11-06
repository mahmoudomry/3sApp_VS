using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("OurValue")]
    public class OurValue
    {
       
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Icon")]
        public string Icon { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required, Display(Name = " Desc")]
        public string Desc { get; set; }

    }
}
