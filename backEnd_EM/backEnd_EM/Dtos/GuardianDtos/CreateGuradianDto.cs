namespace backEnd_EM.Dtos
{
    public class CreateGuardianDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public long Number { get; set; }
    }
}