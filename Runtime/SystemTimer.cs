using System;

namespace _Game.Domains.Timer
{
    public static class SystemTimer
    {
#if UNITY_EDITOR
        public static int DayOffset;
        public static int HourOffset;
        public static int MinuteOffset;

        public static DateTime Now =>
            DateTime.Now.Add(
                new TimeSpan(DayOffset, HourOffset, MinuteOffset, 0, 0)
            );

#else
        public static DateTime Now => DateTime.Now;
#endif
    }
}