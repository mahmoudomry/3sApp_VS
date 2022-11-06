using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("ContactUsMessage")]
    public class ContactUsMessage
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Full Name ")]
        public string FullName { get; set; }

        [Required, Display(Name = "Email "),EmailAddress]
        public string Email { get; set; }
        [Required, Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required, Display(Name = "Message")]
        public string Message { get; set; }

    }
}
