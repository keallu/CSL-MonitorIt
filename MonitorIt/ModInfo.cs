using ICities;
using System;
using System.Reflection;

namespace MonitorIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Monitor It!";
        public string Description => "Allows to monitor system resources and performance.";

        private static readonly string[] PanelColorLabels =
        {
            "Red",
            "Green",
            "Blue"
        };

        private static readonly string[] PanelColorValues =
        {
            "red",
            "green",
            "blue"
        };

        private static readonly string[] RefreshIntervalLabels =
        {
            "Every 1 seconds",
            "Every 2 seconds",
            "Every 3 seconds",
            "Every 4 seconds",
            "Every 5 seconds",
            "Every 10 seconds",
            "Every 30 seconds"
        };

        private static readonly float[] RefreshIntervalValues =
        {
            1f,
            2f,
            3f,
            4f,
            5f,
            10f,
            30f
        };

        private static readonly string[] ResetIntervalLabels =
        {
            "Every 1 minutes",
            "Every 2 minutes",
            "Every 3 minutes",
            "Every 4 minutes",
            "Every 5 minutes",
            "Every 10 minutes",
            "Every 30 minutes"
        };

        private static readonly float[] ResetIntervalValues =
        {
            60f,
            120f,
            180f,
            240f,
            300f,
            600f,
            1800f
        };

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;
            bool selected;
            int selectedIndex;

            group = helper.AddGroup(Name);

            selected = ModConfig.Instance.ShowButton;
            group.AddCheckbox("Show Button", selected, sel =>
            {
                ModConfig.Instance.ShowButton = sel;
                ModConfig.Instance.Save();
            });

            selectedIndex = GetSelectedOptionIndex(PanelColorValues, ModConfig.Instance.PanelColor);
            group.AddDropdown("Panel Color", PanelColorLabels, selectedIndex, sel =>
            {
                ModConfig.Instance.PanelColor = PanelColorValues[sel];
                ModConfig.Instance.Save();
            });

            selectedIndex = GetSelectedOptionIndex(RefreshIntervalValues, ModConfig.Instance.RefreshInterval);
            group.AddDropdown("Refresh Interval", RefreshIntervalLabels, selectedIndex, sel =>
            {
                ModConfig.Instance.RefreshInterval = RefreshIntervalValues[sel];
                ModConfig.Instance.Save();
            });

            selectedIndex = GetSelectedOptionIndex(ResetIntervalValues, ModConfig.Instance.ResetInterval);
            group.AddDropdown("Reset Interval", ResetIntervalLabels, selectedIndex, sel =>
            {
                ModConfig.Instance.ResetInterval = ResetIntervalValues[sel];
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Monitoring");

            selected = ModConfig.Instance.ShowTimePanel;
            group.AddCheckbox("Time", selected, sel =>
            {
                ModConfig.Instance.ShowTimePanel = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowFrameRatePanel;
            group.AddCheckbox("Frame Rate", selected, sel =>
            {
                ModConfig.Instance.ShowFrameRatePanel = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowGameMemoryPanel;
            group.AddCheckbox("Game Memory (only available for Windows systems)", selected, sel =>
            {
                ModConfig.Instance.ShowGameMemoryPanel = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowUnityMemoryPanel;
            group.AddCheckbox("Unity Memory", selected, sel =>
            {
                ModConfig.Instance.ShowUnityMemoryPanel = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowMonoMemoryPanel;
            group.AddCheckbox("Mono Memory", selected, sel =>
            {
                ModConfig.Instance.ShowMonoMemoryPanel = sel;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Advanced");

            selected = ModConfig.Instance.UseFrameRateSmoothing;
            group.AddCheckbox("Use Frame Rate Smoothing", selected, sel =>
            {
                ModConfig.Instance.UseFrameRateSmoothing = sel;
                ModConfig.Instance.Save();
            });
        }

        private int GetSelectedOptionIndex(float[] option, float value)
        {
            int index = Array.IndexOf(option, value);
            if (index < 0) index = 0;

            return index;
        }

        private int GetSelectedOptionIndex(string[] option, string value)
        {
            int index = Array.IndexOf(option, value);
            if (index < 0) index = 0;

            return index;
        }
    }
}