/*
* MetroSet UI - MetroSet UI Framework
* 
* The MIT License (MIT)
* Copyright (c) 2017 Narwin, https://github.com/N-a-r-w-i-n
 * Copyright (c) 2023 Paulo Santos, https://github.com/PaulStSmith
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy of 
* this software and associated documentation files (the "Software"), to deal in the 
* Software without restriction, including without limitation the rights to use, copy, 
* modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
* and to permit persons to whom the Software is furnished to do so, subject to the 
* following conditions:
* 
* The above copyright notice and this permission notice shall be included in 
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
* INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
* PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
* HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
* CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
* OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetLinkLabel), "Bitmaps.LinkLabel.bmp")]
	[Designer(typeof(MetroSetLinkDesigner))]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class MetroSetLinkLabel : LinkLabel, IMetroSetControl
	{
		public MetroSetLinkLabel()
		{
			SetStyle(
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true
				);
			UpdateStyles();
			base.Font = MetroSetFonts.Light(10);
			base.Cursor = Cursors.Hand;
			
			_style = Style.Dark;
			ApplyTheme();
			LinkBehavior = LinkBehavior.HoverUnderline;
		}

        /// <summary>
        /// Gets a value indicating whether or not the light theme should be used.
        /// </summary>
        public static bool UseLightTheme => Utils.UseLightTheme;

        /// <inheritdoc />
        public Style Style
        {
            get => (Style)(_style ?? StyleManager?.Style);
            set
            {
                if (_style != value)
                    ApplyTheme((Style)(_style = value));
            }
        }
        private Style? _style = UseLightTheme ? Style.Light : Style.Dark;

        /// <inheritdoc />
        public StyleManager StyleManager
        {
            get => _StyleManager;
            set
            {
                if (value != _StyleManager)
                {
                    _StyleManager = value;
                    Refresh();
                }
            }
        }
        private StyleManager _StyleManager;

        /// <inheritdoc />
        public bool IsCustomStyle
        {
            get => _IsCustomStyle;
            set
            {
                if (value == _IsCustomStyle)
                    return;
                _IsCustomStyle = value;
                Refresh();
            }
        }
        private bool _IsCustomStyle = false;

        /// <summary>
        /// Gets the style dictionary for the control.
        /// </summary>
        public IDictionary<string, object> StyleDictionary => _StyleManager?.StyleDictionary(ControlKind.LinkLabel);

        /// <summary>
        /// Applies the current style to the control.
        /// </summary>
        public void ApplyTheme()
        {
            if (IsCustomStyle)
                return;
            ApplyTheme(Style);
        }

        /// <summary>
        /// Applies the specified style to the control.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        public void ApplyTheme(Style style)
        {
            if (IsCustomStyle)
                return;
            SuspendLayout();
            switch (style)
			{
				case Style.Light:
					ForeColor = Color.Black;
					BackColor = Color.Transparent;
					ActiveLinkColor = Color.FromArgb(85, 197, 245);
					LinkColor = Color.FromArgb(65, 177, 225);
					VisitedLinkColor = Color.FromArgb(45, 157, 205);
					break;

				case Style.Dark:
					ForeColor = Color.FromArgb(170, 170, 170);
					BackColor = Color.Transparent;
					ActiveLinkColor = Color.FromArgb(85, 197, 245);
					LinkColor = Color.FromArgb(65, 177, 225);
					VisitedLinkColor = Color.FromArgb(45, 157, 205);
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        ForeColor = Utils.HexColor(StyleDictionary["ForeColor"]);
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        LinkColor = Utils.HexColor(StyleDictionary["LinkColor"]);
                        ActiveLinkColor = Utils.HexColor(StyleDictionary["ActiveLinkColor"]);
                        VisitedLinkColor = Utils.HexColor(StyleDictionary["VisitedLinkColor"]);
						LinkBehavior = Enum.Parse<LinkBehavior>((string)StyleDictionary["LinkBehavior"]);
                    }
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
			}
			ResumeLayout();
			Refresh();
		}

		/// <summary>
		/// Here we set the smooth mouse hand.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			Utils.SmoothCursor(ref m);

			base.WndProc(ref m);
		}
	}
}