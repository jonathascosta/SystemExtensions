using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SystemExtensions
{
    public struct MonthYear : IComparable<MonthYear>, IEquatable<MonthYear>, IComparable<DateTime>, IEquatable<DateTime>, IFormattable
    {
        private const string MonthYearFormat = @"^((?<month>MM).*(?<year>YYYY))|((?<year>YYYY).*(?<month>MM))|((?<month>MM).*(?<year>YY))|((?<year>YY).*(?<month>MM))$";

        public int Month { get; }
        public int Year { get; }

        public MonthYear(int month, int year)
            : this()
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            Month = month;
            Year = year;
        }

        public DateTime GetDate(int day)
        {
            return new DateTime(Year, Month, day);
        }

        public DateTime GetFirstDay()
        {
            return GetDate(1);
        }

        public DateTime GetLastDay()
        {
            return GetDate(1).LastDayOfMonth();
        }

        public MonthYear AddMonths(int months)
        {
            return GetDate(1).AddMonths(months).ToMonthYear();
        }

        public MonthYear Next()
        {
            return AddMonths(1);
        }

        public MonthYear Previous()
        {
            return AddMonths(-1);
        }

        public int CompareTo(MonthYear other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return ToString("MM/yyyy", CultureInfo.CurrentCulture);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MonthYear))
                return false;

            return this == (MonthYear)obj;
        }

        public override int GetHashCode()
        {
            return Year * 12 + Month;
        }

        public static MonthYear Parse(string strValue)
        {
            var parts = strValue.Split('/');
            if (parts.Length != 2)
                throw new FormatException();

            var month = int.Parse(parts[0]);
            var year = int.Parse(parts[1]);

            return new MonthYear(month, year);
        }

        public static MonthYear Parse(string strValue, string format)
        {
            var match = Regex.Match(format, MonthYearFormat, RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new FormatException();

            var month = int.Parse(PartialParse(strValue, format, match.Groups["month"].Value));
            var year = int.Parse(PartialParse(strValue, format, match.Groups["year"].Value));

            return new MonthYear(month, year);
        }

        private static string PartialParse(string strValue, string format, string part)
        {
            return strValue.Substring(format.IndexOf(part), part.Length);
        }

        public bool Equals(MonthYear other)
        {
            return this == other;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return GetDate(1).ToString(format, formatProvider);
        }

        public int CompareTo(DateTime other)
        {
            return GetHashCode() - other.ToMonthYear().GetHashCode();
        }

        public bool Equals(DateTime other)
        {
            return GetHashCode() == other.ToMonthYear().GetHashCode();
        }

        #region " Operators "

        public static bool operator ==(MonthYear a, MonthYear b)
        {
            return a.GetHashCode() == b.GetHashCode();
        }

        public static bool operator !=(MonthYear a, MonthYear b)
        {
            return a.GetHashCode() != b.GetHashCode();
        }

        public static bool operator <(MonthYear a, MonthYear b)
        {
            return a.GetHashCode() < b.GetHashCode();
        }

        public static bool operator >(MonthYear a, MonthYear b)
        {
            return a.GetHashCode() > b.GetHashCode();
        }

        public static bool operator <=(MonthYear a, MonthYear b)
        {
            return a.GetHashCode() <= b.GetHashCode();
        }

        public static bool operator >=(MonthYear a, MonthYear b)
        {
            return a.GetHashCode() >= b.GetHashCode();
        }

        public static MonthYear operator ++(MonthYear monthYear)
        {
            return monthYear + 1;
        }

        public static MonthYear operator --(MonthYear monthYear)
        {
            return monthYear - 1;
        }

        public static MonthYear operator +(MonthYear a, int b)
        {
            return a.AddMonths(b);
        }

        public static MonthYear operator -(MonthYear a, int b)
        {
            return a.AddMonths(-b);
        }

        public static int operator -(MonthYear a, MonthYear b)
        {
            return a.GetHashCode() - b.GetHashCode();
        }

        public static implicit operator MonthYear(DateTime date)
        {
            return date.ToMonthYear();
        }

        #endregion
    }
}
