using System.ComponentModel.DataAnnotations;

namespace TranslationsTask.Models
{
    public class TranslationProject
    {
        public long Id { get; private set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; private set; }

        public virtual ICollection<TranslationTask> Tasks { get; private set; }

        public TranslationProject(string name)
        {
            Name = name;

            Tasks = new List<TranslationTask>();
        }

        protected TranslationProject() { }
    }
}
