using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Responses
{
    public class StatsResponse
    {
        public int TotalAccesses { get; set; }
        public int TotalGalleries { get; set; }
        public int TotalImages { get; set; }

        public StatsResponse() {}
    }
}
