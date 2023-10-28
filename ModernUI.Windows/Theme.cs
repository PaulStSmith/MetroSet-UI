using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

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
        /// Gets or sets the background color of the close button when the mouse is hovering over it.
        /// </summary>
        public Color CloseButtonHoverBackColor { get; set; } = Color.Red;

        /// <summary>
        /// Gets or sets the foreground color of the close button when the mouse is hovering over it.
        /// </summary>
        public Color CloseButtonHoverForeColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for an active caption element.
        /// </summary>
        public Color ActiveCaption { get; set; } = UISettings.UIElementColor(UIElementType.ActiveCaption).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a background element.
        /// </summary>
        public Color Background { get; set; } = UISettings.UIElementColor(UIElementType.Background).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a button face element.
        /// </summary>
        public Color ButtonFace { get; set; } = UISettings.UIElementColor(UIElementType.ButtonFace).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed on a button.
        /// </summary>
        public Color ButtonText { get; set; } = UISettings.UIElementColor(UIElementType.ButtonText).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed in a caption.
        /// </summary>
        public Color CaptionText { get; set; } = UISettings.UIElementColor(UIElementType.CaptionText).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for greyed text.
        /// </summary>
        public Color GrayText { get; set; } = UISettings.UIElementColor(UIElementType.GrayText).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a highlighted user interface (UI) element.
        /// </summary>
        public Color Highlight { get; set; } = UISettings.UIElementColor(UIElementType.Highlight).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for highlighted text.
        /// </summary>
        public Color HighlightText { get; set; } = UISettings.UIElementColor(UIElementType.HighlightText).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a hotlighted UI element.
        /// </summary>
        public Color Hotlight { get; set; } = UISettings.UIElementColor(UIElementType.Hotlight).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for an inactive caption element.
        /// </summary>
        public Color InactiveCaption { get; set; } = UISettings.UIElementColor(UIElementType.InactiveCaption).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed in an inactive caption element.
        /// </summary>
        public Color InactiveCaptionText { get; set; } = UISettings.UIElementColor(UIElementType.InactiveCaptionText).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a window.
        /// </summary>
        public Color Window { get; set; } = UISettings.UIElementColor(UIElementType.Window).ToColor();

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed in a window's UI decoration.
        /// </summary>
        public Color WindowText { get; set; } = UISettings.UIElementColor(UIElementType.WindowText).ToColor();

        public Color AccentColor { get; set; } = UISettings.UIElementColor(UIElementType.AccentColor).ToColor();

        public Color TextHigh { get; set; } = UISettings.UIElementColor(UIElementType.TextHigh).ToColor();

        public Color TextMedium { get; set; } = UISettings.UIElementColor(UIElementType.TextMedium).ToColor();

        public Color TextLow { get; set; } = UISettings.UIElementColor(UIElementType.TextLow).ToColor();

        public Color TextContrastWithHigh { get; set; } = UISettings.UIElementColor(UIElementType.TextContrastWithHigh).ToColor();

        public Color NonTextHigh { get; set; } = UISettings.UIElementColor(UIElementType.NonTextHigh).ToColor();

        public Color NonTextMediumHigh { get; set; } = UISettings.UIElementColor(UIElementType.NonTextMediumHigh).ToColor();

        public Color NonTextMedium { get; set; } = UISettings.UIElementColor(UIElementType.NonTextMedium).ToColor();

        public Color NonTextMediumLow { get; set; } = UISettings.UIElementColor(UIElementType.NonTextMediumLow).ToColor();

        public Color NonTextLow { get; set; } = UISettings.UIElementColor(UIElementType.NonTextLow).ToColor();

        public Color PageBackground { get; set; } = UISettings.UIElementColor(UIElementType.PageBackground).ToColor();

        public Color PopupBackground { get; set; } = UISettings.UIElementColor(UIElementType.PopupBackground).ToColor();

        public Color OverlayOutsidePopup { get; set; } = UISettings.UIElementColor(UIElementType.OverlayOutsidePopup).ToColor();

    }

    internal static class Utils
    {
        /// <summary>
        /// Converts the specified <see cref="Windows.UI.Color"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="winColor">The <see cref="Windows.UI.Color"/> to be converted.</param>
        /// <returns>A <see cref="Color"/> that is qualitatively equivalent to the <see cref="Windows.UI.Color"/>.</returns>
        internal static Color ToColor(this global::Windows.UI.Color winColor) => Color.FromArgb(winColor.A, winColor.R, winColor.G, winColor.B);
    }
}