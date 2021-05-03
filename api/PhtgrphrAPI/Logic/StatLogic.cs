using PhtgrphrAPI.Models;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Logic
{
    public class StatsLogic : IStatsLogic
    {
        private IGalleryRepository galleryRepository;
        private IUserRepository userRepository;

        public StatsLogic(IGalleryRepository galleryRepository, IUserRepository userRepository)
        {
            this.galleryRepository = galleryRepository;
            this.userRepository = userRepository;
        }

        public PhtgrphrResponse<StatsResponse> GetStats()
        {
            StatsResponse response = new StatsResponse();

            response.TotalAccesses = galleryRepository.GetGalleryAccessTokenCount();
            response.TotalGalleries = galleryRepository.GetGalleryCount();
            response.TotalImages = galleryRepository.GetImageCount();

            return PhtgrphrResponse<StatsResponse>.OkResponse(response);
        }
    }
}
