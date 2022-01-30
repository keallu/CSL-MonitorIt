using ColossalFramework.UI;
using System;
using UnityEngine;

namespace MonitorIt
{
    public class ModManager : MonoBehaviour
    {
        private bool _initialized;

        private UITextureAtlas _monitorItAtlas;
        private UIPanel _buttonPanel;
        private UIButton _button;
        private UIPanel _mainPanel;
        private UIPanel _titlePanel;
        private UILabel _titleLabel;
        private UIDragHandle _titleDragHandle;
        private TimePanel _timePanel;
        private FrameRatePanel _frameRatePanel;
        private GameProcessorPanel _gameProcessorPanel;
        private GameMemoryPanel _gameMemoryPanel;
        private UnityMemoryPanel _unityMemoryPanel;
        private MonoMemoryPanel _monoMemoryPanel;

        public void Awake()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:Awake -> Exception: " + e.Message);
            }
        }

        public void Start()
        {
            try
            {
                if (ModConfig.Instance.ButtonPositionX == 0f && ModConfig.Instance.ButtonPositionY == 0f)
                {
                    ModProperties.Instance.ResetButtonPosition();
                }

                if (ModConfig.Instance.PanelPositionX == 0f && ModConfig.Instance.PanelPositionY == 0f)
                {
                    ModProperties.Instance.ResetPanelPosition();
                }

                _monitorItAtlas = LoadResources();

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
                    Destroy(_monoMemoryPanel.gameObject);
                }
                if (_unityMemoryPanel != null)
                {
                    Destroy(_unityMemoryPanel.gameObject);
                }
                if (_gameMemoryPanel != null)
                {
                    Destroy(_gameMemoryPanel.gameObject);
                }
                if (_gameProcessorPanel != null)
                {
                    Destroy(_gameProcessorPanel.gameObject);
                }
                if (_frameRatePanel != null)
                {
                    Destroy(_frameRatePanel.gameObject);
                }
                if (_titleDragHandle != null)
                {
                    Destroy(_titleDragHandle.gameObject);
                }
                if (_titleLabel != null)
                {
                    Destroy(_titleLabel.gameObject);
                }
                if (_titlePanel != null)
                {
                    Destroy(_titlePanel.gameObject);
                }
                if (_mainPanel != null)
                {
                    Destroy(_mainPanel.gameObject);
                }
                if (_button != null)
                {
                    Destroy(_button.gameObject);
                }
                if (_buttonPanel != null)
                {
                    Destroy(_buttonPanel.gameObject);
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
                        "monitorit"
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
                _mainPanel = UIUtils.CreatePanel("MonitorItMainPanel");
                _mainPanel.width = 250f;
                _mainPanel.height = 35f;
                _mainPanel.isInteractive = false;
                _mainPanel.autoLayout = true;
                _mainPanel.autoLayoutDirection = LayoutDirection.Vertical;
                _mainPanel.autoLayoutStart = LayoutStart.TopLeft;
                _mainPanel.autoLayoutPadding = new RectOffset(0, 0, 5, 0);

                _titlePanel = UIUtils.CreatePanel(_mainPanel, "MonitorItTitlePanel");
                _titlePanel.clipChildren = true;
                _titlePanel.atlas = _monitorItAtlas;
                _titlePanel.width = 250f;
                _titlePanel.height = 35f;

                _titleLabel = UIUtils.CreateLabel(_titlePanel, "MonitorItTitleLabel", "Monitor It!");
                _titleLabel.autoSize = false;
                _titleLabel.width = 250f;
                _titleLabel.height = 25f;
                _titleLabel.textAlignment = UIHorizontalAlignment.Center;
                _titleLabel.textScale = 1.2f;
                _titleLabel.relativePosition = new Vector3(0f, 5f);

                _titleDragHandle = UIUtils.CreateDragHandle(_titlePanel, "MonitorItTitleDragHandle");
                _titleDragHandle.tooltip = "Drag to move panel";
                _titleDragHandle.target = _mainPanel;
                _titleDragHandle.width = 250f;
                _titleDragHandle.height = 35f;
                _titleDragHandle.relativePosition = new Vector3(0f, 0f);
                _titleDragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.PanelPositionX = _mainPanel.absolutePosition.x;
                    ModConfig.Instance.PanelPositionY = _mainPanel.absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                _timePanel = _mainPanel.AddUIComponent<TimePanel>();
                _frameRatePanel = _mainPanel.AddUIComponent<FrameRatePanel>();
                if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    _gameProcessorPanel = _mainPanel.AddUIComponent<GameProcessorPanel>();
                    _gameMemoryPanel = _mainPanel.AddUIComponent<GameMemoryPanel>();
                }
                _unityMemoryPanel = _mainPanel.AddUIComponent<UnityMemoryPanel>();
                _monoMemoryPanel = _mainPanel.AddUIComponent<MonoMemoryPanel>();

                _timePanel.atlas = _monitorItAtlas;
                _frameRatePanel.atlas = _monitorItAtlas;
                if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    _gameProcessorPanel.atlas = _monitorItAtlas;
                    _gameMemoryPanel.atlas = _monitorItAtlas;
                }
                _unityMemoryPanel.atlas = _monitorItAtlas;
                _monoMemoryPanel.atlas = _monitorItAtlas;

                _buttonPanel = UIUtils.CreatePanel("MonitorItButtonPanel");
                _buttonPanel.zOrder = 25;
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
                        if (_mainPanel != null)
                        {
                            if (_mainPanel.isVisible)
                            {
                                _mainPanel.Hide();
                                ModConfig.Instance.ShowPanel = false;
                                ModConfig.Instance.Save();
                            }
                            else
                            {
                                _mainPanel.Show();
                                ModConfig.Instance.ShowPanel = true;
                                ModConfig.Instance.Save();
                            }
                        }

                        eventParam.Use();
                    }
                };

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
                if (_mainPanel != null)
                {
                    _mainPanel.isVisible = ModConfig.Instance.ShowPanel;
                    _mainPanel.absolutePosition = new Vector3(ModConfig.Instance.PanelPositionX, ModConfig.Instance.PanelPositionY);

                    _titlePanel.isVisible = ModConfig.Instance.ShowTitlePanel;

                    _timePanel.isVisible = ModConfig.Instance.ShowTimePanel;
                    _frameRatePanel.isVisible = ModConfig.Instance.ShowFrameRatePanel;
                    if (Application.platform == RuntimePlatform.WindowsPlayer)
                    {
                        _gameProcessorPanel.isVisible = ModConfig.Instance.ShowGameCpuPanel;
                        _gameMemoryPanel.isVisible = ModConfig.Instance.ShowGameMemoryPanel;
                    }
                    _unityMemoryPanel.isVisible = ModConfig.Instance.ShowUnityMemoryPanel;
                    _monoMemoryPanel.isVisible = ModConfig.Instance.ShowMonoMemoryPanel;

                    float mainPanelHeight = 0f;

                    foreach (UIComponent component in _mainPanel.components)
                    {
                        if (component is UIPanel && component.isVisible)
                        {
                            mainPanelHeight += component.height;
                            mainPanelHeight += 5f;
                        }
                    }

                    _mainPanel.height = mainPanelHeight;
                }

                if (_buttonPanel != null)
                {
                    _buttonPanel.isVisible = ModConfig.Instance.ShowButton;
                    _buttonPanel.absolutePosition = new Vector3(ModConfig.Instance.ButtonPositionX, ModConfig.Instance.ButtonPositionY);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] ModManager:UpdateUI -> Exception: " + e.Message);
            }
        }
    }
}
