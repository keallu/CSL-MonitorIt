using ColossalFramework.UI;
using System;
using UnityEngine;

namespace MonitorIt
{
    public class ModProperties
    {
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
                int modCollectionButtonPosition = 1;

                ModConfig.Instance.ButtonPositionX = 10f;
                ModConfig.Instance.ButtonPositionY = UIView.GetAView().GetScreenResolution().y * 0.875f - (modCollectionButtonPosition * 36f) - 5f;
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
                ModConfig.Instance.PanelPositionX = 10f;
                ModConfig.Instance.PanelPositionY = UIView.GetAView().GetScreenResolution().y / 6f;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModProperties:ResetPanelPosition -> Exception: " + e.Message);
            }
        }
    }
}
