namespace backEnd_EM.Dtos.Athletes
{

    public class CreateAthleteRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string Guardian { get; set; } = string.Empty;

        public string Birthday { get; set; }

        public string Classification { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        public float Weight { get; set; }

        public string School { get; set; } = string.Empty;

        public long Phone { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}