using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// This class is used to catch global exception on api calls
    /// </summary>
    public class ExceptionFilterAttribute : IExceptionFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// This method will be called when controller action
        /// throw an exception
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Exception exception;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // only add a pattern matching for exception type if exception
            // has a custom field
            switch (context.Exception)
            {
                default:
                    exception = context.Exception;
                    break;
            }

            context.Result = new BadRequestObjectResult(new
            {
                BusinessLogicErrorState = exception
            });
        }
    }
}