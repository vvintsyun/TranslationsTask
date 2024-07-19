using System.ComponentModel.DataAnnotations;

namespace TranslationsTask.Dtos
{
    public class EditTaskDto
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public string Deadline { get; set; }

        [Required]
        public long ProjectId { get; set; }
        [Required]
        public long Id { get; set; }

        public long? AssigneeId { get; set; }
    }
}
