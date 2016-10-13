using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace PerformanceDemos
{
    public class EnableCompressionAttribute : ActionFilterAttribute
    {
        const CompressionMode compress = CompressionMode.Compress;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;

            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (acceptEncoding == null)
                return;
            else if (acceptEncoding.ToLower().Contains("gzip"))
            {
                response.Filter = new GZipStream(response.Filter, compress);
                response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (acceptEncoding.ToLower().Contains("deflate"))
            {
                response.Filter = new DeflateStream(response.Filter, compress);
                response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }

}