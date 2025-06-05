using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Views.ViewModels
{
    public class TaskItemViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }

        public SelectList? Projects { get; set; }
    }
}
