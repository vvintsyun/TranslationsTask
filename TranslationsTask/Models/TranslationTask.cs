using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using TranslationsTask.Dtos;
using TranslationsTask.Helpers;

namespace TranslationsTask.Models
{
    public class TranslationTask
    {
        public long Id { get; private set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; private set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Deadline { get; private set; }

        [Required]
        public long ProjectId { get; private set; }
        public virtual TranslationProject Project { get; private set; }

        public long? AssigneeId { get; private set; }
        public virtual Translator? Assignee { get; private set; }

        public TranslationTask(string title, string description, DateTime deadline, long projectId, long? assigneeId)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
            ProjectId = projectId;
            AssigneeId = assigneeId;
        }

        public void UpdateTask(EditTaskDto task)
        {
            DateTime deadline;
            if (!DateTime.TryParseExact(task.Deadline, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out deadline))
            {
                throw new UserFriendlyException("Incorrect deadline date");
            }

            AssigneeId = task.AssigneeId;
            Title = task.Title;
            Description = task.Description;
            Deadline = deadline;
        }

        protected TranslationTask() { }
    }
}
