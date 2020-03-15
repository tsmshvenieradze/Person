using System;
using System.Collections.Generic;
using System.Text;

namespace Person.Domain.Services.Models
{
    public enum ServiceResultStatus
    {
        Success = 1,
        Error = 2,
        Unauthorized = 3,
        AccessDenied = 4,
        NotFound = 5,
        FatalError = 6,
    }
    public class ServiceResult<TResult>
    {
        public string Message { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public ServiceResultStatus Status { get; set; }
        public TResult Result { get; set; }

        public ServiceResult()
        {
            Parameters = new Dictionary<string, object>();
            Status = ServiceResultStatus.Success;
        } 

       
        public ServiceResult(string parameterName, object data)
        {
            Parameters = new Dictionary<string, object>
            {
                { parameterName, data }
            };
            Status = ServiceResultStatus.Success;
        }



    }
}
