using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExtensions
{
    public struct DayMonth : IComparable<DayMonth>, IEquatable<DayMonth>, IComparable<DateTime>, IEquatable<DateTime>, IFormattable
    {
        public int Day { get; }
        public int Month { get; }

        // Used internally to test february dates
        private const int ExampleLeapYear = 2000;

        public DayMonth(int day, int month)
            : this()
        {
            var date = new DateTime(ExampleLeapYear, month, day);

            Day = date.Day;
            Month = date.Month;
        }

        public DateTime GetDate(int year)
        {
            return new DateTime(year, Month, Day);
        }

        public int CompareTo(DayMonth other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return ToString("dd/MM");
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return GetDate(ExampleLeapYear).ToString(format, formatProvider);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DayMonth))
                return false;

            return this == (DayMonth)obj;
        }

        public override int GetHashCode()
        {
            return ((Month - 1) << 5) + Day;
        }

        public bool Equals(DayMonth other)
        {
            return this == other;
        }

        public int CompareTo(DateTime other)
        {
            return GetHashCode() - other.ToDayMonth().GetHashCode();
        }

        public bool Equals(DateTime other)
        {
            return this == other.ToDayMonth();
        }

        #region " Operators "

        public static bool operator ==(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() == b.GetHashCode();
        }

        public static bool operator !=(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() != b.GetHashCode();
        }

        public static bool operator <(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() < b.GetHashCode();
        }

        public static bool operator >(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() > b.GetHashCode();
        }

        public static bool operator <=(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() <= b.GetHashCode();
        }

        public static bool operator >=(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() >= b.GetHashCode();
        }

        public static bool operator ==(DayMonth dayMonth, DateTime date)
        {
            return date.ToDayMonth() == dayMonth;
        }

        public static bool operator !=(DayMonth dayMonth, DateTime date)
        {
            return date.ToDayMonth() != dayMonth;
        }

        public static bool operator <(DayMonth dayMonth, DateTime date)
        {
            return dayMonth < date.ToDayMonth();
        }

        public static bool operator >(DayMonth dayMonth, DateTime date)
        {
            return dayMonth > date.ToDayMonth();
        }

        public static bool operator <=(DayMonth dayMonth, DateTime date)
        {
            return dayMonth <= date.ToDayMonth();
        }

        public static bool operator >=(DayMonth dayMonth, DateTime date)
        {
            return dayMonth >= date.ToDayMonth();
        }

        public static bool operator ==(DateTime date, DayMonth dayMonth)
        {
            return dayMonth == date;
        }

        public static bool operator !=(DateTime date, DayMonth dayMonth)
        {
            return dayMonth != date;
        }

        public static bool operator <(DateTime date, DayMonth dayMonth)
        {
            return date.ToDayMonth() < dayMonth;
        }

        public static bool operator >(DateTime date, DayMonth dayMonth)
        {
            return date.ToDayMonth() > dayMonth;
        }

        public static bool operator <=(DateTime date, DayMonth dayMonth)
        {
            return date.ToDayMonth() <= dayMonth;
        }

        public static bool operator >=(DateTime date, DayMonth dayMonth)
        {
            return date.ToDayMonth() >= dayMonth;
        }

        public static DayMonth operator ++(DayMonth dayMonth)
        {
            return dayMonth + 1;
        }

        public static DayMonth operator --(DayMonth dayMonth)
        {
            return dayMonth - 1;
        }

        public static DayMonth operator +(DayMonth dayMonth, int days)
        {
            return dayMonth.GetDate(ExampleLeapYear).AddDays(days).ToDayMonth();
        }

        public static DayMonth operator -(DayMonth dayMonth, int days)
        {
            return dayMonth.GetDate(ExampleLeapYear).AddDays(-days).ToDayMonth();
        }

        public static implicit operator DayMonth(DateTime date)
        {
            return date.ToDayMonth();
        }

        #endregion
    }
}
