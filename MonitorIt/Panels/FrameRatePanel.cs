using ColossalFramework.UI;
using System;
using UnityEngine;

namespace MonitorIt
{
    class FrameRatePanel : UIPanel
    {
        private float _deltaTime;
        private int _frames;
        private float _totalDeltaTime;
        private float _totalFrames;

        private float _currentFps;
        private float _currentMs;
        private float _avgFps;
        private float _minFps;
        private float _maxFps;

        private UILabel _currentFpsValue;
        private UILabel _currentMsValue;
        private UILabel _avgFpsValue;
        private UILabel _minFpsValue;
        private UILabel _maxFpsValue;
        private UILabel _currentFpsText;
        private UILabel _currentMsText;
        private UILabel _avgFpsText;
        private UILabel _minFpsText;
        private UILabel _maxFpsText;

        public override void Start()
        {
            base.Start();

            try
            {
                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] FrameRatePanel:Start -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                if (_maxFpsText != null)
                {
                    Destroy(_maxFpsText.gameObject);
                }
                if (_minFpsText != null)
                {
                    Destroy(_minFpsText.gameObject);
                }
                if (_avgFpsText != null)
                {
                    Destroy(_avgFpsText.gameObject);
                }
                if (_currentMsText != null)
                {
                    Destroy(_currentMsText.gameObject);
                }
                if (_currentFpsText != null)
                {
                    Destroy(_currentFpsText.gameObject);
                }
                if (_maxFpsValue != null)
                {
                    Destroy(_maxFpsValue.gameObject);
                }
                if (_minFpsValue != null)
                {
                    Destroy(_minFpsValue.gameObject);
                }
                if (_avgFpsValue != null)
                {
                    Destroy(_avgFpsValue.gameObject);
                }
                if (_currentMsValue != null)
                {
                    Destroy(_currentMsValue.gameObject);
                }
                if (_currentFpsValue != null)
                {
                    Destroy(_currentFpsValue.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] FrameRatePanel:OnDestroy -> Exception: " + e.Message);
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
                    _frames++;

                    _totalDeltaTime += _deltaTime;
                    _totalFrames += _frames;

                    if (_deltaTime > ModConfig.Instance.RefreshInterval)
                    {
                        RefreshData();

                        _deltaTime = 0f;
                        _frames = 0;
                    }

                    if (_totalDeltaTime > ModConfig.Instance.ResetInterval)
                    {
                        _totalDeltaTime = 0f;
                        _totalFrames = 0f;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] FrameRatePanel:Update -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                name = "MonitorItFrameRatePanel";
                clipChildren = true;
                
                if (parent != UIView.GetAView())
                {
                    size = new Vector2(parent.width, 75f);
                }
                else
                {
                    size = new Vector2(250f, 75f);
                }

                _currentFpsValue = UIUtils.CreateLabel(this, "CurrentFpsValue", "0");
                _currentFpsValue.tooltip = "Frames per second";
                _currentFpsValue.autoSize = false;
                _currentFpsValue.height = 27f;
                _currentFpsValue.width = width * (1f / 4f) - 5f;
                _currentFpsValue.textAlignment = UIHorizontalAlignment.Right;
                _currentFpsValue.textScale = 1.5f;
                _currentFpsValue.relativePosition = new Vector3(0f, 15f);

                _currentMsValue = UIUtils.CreateLabel(this, "CurrentMsValue", "0");
                _currentMsValue.tooltip = "Milliseconds per frame";
                _currentMsValue.autoSize = false;
                _currentMsValue.height = 18f;
                _currentMsValue.width = width * (1f / 4f) - 5f;
                _currentMsValue.textAlignment = UIHorizontalAlignment.Right;
                _currentMsValue.textScale = 0.8f;
                _currentMsValue.relativePosition = new Vector3(0f, 45f);

                _avgFpsValue = UIUtils.CreateLabel(this, "AvgValue", "0");
                _avgFpsValue.autoSize = false;
                _avgFpsValue.width = width * (1f / 6f) - 10f;
                _avgFpsValue.textAlignment = UIHorizontalAlignment.Center;
                _avgFpsValue.textScale = 1f;
                _avgFpsValue.relativePosition = new Vector3(width * (3f / 6f) - 5f, 35f);

                _minFpsValue = UIUtils.CreateLabel(this, "MinValue", "0");
                _minFpsValue.autoSize = false;
                _minFpsValue.width = width * (1f / 6f) - 10f;
                _minFpsValue.textAlignment = UIHorizontalAlignment.Center;
                _minFpsValue.textScale = 1f;
                _minFpsValue.relativePosition = new Vector3(width * (4f / 6f) - 5f, 35f);

                _maxFpsValue = UIUtils.CreateLabel(this, "MaxValue", "0");
                _maxFpsValue.autoSize = false;
                _maxFpsValue.width = width * (1f / 6f) - 10f;
                _maxFpsValue.textAlignment = UIHorizontalAlignment.Center;
                _maxFpsValue.textScale = 1f;
                _maxFpsValue.relativePosition = new Vector3(width * (5f / 6f) - 5f, 35f);

                _currentFpsText = UIUtils.CreateLabel(this, "FpsText", "fps");
                _currentFpsText.tooltip = "Frames per second";
                _currentFpsText.height = 27f;
                _currentFpsText.textScale = 1.5f;
                _currentFpsText.relativePosition = new Vector3(width * (1f / 4f) + 5f, 15f);

                _currentMsText = UIUtils.CreateLabel(this, "MsText", "ms");
                _currentMsText.tooltip = "Milliseconds per frame";
                _currentMsText.height = 18f;
                _currentMsText.textScale = 0.8f;
                _currentMsText.relativePosition = new Vector3(width * (1f / 4f) + 5f, 45f);

                _avgFpsText = UIUtils.CreateLabel(this, "AvgText", "avg");
                _avgFpsText.autoSize = false;
                _avgFpsText.width = width * (1f / 6f) - 10f;
                _avgFpsText.textAlignment = UIHorizontalAlignment.Center;
                _avgFpsText.textScale = 0.8f;
                _avgFpsText.relativePosition = new Vector3(width * (3f / 6f) - 5f, 15f);

                _minFpsText = UIUtils.CreateLabel(this, "MinText", "min");
                _minFpsText.autoSize = false;
                _minFpsText.width = width * (1f / 6f) - 10f;
                _minFpsText.textAlignment = UIHorizontalAlignment.Center;
                _minFpsText.textScale = 0.8f;
                _minFpsText.relativePosition = new Vector3(width * (4f / 6f) - 5f, 15f);

                _maxFpsText = UIUtils.CreateLabel(this, "MaxText", "max");
                _maxFpsText.autoSize = false;
                _maxFpsText.width = width * (1f / 6f) - 10f;
                _maxFpsText.textAlignment = UIHorizontalAlignment.Center;
                _maxFpsText.textScale = 0.8f;
                _maxFpsText.relativePosition = new Vector3(width * (5f / 6f) - 5f, 15f);
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] FrameRatePanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshData()
        {
            try
            {
                _currentFps = _frames / _deltaTime;
                _currentMs = _deltaTime / _frames * 1000f;
                _avgFps = _totalFrames / _totalDeltaTime;
                if (_currentFps < _minFps || _minFps <= 0f)
                {
                    _minFps = _currentFps;
                }
                if (_currentFps > _maxFps || _minFps <= 0f)
                {
                    _maxFps = _currentFps;
                }

                _currentFpsValue.text = $"{_currentFps:0}";
                _currentMsValue.text = $"{_currentMs:0.##}";
                _avgFpsValue.text = $"{_avgFps:0}";
                _minFpsValue.text = $"{_minFps:0}";
                _maxFpsValue.text = $"{_maxFps:0}";
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] FrameRatePanel:RefreshData -> Exception: " + e.Message);
            }
        }
    }
}
