namespace Netflix.Contracts.CastingCalls
{
    public class AuditionDto
    {
        public Guid Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Location { get; set; } = null!;
    }
}