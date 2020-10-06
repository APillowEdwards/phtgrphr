using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Responses
{
    public class TokenResponse
    {
        public Guid Token { get; set; }

        public TokenResponse(Guid token)
        {
            Token = token;
        }
    }

    public class TokenListResponse
    {
        public List<Guid> Tokens { get; set; }

        public TokenListResponse(List<Guid> tokens)
        {
            Tokens = new List<Guid>();
            foreach (Guid token in tokens)
            {
                Tokens.Add(token);
            }
        }
    }
}
