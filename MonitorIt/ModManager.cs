﻿using ColossalFramework.UI;
using System;
using UnityEngine;

namespace MonitorIt
{
    public class ModManager : MonoBehaviour
    {
        private bool _initialized;
        private UIMultiStateButton _zoomButton;
        private UIComponent _optionsPanel;
        private OptionsMainPanel _optionsMainPanel;

        private UITextureAtlas _monitorItAtlas;
        private UIPanel _buttonPanel;
        private UIButton _button;
        private UIPanel _mainPanel;
        private UIDragHandle _mainDragHandle;
        private FrameRatePanel _frameRatePanel;
        private GameMemoryPanel _gameMemoryPanel;
        private UnityMemoryPanel _unityMemoryPanel;
        private MonoMemoryPanel _monoMemoryPanel;

        public void Start()
        {
            try
            {
                _zoomButton = GameObject.Find("ZoomButton").GetComponent<UIMultiStateButton>();
                _optionsPanel = UIView.library.Get("OptionsPanel");
                _optionsMainPanel = GameObject.Find("(Library) OptionsPanel").GetComponent<OptionsMainPanel>();

                _monitorItAtlas = LoadResources();
                ModProperties.Instance.Atlas = _monitorItAtlas;

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:Start -> Exception: " + e.Message);
            }
        }

