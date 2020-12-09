using NUnit.Framework;
using FakeItEasy;
using Shouldly;
using PhtgrphrAPI.Repositories;
using System.Collections.Generic;
using PhtgrphrAPI.Models;
using PhtgrphrAPI.Logic;
using System.Linq;
using System;
using PhtgrphrAPI.Responses;

namespace PhtgrphrAPITest
{
    [TestFixture]
    public class GalleryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void Gallery_GetGalleryImagesByPage_InBounds()
        //{
        //    var galleryRepository = A.Fake<IGalleryRepository>();

        //    var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

        //    var galleryAccessToken = new GalleryAccessToken { ID = 1, Expiry = DateTime.Now.AddHours(1), Token = System.Guid.NewGuid(), Gallery = gallery };

        //    var images = new List<Image>();
        //    foreach (var i in Enumerable.Range(13, 4))
        //    {
        //        images.Add(new Image() { ID = i, Sort = i, Gallery = gallery });
        //    }

        //    gallery.Images = images;

        //    A.CallTo(() => galleryRepository.GetGalleryAccessTokenByToken(galleryAccessToken.Token)).Returns(galleryAccessToken);
        //    A.CallTo(() => galleryRepository.GetImagesByPage(gallery, 4, 4)).Returns(images);
        //    A.CallTo(() => galleryRepository.GetTotalImageCount(gallery)).Returns(20);

        //    GalleryLogic g = new GalleryLogic(galleryRepository, null);

        //    var response = g.GetGalleryImagesByPage(galleryAccessToken.Token, 4, 4);
        //    response.Code.ShouldBe(200);

        //    response.Result.TotalCount.ShouldBe(20);

        //    List<ImageResponseImage> responseImages = response.Result.Images;
        //    responseImages.Count.ShouldBe(4);

        //    foreach (var i in Enumerable.Range(0, 3))
        //    {
        //        responseImages[i].ID.ShouldBe(i + 13);
        //    }
        //}

        //[Test]
        //public void Gallery_GetGalleryImagesByPage_OutOfBounds()
        //{
        //    var galleryRepository = A.Fake<IGalleryRepository>();

        //    var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

        //    var galleryAccessToken = new GalleryAccessToken { ID = 1, Expiry = DateTime.Now.AddHours(1), Token = System.Guid.NewGuid(), Gallery = gallery };

        //    var images = new List<Image>();

        //    gallery.Images = images;

        //    A.CallTo(() => galleryRepository.GetGalleryAccessTokenByToken(galleryAccessToken.Token)).Returns(galleryAccessToken);
        //    A.CallTo(() => galleryRepository.GetImagesByPage(gallery, 4, 4)).Returns(images);
        //    A.CallTo(() => galleryRepository.GetTotalImageCount(gallery)).Returns(20);

        //    GalleryLogic g = new GalleryLogic(galleryRepository, null);

        //    var response = g.GetGalleryImagesByPage(galleryAccessToken.Token, 4, 4);
        //    response.Code.ShouldBe(404);
        //    response.Result.ShouldBeNull();
        //}

        //[Test]
        //public void GalleryRepository_GetImagesByPage_InBounds()
        //{
        //    GalleryRepository repo = new GalleryRepository(null); // Can only do this for methods that don't touch the context directly

        //    var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

        //    var images = new List<Image>();
        //    foreach (var i in Enumerable.Range(1, 20))
        //    {
        //        images.Add(new Image() { ID = i, Sort = i, Gallery = gallery });
        //    }

        //    var random = new System.Random();
        //    images = images.OrderBy(a => random.Next()).ToList();

        //    gallery.Images = images;

        //    List<Image> returnImages = repo.GetImagesByPage(gallery, 4, 4);
        //    returnImages.Count.ShouldBe(4);

        //    foreach (var i in Enumerable.Range(0, 3))
        //    {
        //        returnImages[i].ID.ShouldBe(i + 13);
        //    }
        //}

        //[Test]
        //public void GalleryRepository_GetGalleryImagesByPage_PartiallyInBounds()
        //{
        //    GalleryRepository repo = new GalleryRepository(null); // Can only do this for methods that don't touch the context directly

        //    var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

        //    var images = new List<Image>();
        //    foreach (var i in Enumerable.Range(1, 20))
        //    {
        //        images.Add(new Image() { ID = i, Sort = i, Gallery = gallery });
        //    }

        //    var random = new System.Random();
        //    images = images.OrderBy(a => random.Next()).ToList();

        //    gallery.Images = images;

        //    List<Image> returnImages = repo.GetImagesByPage(gallery, 8, 3);
        //    returnImages.Count.ShouldBe(4);

