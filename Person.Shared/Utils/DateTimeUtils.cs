using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Person.Shared.Utils
{
    public static class DateTimeUtils
    { 
        public static DateTime? migrateDate;

        public static DateTime Now
        {
            get
            {
                if (migrateDate.HasValue)
                {
                    return migrateDate.Value;
                }

                return DateTime.Now;
            }
        }
    }
}
