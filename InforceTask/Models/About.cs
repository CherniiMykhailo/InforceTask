using InforceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace InforceTask.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
