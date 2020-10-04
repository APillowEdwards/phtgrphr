using NUnit.Framework;
using FakeItEasy;
using Shouldly;
using PhtgrphrAPI.Repositories;
using System.Collections.Generic;
using PhtgrphrAPI.Models;
using PhtgrphrAPI.Logic;
using System.Linq;

namespace PhtgrphrAPITest
{
    [TestFixture]
    public class UserTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void User_Authenticate_UserDoesntExist()
        {
            var userRepository = A.Fake<IUserRepository>();

            var user = new User { ID = 1, Name = "Test User", Username = "username", Password = "password" };

            A.CallTo(() => userRepository.GetUserByUsername(user.Username)).Returns(null);

            UserLogic u = new UserLogic(userRepository);

            var response = u.Authenticate(user.Username, "");
            response.Code.ShouldBe(401);
            response.Result.ShouldBeNull();
        }

        [Test]
        public void User_Authenticate_IncorrectPassword()
        {
            var userRepository = A.Fake<IUserRepository>();

            var user = new User { ID = 1, Name = "Test User", Username = "username", Password = "password" };

            A.CallTo(() => userRepository.GetUserByUsername(user.Username)).Returns(user);

            UserLogic u = new UserLogic(userRepository);

            var response = u.Authenticate(user.Username, "");
            response.Code.ShouldBe(401);
            response.Result.ShouldBeNull();
        }

        [Test]

        public void User_Authenticate_CorrectPassword()
        {
            var userRepository = A.Fake<IUserRepository>();

            var user = new User { ID = 1, Name = "Test User", Username = "username", Password = "password" };
            var userAccessToken = new UserAccessToken(user);

            A.CallTo(() => userRepository.GetUserByUsername(user.Username)).Returns(user);
            A.CallTo(() => userRepository.CreateUserAccessToken(userAccessToken)).WithAnyArguments().Returns(true);

            UserLogic u = new UserLogic(userRepository);

            var response = u.Authenticate(user.Username, user.Password);
            response.Code.ShouldBe(200);
            response.Result.Token.ShouldNotBe(System.Guid.Empty);

            A.CallTo(() => userRepository.CreateUserAccessToken(userAccessToken)).WithAnyArguments().MustHaveHappened();
        }
    }
}