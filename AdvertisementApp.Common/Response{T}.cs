using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Common
{
    public class Response<T>:Response,IResponse<T>
    {
        //Response classımız business katmanındaki işin gerçekleşip gerçekleşmeme durumuna göre hareket edicek
        public T Data { get; set; }
        public List<CustomValidationError> ValidationErrors { get; set; }

        public Response(ResponseType responseType,string message):base(responseType,message)
        {

        }
        public Response(ResponseType responseType,T data):base(responseType)
        {
            Data = data;
        }
        public Response(ResponseType responseType,T data,List<CustomValidationError> errors):base(ResponseType.ValidationError)
        {
            Data=data;
            ValidationErrors = errors;
        }

    }
}
