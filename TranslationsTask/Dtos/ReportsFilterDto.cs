namespace TranslationsTask.Dtos
{
    public class ReportsFilterDto
    {
        public ICollection<long> Projects { get; set; } = new List<long>();
        public ICollection<long> Translators { get; set; } = new List<long>();
    }
}
