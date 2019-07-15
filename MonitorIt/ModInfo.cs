using Harmony;
using ICities;
using System;
using System.Reflection;

namespace MonitorIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Monitor It!";
        public string Description => "Allows to monitor system resources and performance.";

        public void OnEnabled()
        {
            var harmony = HarmonyInstance.Create("com.github.keallu.csl.monitorit");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnDisabled()
        {
            var harmony = HarmonyInstance.Create("com.github.keallu.csl.monitorit");
            harmony.UnpatchAll();
        }

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

            selected = ModConfig.Instance.ShowPanel;
            group.AddCheckbox("Show Panel", selected, sel =>
            {
                ModConfig.Instance.ShowPanel = sel;
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
    }
}