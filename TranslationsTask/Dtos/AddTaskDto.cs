using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TranslationsTask.Models;

namespace TranslationsTask.Dtos
{
    public class AddTaskDto
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

        public long? AssigneeId { get; set; }
    }
}
