using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Person.Shared.Utils
{
    public static class DateTimeUtils
    {
        public static object Project;
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

        public static DateTime? ToNullableDate(string input)
        {
            DateTime date;
            if (!DateTime.TryParseExact(input, SystemSettings.ShortDatePattern, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
            {
                return null;
            }
            return date;
        }

        public static DateTime? ToNullableDateTime(string input)
        {

            DateTime dateTime;
            if (!DateTime.TryParseExact(input, SystemSettings.LongDatePattern, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime))
            {
                return null;
            }
            return dateTime;
        }

        public static DateTime ToDateShortTime(string input)
        {
            DateTime dateTime;
            return !DateTime.TryParseExact(input, SystemSettings.DateShortTimePattern, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime)
                ? new DateTime()
                : dateTime;
        }

        public static DateTime ToDate(string input)
        {
            DateTime date;
            DateTime.TryParseExact(input, SystemSettings.ShortDatePattern, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
            return date;
        }

        public static DateTime ToDateTime(string input)
        {
            DateTime date;
            DateTime.TryParseExact(input, SystemSettings.LongDatePattern, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
            return date;
        }


        public static string ToDateTimeString(DateTime? dateTime)
        {
            return dateTime?.ToString(SystemSettings.LongDatePattern) ?? "";
        }

        public static string ToDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString(SystemSettings.LongDatePattern);
        }

        public static string ToDateTimeShortTimeString(DateTime? dateTime)
        {
            return dateTime?.ToString(SystemSettings.DateTimeShortTimePattern) ?? "";
        }

        public static string ToDateTimeShortTimeString(DateTime dateTime)
        {
            return dateTime.ToString(SystemSettings.DateTimeShortTimePattern);
        }

        public static string ToReportDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString(SystemSettings.PdfReportDateShortTimePattern);
        }
        public static string ToReportDate(DateTime dateTime)
        {
            return dateTime.ToString(SystemSettings.PdfReportDatePattern);
        }


        public static string ToReportDateTimeString(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                return "";
            }
            return dateTime.Value.ToString(SystemSettings.PdfReportDateShortTimePattern);
        }



        public static string ToReportDateTimeWithSecondString(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                return "";
            }
            return dateTime.Value.ToString(SystemSettings.PdfReportDateTimeShortTimePattern);
        }

        public static string ToDateString(DateTime? dateTime)
        {
            return dateTime?.ToString(SystemSettings.ShortDatePattern) ?? "";
        }
        public static string ReportToDateString(DateTime? dateTime)
        {
            return dateTime?.ToString(SystemSettings.PdfReportDateTimeShortDatePattern) ?? "";
        }

        public static string ToTimeString(DateTime? dateTime)
        {
            return dateTime?.ToString(SystemSettings.ShortTimePattern) ?? "";
        }

        public static string ToDateString(DateTime dateTime)
        {
            return dateTime.ToString(SystemSettings.ShortDatePattern);
        }


        public static bool IsToday(DateTime date)
        {
            return date.Date == Now.Date;
        }

        public static bool IsValid(DateTime date)
        {
            return date.Year > 1900 && date.Year < 2100;
        }

        public static bool IsValid(DateTime? date)
        {
            return date.HasValue && date.Value.Year > 1900 && date.Value.Year < 2100;
        }

        public static bool IsUnderage(DateTime? birthdate)
        {
            if (!birthdate.HasValue)
            {
                return false;
            }
            var age = Now.Year - birthdate.Value.Year;
            if (birthdate.Value.AddYears(age) > Now)
            {
                age--;
            }
            return age >= 18;
        }

        public static void SetStateMigration(DateTime metadataSignDate)
        {
            migrateDate = metadataSignDate;
        }
        public static void ClearMigrationDate()
        {
            migrateDate = null;
        }




        public static double GetDays(DateTime StartDate, DateTime EndDate)
        {
            return 1 + (EndDate - StartDate).TotalDays;
        }


        public static double GetDaysThisYear(DateTime startD, DateTime endD)
        {
            var daysCount = GetDays(startD, endD);
            if (startD.Year == endD.Year)
            {
                return daysCount;
            }
            var daysthisYear = GetDays(startD, new DateTime(startD.Year, 12, 31));

            return daysCount <= daysthisYear ? daysCount : daysthisYear;
        }

        public static double GetDaysNextYear(DateTime startD, DateTime endD)
        {
            if (startD.Year == endD.Year)
            {
                return 0.0;
            }
            var daysNextYear = GetDays(new DateTime(endD.Year, 1, 1), endD);

            return daysNextYear;
        }


    }
}
