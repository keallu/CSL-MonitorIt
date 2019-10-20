using ColossalFramework.UI;
using System;
using UnityEngine;

namespace MonitorIt
{
    class TimePanel : UIPanel
    {
        private float _deltaTime;

        private DateTime _loaded;

        private UILabel _currentTimeValue;
        private UILabel _currentDateValue;
        private UILabel _currentGameTimeValue;

        public override void Start()
        {
            base.Start();

            try
            {
                _loaded = DateTime.Now;

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] TimePanel:Start -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                if (_currentDateValue != null)
                {
                    Destroy(_currentDateValue);
                }
                if (_currentTimeValue != null)
                {
                    Destroy(_currentTimeValue);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] TimePanel:OnDestroy -> Exception: " + e.Message);
            }
        }

        public override void Update()
        {
            base.Update();

            try
            {
                if (isVisible)
                {
                    _deltaTime += Time.deltaTime;

                    if (_deltaTime > 1f)
                    {
                        RefreshData();

                        _deltaTime = 0f;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] TimePanel:Update -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                name = "MonitorItTimePanel";
                clipChildren = true;

                if (parent != UIView.GetAView())
                {
                    size = new Vector2(parent.width, 75f);
                }
                else
                {
                    size = new Vector2(250f, 75f);
                }

                _currentTimeValue = UIUtils.CreateLabel(this, "CurrentTime", "00:00:00");
                _currentTimeValue.autoSize = false;
                _currentTimeValue.height = 27f;
                _currentTimeValue.width = width / 2f - 5f;
                _currentTimeValue.textScale = 1.5f;
                _currentTimeValue.relativePosition = new Vector3(15f, 15f);

                _currentDateValue = UIUtils.CreateLabel(this, "CurrentDate", "Friday, 29 May 2015");
                _currentDateValue.autoSize = false;
                _currentDateValue.height = 18f;
                _currentDateValue.width = width - 5f;
                _currentDateValue.textScale = 0.5f;
                _currentDateValue.relativePosition = new Vector3(15f, 45f);

                _currentGameTimeValue = UIUtils.CreateLabel(this, "CurrentGameTime", "00:00:00");
                _currentGameTimeValue.autoSize = false;
                _currentGameTimeValue.height = 27f;
                _currentGameTimeValue.width = width / 2f - 5f;
                _currentGameTimeValue.textScale = 1.5f;
                _currentGameTimeValue.relativePosition = new Vector3(15f + width / 2f, 15f);
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] TimePanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshData()
        {
            try
            {
                _currentTimeValue.text = DateTime.Now.ToString("HH:mm:ss");
                _currentDateValue.text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                TimeSpan curentGameTime = DateTime.Now - _loaded;
                _currentGameTimeValue.text = (curentGameTime.Days * 24 + curentGameTime.Hours).ToString("00") + ":" + curentGameTime.Minutes.ToString("00") + ":" + curentGameTime.Seconds.ToString("00");
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] TimePanel:RefreshData -> Exception: " + e.Message);
            }
        }
    }
}