        //    foreach (var i in Enumerable.Range(0, 3))
        //    {
        //        returnImages[i].ID.ShouldBe(i + 17);
        //    }
        //}

        //[Test]
        //public void GalleryRepository_GetGalleryImagesByPage_OutOfBounds()
        //{
        //    GalleryRepository repo = new GalleryRepository(null); // Can only do this for methods that don't touch the context directly

        //    var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

        //    var images = new List<Image>();
        //    foreach (var i in Enumerable.Range(1, 20))
        //    {
        //        images.Add(new Image() { ID = i, Sort = i, Gallery = gallery });
        //    }

        //    var random = new System.Random();
        //    images = images.OrderBy(a => random.Next()).ToList();

        //    gallery.Images = images;

        //    List<Image> returnImages = repo.GetImagesByPage(gallery, 8, 4);
        //    returnImages.Count.ShouldBe(0);
        //}

        [Test]
        public void Gallery_Authenticate_GalleryDoesntExist()
        {
            var galleryRepository = A.Fake<IGalleryRepository>();

            var guid = System.Guid.NewGuid();

            A.CallTo(() => galleryRepository.GetGalleryByGuid(guid)).Returns(null);

            GalleryLogic g = new GalleryLogic(galleryRepository, null);

            var response = g.Authenticate(guid, "");
            response.Code.ShouldBe(404);
            response.Result.ShouldBeNull();
        }

        [Test]
        public void Gallery_Authenticate_IncorrectPassword()
        {
            var galleryRepository = A.Fake<IGalleryRepository>();

            var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

            A.CallTo(() => galleryRepository.GetGalleryByGuid(gallery.Guid)).Returns(gallery);

            GalleryLogic g = new GalleryLogic(galleryRepository, null);

            var response = g.Authenticate(gallery.Guid, "");
            response.Code.ShouldBe(401);
            response.Result.ShouldBeNull();
        }

        [Test]
        public void Gallery_Authenticate_CorrectPassword()
        {
            var galleryRepository = A.Fake<IGalleryRepository>();

            var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

            A.CallTo(() => galleryRepository.GetGalleryByGuid(gallery.Guid)).Returns(gallery);
            A.CallTo(() => galleryRepository.CreateGalleryAccessToken(new GalleryAccessToken())).WithAnyArguments().Returns(true);

            GalleryLogic g = new GalleryLogic(galleryRepository, null);

            var response = g.Authenticate(gallery.Guid, gallery.Password);
            response.Code.ShouldBe(200);
            response.Result.Token.ShouldNotBe(System.Guid.Empty);
        }

        [Test]
        public void Gallery_Exists_DoesExist()
        {
            var galleryRepository = A.Fake<IGalleryRepository>();

            var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

            var guid = System.Guid.NewGuid();

            A.CallTo(() => galleryRepository.GetGalleryByGuid(guid)).Returns(gallery);

            GalleryLogic g = new GalleryLogic(galleryRepository, null);

            var response = g.Exists(guid);
            response.Code.ShouldBe(200);
            response.Result["exists"].ShouldBeTrue();
        }

        [Test]
        public void Gallery_Exists_DoesntExist()
        {
            var galleryRepository = A.Fake<IGalleryRepository>();

            var guid = System.Guid.NewGuid();

            A.CallTo(() => galleryRepository.GetGalleryByGuid(guid)).Returns(null);

            GalleryLogic g = new GalleryLogic(galleryRepository, null);

            var response = g.Exists(guid);
            response.Code.ShouldBe(200);
            response.Result["exists"].ShouldBeFalse();
        }

        [Test]
        public void Gallery_Sanitise()
        {
            var galleryRepository = A.Fake<IGalleryRepository>();
            
            GalleryLogic g = new GalleryLogic(galleryRepository, null);

            var password = "password";

            var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = password, Guid = System.Guid.NewGuid() };

            var sanitisedGallery = g.SanitiseGallery(gallery);

