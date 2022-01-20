using ColossalFramework.UI;
using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace MonitorIt
{
    class UnityMemoryPanel : UIPanel
    {
        private float _deltaTime;

        private float _totalAllocated;
        private float _totalReserved;
        private float _totalUnused;

        private UILabel _totalAllocatedDesc;
        private UILabel _totalReservedDesc;
        private UILabel _totalUnusedDesc;
        private UILabel _totalAllocatedValue;
        private UILabel _totalReservedValue;
        private UILabel _totalUnusedValue;
        private UILabel _totalAllocatedText;
        private UILabel _totalReservedText;
        private UILabel _totalUnusedText;

        public override void Start()
        {
            base.Start();

            try
            {
                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] UnityMemoryPanel:Start -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                if (_totalUnusedText != null)
                {
                    Destroy(_totalUnusedText.gameObject);
                }
                if (_totalReservedText != null)
                {
                    Destroy(_totalReservedText.gameObject);
                }
                if (_totalAllocatedText != null)
                {
                    Destroy(_totalAllocatedText.gameObject);
                }
                if (_totalUnusedValue != null)
                {
                    Destroy(_totalUnusedValue.gameObject);
                }
                if (_totalReservedValue != null)
                {
                    Destroy(_totalReservedValue.gameObject);
                }
                if (_totalAllocatedValue != null)
                {
                    Destroy(_totalAllocatedValue.gameObject);
                }
                if (_totalUnusedDesc != null)
                {
                    Destroy(_totalUnusedDesc.gameObject);
                }
                if (_totalReservedDesc != null)
                {
                    Destroy(_totalReservedDesc.gameObject);
                }
                if (_totalAllocatedDesc != null)
                {
                    Destroy(_totalAllocatedDesc.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] UnityMemoryPanel:OnDestroy -> Exception: " + e.Message);
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
                Debug.Log("[Monitor It!] UnityMemoryPanel:Update -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                name = "MonitorItUnityMemoryPanel";
                clipChildren = true;

                if (parent != UIView.GetAView())
                {
                    size = new Vector2(parent.width, 85f);
                }
                else
                {
                    size = new Vector2(250f, 85f);
                }

                _totalAllocatedDesc = UIUtils.CreateLabel(this, "TotalAllocatedDesc", "allocated");
                _totalAllocatedDesc.tooltip = "The amount of allocated memory by Unity";
                _totalAllocatedDesc.textScale = 0.7f;
                _totalAllocatedDesc.relativePosition = new Vector3(15f, 15f);

                _totalReservedDesc = UIUtils.CreateLabel(this, "TotalReservedDesc", "reserved");
                _totalReservedDesc.tooltip = "The amount of reserved memory by Unity";
                _totalReservedDesc.textScale = 0.7f;
                _totalReservedDesc.relativePosition = new Vector3(15f, 35f);

                _totalUnusedDesc = UIUtils.CreateLabel(this, "TotalUnusedDesc", "unused");
                _totalUnusedDesc.tooltip = "The amount of unused memory by Unity";
                _totalUnusedDesc.textScale = 0.7f;
                _totalUnusedDesc.relativePosition = new Vector3(15f, 55f);

                _totalAllocatedValue = UIUtils.CreateLabel(this, "TotalAllocatedValue", "0");
                _totalAllocatedValue.autoSize = false;
                _totalAllocatedValue.width = width / 2f - 45f;
                _totalAllocatedValue.textAlignment = UIHorizontalAlignment.Right;
                _totalAllocatedValue.textScale = 0.7f;
                _totalAllocatedValue.relativePosition = new Vector3(width / 2f, 15f);

                _totalReservedValue = UIUtils.CreateLabel(this, "TotalReservedValue", "0");
                _totalReservedValue.autoSize = false;
                _totalReservedValue.width = width / 2f - 45f;
                _totalReservedValue.textAlignment = UIHorizontalAlignment.Right;
                _totalReservedValue.textScale = 0.7f;
                _totalReservedValue.relativePosition = new Vector3(width / 2f, 35f);

                _totalUnusedValue = UIUtils.CreateLabel(this, "TotalUnusedValue", "0");
                _totalUnusedValue.autoSize = false;
                _totalUnusedValue.width = width / 2f - 45f;
                _totalUnusedValue.textAlignment = UIHorizontalAlignment.Right;
                _totalUnusedValue.textScale = 0.7f;
                _totalUnusedValue.relativePosition = new Vector3(width / 2f, 55f);

                _totalAllocatedText = UIUtils.CreateLabel(this, "TotalAllocatedText", "MB");
                _totalAllocatedText.textScale = 0.7f;
                _totalAllocatedText.relativePosition = new Vector3(width - 40f, 15f);

                _totalReservedText = UIUtils.CreateLabel(this, "TotalReservedText", "MB");
                _totalReservedText.textScale = 0.7f;
                _totalReservedText.relativePosition = new Vector3(width - 40f, 35f);

                _totalUnusedText = UIUtils.CreateLabel(this, "TotalUnusedText", "MB");
                _totalUnusedText.textScale = 0.7f;
                _totalUnusedText.relativePosition = new Vector3(width - 40f, 55f);
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] UnityMemoryPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshData()
        {
            try
            {
                _totalAllocated = (float)Profiler.GetTotalAllocatedMemoryLong() / 1048576;
                _totalReserved = (float)Profiler.GetTotalReservedMemoryLong() / 1048576;
                _totalUnused = (float)Profiler.GetTotalUnusedReservedMemoryLong() / 1048576;

                _totalAllocatedValue.text = $"{_totalAllocated:0.00}";
                _totalReservedValue.text = $"{_totalReserved:0.00}";
                _totalUnusedValue.text = $"{_totalUnused:0.00}";
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] UnityMemoryPanel:RefreshData -> Exception: " + e.Message);
            }
        }
    }
}
