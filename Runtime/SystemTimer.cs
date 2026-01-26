using System;
using UnityEditor;

namespace Gnarly.Timer
{
    public static class SystemTimer
    {
#if UNITY_EDITOR
        private const string DayOffsetKey = "SystemTimer.DayOffset";
        private const string HourOffsetKey = "SystemTimer.HourOffset";
        private const string MinuteOffsetKey = "SystemTimer.MinuteOffset";

        private static int _dayOffset;
        private static int _hourOffset;
        private static int _minuteOffset;

        public static int DayOffset
        {
            get => _dayOffset;
            set { _dayOffset = value; Save(); }
        }

        public static int HourOffset
        {
            get => _hourOffset;
            set { _hourOffset = value; Save(); }
        }

        public static int MinuteOffset
        {
            get => _minuteOffset;
            set { _minuteOffset = value; Save(); }
        }

        public static DateTime Now => DateTime.Now
            .AddDays(_dayOffset)
            .AddHours(_hourOffset)
            .AddMinutes(_minuteOffset);

        static SystemTimer()
        {
            Load();
        }
        
        private static void Load()
        {
            _dayOffset = EditorPrefs.GetInt(DayOffsetKey, 0);
            _hourOffset = EditorPrefs.GetInt(HourOffsetKey, 0);
            _minuteOffset = EditorPrefs.GetInt(MinuteOffsetKey, 0);
        }

        private static void Save()
        {
            EditorPrefs.SetInt(DayOffsetKey, _dayOffset);
            EditorPrefs.SetInt(HourOffsetKey, _hourOffset);
            EditorPrefs.SetInt(MinuteOffsetKey, _minuteOffset);
        }

#else
        public static DateTime Now => DateTime.Now;
#endif
    }
}