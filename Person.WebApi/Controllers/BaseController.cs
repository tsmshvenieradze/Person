using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Person.Domain;
using Person.Domain.Services.Models;

namespace Person.WebApi.Controllers
{
    public class BaseController : Controller
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public BaseController(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }
        public ServiceResult<T> Try<T>(Func<ServiceResult<T>> action)
        {
            try
            {
                return action();
            }
            catch (DomainException ex)
            {
                return new ServiceResult<T>()
                {
                    Message = _localizer[ex.Message],
                    Status = ServiceResultStatus.Error
                };
            }
            catch (Exception ex)
            {
                var result = new ServiceResult<T>()
                {
                    Message = ex.Message,
                    Status = ServiceResultStatus.FatalError,
                    Parameters = new Dictionary<string, object>()
                };
                result.Parameters.Add("ex", ex.ToString());
            }

            return null;
        }

        public BaseController()
        {
        }

    }
}
