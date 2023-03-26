using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Services.Helper.Extensions
{
    public static class DateTimeExtension
    {
        public static long ToTimeStamp (this DateTime dateTime)
        {
            long epochTicks = new DateTime(2000, 1, 1).Ticks;
            long unixTime = ((dateTime.Ticks - epochTicks) / TimeSpan.TicksPerMillisecond);
            return unixTime;
        }
    }
}
