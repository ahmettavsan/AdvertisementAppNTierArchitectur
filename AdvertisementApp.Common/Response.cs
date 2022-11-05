using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Common
{
    public class Response:IResponse
    {
        public string Message { get; set; }
        public ResponseType ResponseType { get; set; }
        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }
        public Response(ResponseType responseType,string message)
        {
            ResponseType = responseType;
            Message = message;
        }
    }

    public enum ResponseType
    {
        Success,
        ValidationError,
        NotFound,
        Error
    }
}
