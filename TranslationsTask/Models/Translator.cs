using System.ComponentModel.DataAnnotations;

namespace TranslationsTask.Models
{
    public class Translator
    {
        public long Id { get; private set; }

        [Required]
        [MaxLength(40)]
        public string FullName { get; private set; }

        public virtual ICollection<TranslationTask> Tasks { get; private set; }

        public Translator(string name)
        {
            FullName = name;
        }

        protected Translator() { }

    }
}
