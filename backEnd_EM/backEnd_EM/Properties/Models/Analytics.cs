namespace backEnd_EM.Models
{
    public class Analytics
    {
        public int Id { get; set; }

        public DateTime RecentLoginDate { get; set; }

        public int AmountOfTimesLogin { get; set; }

        public string Rank { get; set; } = string.Empty;

        public int? AthletesId { get; set; }
    }
}