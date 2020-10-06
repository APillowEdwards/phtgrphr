﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhtgrphrAPI.FileManagers;
using PhtgrphrAPI.Models;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Logic
{
    public class GalleryLogic : IGalleryLogic 
    {
        private IGalleryRepository galleryRepository;
        private IUserRepository userRepository;

        public GalleryLogic(IGalleryRepository galleryRepository, IUserRepository userRepository)
        {
            this.galleryRepository = galleryRepository;
            this.userRepository = userRepository;
        }

        public PhtgrphrResponse<GalleryListResponse> GetGalleriesByUserAccessToken(Guid token)
        {
            UserAccessToken userAccessToken = userRepository.GetUserAccessTokenByToken(token);

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<GalleryListResponse>.UnauthorisedResponse(messages);
            }

            List<Gallery> galleries = new List<Gallery>();
            foreach (Gallery g in userAccessToken.User.Galleries)
            {
                galleries.Add(SanitiseGallery(g));
            }

            GalleryListResponse response = new GalleryListResponse(galleries);

            return PhtgrphrResponse<GalleryListResponse>.OkResponse(response);
        }

        public PhtgrphrResponse<GalleryResponse> GetGalleryDetailsByGalleryAccessToken(Guid token)
        {
            GalleryAccessToken galleryAccessToken = galleryRepository.GetGalleryAccessTokenByToken(token);

            if (galleryAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<GalleryResponse>.UnauthorisedResponse(messages);
            }

            Gallery gallery = SanitiseGallery(galleryAccessToken.Gallery);

            GalleryResponse response = new GalleryResponse(gallery);

            return PhtgrphrResponse<GalleryResponse>.OkResponse(response);
        }

        public PhtgrphrResponse<GalleryResponse> GetGalleryDetailsByGalleryId(Guid token, int galleryId)
        {
            UserAccessToken userAccessToken = userRepository.GetUserAccessTokenByToken(token);

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<GalleryResponse>.UnauthorisedResponse(messages);
            }

            Gallery gallery = userAccessToken.User.Galleries
                .Where(g => g.ID == galleryId)
                .SingleOrDefault();

            if (gallery == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "You are not authorised to view this gallery.");

                return PhtgrphrResponse<GalleryResponse>.UnauthorisedResponse(messages);
            }

            Gallery sanitisedGallery = SanitiseGallery(gallery);

            GalleryResponse response = new GalleryResponse(sanitisedGallery);

            return PhtgrphrResponse<GalleryResponse>.OkResponse(response);
        }

        public PhtgrphrResponse<Dictionary<string, bool>> Exists(Guid guid)
        {
            Gallery gallery = galleryRepository.GetGalleryByGuid(guid);

            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            
            dictionary.Add("exists", gallery != null);

            return PhtgrphrResponse<Dictionary<string, bool>>.OkResponse(dictionary);
        }

        public PhtgrphrResponse<ImageListResponse> GetGalleryImagesByPage(Guid token, int pageSize, int pageNumber)
        {
            GalleryAccessToken galleryAccessToken = galleryRepository.GetGalleryAccessTokenByToken(token);

            if (galleryAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<ImageListResponse>.UnauthorisedResponse(messages);
            }

            Gallery gallery = galleryAccessToken.Gallery;

            List<Image> images = galleryRepository.GetImagesByPage(gallery, pageSize, pageNumber);
            int imageCount = galleryRepository.GetTotalImageCount(gallery);

            if (images.Count == 0)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "No images were found on this page.");

                return PhtgrphrResponse<ImageListResponse>.NotFoundResponse(messages);
            }

            ImageListResponse response = new ImageListResponse(images, imageCount);

            return PhtgrphrResponse<ImageListResponse>.OkResponse(response);
        }

        public PhtgrphrResponse<TokenResponse> Authenticate(Guid guid, string password)
        {
            Gallery gallery = galleryRepository.GetGalleryByGuid(guid);

            if (gallery == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "This gallery doesn't exist, please contact the link provider.");

                return PhtgrphrResponse<TokenResponse>.NotFoundResponse(messages);
            }

            if (gallery.Password != password)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Incorrect password. Please try again.");

                return PhtgrphrResponse<TokenResponse>.UnauthorisedResponse(messages);
            }

            GalleryAccessToken galleryAccessToken = new GalleryAccessToken(gallery);

            galleryRepository.CreateGalleryAccessToken(galleryAccessToken);

            TokenResponse response = new TokenResponse(galleryAccessToken.Token);

            return PhtgrphrResponse<TokenResponse>.OkResponse(response);
        }

        public Gallery SanitiseGallery(Gallery gallery)
        {
            gallery.Password = "********"; // Don't send the password verbatim

            return gallery;
        }

        public GalleryAccessToken GetGalleryAccessTokenByToken(Guid token)
        {
            return galleryRepository.GetGalleryAccessTokenByToken(token);
        }

        public PhtgrphrResponse<ImageListResponse> GetGalleryImagesByGalleryId(Guid token, int galleryId)
        {
            UserAccessToken userAccessToken = userRepository.GetUserAccessTokenByToken(token);

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<ImageListResponse>.UnauthorisedResponse(messages);
            }

            Gallery gallery = userAccessToken.User.Galleries
                .Where(g => g.ID == galleryId)
                .SingleOrDefault();

            if (gallery == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "You are not authorised to view this gallery.");

                return PhtgrphrResponse<ImageListResponse>.UnauthorisedResponse(messages);
            }

            List<Image> images = gallery.Images
                .OrderBy(i => i.Sort)
                .ToList();

            ImageListResponse response = new ImageListResponse(images, images.Count);

            return PhtgrphrResponse<ImageListResponse>.OkResponse(response);
        }

        public PhtgrphrResponse<GalleryResponse> AddEditGallery(Guid token, Gallery gallery)
        {
            UserAccessToken userAccessToken = userRepository.GetUserAccessTokenByToken(token);

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<GalleryResponse>.UnauthorisedResponse(messages);
            }

            gallery.User = userAccessToken.User;

            bool newGallery = gallery.ID == 0;

            // Check if the ID of the gallery isn't one of the user's
            if (!newGallery && userAccessToken.User.Galleries.Where(g => g.ID == gallery.ID).Count() == 0)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "You aren't authorised to edit this gallery.");

                return PhtgrphrResponse<GalleryResponse>.UnauthorisedResponse(messages);
            }

            if (newGallery) // Add
            {
                gallery.Guid = System.Guid.NewGuid();

                int newId = galleryRepository.CreateGallery(gallery);

                if (newId < 1)
                {
                    throw new Exception("Failed to create gallery.");
                }

                gallery.ID = newId;
            }
            else // Edit
            {
                if (!galleryRepository.UpdateGallery(gallery))
                {
                    throw new Exception("Failed to update gallery.");
                }
            }

            Gallery sanitisedGallery = SanitiseGallery(gallery);

            GalleryResponse response = new GalleryResponse(sanitisedGallery);

            return PhtgrphrResponse<GalleryResponse>.OkResponse(response);
        }

        public PhtgrphrResponse<Dictionary<string, bool>> DeleteGalleryByGalleryId(Guid token, int galleryId)
        {
            UserAccessToken userAccessToken = userRepository.GetUserAccessTokenByToken(token);

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<Dictionary<string, bool>>.UnauthorisedResponse(messages);
            }

            Gallery gallery = userAccessToken.User.Galleries
                .Where(g => g.ID == galleryId)
                .SingleOrDefault();

            if (gallery == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "You are not authorised to view this gallery.");

                return PhtgrphrResponse<Dictionary<string, bool>>.UnauthorisedResponse(messages);
            }

            bool success = galleryRepository.DeleteGallery(gallery);

            Dictionary<string, bool> response = new Dictionary<string, bool>();
            response.Add("success", success);

            return PhtgrphrResponse<Dictionary<string, bool>>.OkResponse(response);
        }

        public FileManagerFile GetImageFileWithGalleryAccessToken(Guid token, int imageId, IFileManager fileManager)
        {
            GalleryAccessToken galleryAccessToken = galleryRepository.GetGalleryAccessTokenByToken(token);

            if (galleryAccessToken == null)
            {
                return null;
            }

            Image image = galleryAccessToken.Gallery.Images.Where(i => i.ID == imageId).SingleOrDefault();

            if (image == null)
            {
                return null;
            }

            return fileManager.RetrieveImage(image);
        }

        public FileManagerFile GetImageFileWithUserAccessToken(Guid token, int imageId, IFileManager fileManager)
        {
            Image image = galleryRepository.GetImageById(imageId);

            if (image == null)
            {
                return null;
            }

            UserAccessToken userAccessToken = image.Gallery.User.UserAccessTokens
                .Where(uat => uat.Token == token)
                .SingleOrDefault();

            if (userAccessToken == null)
            {
                return null;
            }

            return fileManager.RetrieveImage(image);
        }

        public PhtgrphrResponse<Dictionary<string, bool>> AddImagesToGallery(Guid token, int galleryId, List<IFormFile> files, IFileManager fileManager)
        {
            UserAccessToken userAccessToken = userRepository.GetUserAccessTokenByToken(token);

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<Dictionary<string, bool>>.UnauthorisedResponse(messages);
            }

            Gallery gallery = userAccessToken.User.Galleries
                .Where(g => g.ID == galleryId)
                .SingleOrDefault();

            if (gallery == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "You are not authorised to add images to this gallery.");

                return PhtgrphrResponse<Dictionary<string, bool>>.UnauthorisedResponse(messages);
            }

            int sort = gallery.Images.Max(i => i.Sort) + 1;
            foreach (IFormFile file in files)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                if (fileManager.StoreFile(file, fileName))
                {
                    // If we successfully store the file, create the record
                    Image image = new Image();
                    image.FileName = fileName;
                    image.Sort = sort;
                    image.Gallery = gallery;

                    galleryRepository.CreateImage(image);
                }
                else
                {
                    throw new Exception("Failed to save " + file.FileName + " to gallery " + gallery.Name + " (" + gallery.ID + ")");
                }

                sort++;
            }

            Dictionary<string, bool> result = new Dictionary<string, bool>();
            result.Add("success", true);

            return PhtgrphrResponse<Dictionary<string, bool>>.OkResponse(result);
        }

        public PhtgrphrResponse<Dictionary<string, bool>> SortImages(Guid token, int galleryId, List<Image> images)
        {
            UserAccessToken userAccessToken = userRepository.GetUserAccessTokenByToken(token);

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<Dictionary<string, bool>>.UnauthorisedResponse(messages);
            }

            Gallery gallery = userAccessToken.User.Galleries
                .Where(g => g.ID == galleryId)
                .SingleOrDefault();

            if (gallery == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "You are not authorised to add images to this gallery.");

                return PhtgrphrResponse<Dictionary<string, bool>>.UnauthorisedResponse(messages);
            }

            bool success = true;

            try
            {
                int sort = 0;
                foreach (Image image in images)
                {
                    // If we're not changing the sort value, we don't need to bother the DB
                    if (image.Sort != sort)
                    {
                        image.Sort = sort;

                        if (!galleryRepository.UpdateImage(image))
                        {
                            throw new Exception("Failed to save image sort.");
                        }
                    }

                    sort++;
                }
            } catch
            {
                success = false;
            }

            Dictionary<string, bool> result = new Dictionary<string, bool>();
            result.Add("success", success);

            return PhtgrphrResponse<Dictionary<string, bool>>.OkResponse(result);
        }

        public PhtgrphrResponse<Dictionary<string, bool>> DeleteImageByImageId(Guid token, int imageId)
        {
            Image image = galleryRepository.GetImageById(imageId);

            if (image == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Image doesn't exist. It may have already been deleted.");

                return PhtgrphrResponse<Dictionary<string, bool>>.NotFoundResponse(messages);
            }

            UserAccessToken userAccessToken = image.Gallery.User.UserAccessTokens
                .Where(uat => uat.Token == token)
                .SingleOrDefault();

            if (userAccessToken == null)
            {
                Dictionary<string, string> messages = new Dictionary<string, string>();

                messages.Add("friendlyError", "Token does not exist or has expired. Please refresh the page and log-in again.");

                return PhtgrphrResponse<Dictionary<string, bool>>.UnauthorisedResponse(messages);
            }

            bool success = galleryRepository.DeleteImage(image);

            Dictionary<string, bool> response = new Dictionary<string, bool>();
            response.Add("success", success);

            return PhtgrphrResponse<Dictionary<string, bool>>.OkResponse(response);
        }
    }
}