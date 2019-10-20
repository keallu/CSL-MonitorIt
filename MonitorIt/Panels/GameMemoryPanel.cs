using ColossalFramework.UI;
using System;
using UnityEngine;

namespace MonitorIt
{
    class GameMemoryPanel : UIPanel
    {
        private float _deltaTime;

        private float _totalUsedPhysical;
        private float _totalUsedVirtual;
        private float _totalUsedPagedPool;
        private float _totalUsedNonPagedPool;

        private UILabel _totalUsedPhysicalDesc;
        private UILabel _totalUsedVirtualDesc;
        private UILabel _totalUsedPagedPoolDesc;
        private UILabel _totalUsedNonPagedPoolDesc;
        private UILabel _totalUsedPhysicalValue;
        private UILabel _totalUsedVirtualValue;
        private UILabel _totalUsedPagedPoolValue;
        private UILabel _totalUsedNonPagedPoolValue;
        private UILabel _totalUsedPhysicalText;
        private UILabel _totalUsedVirtualText;
        private UILabel _totalUsedPagedPoolText;
        private UILabel _totalUsedNonPagedPoolText;

        public override void Start()
        {
            base.Start();

            try
            {
                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameMemoryPanel:Start -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                if (_totalUsedNonPagedPoolText != null)
                {
                    Destroy(_totalUsedNonPagedPoolText);
                }
                if (_totalUsedPagedPoolText != null)
                {
                    Destroy(_totalUsedPagedPoolText);
                }
                if (_totalUsedVirtualText != null)
                {
                    Destroy(_totalUsedVirtualText);
                }
                if (_totalUsedPhysicalText != null)
                {
                    Destroy(_totalUsedPhysicalText);
                }
                if (_totalUsedNonPagedPoolValue != null)
                {
                    Destroy(_totalUsedNonPagedPoolValue);
                }
                if (_totalUsedPagedPoolValue != null)
                {
                    Destroy(_totalUsedPagedPoolValue);
                }
                if (_totalUsedVirtualValue != null)
                {
                    Destroy(_totalUsedVirtualValue);
                }
                if (_totalUsedPhysicalValue != null)
                {
                    Destroy(_totalUsedPhysicalValue);
                }
                if (_totalUsedNonPagedPoolDesc != null)
                {
                    Destroy(_totalUsedNonPagedPoolDesc);
                }
                if (_totalUsedPagedPoolDesc != null)
                {
                    Destroy(_totalUsedPagedPoolDesc);
                }
                if (_totalUsedVirtualDesc != null)
                {
                    Destroy(_totalUsedVirtualDesc);
                }
                if (_totalUsedPhysicalDesc != null)
                {
                    Destroy(_totalUsedPhysicalDesc);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameMemoryPanel:OnDestroy -> Exception: " + e.Message);
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
                Debug.Log("[Monitor It!] GameMemoryPanel:Update -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                name = "MonitorItGameMemoryPanel";
                clipChildren = true;
                
                if (parent != UIView.GetAView())
                {
                    size = new Vector2(parent.width, 105f);
                }
                else
                {
                    size = new Vector2(250f, 105f);
                }

                _totalUsedPhysicalDesc = UIUtils.CreateLabel(this, "TotalUsedPhysicalDesc", "used physical");
                _totalUsedPhysicalDesc.tooltip = "The amount of used physical memory by game";
                _totalUsedPhysicalDesc.textScale = 0.6f;
                _totalUsedPhysicalDesc.relativePosition = new Vector3(15f, 15f);

                _totalUsedVirtualDesc = UIUtils.CreateLabel(this, "TotalUsedVirtualDesc", "used virtual");
                _totalUsedVirtualDesc.tooltip = "The amount of used virtual memory by game";
                _totalUsedVirtualDesc.textScale = 0.6f;
                _totalUsedVirtualDesc.relativePosition = new Vector3(15f, 35f);

                _totalUsedPagedPoolDesc = UIUtils.CreateLabel(this, "TotalUsedPagedPoolDesc", "used paged");
                _totalUsedPagedPoolDesc.tooltip = "The amount of used paged pool by game";
                _totalUsedPagedPoolDesc.textScale = 0.6f;
                _totalUsedPagedPoolDesc.relativePosition = new Vector3(15f, 55f);

                _totalUsedNonPagedPoolDesc = UIUtils.CreateLabel(this, "TotalUsedNonPagedPoolDesc", "used non-paged");
                _totalUsedNonPagedPoolDesc.tooltip = "The amount of used non-paged pool by game";
                _totalUsedNonPagedPoolDesc.textScale = 0.6f;
                _totalUsedNonPagedPoolDesc.relativePosition = new Vector3(15f, 75f);

                _totalUsedPhysicalValue = UIUtils.CreateLabel(this, "TotalUsedPhysicalValue", "0");
                _totalUsedPhysicalValue.autoSize = false;
                _totalUsedPhysicalValue.width = width / 2f - 45f;
                _totalUsedPhysicalValue.textAlignment = UIHorizontalAlignment.Right;
                _totalUsedPhysicalValue.textScale = 1f;
                _totalUsedPhysicalValue.relativePosition = new Vector3(width / 2f, 15f);

                _totalUsedVirtualValue = UIUtils.CreateLabel(this, "TotalUsedVirtualValue", "0");
                _totalUsedVirtualValue.autoSize = false;
                _totalUsedVirtualValue.width = width / 2f - 45f;
                _totalUsedVirtualValue.textAlignment = UIHorizontalAlignment.Right;
                _totalUsedVirtualValue.textScale = 1f;
                _totalUsedVirtualValue.relativePosition = new Vector3(width / 2f, 35f);

                _totalUsedPagedPoolValue = UIUtils.CreateLabel(this, "TotalUsedPagedPoolValue", "0");
                _totalUsedPagedPoolValue.autoSize = false;
                _totalUsedPagedPoolValue.width = width / 2f - 45f;
                _totalUsedPagedPoolValue.textAlignment = UIHorizontalAlignment.Right;
                _totalUsedPagedPoolValue.textScale = 1f;
                _totalUsedPagedPoolValue.relativePosition = new Vector3(width / 2f, 55f);

                _totalUsedNonPagedPoolValue = UIUtils.CreateLabel(this, "TotalUsedNonPagedPoolValue", "0");
                _totalUsedNonPagedPoolValue.autoSize = false;
                _totalUsedNonPagedPoolValue.width = width / 2f - 45f;
                _totalUsedNonPagedPoolValue.textAlignment = UIHorizontalAlignment.Right;
                _totalUsedNonPagedPoolValue.textScale = 1f;
                _totalUsedNonPagedPoolValue.relativePosition = new Vector3(width / 2f, 75f);

                _totalUsedPhysicalText = UIUtils.CreateLabel(this, "TotalUsedPhysicalText", "MB");
                _totalUsedPhysicalText.textScale = 1f;
                _totalUsedPhysicalText.relativePosition = new Vector3(width - 40f, 15f);

                _totalUsedVirtualText = UIUtils.CreateLabel(this, "TotalUsedVirtualText", "MB");
                _totalUsedVirtualText.textScale = 1f;
                _totalUsedVirtualText.relativePosition = new Vector3(width - 40f, 35f);

                _totalUsedPagedPoolText = UIUtils.CreateLabel(this, "TotalUsedPagedPoolText", "MB");
                _totalUsedPagedPoolText.textScale = 1f;
                _totalUsedPagedPoolText.relativePosition = new Vector3(width - 40f, 55f);

                _totalUsedNonPagedPoolText = UIUtils.CreateLabel(this, "TotalUsedNonPagedPoolText", "MB");
                _totalUsedNonPagedPoolText.textScale = 1f;
                _totalUsedNonPagedPoolText.relativePosition = new Vector3(width - 40f, 75f);
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameMemoryPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshData()
        {
            try
            {
                MemoryInfo.Get(out ulong totalUsedPhysical, out ulong totalUsedVirtual, out ulong totalUsedPagedPool, out ulong totalUsedNonPagedPool);

                _totalUsedPhysical = (float)totalUsedPhysical / 1048576;
                _totalUsedVirtual = (float)totalUsedVirtual / 1048576;
                _totalUsedPagedPool = (float)totalUsedPagedPool / 1048576;
                _totalUsedNonPagedPool = (float)totalUsedNonPagedPool / 1048576;

                _totalUsedPhysicalValue.text = $"{_totalUsedPhysical:0.00}";
                _totalUsedVirtualValue.text = $"{_totalUsedVirtual:0.00}";
                _totalUsedPagedPoolValue.text = $"{_totalUsedPagedPool:0.00}";
                _totalUsedNonPagedPoolValue.text = $"{_totalUsedNonPagedPool:0.00}";
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameMemoryPanel:RefreshData -> Exception: " + e.Message);
            }
        }
    }
}
