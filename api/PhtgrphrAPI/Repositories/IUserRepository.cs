using PhtgrphrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Repositories
{
    public interface IUserRepository
    {
        UserAccessToken GetUserAccessTokenByToken(Guid token);
        User GetUserByUsername(string username);
        bool CreateUserAccessToken(UserAccessToken userAccessToken);
    }
}
