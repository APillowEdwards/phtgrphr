using PhtgrphrAPI.Models;
using PhtgrphrAPI.Responses;

namespace PhtgrphrAPI.Logic
{
    public interface IUserLogic
    {
        PhtgrphrResponse<TokenResponse> Authenticate(string username, string password);
    }
}