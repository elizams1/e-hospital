using System.Net;

namespace BATCH336B.ViewModel
{
    public class VMResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }

        public VMResponse() 
        {
            StatusCode = HttpStatusCode.InternalServerError;
            message = string.Empty;
            data = null;
        }
    }
}
