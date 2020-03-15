using System;
using System.Collections.Generic;
using System.Text;

namespace Person.Domain
{

    public enum ExceptionLevel
    {
        Info = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4
    }

    public class DomainException : Exception
    {
        public dynamic DeveloperInfo { get; protected set; }
        public ExceptionLevel Level { get; protected set; }

        public DomainException(string message, ExceptionLevel level, dynamic developerInfo = null, Exception innerException = null) : base(message, innerException)
        {
            Level = level;
            DeveloperInfo = developerInfo;
        }
    }
}
