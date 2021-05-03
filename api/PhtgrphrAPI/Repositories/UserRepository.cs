using Microsoft.EntityFrameworkCore;
using PhtgrphrAPI.DbContexts;
using PhtgrphrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private PhtgrphrContext context;

        public UserRepository(PhtgrphrContext context)
        {
            this.context = context;
        }

        public bool CreateUserAccessToken(UserAccessToken userAccessToken)
        {
            context.UserAccessTokens.Add(userAccessToken);

            int result = context.SaveChanges();

            return result > 0;
        }

        public UserAccessToken GetUserAccessTokenByToken(Guid token)
        {
            return context.UserAccessTokens 
                .Where(uat => uat.Token == token)
                .Where(uat => uat.Expiry > DateTime.UtcNow)
                .Include("User.Galleries")
                .SingleOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return context.Users
                .Where(u => u.Username == username)
                .SingleOrDefault();
        }
    }
}
