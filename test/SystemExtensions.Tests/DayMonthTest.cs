using System;
using System.Collections.Generic;
using Xunit;

namespace SystemExtensions.Tests
{
    public class DayMonthTest
    {
        [Fact]
        public void ConstructorShouldFailWithInvalidDayBiggerThanLastDayOfMonth()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DayMonth(30, 2));
        }

        [Fact]
        public void ConstructorShouldFailWithInvalidDayLesserOne()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DayMonth(0, 2));
        }

        [Fact]
        public void ConstructorShouldFailWithInvalidMonthBiggerThanTwelve()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DayMonth(1, 13));
        }

        [Fact]
        public void ConstructorShouldFailWithInvalidMonthLesserThanOne()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DayMonth(1, 0));
        }

        [Fact]
        public void ConstructorShouldSuccessWithValidLeapYearDate()
        {
            const int day = 29;
            const int month = 2;
            var dm = new DayMonth(day, month);

            Assert.Equal(day, dm.Day);
            Assert.Equal(month, dm.Month);
        }

        [Fact]
        public void GetDateShouldFailIfDayMonthIsValidOnlyInLeapYear()
        {
            const int day = 29;
            const int month = 2;
            const int year = 1999;
            var dm = new DayMonth(day, month);
            Assert.Throws<ArgumentOutOfRangeException>(() => dm.GetDate(year));
        }

        [Fact]
        public void GetDateShouldSuccessDayMonthIsValid()
        {
            var year = 2000;
            var dm = new DayMonth(29, 2);
            var date = dm.GetDate(year);
            var expected = new DateTime(year, 2, 29);
            Assert.Equal(expected, date);

            year = 1999;
            dm = new DayMonth(31, 1);
            date = dm.GetDate(year);
            expected = new DateTime(year, 1, 31);
            Assert.Equal(expected, date);
        }

        [Fact]
        public void ToStringTest()
        {
            var exampleLeapYear = 2000;

            var dm = new DayMonth(1, 2);
            var expected = dm.GetDate(exampleLeapYear).ToString("dd/MM");
            var actual = dm.ToString();
            Assert.Equal(expected, actual);

            dm = new DayMonth(29, 2);
            expected = dm.GetDate(exampleLeapYear).ToString("dd/MM");
            actual = dm.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CompareToShouldSuccessWithValidArguments()
        {
            var dm1 = new DayMonth(1, 1);
            var dm2 = new DayMonth(1, 2);
            var dm3 = new DayMonth(1, 2);

            Assert.True(dm1.CompareTo(dm2) < 0);
            Assert.Equal(0, dm3.CompareTo(dm2));
            Assert.Equal(0, dm2.CompareTo(dm3));
            Assert.True(dm2.CompareTo(dm1) > 0);
        }

        [Fact]
        public void EqualsTest()
        {
            var dm1 = new DayMonth(1, 2);
            var dm2 = new DayMonth(2, 2);
            var actual = dm1.Equals(dm2);
            Assert.False(actual);

            dm1 = new DayMonth(1, 2);
            dm2 = new DayMonth(1, 2);
            actual = dm1.Equals(dm2);
            Assert.True(actual);

            actual = dm1.Equals(null);
            Assert.False(actual);

            actual = dm1.Equals(0);
            Assert.False(actual);
        }

        [Fact]
        public void OperatorsTest()
        {
            var dm1 = new DayMonth(1, 1);
            var dm2 = new DayMonth(1, 2);
            var dm3 = new DayMonth(1, 2);
            var dm4 = new DayMonth(2, 1);

            var dt1 = new DateTime(2000, 1, 1);
            var dt2 = new DateTime(2000, 2, 1);
            var dt3 = new DateTime(2000, 2, 1);

            Assert.False(dm1 == dm4);
            Assert.True(++dm1 == dm4);
            Assert.True(dm1 == dm4);
            Assert.True(dm1-- == dm4);
            Assert.False(dm1 == dm4);

            Assert.True(dm1.Equals(dt1));
            Assert.False(dm1.Equals(dt2));
            Assert.True(dm1.CompareTo(dt1) == 0);
            Assert.True(dm2.CompareTo(dt1) > 0);
            Assert.True(dm1.Equals(dt1));
            Assert.True(dm1.Equals((object)dm1));

            Assert.True(dm1 != dm2);
            Assert.True(dm2 != dm1);
            Assert.True(dm3 == dm2);
            Assert.True(dm2 == dm3);
            Assert.False(dm2 < dm3);
            Assert.False(dm2 > dm3);
            Assert.True(dm1 < dm2);
            Assert.True(dm2 > dm1);
            Assert.True(dm1 <= dm2);
            Assert.True(dm2 >= dm1);
            Assert.True(dm3 <= dm2);
            Assert.True(dm2 <= dm3);
            Assert.True(dm3 >= dm2);
            Assert.True(dm2 >= dm3);

            Assert.True(dm1 < dt2);
            Assert.True(dm2 > dt1);
            Assert.True(dm1 <= dt2);
            Assert.True(dm2 >= dt1);
            Assert.True(dm3 <= dt2);
            Assert.True(dm2 <= dt3);
            Assert.True(dm3 >= dt2);
            Assert.True(dm2 >= dt3);
            Assert.True(dt1 < dm2);
            Assert.True(dt2 > dm1);
            Assert.True(dt1 <= dm2);
            Assert.True(dt2 >= dm1);
            Assert.True(dt3 <= dm2);
            Assert.True(dt2 <= dm3);
            Assert.True(dt3 >= dm2);
            Assert.True(dt2 >= dm3);

            Assert.True(dm1 == dt1);
            Assert.False(dm1 < dt1);
            Assert.False(dm1 > dt1);
            Assert.True(dt1 == dm1);
            Assert.False(dt1 < dm1);
            Assert.False(dt1 > dm1);
            Assert.True(dm1 != dt2);
            Assert.True(dt2 != dm1);
        }

        [Fact]
        public void GetHashCode_Should_BeUniqueForEachDay()
        {
            var dm = new DayMonth(1, 1);
            var hs = new HashSet<int>();
            for (int i = 0; i < 366; i++)
            {
                Assert.DoesNotContain(dm.GetHashCode(), hs);
                hs.Add(dm.GetHashCode());
                dm++;
            }
        }
    }
}
