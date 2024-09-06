namespace Portfolio_Blazor
{
    public class Technology
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
