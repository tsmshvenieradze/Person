using System;
using System.Collections.Generic;
using System.Text;

namespace Person.Shared.Utils
{
    public static class SystemSettings
    {
        public const string ShortDatePattern = "dd-MM-yyyy";
        public const string LongDatePattern = "dd-MM-yyyy HH:mm:ss.FFFFFFFK";
        public const string LongDateMomentJsPattern = "yyyy-MM-ddTHH:mm:ss.FFFFFFFK";
        public const string DateShortTimePattern = "dd-MM-yyyy HH:mm";
        public const string DateTimeShortTimePattern = "dd-MM-yyyy HH:mm:ss";
        public const string PdfReportDateTimeShortTimePattern = "dd.MM.yyyy HH:mm:ss";
        public const string PdfReportDateTimeShortDatePattern = "dd.MM.yyyy";
        public const string PdfReportDateShortTimePattern = "dd.MM.yyyy HH:mm";
        public const string PdfReportDatePattern = "dd.MM.yyyy";
        public const string ShortTimePattern = "HH:mm";
        public const string LongTimePattern = "HH:mm:ss";
        public const string NumberDecimalSeparator = ".";
        public const string NumberGroupSeparator = " ";
        public const string DatePatternForMomentjs = "yyyy-MM-dd HH:mm:ss";
        public const string MomentToDateTime = "ddd MMM dd yyyy HH:mm:ss";
        public static string FullDateTimePattern = "dd-MM-yyyyTHH:mm:ss.FFFFFFFK";
    }
}
