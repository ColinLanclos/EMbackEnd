namespace backEnd_EM.Dtos.Analytics
{
    //migt not need this
    public class CreateRequestAnalyticsDto
    {
        public int AthleteId { get; set; }
        public int AmountOfTimesLogin { get; set; }
        public string Rank { get; set; } = string.Empty;
    }
}