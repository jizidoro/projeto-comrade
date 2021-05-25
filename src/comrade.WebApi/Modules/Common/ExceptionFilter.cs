#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace comrade.WebApi.Modules.Common
{
    /// <summary>
    ///     Exception Filter.
    /// </summary>
    public sealed class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        ///     Add Problem Details when occurs Domain Exception.
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            ProblemDetails problemDetails = new() {Status = 500, Title = "Bad Request"};

            context.Result = new JsonResult(problemDetails);
            context.Exception = null!;
        }
    }
}