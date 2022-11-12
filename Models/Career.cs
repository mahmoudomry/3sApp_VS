using _3sApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("Career")]
    public class Career
    {
        [Key]
        public int Id { get; set; }
        [Required ,Display(Name ="Type")]
        public string Type { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        [Required, Display(Name = "Description")]
        public string Description { get; set; }
        [Required, Display(Name = "Responsibilities")]
        public string Responsibilities { get; set; }
        [Required, Display(Name = "Requirements")]
        public string Requirements { get; set; }
        [ Display(Name = "IsActive")]
        public bool? IsActive { get; set; }
        [ Display(Name = "Created on")]
        public DateTime? Createdon { get; set; }
        [ Display(Name = "Closed Date")]
        public DateTime? ClosedDate { get; set; }
        public List<CareerApplication>? careerApplications { get; set; }
    }

    [Table("CareerApplication")]
    public class CareerApplication
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required, Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required, Display(Name = "Email"),EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Phone number"), Phone]
        public string Phone { get; set; }

        [Required, Display(Name = "Address")]
        public string Address { get; set; }

        [Required, Display(Name = "Cover Letter")]
        public string CoverLetter { get; set; }

        [Required, Display(Name = "Resume")]
        public string Resume { get; set; }

        public DateTime? Created_on { get; set; }

        public int CareerId { get; set; }
        public Career? Career { get; set; }

    }
}
