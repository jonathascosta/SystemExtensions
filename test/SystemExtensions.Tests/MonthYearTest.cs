using System;
using Xunit;

namespace SystemExtensions.Tests
{
    public class MonthYearTest
    {
        [Fact]
        public void ConstructorShouldFailWithInvalidMonth()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MonthYear(13, 2000));
        }

        [Fact]
        public void GetFirstDayTest()
        {
            var my = new MonthYear(1, 2000);
            var actual = my.GetFirstDay();
            var expected = new DateTime(2000, 1, 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetLastDayTest()
        {
            var my = new MonthYear(1, 2000);
            var actual = my.GetLastDay();
            var expected = new DateTime(2000, 1, 31);

            Assert.Equal(expected, actual);

            my = new MonthYear(2, 2000);
            actual = my.GetLastDay();
            expected = new DateTime(2000, 2, 29);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DateComparisonTests()
        {
            var my1 = new MonthYear(12, 1999);
            var my2 = new MonthYear(1, 2000);

            var dt1 = new DateTime(1999, 12, 1);
            var dt2 = new DateTime(2000, 1, 1);
            var dt3 = new DateTime(2000, 2, 1);

            MonthYear my3 = dt3;

            Assert.True(my1.Equals(dt1));
            Assert.False(my1.Equals(dt2));
            Assert.True(my1.CompareTo(dt1) == 0);
            Assert.True(my2.CompareTo(dt1) > 0);
            Assert.True(my1.Equals(dt1));
            Assert.True(my1.Equals((object)my1));

            Assert.True(my1.Equals(dt1));
            Assert.False(my1.Equals(dt2));
            my1++;
            Assert.True(my1.Equals(dt2));
            Assert.False(my1.Equals(dt1));
            my1--;
            Assert.False(my1.Equals(dt2));
            Assert.True(my1.Equals(dt1));
        }

        [Fact]
        public void ComparisonOperatorsTest()
        {
            var my1 = new MonthYear(12, 1999);
            var my2 = new MonthYear(1, 2000);
            var my3 = new MonthYear(1, 2000);

            Assert.True(my1 != my2);
            Assert.True(my2 != my1);
            Assert.True(my1 < my2);
            Assert.True(my2 > my1);
            Assert.False(my2 < my3);
            Assert.False(my2 > my3);
            Assert.True(my1 <= my2);
            Assert.True(my2 >= my1);
            Assert.True(my2 == my3);
            Assert.True(my3 == my2);
            Assert.True(my3 <= my2);
            Assert.True(my2 <= my3);
            Assert.True(my2 >= my3);
            Assert.True(my3 >= my2);
        }

        [Fact]
        public void ArithmeticOperatorsTest()
        {
            var my1 = new MonthYear(12, 1999);
            var my2 = new MonthYear(1, 2000);

            Assert.Equal(1, my2.Month);
            Assert.Equal(2, (my2 + 1).Month);
            Assert.Equal(1, (my1 + 1).Month);
            Assert.Equal(11, (my1 - 1).Month);
            Assert.Equal(12, (my2 - 1).Month);
            Assert.Equal(1, my2 - my1);
            Assert.Equal(-1, my1 - my2);
            Assert.Equal(0, my1 - my1);
        }

        [Fact]
        public void GetDateTest()
        {
            var date = new DateTime(2000, 1, 14);
            var my = new MonthYear(1, 2000);
            Assert.Equal(date, my.GetDate(14));

            date = new DateTime(2000, 2, 29);
            my = new MonthYear(2, 2000);
            Assert.Equal(date, my.GetDate(29));
        }

        [Fact]
        public void GetDateTestShouldFailIfDayIsInvalid()
        {
            var my = new MonthYear(2, 2001);
            Assert.Throws<ArgumentOutOfRangeException>(() => my.GetDate(29));
        }

        [Fact]
        public void AddMonthsTest()
        {
            var my = new MonthYear(1, 2000);

            var actual = my.AddMonths(1);
            var expected = new MonthYear(2, 2000);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Year, actual.Year);

            actual = my.AddMonths(-1);
            expected = new MonthYear(12, 1999);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Year, actual.Year);
        }

        [Fact]
        public void NextTest()
        {
            var my = new MonthYear(1, 2000);
            var actual = my.Next();
            var expected = new MonthYear(2, 2000);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Year, actual.Year);

            my = new MonthYear(12, 2000);
            actual = my.Next();
            expected = new MonthYear(1, 2001);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Year, actual.Year);
        }

        [Fact]
        public void PreviousTest()
        {
            var my = new MonthYear(1, 2000);
            var actual = my.Previous();
            var expected = new MonthYear(12, 1999);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Year, actual.Year);

            my = new MonthYear(2, 2000);
            actual = my.Previous();
            expected = new MonthYear(1, 2000);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Year, actual.Year);
        }

        [Fact]
        public void CompareToTest()
        {
            var my1 = new MonthYear(1, 2000);
            var my2 = new MonthYear(2, 2000);
            var actual = my1.CompareTo(my2);
            Assert.True(actual < 0);

            my1 = new MonthYear(1, 2000);
            my2 = new MonthYear(1, 2001);
            actual = my1.CompareTo(my2);
            Assert.True(actual < 0);

            my1 = new MonthYear(1, 2000);
            my2 = new MonthYear(1, 2000);
            actual = my1.CompareTo(my2);
            Assert.Equal(0, actual);

            my1 = new MonthYear(3, 2000);
            my2 = new MonthYear(2, 2000);
            actual = my1.CompareTo(my2);
            Assert.True(actual > 0);

            my1 = new MonthYear(1, 2002);
            my2 = new MonthYear(1, 2001);
            actual = my1.CompareTo(my2);
            Assert.True(actual > 0);
        }

        [Fact]
        public void EqualsTest()
        {
            var my1 = new MonthYear(1, 2000);
            var my2 = new MonthYear(2, 2000);
            var actual = my1.Equals(my2);
            Assert.False(actual);

            my1 = new MonthYear(1, 2000);
            my2 = new MonthYear(1, 2000);
            actual = my1.Equals(my2);
            Assert.True(actual);
        }

        [Fact]
        public void ToStringTest()
        {
            var my = new MonthYear(1, 2000);
            var expected = my.GetDate(1).ToString("MM/yyyy");
            var actual = my.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EqualsReturnFalseIfIsNotAMonthYear()
        {
            var my1 = new MonthYear(1, 2000);
            var my2 = 0;
            var actual = my1.Equals(my2);

            Assert.False(actual);
        }

        [Fact]
        public void ParseTest()
        {
            var my = MonthYear.Parse("1/2000");
            var expected = new MonthYear(1, 2000);

            Assert.Equal(expected.Month, my.Month);
            Assert.Equal(expected.Year, my.Year);
        }

        [Fact]
        public void ParseShouldFailIfMonthIsLessThanOne()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => MonthYear.Parse("0/2000"));
        }

        [Fact]
        public void ParseShouldFailIfMonthIsMoreThanTwelve()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => MonthYear.Parse("13/2000"));
        }

        [Fact]
        public void ParseShouldFailIfMonthYearIsInvalid()
        {
            Assert.Throws<FormatException>(() => MonthYear.Parse("a/b"));
        }

        [Fact]
        public void ParseShouldFailIfFormatIsIncorrect()
        {
            Assert.Throws<FormatException>(() => MonthYear.Parse("1-2012"));
        }

        [Fact]
        public void ParseWithFormatShouldFailIfFormatIsInvalid()
        {
            Assert.Throws<FormatException>(() => MonthYear.Parse("12.2000", "MM-XXX"));
        }

        [Fact]
        public void ParseWithFormatTest()
        {
            var my = MonthYear.Parse("12.2000", "MM.YYYY");
            var expected = new MonthYear(12, 2000);

            Assert.Equal(expected.Month, my.Month);
            Assert.Equal(expected.Year, my.Year);
        }
    }
}
