using ColossalFramework.UI;
using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace MonitorIt
{
    class MonoMemoryPanel : UIPanel
    {
        private float _deltaTime;

        private float _totalReserved;
        private float _totalUsed;

        private UILabel _totalReservedDesc;
        private UILabel _totalUsedDesc;
        private UILabel _totalReservedValue;
        private UILabel _totalUsedValue;
        private UILabel _totalReservedText;
        private UILabel _totalUsedText;

        public override void Start()
        {
            base.Start();

            try
            {
                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] MonoMemoryPanel:Start -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                if (_totalUsedText != null)
                {
                    Destroy(_totalUsedText);
                }
                if (_totalReservedText != null)
                {
                    Destroy(_totalReservedText);
                }
                if (_totalUsedValue != null)
                {
                    Destroy(_totalUsedValue);
                }
                if (_totalReservedValue != null)
                {
                    Destroy(_totalReservedValue);
                }
                if (_totalUsedDesc != null)
                {
                    Destroy(_totalUsedDesc);
                }
                if (_totalReservedDesc != null)
                {
                    Destroy(_totalReservedDesc);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] MonoMemoryPanel:OnDestroy -> Exception: " + e.Message);
            }
        }

        public override void Update()
        {
            base.Update();

            try
            {
                if (isVisible)
                {
                    _deltaTime += ModConfig.Instance.UseFrameRateSmoothing ? Time.smoothDeltaTime : Time.deltaTime;

                    if (_deltaTime > ModConfig.Instance.RefreshInterval)
                    {
                        RefreshData();

                        _deltaTime = 0f;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] MonoMemoryPanel:Update -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                name = "MonitorItMonoMemoryPanel";
                atlas = ModProperties.Instance.Atlas;
                backgroundSprite = "boxblue";
                clipChildren = true;

                if (parent != UIView.GetAView())
                {
                    size = new Vector2(parent.width, 65f);
                }
                else
                {
                    size = new Vector2(250f, 65f);
                }

                _totalReservedDesc = UIUtils.CreateLabel(this, "TotalReservedDesc", "reserved");
                _totalReservedDesc.tooltip = "The amount of reserved memory by Mono";
                _totalReservedDesc.textScale = 1f;
                _totalReservedDesc.relativePosition = new Vector3(15f, 15f);

                _totalUsedDesc = UIUtils.CreateLabel(this, "TotalUsedDesc", "used");
                _totalUsedDesc.tooltip = "The amount of used memory by Mono";
                _totalUsedDesc.textScale = 1f;
                _totalUsedDesc.relativePosition = new Vector3(15f, 35f);

                _totalReservedValue = UIUtils.CreateLabel(this, "TotalReservedValue", "0");
                _totalReservedValue.autoSize = false;
                _totalReservedValue.width = width / 2f - 45f;
                _totalReservedValue.textAlignment = UIHorizontalAlignment.Right;
                _totalReservedValue.textScale = 1f;
                _totalReservedValue.relativePosition = new Vector3(width / 2f, 15f);

                _totalUsedValue = UIUtils.CreateLabel(this, "TotalUsedValue", "0");
                _totalUsedValue.autoSize = false;
                _totalUsedValue.width = width / 2f - 45f;
                _totalUsedValue.textAlignment = UIHorizontalAlignment.Right;
                _totalUsedValue.textScale = 1f;
                _totalUsedValue.relativePosition = new Vector3(width / 2f, 35f);

                _totalReservedText = UIUtils.CreateLabel(this, "TotalReservedText", "MB");
                _totalReservedText.textScale = 1f;
                _totalReservedText.relativePosition = new Vector3(width - 40f, 15f);

                _totalUsedText = UIUtils.CreateLabel(this, "TotalUsedText", "MB");
                _totalUsedText.textScale = 1f;
                _totalUsedText.relativePosition = new Vector3(width - 40f, 35f);

            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] MonoMemoryPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshData()
        {
            try
            {
                _totalReserved = (float)Profiler.GetMonoHeapSizeLong() / 1048576;
                _totalUsed = (float)Profiler.GetMonoUsedSizeLong() / 1048576;

                _totalReservedValue.text = $"{_totalReserved:0.##}";
                _totalUsedValue.text = $"{_totalUsed:0.##}";
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] MonoMemoryPanel:RefreshData -> Exception: " + e.Message);
            }
        }
    }
}
