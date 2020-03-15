using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Person.Shared.Utils
{
    public static class EnumUtils
    {
        public static bool HasValue<T>(T val) where T : struct
        {
            var defined = Enum.IsDefined(typeof(T), val);
            if (!defined)
            {
                return false;
            }

            return Convert.ToInt32(val) > 0;
        }
    }
}