            sanitisedGallery.Password.ShouldNotBe(password);
        }

        [Test]
        public void Gallery_GetGalleriesByUserAccessToken()
        {
            var userRepository = A.Fake<IUserRepository>();

            List<Gallery> galleries = new List<Gallery> {
                new Gallery {ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() },
                new Gallery {ID = 2, Name = "Test Gallery 2", Password = "password2", Guid = System.Guid.NewGuid() },
                new Gallery {ID = 3, Name = "Test Gallery 3", Password = "password3", Guid = System.Guid.NewGuid() }
            };

            var user = new User { ID = 1, Name = "Test User", Username = "username", Password = "password", Galleries = galleries };

            var userAccessToken = new UserAccessToken { ID = 1, Expiry = DateTime.Now.AddHours(1), Token = System.Guid.NewGuid(), User = user };

            A.CallTo(() => userRepository.GetUserAccessTokenByToken(userAccessToken.Token)).Returns(userAccessToken);

            GalleryLogic g = new GalleryLogic(null, userRepository);

            var response = g.GetGalleriesByUserAccessToken(userAccessToken.Token);
            response.Code.ShouldBe(200);
            response.Result.Galleries.Count.ShouldBe(3);
        }

        [Test]
        public void Gallery_GetGalleryImagesByGalleryId_Owned()
        {
            var userRepository = A.Fake<IUserRepository>();

            List<Image> images = new List<Image>
            {
                new Image {ID = 1, FileName = "photo", Sort = 1},
                new Image {ID = 2, FileName = "photo2", Sort = 2},
                new Image {ID = 3, FileName = "photo3", Sort = 3},
            };

            List<Gallery> galleries = new List<Gallery> {
                new Gallery {ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid(), Images = images },
                new Gallery {ID = 2, Name = "Test Gallery 2", Password = "password2", Guid = System.Guid.NewGuid() },
                new Gallery {ID = 3, Name = "Test Gallery 3", Password = "password3", Guid = System.Guid.NewGuid() }
            };

            var user = new User { ID = 1, Name = "Test User", Username = "username", Password = "password", Galleries = galleries };

            var userAccessToken = new UserAccessToken { ID = 1, Expiry = DateTime.Now.AddHours(1), Token = System.Guid.NewGuid(), User = user };

            A.CallTo(() => userRepository.GetUserAccessTokenByToken(userAccessToken.Token)).Returns(userAccessToken);

            GalleryLogic g = new GalleryLogic(null, userRepository);

            var response = g.GetGalleryImagesByGalleryId(userAccessToken.Token, 1);
            response.Code.ShouldBe(200);
            response.Result.Images.Count.ShouldBe(3);
        }

        [Test]
        public void Gallery_GetGalleryImagesByGalleryId_NotOwned()
        {
            var userRepository = A.Fake<IUserRepository>();

            var user = new User { ID = 1, Name = "Test User", Username = "username", Password = "password", Galleries = new List<Gallery>() };

            var userAccessToken = new UserAccessToken { ID = 1, Expiry = DateTime.Now.AddHours(1), Token = System.Guid.NewGuid(), User = user };

            A.CallTo(() => userRepository.GetUserAccessTokenByToken(userAccessToken.Token)).Returns(userAccessToken);

            GalleryLogic g = new GalleryLogic(null, userRepository);

            var response = g.GetGalleryImagesByGalleryId(userAccessToken.Token, 1);
            response.Code.ShouldBe(401);
            response.Result.ShouldBeNull();
        }

        [Test]
        public void Gallery_GetGalleryDetailsByGalleryAccessToken()
        {
            var galleryRepository = A.Fake<IGalleryRepository>();

            var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

            var galleryAccessToken = new GalleryAccessToken { ID = 1, Expiry = DateTime.Now.AddHours(1), Token = System.Guid.NewGuid(), Gallery = gallery };

            A.CallTo(() => galleryRepository.GetGalleryAccessTokenByToken(galleryAccessToken.Token)).Returns(galleryAccessToken);

            GalleryLogic g = new GalleryLogic(galleryRepository, null);

            var response = g.GetGalleryDetailsByGalleryAccessToken(galleryAccessToken.Token);
            response.Code.ShouldBe(200);
            response.Result.Gallery.ID.ShouldBe(gallery.ID);
            response.Result.Gallery.Password.ShouldBe(g.SanitiseGallery(gallery).Password);
        }

        [Test]
        public void Gallery_GetGalleryDetailsByGalleryId()
        {
            var userRepository = A.Fake<IUserRepository>();

            var gallery = new Gallery { ID = 1, Name = "Test Gallery", Password = "password", Guid = System.Guid.NewGuid() };

            var user = new User { ID = 1, Name = "Test User", Username = "username", Password = "password", Galleries = new List<Gallery> { gallery } };

            var userAccessToken = new UserAccessToken { ID = 1, Expiry = DateTime.Now.AddHours(1), Token = System.Guid.NewGuid(), User = user };

            A.CallTo(() => userRepository.GetUserAccessTokenByToken(userAccessToken.Token)).Returns(userAccessToken);

            GalleryLogic g = new GalleryLogic(null, userRepository);

            var response = g.GetGalleryDetailsByGalleryId(userAccessToken.Token, gallery.ID);
            response.Code.ShouldBe(200);
            response.Result.Gallery.ID.ShouldBe(gallery.ID);
            response.Result.Gallery.Password.ShouldBe(g.SanitiseGallery(gallery).Password);
        }

        //TODO gallery addedit

    }
}