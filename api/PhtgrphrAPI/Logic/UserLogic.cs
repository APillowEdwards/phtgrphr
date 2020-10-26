using PhtgrphrAPI.Models;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Logic
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository userRepository;

        public UserLogic(IUserRepository userRepository)//IGalleryRepository galleryRepository)
        {
            this.userRepository = userRepository;
        }

        public PhtgrphrResponse<TokenResponse> Authenticate(string username, string password)
        {
            User user = userRepository.GetUserByUsername(username);

            if (user == null || user.Password != password)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Incorrect username or password. Please try again.");

                return PhtgrphrResponse<TokenResponse>.UnauthorisedResponse(messages);
            }

            UserAccessToken userAccessToken = new UserAccessToken(user);

            userRepository.CreateUserAccessToken(userAccessToken);

            TokenResponse response = new TokenResponse(userAccessToken.Token);

            return PhtgrphrResponse<TokenResponse>.OkResponse(response);
        }
    }
}
