using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KermanCraft.Application.Tools.PersianDate
{
    public static class PersianDate
    {
        public static DateTime ToGeorgianDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.Now;
            }
            var persianCalendar = new System.Globalization.PersianCalendar();

            var str = date.Split("/");
            var y = int.Parse(PersianToEnglish(str[0]));
            var m = int.Parse(PersianToEnglish(str[1]));
            var d = int.Parse(PersianToEnglish(str[2]));
            var gDateTime = persianCalendar.ToDateTime(y, m, d, 0, 0, 0, 0);
            return gDateTime;
        }

        public static string ToPersianDateString(DateTime date)
        {
            var persian = new System.Globalization.PersianCalendar();
            var persianDate = $"{persian.GetYear(date)}/{persian.GetMonth(date)}/{persian.GetDayOfMonth(date)}";
            return persianDate;
        }

        public static string ToPersianDateString(string date)
        {
           
            var d = DateTime.Parse(date);
            var pc = new PersianCalendar();
            return $"{pc.GetYear(d)}/{pc.GetMonth(d)}/{pc.GetDayOfMonth(d)}";
        }
        public static string PersianToEnglish(this string persianStr)
        {
            if (string.IsNullOrEmpty(persianStr))
            {
                return string.Empty;
            }
            var lettersDictionary = new Dictionary<string, string>
            {
                ["۰"] = "0",
                ["۱"] = "1",
                ["۲"] = "2",
                ["۳"] = "3",
                ["۴"] = "4",
                ["۵"] = "5",
                ["۶"] = "6",
                ["۷"] = "7",
                ["۸"] = "8",
                ["۹"] = "9"
            };
            return lettersDictionary.Aggregate(persianStr, (current, item) =>
                current.Replace(item.Key, item.Value));
        }

        public static string ToPersianDateString(DateTime? date)
        {
            var persian = new System.Globalization.PersianCalendar();
            var persianDate = $"{persian.GetYear((DateTime) date)}/{persian.GetMonth((DateTime) date)}/{persian.GetDayOfMonth((DateTime) date)}";
            return persianDate;
        }
    }
}
