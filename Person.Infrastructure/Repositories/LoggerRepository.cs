using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Person.Domain.Contracts;
using Person.Domain.Models.ExceptionModels;
using Person.Domain.Models.LogModels;
using Person.Domain.Services.Models;
using Person.Infrastructure.Db;
using Person.Infrastructure.Repositories;

namespace Person.Infrastructure.Repositories
{
    public class LoggerRepository : Repository<PersonDbContext>, ILoggerRepository
    {
        public LoggerRepository(PersonDbContext context) : base(context)
        {
        }

        public async Task<int> Log(dynamic request, dynamic response, int status = 0, Exception ex = null)
        {
            var exString = FlattenException(ex);
            var entity = new LogData
            {
                CreateDate = DateTime.Now,
                Status = status,
                Exception = exString,
                StackTrace = ex?.StackTrace != null ? ex.StackTrace : "",
                RequestData = request != null ? JsonConvert.SerializeObject(request) : null,
                ResponseData = response != null ? JsonConvert.SerializeObject(response) : null,
            };

            _db.Logs.Add(entity);

            return await _db.SaveChangesAsync();
        }

        public static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();
            var deepSize = 50;
            var i = 0;
            while (exception != null && i < deepSize)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
                i++;
            }

            return stringBuilder.ToString();
        }
    }
}
