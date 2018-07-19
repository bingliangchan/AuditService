using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Common.ActionResults
{
    public class NotFoundRequestObjectResult : ObjectResult
    {
        public NotFoundRequestObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status404NotFound;
        }
    }
}