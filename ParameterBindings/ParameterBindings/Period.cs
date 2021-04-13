using Microsoft.AspNetCore.Mvc;
using System;

namespace ParameterBindings
{
    //[ModelBinder(BinderType = typeof(PeriodBinder))]
    public readonly struct Period
    {
        internal int periodData { get; }

        public Period(int periodAsNumber) =>
            periodData = periodAsNumber;

        public Period(int year, int month)
            : this(year * 100 + month)
        {
        }

        public Period(DateTime date)
            : this(date.Year, date.Month)
        {
        }

        public int Year
        {
            get { return periodData / 100; }
        }

        public int Month
        {
            get { return periodData - (periodData - periodData % 100); }
        }

        public DateTime StartDate
        {
            get { return new DateTime(Year, Month, 1); }
        }

        public DateTime EndDate
        {
            get { return StartDate.AddMonths(1).AddDays(-1); }
        }

        public int IntStartDate
        {
            get { return periodData * 100 + 1; }
        }

        public int IntEndDate
        {
            get { return periodData * 100 + EndDate.Day; }
        }

        /// <summary>
        /// Returns a new Period that adds the specified number of years to the value of this instance.
        /// </summary>
        /// <param name="years">
        /// A number of years. The value parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose value is the sum of the period represented by this instance and the number of years.
        /// </returns>
        public Period AddYears(int years) =>
            new Period(periodData + years * 100);

        /// <summary>
        /// Returns a new Period that adds the specified number of months to the value of this instance.
        /// </summary>
        /// <param name="months">
        /// A number of months. The months parameter can be negative or positive.
        /// </param>
        /// <returns>
        /// An object whose value is the sum of the period represented by this instance and months.
        /// </returns>
        public Period AddMonth(int months) =>
            new Period(periodData + months / 12 * 100 + months % 12);

        public static Period UtcCurrent() => new Period(DateTime.UtcNow);

        public static implicit operator int(Period p) => p.periodData;

        public static explicit operator Period(int p) => new Period(p);

        public static bool operator >=(Period p1, Period p2) =>
            p1.periodData >= p2.periodData;

        public static bool operator <=(Period p1, Period p2) =>
            p1.periodData <= p2.periodData;

        public override string ToString() => $"{periodData:D6}";
    }
}