using System;
using System.Collections.Generic;
using System.Text;

namespace Person.Domain.Models.ExceptionModels
{
    public class ExceptionLog
    {
        public ExceptionLog()
        {
            Id = Guid.NewGuid();
            Date = DateTimeOffset.Now;
        }
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string IpAddress { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Metadata { get; set; }
    }
}
