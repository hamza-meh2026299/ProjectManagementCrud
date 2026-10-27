using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Models
{
    public enum ProjectStatus
    {
        [Display(Name = "Not Started")]  NotStarted,
        [Display(Name = "In Progress")]  InProgress,
        [Display(Name = "On Hold")]      OnHold,
        [Display(Name = "Completed")]    Completed,
        [Display(Name = "Cancelled")]    Cancelled
    }

    public enum ProjectPriority
    {
        Low, Medium, High, Critical
    }

    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        [Display(Name = "Project Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Client / Owner is required.")]
        [StringLength(100, ErrorMessage = "Client name cannot exceed 100 characters.")]
        [Display(Name = "Client / Owner")]
        public string Client { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Status")]
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;

        [Required(ErrorMessage = "Priority is required.")]
        [Display(Name = "Priority")]
        public ProjectPriority Priority { get; set; } = ProjectPriority.Medium;

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Range(0, 99_999_999, ErrorMessage = "Budget must be a positive number.")]
        [Column(TypeName = "REAL")]
        [Display(Name = "Budget ($)")]
        public decimal? Budget { get; set; }

        [StringLength(200, ErrorMessage = "Team lead name cannot exceed 200 characters.")]
        [Display(Name = "Team Lead")]
        public string? TeamLead { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Last Updated")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
