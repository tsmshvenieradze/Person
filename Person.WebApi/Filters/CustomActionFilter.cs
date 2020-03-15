using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Person.Domain;
using Person.Infrastructure;
using Person.WebApi.Middlewares;

namespace Person.WebApi.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private IStringLocalizer<SharedResources> _sharedLocalizer;

        public ValidationFilterAttribute(  IStringLocalizer<SharedResources> sharedLocalizer)
        { 
            _sharedLocalizer = sharedLocalizer;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is ValidationObject);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            if (param.Value is ValidationObject validationObject)
            {
                validationObject.Validate();

            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        } 

    }
}
