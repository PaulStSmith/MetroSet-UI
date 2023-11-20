using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUI.Windows
{
    public class Theme
    {
        /// <summary>
        /// Gets a reference to the singleton instance.
        /// </summary>
        public static Theme Instance { get; private set; } = new Theme();

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
        /// Gets or sets the <see cref="Color"/> for a button face element.
        /// </summary>
        public Color ButtonFace { get; set; } = Color.FromKnownColor(KnownColor.ButtonFace);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed on a button.
        /// </summary>
        public Color ButtonText { get; set; } = Color.FromKnownColor(KnownColor.ControlText);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for a window.
        /// </summary>
        public Color Window { get; set; } = Color.FromKnownColor(KnownColor.Window);

        /// <summary>
        /// Gets or sets the <see cref="Color"/> for the text displayed in a window's UI decoration.
        /// </summary>
        public Color WindowText { get; set; } = Color.FromKnownColor(KnownColor.WindowText);
    }
}