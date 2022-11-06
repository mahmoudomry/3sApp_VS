using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _3sApp.Models
{
    [Table("Media")]
    public class Media
    {

        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        [Required, Display(Name = "Client")]

        public string Client { get; set; }
        [Required, Display(Name = "Describtion")]
        public string Describtion { get; set; }
        [Required, Display(Name = "Image")]
        public string FileName { get; set; }
        /// <summary>
        /// Site post category project - solution - industrty - etc
        /// </summary>

        [Required, Display(Name = "CategoryId")]
        public int CategoryId { get; set; }
        /// <summary>
        /// Image or vedio
        /// </summary>
        [Required, Display(Name = "TypeId")]
        public int TypeId { get; set; }
        /// <summary>
        /// Site post  project - solution - industrty - etc 
        /// </summary>
        [Required, Display(Name = "PostId")]
        public int PostId { get; set; }

        [Required, Display(Name = "Order")]
        public int Order { get; set; }

        [ Display(Name = "IsActive")]
        public bool IsActive { get; set; }


    }
}
