using Microsoft.AspNetCore.Mvc;
using PhtgrphrAPI.DbContexts;
using PhtgrphrAPI.FileManagers;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhtgrphrAPIFunctions
{
    public class BaseFunction
    {
        protected IGalleryLogic GetGalleryLogic()
        {
            var context = new FunctionContext(new Microsoft.EntityFrameworkCore.DbContextOptions<PhtgrphrContext>());

            return new GalleryLogic(
                new GalleryRepository(context),
                new UserRepository(context)
            );
        }

        protected IStatsLogic GetStatsLogic()
        {
            var context = new FunctionContext(new Microsoft.EntityFrameworkCore.DbContextOptions<PhtgrphrContext>());

            return new StatsLogic(
                new GalleryRepository(context),
                new UserRepository(context)
            );
        }

        protected IUserLogic GetUserLogic()
        {
            var context = new FunctionContext(new Microsoft.EntityFrameworkCore.DbContextOptions<PhtgrphrContext>());

            return new UserLogic(
                new UserRepository(context)
            );
        }

        protected IFileManager GetFileManager()
        {
            return new AzureBlobFileManager(Environment.GetEnvironmentVariable("AzureStorageConnectionString"));
        }

        protected ActionResult AsActionResult<T>(PhtgrphrResponse<T> response)
        {
            switch (response.Code)
            {
                case 200:
                    return new OkObjectResult(response);
                case 401:
                    return new UnauthorizedObjectResult(response);
                case 404:
                    return new NotFoundObjectResult(response);
                default:
                    throw new Exception("Invalid response code.");
            }
        }
    }
}
