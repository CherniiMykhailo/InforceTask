using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace InforceTask.Models
{
    public class Url
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a URL")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? OriginalUrl { get; set; }

        [Required]
        [BindNever]
        public string? ShortUrl { get; set; }

        [BindNever]
        public string? CreatedBy { get; set; }

        [BindNever]
        public DateTime CreatedDate { get; set; }
    }
}
