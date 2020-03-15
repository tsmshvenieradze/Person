using System;
using System.Collections.Generic;
using System.Text;

namespace Person.Domain.Models.LogModels
{
    public class LogData : Entity
    {
        public string RequestData { get; set; }
        public string ResponseData { get; set; } 
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public int Status { get; set; }
        public string MetaData { get; set; }
        public string DebugInfo { get; set; }
    }
}
