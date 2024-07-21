namespace TranslationsTask.Dtos
{
    public class TasksCountByMonthProjectDto
    {
        public List<string> X { get; set; } = new List<string>();
        public List<ChartData> Data { get; set; } = new List<ChartData>();
    }

    public class ChartData
    {
        public int[] Data { get; set; } = new int[] { };
        public string Label { get; set; }
    }
}
