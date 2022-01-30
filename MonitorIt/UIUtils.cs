using ColossalFramework.UI;

namespace MonitorIt
{
    public class UIUtils
    {
        public static UIPanel CreatePanel(string name)
        {
            UIPanel panel = UIView.GetAView()?.AddUIComponent(typeof(UIPanel)) as UIPanel;
            panel.name = name;

            return panel;
        }

        public static UIPanel CreatePanel(UIComponent parent, string name)
        {
            UIPanel panel = parent.AddUIComponent<UIPanel>();
            panel.name = name;

            return panel;
        }

        public static UIDragHandle CreateDragHandle(UIComponent parent, string name)
        {
            UIDragHandle dragHandle = parent.AddUIComponent<UIDragHandle>();
            dragHandle.name = name;
            dragHandle.target = parent;

            return dragHandle;
        }

        public static UILabel CreateLabel(UIComponent parent, string name, string text)
        {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.name = name;
            label.text = text;

            return label;
        }

        public static UIButton CreateButton(UIComponent parent, string name, UITextureAtlas atlas, string spriteName)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = name;
            button.atlas = atlas;

            button.normalBgSprite = "OptionBase";
            button.hoveredBgSprite = "OptionBaseHovered";
            button.pressedBgSprite = "OptionBasePressed";
            button.disabledBgSprite = "OptionBaseDisabled";

            button.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            button.normalFgSprite = spriteName;
            button.hoveredFgSprite = spriteName;
            button.pressedFgSprite = spriteName;
            button.disabledFgSprite = spriteName;

            return button;
        }
    }
}
