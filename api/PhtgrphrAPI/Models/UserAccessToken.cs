using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Models
{
    public class UserAccessToken
    {
        public UserAccessToken() { }

        public UserAccessToken(User user, DateTime? expiry = null)
        {
            Token = System.Guid.NewGuid();
            Expiry = expiry ?? DateTime.Now.AddHours(1);
            User = user;
        }

        public int ID { get; set; }
        public Guid Token { get; set; }
        public DateTime Expiry { get; set; }

        public virtual User User { get; set; }
    }
}
