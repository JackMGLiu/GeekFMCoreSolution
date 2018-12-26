using Microsoft.AspNetCore.Http;

namespace Geek.Project.Portal.Models
{
    public static class RequestHelper
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            bool result = false;
            var xreq = request.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = request.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }
    }
}
