using System;
using Xunit;

namespace SystemExtensions.Tests
{
    public class DateTimeExtensionsTest
    {
        [Fact]
        public void FirstDayOfMonthTest()
        {
            var date = new DateTime(2000, 1, 31, 23, 59, 59, 999);
            var expected = new DateTime(2000, 1, 1);
            var actual = date.FirstDayOfMonth();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LastDayOfMonthTest()
        {
            var date = new DateTime(2000, 2, 1);
            var expected = new DateTime(2000, 2, 29);
            var actual = date.LastDayOfMonth();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsWeekendTest()
        {
            var sunday = new DateTime(2012, 4, 1);
            Assert.Equal(DayOfWeek.Sunday, sunday.DayOfWeek);
            Assert.True(sunday.IsWeekend());

            var monday = sunday.AddDays(1);
            Assert.Equal(DayOfWeek.Monday, monday.DayOfWeek);
            Assert.False(monday.IsWeekend());

            var tuesday = sunday.AddDays(2);
            Assert.Equal(DayOfWeek.Tuesday, tuesday.DayOfWeek);
            Assert.False(tuesday.IsWeekend());

            var wednesday = sunday.AddDays(3);
            Assert.Equal(DayOfWeek.Wednesday, wednesday.DayOfWeek);
            Assert.False(wednesday.IsWeekend());

            var thursday = sunday.AddDays(4);
            Assert.Equal(DayOfWeek.Thursday, thursday.DayOfWeek);
            Assert.False(thursday.IsWeekend());

            var friday = sunday.AddDays(5);
            Assert.Equal(DayOfWeek.Friday, friday.DayOfWeek);
            Assert.False(friday.IsWeekend());

            var saturday = sunday.AddDays(6);
            Assert.Equal(DayOfWeek.Saturday, saturday.DayOfWeek);
            Assert.True(saturday.IsWeekend());
        }

        [Fact]
        public void ToMonthYearTest()
        {
            var date = new DateTime(2000, 1, 14);
            var expected = new MonthYear(1, 2000);
            var actual = date.ToMonthYear();
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Year, actual.Year);
        }

        [Fact]
        public void ToDayMonthTest()
        {
            var date = new DateTime(2000, 1, 14);
            var expected = new DayMonth(14, 1);
            var actual = date.ToDayMonth();
            Assert.Equal(expected.Day, actual.Day);
            Assert.Equal(expected.Month, actual.Month);
        }
    }
}
