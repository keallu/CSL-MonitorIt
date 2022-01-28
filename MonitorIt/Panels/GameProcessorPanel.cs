using ColossalFramework.UI;
using System;
using UnityEngine;

namespace MonitorIt
{
    class GameProcessorPanel : UIPanel
    {
        private float _deltaTime;
        private float _totalDeltaTime;
        private long _previousMicroseconds;
        private float _totalUsage;
        private int _measurements;

        private float _currentUsage;
        private float _avgUsage;
        private float _minUsage;
        private float _maxUsage;

        private UILabel _currentUsageValue;
        private UILabel _processorsUsageValue;
        private UILabel _avgUsageValue;
        private UILabel _minUsageValue;
        private UILabel _maxUsageValue;
        private UILabel _currentUsageText;
        private UILabel _processorsUsageText;
        private UILabel _avgUsageText;
        private UILabel _minUsageText;
        private UILabel _maxUsageText;

        public override void Start()
        {
            base.Start();

            try
            {
                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameProcessorPanel:Start -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                if (_maxUsageText != null)
                {
                    Destroy(_maxUsageText.gameObject);
                }
                if (_minUsageText != null)
                {
                    Destroy(_minUsageText.gameObject);
                }
                if (_avgUsageText != null)
                {
                    Destroy(_avgUsageText.gameObject);
                }
                if (_processorsUsageText != null)
                {
                    Destroy(_processorsUsageText.gameObject);
                }
                if (_currentUsageText != null)
                {
                    Destroy(_currentUsageText.gameObject);
                }
                if (_maxUsageValue != null)
                {
                    Destroy(_maxUsageValue.gameObject);
                }
                if (_minUsageValue != null)
                {
                    Destroy(_minUsageValue.gameObject);
                }
                if (_avgUsageValue != null)
                {
                    Destroy(_avgUsageValue.gameObject);
                }
                if (_processorsUsageValue != null)
                {
                    Destroy(_processorsUsageValue.gameObject);
                }
                if (_currentUsageValue != null)
                {
                    Destroy(_currentUsageValue.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameProcessorPanel:OnDestroy -> Exception: " + e.Message);
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

                    _totalDeltaTime += _deltaTime;

                    if (_deltaTime > ModConfig.Instance.RefreshInterval)
                    {
                        RefreshData();

                        _deltaTime = 0f;
                    }

                    if (_totalDeltaTime > ModConfig.Instance.ResetInterval)
                    {
                        _totalDeltaTime = 0f;
                        _measurements = 0;
                        _totalUsage = 0f;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameProcessorPanel:Update -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                name = "MonitorItGameProcessorPanel";
                clipChildren = true;

                if (parent != UIView.GetAView())
                {
                    size = new Vector2(parent.width, 75f);
                }
                else
                {
                    size = new Vector2(250f, 75f);
                }

                _currentUsageValue = UIUtils.CreateLabel(this, "CurrentValue", "0");
                _currentUsageValue.tooltip = "Processors utilization by the game";
                _currentUsageValue.autoSize = false;
                _currentUsageValue.height = 27f;
                _currentUsageValue.width = width * (1f / 4f) - 5f;
                _currentUsageValue.textAlignment = UIHorizontalAlignment.Right;
                _currentUsageValue.textScale = 1.3f;
                _currentUsageValue.relativePosition = new Vector3(10f, 15f);

                _processorsUsageValue = UIUtils.CreateLabel(this, "ProcessorsValue", "0");
                _processorsUsageValue.tooltip = "Number of processors available to the game";
                _processorsUsageValue.autoSize = false;
                _processorsUsageValue.height = 18f;
                _processorsUsageValue.width = width * (1f / 4f) - 5f;
                _processorsUsageValue.textAlignment = UIHorizontalAlignment.Right;
                _processorsUsageValue.textScale = 0.7f;
                _processorsUsageValue.relativePosition = new Vector3(0f, 45f);

                _avgUsageValue = UIUtils.CreateLabel(this, "AvgValue", "0");
                _avgUsageValue.autoSize = false;
                _avgUsageValue.width = width * (1f / 6f) - 10f;
                _avgUsageValue.textAlignment = UIHorizontalAlignment.Center;
                _avgUsageValue.textScale = 0.7f;
                _avgUsageValue.relativePosition = new Vector3(width * (3f / 6f) - 5f, 35f);

                _minUsageValue = UIUtils.CreateLabel(this, "MinValue", "0");
                _minUsageValue.autoSize = false;
                _minUsageValue.width = width * (1f / 6f) - 10f;
                _minUsageValue.textAlignment = UIHorizontalAlignment.Center;
                _minUsageValue.textScale = 0.7f;
                _minUsageValue.relativePosition = new Vector3(width * (4f / 6f) - 5f, 35f);

                _maxUsageValue = UIUtils.CreateLabel(this, "MaxValue", "0");
                _maxUsageValue.autoSize = false;
                _maxUsageValue.width = width * (1f / 6f) - 10f;
                _maxUsageValue.textAlignment = UIHorizontalAlignment.Center;
                _maxUsageValue.textScale = 0.7f;
                _maxUsageValue.relativePosition = new Vector3(width * (5f / 6f) - 5f, 35f);

                _currentUsageText = UIUtils.CreateLabel(this, "CurrentText", "%");
                _currentUsageText.tooltip = "Processors utilization by the game";
                _currentUsageText.height = 27f;
                _currentUsageText.textScale = 1.3f;
                _currentUsageText.relativePosition = new Vector3(width * (1f / 4f) + 15f, 15f);

                _processorsUsageText = UIUtils.CreateLabel(this, "ProcessorsText", "Cores");
                _processorsUsageText.tooltip = "Number of virtual cores available to the game";
                _processorsUsageText.height = 18f;
                _processorsUsageText.textScale = 0.7f;
                _processorsUsageText.relativePosition = new Vector3(width * (1f / 4f) + 5f, 45f);

                _avgUsageText = UIUtils.CreateLabel(this, "AvgText", "avg");
                _avgUsageText.autoSize = false;
                _avgUsageText.width = width * (1f / 6f) - 10f;
                _avgUsageText.textAlignment = UIHorizontalAlignment.Center;
                _avgUsageText.textScale = 0.8f;
                _avgUsageText.relativePosition = new Vector3(width * (3f / 6f) - 5f, 15f);

                _minUsageText = UIUtils.CreateLabel(this, "MinText", "min");
                _minUsageText.autoSize = false;
                _minUsageText.width = width * (1f / 6f) - 10f;
                _minUsageText.textAlignment = UIHorizontalAlignment.Center;
                _minUsageText.textScale = 0.8f;
                _minUsageText.relativePosition = new Vector3(width * (4f / 6f) - 5f, 15f);

                _maxUsageText = UIUtils.CreateLabel(this, "MaxText", "max");
                _maxUsageText.autoSize = false;
                _maxUsageText.width = width * (1f / 6f) - 10f;
                _maxUsageText.textAlignment = UIHorizontalAlignment.Center;
                _maxUsageText.textScale = 0.8f;
                _maxUsageText.relativePosition = new Vector3(width * (5f / 6f) - 5f, 15f);
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameProcessorPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshData()
        {
            try
            {

                SysInfo.GetProcessorTimes(out long kernelMicroseconds, out long userMicroseconds);

                int processorNumber = Environment.ProcessorCount;
                long currentMicroseconds = kernelMicroseconds + userMicroseconds;

                if (_previousMicroseconds != 0L)
                {
                    _measurements++;

                    _currentUsage = (float)TimeSpan.FromTicks(currentMicroseconds - _previousMicroseconds).TotalSeconds / processorNumber / _deltaTime * 100;

                    _totalUsage += _currentUsage;

                    _avgUsage = _totalUsage / _measurements;

                    if (_currentUsage < _minUsage || _minUsage <= 0f)
                    {
                        _minUsage = _currentUsage;
                    }
                    if (_currentUsage > _maxUsage || _minUsage <= 0f)
                    {
                        _maxUsage = _currentUsage;
                    }

                    _currentUsageValue.text = $"{_currentUsage:0.0}";
                    _processorsUsageValue.text = $"{processorNumber:0}";
                    _avgUsageValue.text = $"{_avgUsage:0.0}";
                    _minUsageValue.text = $"{_minUsage:0.0}";
                    _maxUsageValue.text = $"{_maxUsage:0.0}";
                }

                _previousMicroseconds = currentMicroseconds;
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] GameProcessorPanel:RefreshData -> Exception: " + e.Message);
            }
        }
    }
}
