using sim_backend_api.Models;

namespace sim_backend_api.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
