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

        public static Dictionary<string, string> ToStringDictionary<T>() where T : struct
        {
            var t = typeof(T);
            var typeName = t.Name;
            return Enum.GetValues(t).Cast<int>().ToDictionary(e => e.ToString(), e => $"Enum.{typeName}.{Enum.GetName(t, e)}");
        }

        public static Dictionary<T, string> ToDictionary<T>() where T : struct
        {
            var t = typeof(T);
            var typeName = t.Name;
            return Enum.GetValues(t).Cast<T>().ToDictionary(e => e, e => $"Enum.{typeName}.{Enum.GetName(t, e)}");
        }

        public static List<T> ToList<T>() where T : struct
        {
            var t = typeof(T);
            return Enum.GetValues(t).OfType<T>().ToList();
        }

        public static List<int> ToList(Type type)
        {
            return Enum.GetValues(type).OfType<int>().ToList();
        }

        public static T Parse<T>(string val) where T : struct
        {
            T enumType;
            Enum.TryParse(val, out enumType);
            return enumType;
        }

        public static List<int> ToIntList<T>() where T : struct
        {
            var t = typeof(T);
            return Enum.GetValues(t).Cast<int>().ToList();
        }

        public static int Random(Type t)
        {
            var enumValues = ToList(t);
            var random = new Random();
            var index = random.Next(0, enumValues.Count - 1);
            return enumValues[index];
        }
    }
}
