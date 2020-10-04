using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhtgrphrAPI.Responses
{
    public class PhtgrphrResponse<T>
    {
        public T Result { get; set; }
        public string Status { get; set; }
        public int Code { get; set; }
        public Dictionary<string, string> Messages { get; set; }

        public PhtgrphrResponse(T result, string status, int code, Dictionary<string, string> messages) {
            Result = result;
            Status = status;
            Code = code;
            Messages = messages;
        }

        public static PhtgrphrResponse<T> OkResponse(T result)
        {
            return OkResponse(result, new Dictionary<string, string>());
        }

        public static PhtgrphrResponse<T> OkResponse(T result, Dictionary<string, string> messages)
        {
            return new PhtgrphrResponse<T>(result, "Ok", 200, messages);
        }

        public static PhtgrphrResponse<T> NotFoundResponse(Dictionary<string, string> messages)
        {
            return new PhtgrphrResponse<T>(default, "NotFound", 404, messages);
        }

        public static PhtgrphrResponse<T> UnauthorisedResponse(Dictionary<string, string> messages)
        {
            return new PhtgrphrResponse<T>(default, "Unauthorised", 401, messages);
        }
    }
}
