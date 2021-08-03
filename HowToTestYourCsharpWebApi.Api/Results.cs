// Copy from https://github.com/davidfowl/CommunityStandUpMinimalAPI/blob/main/TodoApi/Results.cs
// TODO: https://github.com/dotnet/aspnetcore/issues/33729

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    public static class Results
    {
        public static IResult BadRequest() => new StatusCodeResult(400);
        public static IResult Ok() => new StatusCodeResult(200);
        public static IResult Status(int statusCode) => new StatusCodeResult(statusCode);
        public static IResult Ok(object value) => new JsonResult(value);
        public static IResult Created(string location, object value) => new CreatedResult(location, value);

        private class CreatedResult : IResult
        {
            private readonly object _value;
            private readonly string _location;

            public CreatedResult(string location, object value)
            {
                _location = location;
                _value = value;
            }

            public Task ExecuteAsync(HttpContext httpContext)
            {
                httpContext.Response.StatusCode = StatusCodes.Status201Created;
                httpContext.Response.Headers.Location = _location;

                return httpContext.Response.WriteAsJsonAsync(_value);
            }
        }
    }
}