namespace TaskManager.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum TaskStatus
    {
        Pending,
        InProgress,
        Done
    }

    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }

}
