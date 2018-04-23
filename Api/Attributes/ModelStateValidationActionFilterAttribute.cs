using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// This class will catch if model state is invalid
    /// </summary>
    public class ModelStateValidationActionFilterAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// this method gets called before executing controller action
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(new
                {
                    ModelErrorState = modelState.Values.Where(state => state.ValidationState == ModelValidationState.Invalid)
                });

                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}