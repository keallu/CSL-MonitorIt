using System;
using UnityEngine;

namespace MonitorIt
{
    public class ModProperties
    {
        public float ButtonDefaultPositionX;
        public float ButtonDefaultPositionY; 
        public float PanelDefaultPositionX;
        public float PanelDefaultPositionY;

        private static ModProperties instance;

        public static ModProperties Instance
        {
            get
            {
                return instance ?? (instance = new ModProperties());
            }
        }

        public void ResetButtonPosition()
        {
            try
            {
                ModConfig.Instance.ButtonPositionX = ButtonDefaultPositionX;
                ModConfig.Instance.ButtonPositionY = ButtonDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModProperties:ResetButtonPosition -> Exception: " + e.Message);
            }
        }

        public void ResetPanelPosition()
        {
            try
            {
                ModConfig.Instance.PanelPositionX = PanelDefaultPositionX;
                ModConfig.Instance.PanelPositionY = PanelDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModProperties:ResetPanelPosition -> Exception: " + e.Message);
            }
        }
    }
}
