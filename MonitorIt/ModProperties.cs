using ColossalFramework.UI;

namespace MonitorIt
{
    public class ModProperties
    {
        public UITextureAtlas Atlas;
        public bool IsOptionsPanelNonModal;

        private static ModProperties instance;

        public static ModProperties Instance
        {
            get
            {
                return instance ?? (instance = new ModProperties());
            }
        }
    }
}