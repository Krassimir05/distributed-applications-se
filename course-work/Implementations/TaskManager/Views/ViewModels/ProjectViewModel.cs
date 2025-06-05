namespace TaskManager.Views.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

}
