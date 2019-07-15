namespace MonitorIt
{
    [ConfigurationPath("MonitorItConfig.xml")]
    public class ModConfig
    {
        public bool ConfigUpdated { get; set; }
        public bool ShowButton { get; set; } = true;
        public float ButtonPositionX { get; set; } = 0f;
        public float ButtonPositionY { get; set; } = 0f;
        public bool ShowPanel { get; set; } = true;
        public float PanelPositionX { get; set; } = 0f;
        public float PanelPositionY { get; set; } = 0f;
        public float RefreshInterval { get; set; } = 1f;
        public float ResetInterval { get; set; } = 300f;
        public bool ShowFrameRatePanel { get; set; } = true;
        public bool ShowGameMemoryPanel { get; set; } = false;
        public bool ShowUnityMemoryPanel { get; set; } = true;
        public bool ShowMonoMemoryPanel { get; set; } = false;
        public bool UseFrameRateSmoothing { get; set; } = true;

        private static ModConfig instance;

        public static ModConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Configuration<ModConfig>.Load();
                }

                return instance;
            }
        }

        public void Save()
        {
            Configuration<ModConfig>.Save();
            ConfigUpdated = true;
        }
    }
}