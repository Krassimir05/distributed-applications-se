namespace TaskManager.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    }

}
