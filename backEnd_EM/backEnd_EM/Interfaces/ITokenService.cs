using backEnd_EM.Properties.Models;

namespace backEnd_EM.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Athletes athlete);
    }
}
