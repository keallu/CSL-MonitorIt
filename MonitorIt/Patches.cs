using ColossalFramework.UI;
using Harmony;
using System;
using UnityEngine;

namespace MonitorIt
{
    [HarmonyPatch(typeof(OptionsMainPanel), "OnClosed")]
    public static class OptionsMainPanelOnClosedPatch
    {
        static bool Prefix()
        {
            try
            {
                if (ModProperties.Instance.IsOptionsPanelNonModal)
                {
                    UIComponent optionsPanel = UIView.library.Get("OptionsPanel");

                    if (optionsPanel != null)
                    {
                        if (optionsPanel.isVisible)
                        {
                            optionsPanel.Hide();
                            ModProperties.Instance.IsOptionsPanelNonModal = false;
                        }
                    }

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] OptionsMainPanelOnClosedPatch:Prefix -> Exception: " + e.Message);
                return true;
            }
        }
    }
}
