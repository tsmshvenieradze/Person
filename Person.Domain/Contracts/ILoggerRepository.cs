using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Person.Domain.Models.ExceptionModels;
using Person.Domain.Services.Models;

namespace Person.Domain.Contracts
{
    public interface ILoggerRepository
    {
        Task<int> Log(dynamic request, dynamic response, int status = 0, Exception ex = null);
    }
}
