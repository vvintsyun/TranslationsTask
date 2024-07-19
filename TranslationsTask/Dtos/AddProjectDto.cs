using System.ComponentModel.DataAnnotations;

namespace TranslationsTask.Dtos
{
    public class AddProjectDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}
