using System;
using System.Globalization;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Gnarly.Timer.Editor
{
    [InitializeOnLoad]
    public static class SystemTimerToolbar
    {
        private const string TimerPath = "Gnarly/System Timer";
        private static double _lastUpdate;

        static SystemTimerToolbar()
        {
            EditorApplication.update += OnUpdate;
        }

        private static void OnUpdate()
        {
            // Refresh the UI every second to keep the clock accurate
            if (EditorApplication.timeSinceStartup - _lastUpdate > 1.0f)
            {
                _lastUpdate = EditorApplication.timeSinceStartup;
                MainToolbar.Refresh(TimerPath); 
            }
        }

        [MainToolbarElement(TimerPath, defaultDockPosition = MainToolbarDockPosition.Left)]
        public static MainToolbarElement TimerButton()
        {
            var timeString = $"{SystemTimer.Now:MM/dd/yyyy HH:mm}";
            var tooltip = $"Current simulated time. {SystemTimer.Now.DayOfWeek}";
            
            // Using a clock icon, or fallback to simple text if icon missing
            var icon = EditorGUIUtility.IconContent("d_TimelineAsset Icon").image as Texture2D;
            
            var content = new MainToolbarContent(timeString, icon, tooltip);

            return new MainToolbarButton(content, OnTimerClicked);
        }

        private static void OnTimerClicked()
        {
            SystemTimerWindow.ShowWindow();
        }
    }
    
     public class SystemTimerWindow : EditorWindow
    {
        public static void ShowWindow()
        {
            var window = GetWindow<SystemTimerWindow>("System Timer");
            window.minSize = new Vector2(300, 180);
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label($"{SystemTimer.Now.ToString(CultureInfo.InvariantCulture)}", EditorStyles.boldLabel);
                GUILayout.Label($"{SystemTimer.Now.DayOfWeek}", EditorStyles.miniLabel);
            }

            GUILayout.Space(10);

            DrawOffsetControl("Days", SystemTimer.DayOffset, v => SystemTimer.DayOffset = v);
            DrawOffsetControl("Hours", SystemTimer.HourOffset, v => SystemTimer.HourOffset = v);
            DrawOffsetControl("Minutes", SystemTimer.MinuteOffset, v => SystemTimer.MinuteOffset = v);

            GUILayout.Space(20);

            if (GUILayout.Button("Reset Timer", GUILayout.Height(30)))
            {
                SystemTimer.DayOffset = 0;
                SystemTimer.HourOffset = 0;
                SystemTimer.MinuteOffset = 0;
                MainToolbar.Refresh("Gnarly/System Timer");
            }
        }

        private void DrawOffsetControl(string label, int value, Action<int> onValueChanged)
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label(label, GUILayout.Width(60));
                GUILayout.Label(value.ToString(), EditorStyles.boldLabel, GUILayout.Width(40));

                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    onValueChanged(value - 1);
                    MainToolbar.Refresh("Gnarly/System Timer");
                }

                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    onValueChanged(value + 1);
                    MainToolbar.Refresh("Gnarly/System Timer");
                }
            }
        }
        
        // Ensure the window repaints to show ticking time if open
        private void OnInspectorUpdate()
        {
            Repaint();
        }
    }
}