        public void Update()
        {
            try
            {
                if (!_initialized || ModConfig.Instance.ConfigUpdated)
                {
                    UpdateUI();

                    _initialized = true;
                    ModConfig.Instance.ConfigUpdated = false;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:Update -> Exception: " + e.Message);
            }
        }

        public void OnDestroy()
        {
            try
            {
                if (_monoMemoryPanel != null)
                {
                    Destroy(_monoMemoryPanel);
                }
                if (_unityMemoryPanel != null)
                {
                    Destroy(_unityMemoryPanel);
                }
                if (_gameMemoryPanel != null)
                {
                    Destroy(_gameMemoryPanel);
                }
                if (_frameRatePanel != null)
                {
                    Destroy(_frameRatePanel);
                }
                if (_mainDragHandle != null)
                {
                    Destroy(_mainDragHandle);
                }
                if (_mainPanel != null)
                {
                    Destroy(_mainPanel);
                }
                if (_button != null)
                {
                    Destroy(_button);
                }
                if (_buttonPanel != null)
                {
                    Destroy(_buttonPanel);
                }
                if (_monitorItAtlas != null)
                {
                    Destroy(_monitorItAtlas);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:OnDestroy -> Exception: " + e.Message);
            }
        }

        private UITextureAtlas LoadResources()
        {
            try
            {
                if (_monitorItAtlas == null)
                {
                    string[] spriteNames = new string[]
                    {
                        "monitorit",
                        "boxblue"
                    };

                    _monitorItAtlas = ResourceLoader.CreateTextureAtlas("MonitorItAtlas", spriteNames, "MonitorIt.Icons.");

                    UITextureAtlas defaultAtlas = ResourceLoader.GetAtlas("Ingame");
                    Texture2D[] textures = new Texture2D[]
                    {
                        defaultAtlas["OptionBase"].texture,
                        defaultAtlas["OptionBaseFocused"].texture,
                        defaultAtlas["OptionBaseHovered"].texture,
                        defaultAtlas["OptionBasePressed"].texture,
                        defaultAtlas["OptionBaseDisabled"].texture
                    };

                    ResourceLoader.AddTexturesInAtlas(_monitorItAtlas, textures);
                }

                return _monitorItAtlas;
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:LoadResources -> Exception: " + e.Message);
                return null;
            }
        }

        private void CreateUI()
        {
            try
            {
                _buttonPanel = UIUtils.CreatePanel("MonitorItButtonPanel");
                _buttonPanel.zOrder = 0;
                _buttonPanel.size = new Vector2(36f, 36f);
                _buttonPanel.eventMouseMove += (component, eventParam) =>
                {
                    if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                    {
                        var ratio = UIView.GetAView().ratio;
                        component.position = new Vector3(component.position.x + (eventParam.moveDelta.x * ratio), component.position.y + (eventParam.moveDelta.y * ratio), component.position.z);

                        ModConfig.Instance.ButtonPositionX = component.absolutePosition.x;
                        ModConfig.Instance.ButtonPositionY = component.absolutePosition.y;
                        ModConfig.Instance.Save();
                    }
                };

                _button = UIUtils.CreateButton(_buttonPanel, "MonitorItButton", _monitorItAtlas, "monitorit");
                _button.tooltip = "Monitor It!";
                _button.size = new Vector2(36f, 36f);
                _button.relativePosition = new Vector3(0f, 0f);
                _button.eventClick += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        if (_optionsPanel != null)
                        {
                            if (_optionsPanel.isVisible)
                            {
                                _optionsPanel.Hide();
                                ModProperties.Instance.IsOptionsPanelNonModal = false;
                            }
                            else
                            {
                                if (_optionsMainPanel != null)
                                {
                                    _optionsMainPanel.SelectMod("Monitor It!");
                                }

                                _optionsPanel.Show();
                                ModProperties.Instance.IsOptionsPanelNonModal = true;
                            }
                        }

                        eventParam.Use();
                    }
                };

                _mainPanel = UIUtils.CreatePanel("MonitorItMainPanel");
                _mainPanel.width = 250f;
                _mainPanel.zOrder = 0;
                _mainPanel.autoLayout = true;
                _mainPanel.autoLayoutDirection = LayoutDirection.Vertical;
                _mainPanel.autoLayoutStart = LayoutStart.TopLeft;
                _mainPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 5);
                _mainPanel.isInteractive = false;

                _mainDragHandle = UIUtils.CreateDragHandle(_mainPanel, "MonitorItMainDragHandle");
                _mainDragHandle.tooltip = "Drag to move panel";
                _mainDragHandle.height = 10f;
                _mainDragHandle.width = 250f;
                _mainDragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.PanelPositionX = _mainPanel.absolutePosition.x;
                    ModConfig.Instance.PanelPositionY = _mainPanel.absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                _frameRatePanel = _mainPanel.AddUIComponent<FrameRatePanel>();
                if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    _gameMemoryPanel = _mainPanel.AddUIComponent<GameMemoryPanel>();
                }
                _unityMemoryPanel = _mainPanel.AddUIComponent<UnityMemoryPanel>();
                _monoMemoryPanel = _mainPanel.AddUIComponent<MonoMemoryPanel>();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                if (ModConfig.Instance.ButtonPositionX == 0f && ModConfig.Instance.ButtonPositionY == 0f)
                {
                    _buttonPanel.absolutePosition = new Vector3(_zoomButton.absolutePosition.x, _zoomButton.absolutePosition.y - (1 * 36f) - 5f);
                }
                else
                {
                    _buttonPanel.absolutePosition = new Vector3(ModConfig.Instance.ButtonPositionX, ModConfig.Instance.ButtonPositionY);
                }

                _buttonPanel.isVisible = ModConfig.Instance.ShowButton;

                if (ModConfig.Instance.PanelPositionX == 0f && ModConfig.Instance.PanelPositionY == 0f)
                {
                    _mainPanel.absolutePosition = new Vector3(10f, 75f);
                }
                else
                {
                    _mainPanel.absolutePosition = new Vector3(ModConfig.Instance.PanelPositionX, ModConfig.Instance.PanelPositionY);
                }

                _mainPanel.isVisible = ModConfig.Instance.ShowPanel;

                _frameRatePanel.isVisible = ModConfig.Instance.ShowFrameRatePanel;
                if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    _gameMemoryPanel.isVisible = ModConfig.Instance.ShowGameMemoryPanel;
                }
                _unityMemoryPanel.isVisible = ModConfig.Instance.ShowUnityMemoryPanel;
                _monoMemoryPanel.isVisible = ModConfig.Instance.ShowMonoMemoryPanel;

                float mainPanelHeight = 0f;

                foreach (UIComponent component in _mainPanel.components)
                {
                    mainPanelHeight += component.height;
                    mainPanelHeight += 5f;
                }

                _mainPanel.height = mainPanelHeight;
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:UpdateUI -> Exception: " + e.Message);
            }
        }
    }
}
