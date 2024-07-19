using System.Globalization;

namespace TranslationsTask.Dtos
{
    public class TaskVm
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline => DeadlineValue.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        public DateTime DeadlineValue { get; set; }
        public string Project { get; set; }
        public string? Assignee { get; set; }
    }
}
