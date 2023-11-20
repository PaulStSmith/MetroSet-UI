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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Components
{

	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetSetToolTip), "Bitmaps.ToolTip.bmp")]
	[Designer(typeof(MetroSetToolTipDesigner))]
	[DefaultEvent("Popup")]
	public class MetroSetSetToolTip : ToolTip, IMetroSetControl
	{
        public MetroSetSetToolTip()
        {
            OwnerDraw = true;
            Draw += OnDraw;
            Popup += ToolTip_Popup;
            ApplyTheme();
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
            }
        }
        private bool _IsCustomStyle = false;

		private void OnDraw(object sender, DrawToolTipEventArgs e)
		{
			var g = e.Graphics;
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			var rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            using var bg = new SolidBrush(BackColor);
            using var stroke = new Pen(BorderColor);
            using var tb = new SolidBrush(ForeColor);
            g.FillRectangle(bg, rect);
            g.DrawString(e.ToolTipText, MetroSetFonts.Light(11), tb, rect, Methods.SetPosition());
            g.DrawRectangle(stroke, rect);

        }

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

            switch (style)
			{
				case Style.Light:
					ForeColor = Color.FromArgb(170, 170, 170);
					BackColor = Color.White;
					BorderColor = Color.FromArgb(204, 204, 204);
					break;

				case Style.Dark:
					ForeColor = Color.FromArgb(204, 204, 204);
					BackColor = Color.FromArgb(32, 32, 32);
					BorderColor = Color.FromArgb(64, 64, 64);
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
						var dic = StyleManager.StyleDictionary(ControlKind.ToolTip);
                        BackColor = Utils.HexColor(dic["BackColor"]);
                        BorderColor = Utils.HexColor(dic["BorderColor"]);
                        ForeColor = Utils.HexColor(dic["ForeColor"]);
                    }
					break;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether a ToolTip window is displayed, even when its parent control is not active.
		/// </summary>
		[Browsable(false)]
		public new bool ShowAlways { get; } = false;

		/// <summary>
		/// Gets or sets a value indicating whether the ToolTip is drawn by the operating system or by code that you provide.
		/// </summary>
		[Browsable(false)]
		public new bool OwnerDraw
		{
			get => base.OwnerDraw;
			set
			{
				base.OwnerDraw = true;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the ToolTip should use a balloon window.
		/// </summary>
		[Browsable(false)]
		public new bool IsBalloon { get; } = false;

		/// <summary>
		/// Gets or sets the background color for the ToolTip.
		/// </summary>
		[Browsable(false)]
		public new Color BackColor { get; set; }

		/// <summary>
		/// Gets or sets the foreground color for the ToolTip.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the foreground color for the ToolTip.")]
		public new Color ForeColor { get; set; }

		/// <summary>
		/// Gets or sets a title for the ToolTip window.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a title for the ToolTip window.")]
		public new string ToolTipTitle { get; } = string.Empty;

		/// <summary>
		/// Defines a set of standardized icons that can be associated with a ToolTip.
		/// </summary>
		[Browsable(false)]
		public new ToolTipIcon ToolTipIcon { get; } = ToolTipIcon.None;

		/// <summary>
		/// Gets or sets the border color for the ToolTip.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the border color for the ToolTip.")]
		public Color BorderColor { get; set; }

		/// <summary>
		/// The ToolTip text to display when the pointer is on the control.
		/// </summary>
		/// <param name = "control" > The Control to show the tooltip.</param>
		/// <param name = "caption" > The Text that appears in tooltip.</param>
		public new void SetToolTip(Control control, string caption)
		{
			//This Method is useful at runtime.
			base.SetToolTip(control, caption);
			foreach (Control c in control.Controls)
			{
				SetToolTip(c, caption);
			}
		}

		/// <summary>
		/// Here we handle popup event and we set the style of controls for tooltip.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolTip_Popup(object sender, PopupEventArgs e)
		{
			var control = e.AssociatedControl;
			if (control is IMetroSetControl iControl)
			{
				Style = iControl.Style;
				StyleManager = iControl.StyleManager;
			}
			else if (control is IMetroForm metroForm)
			{
				Style = metroForm.Style;
				StyleManager = metroForm.StyleManager;
			}
			e.ToolTipSize = new Size(e.ToolTipSize.Width + 30, e.ToolTipSize.Height + 6);
		}

	}
}