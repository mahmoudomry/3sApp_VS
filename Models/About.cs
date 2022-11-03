using _3sApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3sApp.Models
{
    [Table("About")]
    public class About
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Home Title")]
        public string HomeTitle { get; set; }
        [Required, Display(Name = "Second Title")]
        public string SecondTitle { get; set; }
        [Required, Display(Name = "Home Desc")]
        public string HomeDesc { get; set; }
        [Required, Display(Name = "Home Image")]
        public string HomeImage { get; set; }

        [Required, Display(Name = "Page Desc")]
        public string PageDesc { get; set; }
        [Required, Display(Name = "Page Image")]
        public string PageImage { get; set; }

        [Required, Display(Name = "Security Image")]
        public string SecurityImage { get; set; }
        [Required, Display(Name = "Security Title")]
        public string SecurityTitle { get; set; }
        [Required, Display(Name = "Security Desc")]
        public string SecurityDesc { get; set; }

        [Required, Display(Name = "CEO Image")]
        public string CEOImage { get; set; }
        [Required, Display(Name = "CEO Title")]
        public string CEOTitle { get; set; }
        [Required, Display(Name = "CEO Desc")]
        public string CEODesc { get; set; }
        [Required, Display(Name = "CEO Sign")]
        public string SignImage { get; set; }

        [Required, Display(Name = "Sign Name")]
        public string SignName { get; set; }

        [Required, Display(Name = "Our Values")]
        public string OurValues { get; set; }
        [Required, Display(Name = "Our Values Heading1")]
        public string OurValuesH1 { get; set; }
        [Required, Display(Name = "Our Values Heading2")]
        public string OurValuesH2 { get; set; }


      

    }
}
