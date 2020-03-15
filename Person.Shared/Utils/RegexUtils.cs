using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Person.Shared.Utils
{
    public static class RegexUtils
    {

        private const string OnlyNUmberRegex =
            @"^[0-9]*$";

        private const string OnlyLatinTextRegex = @"^[a-zA-Z]*$";
        private const string OnlyGeoTextRegex = @"^[ა-ჰ]*$";

        public static bool IsValidFirstName(string firstName)
        {

            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 50)
            {
                return false;
            }

            var result = Regex.IsMatch(firstName, OnlyLatinTextRegex, RegexOptions.IgnoreCase);
            if (!result)
            {

                var result2 = Regex.IsMatch(firstName, OnlyGeoTextRegex, RegexOptions.IgnoreCase);
                if (!result2)
                {

                    return false;
                }
            }

            return true;
        }
        public static bool IsValidLastName(string lastName)
        {

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 50)
            {
                return false;
            }

            var result = Regex.IsMatch(lastName, OnlyLatinTextRegex, RegexOptions.IgnoreCase);
            if (!result)
            {

                var result2 = Regex.IsMatch(lastName, OnlyGeoTextRegex, RegexOptions.IgnoreCase);
                if (!result2)
                {

                    return false;
                }
            }

            return true;
        }
        public static bool IsValidPrivateNumber(string privateNumber)
        {
            if (string.IsNullOrWhiteSpace(privateNumber))
            {
                return false;
            }

            var result = Regex.IsMatch(privateNumber, OnlyNUmberRegex, RegexOptions.IgnoreCase);
            if (!result)
            {
                return false;
            }


            if (privateNumber.Length != 11)
            {
                return false;
            }

            return true;
        }
    }
}
