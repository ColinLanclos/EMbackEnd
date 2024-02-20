namespace backEnd_EM.Dtos.Athletes
{

    public class UpdateAthleteRequestDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Guardian { get; set; } = string.Empty;

        public string Birthday { get; set; }

        public string Classification { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        public float Weight { get; set; }

        public string School { get; set; } = string.Empty;

        public long Phone { get; set; }
    }
}