namespace MetaWeblog
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The <see cref="DateTime"/> extension methods class
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a <see cref="string" /> that represents the specified <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>A <see cref="string" /> that represents the specified <paramref name="dateTime"/>.</returns>
        public static string ToISO8601String(this DateTime dateTime) => dateTime.ToString("o", CultureInfo.InvariantCulture);

        /// <summary>
        /// Parses the specified <see cref="string"/> as an ISO8601 formatted <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <param name="rounding">The rounding.</param>
        /// <param name="allowTwoYear">if set to <c>true</c> [allow two year].</param>
        /// <param name="leapSecondMeansNextDay">if set to <c>true</c> [leap second means next day].</param>
        /// <returns>The parsed <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException">
        /// The date string is in an invalid format.
        /// </exception>
        /// <remarks>
        /// Slightly modified and borrowed from https://stackoverflow.com/questions/31243985/how-to-do-formally-correct-parsing-of-iso8601-date-times-in-net/31246449#31246449
        /// </remarks>
        public static DateTime ParseISO8601(this string dateString, MidpointRounding rounding = MidpointRounding.ToEven, bool allowTwoYear = false, bool leapSecondMeansNextDay = false)
        {
            var match = new Regex(@"\b(\d{4})(-W(\d{2})-|W(\d{2}))(\d)(T\S+)?\b").Match(dateString);
            if (match.Success)
            {
                var year = int.Parse(match.Groups[1].Value);
                var week = int.Parse(match.Groups[3].Value + match.Groups[4].Value);
                var day = int.Parse(match.Groups[5].Value);

                if (year < 1 || year > 9999 || week < 1 || week > 53 || day < 1 || day > 7)
                {
                    throw new FormatException();
                }

                var januaryFirst = new DateTime(year, 1, 1);

                var firstWeek = januaryFirst.DayOfWeek >= DayOfWeek.Friday
                  ? januaryFirst.AddDays(januaryFirst.DayOfWeek - DayOfWeek.Monday - 1)
                  : januaryFirst.AddDays(DayOfWeek.Monday - januaryFirst.DayOfWeek);

                var fromWeekAndDay = firstWeek.AddDays((week - 1) * 7 + day - 1);

                if (week > 51 && fromWeekAndDay > ParseISO8601(fromWeekAndDay.Year + "-W01-1"))
                {
                    throw new FormatException();
                }

                if (match.Groups[6].Success)
                {
                    // We're just going to let the handling for the other formats deal with any time fraction:
                    dateString = fromWeekAndDay.ToString("yyyy-MM-dd") + match.Groups[6].Value;
                }

                return fromWeekAndDay;
            }

            var excessiveFractions = new Regex(@"(\d(\.|,‎)\d{8,})");
            if (excessiveFractions.IsMatch(dateString))
            {
                dateString = excessiveFractions.Replace(
                  dateString,
                  m => decimal.Round(
                      decimal.Parse(
                          m.Value.Substring(0, Math.Max(m.Value.Length, 10))),
                      7,
                      rounding).ToString());
            }

            if (dateString.Contains("T24"))
            {
                var yesterday = ParseISO8601(dateString.Replace("T24", "T00"), rounding, allowTwoYear);
                if (yesterday.TimeOfDay != TimeSpan.Zero)
                {
                    throw new FormatException();
                }

                return yesterday.AddDays(1);
            }

            var leapSecond = new Regex("T23:?59:?60");
            if (leapSecond.IsMatch(dateString))
            {
                var secondBefore = ParseISO8601(leapSecond.Replace(dateString, "T23:59:59"));
                if (secondBefore.TimeOfDay != new TimeSpan(23, 59, 59)) // can't have fractions past second 60
                {
                    throw new FormatException();
                }

                // Can only be on --12-31 or --06-30
                if ((secondBefore.Month == 12 && secondBefore.Day == 31) || (secondBefore.Month == 6 && secondBefore.Day == 30))
                {
                    // since DateTime can't handle leap seconds, we need a policy as to which side of it to be on.
                    return leapSecondMeansNextDay ? secondBefore.AddSeconds(1) : secondBefore;
                }

                throw new FormatException();
            }

            var formats = allowTwoYear
              ? new[]
              {
                  "yyyy-MM-ddK",
                  "yyyyMMddK",
                  "yy-MM-ddK",
                  "yyMMddK",
                  "yyyy-MM-ddTHH:mm:ss.fffffffK",
                  "yyyyMMddTHH:mm:ss.fffffffK",
                  "yy-MM-ddTHH:mm:ss.fffffffK",
                  "yyMMddTHH:mm:ss.fffffffK",
                  "yyyy-MM-ddTHH:mm:ss,fffffffK",
                  "yyyyMMddTHH:mm:ss,fffffffK",
                  "yy-MM-ddTHH:mm:ss,fffffffK",
                  "yyMMddTHH:mm:ss,fffffffK",
                  "yyyy-MM-ddTHH:mm:ss.ffffffK",
                  "yyyyMMddTHH:mm:ss.ffffffK",
                  "yy-MM-ddTHH:mm:ss.ffffffK",
                  "yyMMddTHH:mm:ss.ffffffK",
                  "yyyy-MM-ddTHH:mm:ss,ffffffK",
                  "yyyyMMddTHH:mm:ss,ffffffK",
                  "yy-MM-ddTHH:mm:ss,ffffffK",
                  "yyMMddTHH:mm:ss,ffffffK",
                  "yyyy-MM-ddTHH:mm:ss.fffffK",
                  "yyyyMMddTHH:mm:ss.fffffK",
                  "yy-MM-ddTHH:mm:ss.fffffK",
                  "yyMMddTHH:mm:ss.fffffK",
                  "yyyy-MM-ddTHH:mm:ss,fffffK",
                  "yyyyMMddTHH:mm:ss,fffffK",
                  "yy-MM-ddTHH:mm:ss,fffffK",
                  "yyMMddTHH:mm:ss,fffffK",
                  "yyyy-MM-ddTHH:mm:ss.ffffK",
                  "yyyyMMddTHH:mm:ss.ffffK",
                  "yy-MM-ddTHH:mm:ss.ffffK",
                  "yyMMddTHH:mm:ss.ffffK",
                  "yyyy-MM-ddTHH:mm:ss,ffffK",
                  "yyyyMMddTHH:mm:ss,ffffK",
                  "yy-MM-ddTHH:mm:ss,ffffK",
                  "yyMMddTHH:mm:ss,ffffK",
                  "yyyy-MM-ddTHH:mm:ss.ffK",
                  "yyyyMMddTHH:mm:ss.ffK",
                  "yy-MM-ddTHH:mm:ss.ffK",
                  "yyMMddTHH:mm:ss.ffK",
                  "yyyy-MM-ddTHH:mm:ss,ffK",
                  "yyyyMMddTHH:mm:ss,ffK",
                  "yy-MM-ddTHH:mm:ss,ffK",
                  "yyMMddTHH:mm:ss,ffK",
                  "yyyy-MM-ddTHH:mm:ss.fK",
                  "yyyyMMddTHH:mm:ss.fK",
                  "yy-MM-ddTHH:mm:ss.fK",
                  "yyMMddTHH:mm:ss.fK",
                  "yyyy-MM-ddTHH:mm:ss,fK",
                  "yyyyMMddTHH:mm:ss,fK",
                  "yy-MM-ddTHH:mm:ss,fK",
                  "yyMMddTHH:mm:ss,fK",
                  "yyyy-MM-ddTHH:mm:ssK",
                  "yyyyMMddTHH:mm:ssK",
                  "yy-MM-ddTHH:mm:ssK",
                  "yyMMddTHH:mm:ss‎K",
                  "yyyy-MM-ddTHHmmss.fffffffK",
                  "yyyyMMddTHHmmss.fffffffK",
                  "yy-MM-ddTHHmmss.fffffffK",
                  "yyMMddTHHmmss.fffffffK",
                  "yyyy-MM-ddTHHmmss,fffffffK",
                  "yyyyMMddTHHmmss,fffffffK",
                  "yy-MM-ddTHHmmss,fffffffK",
                  "yyMMddTHHmmss,fffffffK",
                  "yyyy-MM-ddTHHmmss.ffffffK",
                  "yyyyMMddTHHmmss.ffffffK",
                  "yy-MM-ddTHHmmss.ffffffK",
                  "yyMMddTHHmmss.ffffffK",
                  "yyyy-MM-ddTHHmmss,ffffffK",
                  "yyyyMMddTHHmmss,ffffffK",
                  "yy-MM-ddTHHmmss,ffffffK",
                  "yyMMddTHHmmss,ffffffK",
                  "yyyy-MM-ddTHHmmss.fffffK",
                  "yyyyMMddTHHmmss.fffffK",
                  "yy-MM-ddTHHmmss.fffffK",
                  "yyMMddTHHmmss.fffffK",
                  "yyyy-MM-ddTHHmmss,fffffK",
                  "yyyyMMddTHHmmss,fffffK",
                  "yy-MM-ddTHHmmss,fffffK",
                  "yyMMddTHHmmss,fffffK",
                  "yyyy-MM-ddTHHmmss.ffffK",
                  "yyyyMMddTHHmmss.ffffK",
                  "yy-MM-ddTHHmmss.ffffK",
                  "yyMMddTHHmmss.ffffK",
                  "yyyy-MM-ddTHHmmss,ffffK",
                  "yyyyMMddTHHmmss,ffffK",
                  "yy-MM-ddTHHmmss,ffffK",
                  "yyMMddTHHmmss,ffffK",
                  "yyyy-MM-ddTHHmmss.ffK",
                  "yyyyMMddTHHmmss.ffK",
                  "yy-MM-ddTHHmmss.ffK",
                  "yyMMddTHHmmss.ffK",
                  "yyyy-MM-ddTHHmmss,ffK",
                  "yyyyMMddTHHmmss,ffK",
                  "yy-MM-ddTHHmmss,ffK",
                  "yyMMddTHHmmss,ffK",
                  "yyyy-MM-ddTHHmmss.fK",
                  "yyyyMMddTHHmmss.fK",
                  "yy-MM-ddTHHmmss.fK",
                  "yyMMddTHHmmss.fK",
                  "yyyy-MM-ddTHHmmss,fK",
                  "yyyyMMddTHHmmss,fK",
                  "yy-MM-ddTHHmmss,fK",
                  "yyMMddTHHmmss,fK",
                  "yyyy-MM-ddTHHmmssK",
                  "yyyyMMddTHHmmssK",
                  "yy-MM-ddTHHmmssK",
                  "yyMMddTHHmmss‎K",
                  "yyyy-MM-ddTHH:mmK",
                  "yyyyMMddTHH:mmK",
                  "yy-MM-ddTHH:mmK",
                  "yyMMddTHH:mmK",
                  "yyyy-MM-ddTHHK",
                  "yyyyMMddTHHK",
                  "yy-MM-ddTHHK",
                  "yyMMddTHHK"
              }
              : new[]
              {
                  "yyyy-MM-ddK",
                  "yyyyMMddK",
                  "yyyy-MM-ddTHH:mm:ss.fffffffK",
                  "yyyyMMddTHH:mm:ss.fffffffK",
                  "yyyy-MM-ddTHH:mm:ss,fffffffK",
                  "yyyyMMddTHH:mm:ss,fffffffK",
                  "yyyy-MM-ddTHH:mm:ss.ffffffK",
                  "yyyyMMddTHH:mm:ss.ffffffK",
                  "yyyy-MM-ddTHH:mm:ss,ffffffK",
                  "yyyyMMddTHH:mm:ss,ffffffK",
                  "yyyy-MM-ddTHH:mm:ss.fffffK",
                  "yyyyMMddTHH:mm:ss.fffffK",
                  "yyyy-MM-ddTHH:mm:ss,fffffK",
                  "yyyyMMddTHH:mm:ss,fffffK",
                  "yyyy-MM-ddTHH:mm:ss.ffffK",
                  "yyyyMMddTHH:mm:ss.ffffK",
                  "yyyy-MM-ddTHH:mm:ss,ffffK",
                  "yyyyMMddTHH:mm:ss,ffffK",
                  "yyyy-MM-ddTHH:mm:ss.ffK",
                  "yyyyMMddTHH:mm:ss.ffK",
                  "yyyy-MM-ddTHH:mm:ss,ffK",
                  "yyyyMMddTHH:mm:ss,ffK",
                  "yyyy-MM-ddTHH:mm:ss.fK",
                  "yyyyMMddTHH:mm:ss.fK",
                  "yyyy-MM-ddTHH:mm:ss,fK",
                  "yyyyMMddTHH:mm:ss,fK",
                  "yyyy-MM-ddTHH:mm:ssK",
                  "yyyyMMddTHH:mm:ssK",
                  "yyyy-MM-ddTHHmmss.fffffffK",
                  "yyyyMMddTHHmmss.fffffffK",
                  "yyyy-MM-ddTHHmmss,fffffffK",
                  "yyyyMMddTHHmmss,fffffffK",
                  "yyyy-MM-ddTHHmmss.ffffffK",
                  "yyyyMMddTHHmmss.ffffffK",
                  "yyyy-MM-ddTHHmmss,ffffffK",
                  "yyyyMMddTHHmmss,ffffffK",
                  "yyyy-MM-ddTHHmmss.fffffK",
                  "yyyyMMddTHHmmss.fffffK",
                  "yyyy-MM-ddTHHmmss,fffffK",
                  "yyyyMMddTHHmmss,fffffK",
                  "yyyy-MM-ddTHHmmss.ffffK",
                  "yyyyMMddTHHmmss.ffffK",
                  "yyyy-MM-ddTHHmmss,ffffK",
                  "yyyyMMddTHHmmss,ffffK",
                  "yyyy-MM-ddTHHmmss.ffK",
                  "yyyyMMddTHHmmss.ffK",
                  "yyyy-MM-ddTHHmmss,ffK",
                  "yyyyMMddTHHmmss,ffK",
                  "yyyy-MM-ddTHHmmss.fK",
                  "yyyyMMddTHHmmss.fK",
                  "yyyy-MM-ddTHHmmss,fK",
                  "yyyyMMddTHHmmss,fK",
                  "yyyy-MM-ddTHHmmssK",
                  "yyyyMMddTHHmmssK",
                  "yyyy-MM-ddTHH:mmK",
                  "yyyyMMddTHH:mmK",
                  "yyyy-MM-ddTHHK",
                  "yyyyMMddTHHK"
              };

            return DateTime.ParseExact(
                dateString,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
        }
    }
}
