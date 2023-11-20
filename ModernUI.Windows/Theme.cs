using System.Runtime;
using System.Windows.Media;

namespace ModernUI.Windows
{
    public class Theme
    {
        /// <summary>
        /// Gets a reference to the singleton instance.
        /// </summary>
        public static Theme Instance { get; private set; } = new Theme();

        private static readonly UISettings UISettings = new UISettings();

        /// <summary>
        /// Default constructor.
        /// </summary>
        private Theme() { }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for an active caption element.
        /// </summary>
        public Color ActiveCaption { get; set; } = UISettings.UIElementColor(UIElementType.ActiveCaption);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a background element.
        /// </summary>
        public Color Background { get; set; } = UISettings.UIElementColor(UIElementType.Background);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a button face element.
        /// </summary>
        public Color ButtonFace { get; set; } = UISettings.UIElementColor(UIElementType.ButtonFace);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed on a button.
        /// </summary>
        public Color ButtonText { get; set; } = UISettings.UIElementColor(UIElementType.ButtonText);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed in a caption.
        /// </summary>
        public Color CaptionText { get; set; } = UISettings.UIElementColor(UIElementType.CaptionText);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for greyed text.
        /// </summary>
        public Color GrayText { get; set; } = UISettings.UIElementColor(UIElementType.GrayText);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a highlighted user interface (UI) element.
        /// </summary>
        public Color Highlight { get; set; } = UISettings.UIElementColor(UIElementType.Highlight);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for highlighted text.
        /// </summary>
        public Color HighlightText { get; set; } = UISettings.UIElementColor(UIElementType.HighlightText);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a hotlighted UI element.
        /// </summary>
        public Color Hotlight { get; set; } = UISettings.UIElementColor(UIElementType.Hotlight);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for an inactive caption element.
        /// </summary>
        public Color InactiveCaption { get; set; } = UISettings.UIElementColor(UIElementType.InactiveCaption);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed in an inactive caption element.
        /// </summary>
        public Color InactiveCaptionText { get; set; } = UISettings.UIElementColor(UIElementType.InactiveCaptionText);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a window.
        /// </summary>
        public Color Window { get; set; } = UISettings.UIElementColor(UIElementType.Window);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed in a window's UI decoration.
        /// </summary>
        public Color WindowText { get; set; } = UISettings.UIElementColor(UIElementType.WindowText);

        public Color AccentColor { get; set; } = UISettings.UIElementColor(UIElementType.AccentColor);

        public Color TextHigh { get; set; } = UISettings.UIElementColor(UIElementType.TextHigh);

        public Color TextMedium { get; set; } = UISettings.UIElementColor(UIElementType.TextMedium);

        public Color TextLow { get; set; } = UISettings.UIElementColor(UIElementType.TextLow);

        public Color TextContrastWithHigh { get; set; } = UISettings.UIElementColor(UIElementType.TextContrastWithHigh);

        public Color NonTextHigh { get; set; } = UISettings.UIElementColor(UIElementType.NonTextHigh);

        public Color NonTextMediumHigh { get; set; } = UISettings.UIElementColor(UIElementType.NonTextMediumHigh);

        public Color NonTextMedium { get; set; } = UISettings.UIElementColor(UIElementType.NonTextMedium);

        public Color NonTextMediumLow { get; set; } = UISettings.UIElementColor(UIElementType.NonTextMediumLow);

        public Color NonTextLow { get; set; } = UISettings.UIElementColor(UIElementType.NonTextLow);

        public Color PageBackground { get; set; } = UISettings.UIElementColor(UIElementType.PageBackground);

        public Color PopupBackground { get; set; } = UISettings.UIElementColor(UIElementType.PopupBackground);

        public Color OverlayOutsidePopup { get; set; } = UISettings.UIElementColor(UIElementType.OverlayOutsidePopup);

    }
}