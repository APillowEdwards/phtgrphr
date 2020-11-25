using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public int Sort { get; set; }

        [NotMapped]
        public bool Visible { get; set; }

        public virtual Gallery Gallery { get; set; }
    }
}
