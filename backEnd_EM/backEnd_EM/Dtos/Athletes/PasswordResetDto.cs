namespace backEnd_EM.Dtos.Athletes
{

    public class PasswordResetDto
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordResetToken { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
