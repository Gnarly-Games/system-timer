using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Gnarly.Timer.Editor
{
    public class SystemTimerWindow : EditorWindow
    {
        public static void ShowWindow()
        {
            GetWindow<SystemTimerWindow>("System Timer").Show();
        }

        private void OnGUI()
        {
            GUILayout.Label(
                $"{SystemTimer.Now.ToString(CultureInfo.InvariantCulture)} - {SystemTimer.Now.DayOfWeek}");

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Days: {SystemTimer.DayOffset}", GUILayout.Width(100.0f));

            if (GUILayout.Button("+"))
            {
                SystemTimer.DayOffset++;
                SystemTimerEditor.Save();
            }

            if (GUILayout.Button("-"))
            {
                SystemTimer.DayOffset--;
                SystemTimerEditor.Save();
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUILayout.Label($"Hours: {SystemTimer.HourOffset}", GUILayout.Width(100.0f));
            if (GUILayout.Button("+"))
            {
                SystemTimer.HourOffset++;
                SystemTimerEditor.Save();
            }

            if (GUILayout.Button("-"))
            {
                SystemTimer.HourOffset--;
                SystemTimerEditor.Save();
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUILayout.Label($"Minutes: {SystemTimer.MinuteOffset}", GUILayout.Width(100.0f));
            if (GUILayout.Button("+"))
            {
                SystemTimer.MinuteOffset++;
                SystemTimerEditor.Save();
            }

            if (GUILayout.Button("-"))
            {
                SystemTimer.MinuteOffset--;
                SystemTimerEditor.Save();
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Reset"))
            {
                SystemTimer.DayOffset = 0;
                SystemTimer.HourOffset = 0;
                SystemTimer.MinuteOffset = 0;

                SystemTimerEditor.Save();
            }
        }
    }
}