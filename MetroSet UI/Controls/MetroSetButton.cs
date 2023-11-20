﻿/*
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
using MetroSet.UI.Components;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetButton), "Bitmaps.Button.bmp")]
	[Designer(typeof(MetroSetButtonDesigner))]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	public class MetroSetButton : MetroSetControl
	{
		private MouseMode _MouseMode;
		private Color _normalColor;
		private Color _normalBorderColor;
		private Color _normalTextColor;
		private Color _hoverColor;
		private Color _hoverBorderColor;
		private Color _hoverTextColor;
		private Color _pressColor;
		private Color _pressBorderColor;
		private Color _pressTextColor;
		private Color _disabledBackColor;
		private Color _disabledForeColor;
		private Color _disabledBorderColor;

		public MetroSetButton() : base(ControlKind.Button)
		{
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			base.Font = MetroSetFonts.Light(10);

			ApplyTheme();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var r = new Rectangle(0, 0, Width - 1, Height - 1);
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			switch (_MouseMode)
			{
				case MouseMode.Normal:

					using (var bg = new SolidBrush(NormalColor))
					using (var p = new Pen(NormalBorderColor))
					using (var tb = new SolidBrush(NormalTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, Methods.SetPosition());
					}

					break;

				case MouseMode.Hovered:

					Cursor = Cursors.Hand;
					using (var bg = new SolidBrush(HoverColor))
					using (var p = new Pen(HoverBorderColor))
					using (var tb = new SolidBrush(HoverTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, Methods.SetPosition());
					}

					break;

				case MouseMode.Pushed:

					using (var bg = new SolidBrush(PressColor))
					using (var p = new Pen(PressBorderColor))
					using (var tb = new SolidBrush(PressTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, Methods.SetPosition());
					}

					break;

				case MouseMode.Disabled:

					using (var bg = new SolidBrush(DisabledBackColor))
					using (var p = new Pen(DisabledBorderColor))
					using (var tb = new SolidBrush(DisabledForeColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, Methods.SetPosition());
					}
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(_MouseMode), "");
			}
		}

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        protected override void ApplyThemeInternal(Style style)
        {
			switch (style)
			{
				case Style.Light:
					NormalColor = Color.FromArgb(65, 177, 225);
					NormalBorderColor = Color.FromArgb(65, 177, 225);
					NormalTextColor = Color.White;
					HoverColor = Color.FromArgb(95, 207, 255);
					HoverBorderColor = Color.FromArgb(95, 207, 255);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(35, 147, 195);
					PressBorderColor = Color.FromArgb(35, 147, 195);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
					DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
					DisabledForeColor = Color.Gray;
					break;

				case Style.Dark:
					NormalColor = Color.FromArgb(65, 177, 225);
					NormalBorderColor = Color.FromArgb(65, 177, 225);
					NormalTextColor = Color.White;
					HoverColor = Color.FromArgb(95, 207, 255);
					HoverBorderColor = Color.FromArgb(95, 207, 255);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(35, 147, 195);
					PressBorderColor = Color.FromArgb(35, 147, 195);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
					DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
					DisabledForeColor = Color.Gray;
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        NormalColor = Utils.HexColor(StyleDictionary["NormalColor"]);
                        NormalBorderColor = Utils.HexColor(StyleDictionary["NormalBorderColor"]);
                        NormalTextColor = Utils.HexColor(StyleDictionary["NormalTextColor"]);
                        HoverColor = Utils.HexColor(StyleDictionary["HoverColor"]);
                        HoverBorderColor = Utils.HexColor(StyleDictionary["HoverBorderColor"]);
                        HoverTextColor = Utils.HexColor(StyleDictionary["HoverTextColor"]);
                        PressColor = Utils.HexColor(StyleDictionary["PressColor"]);
                        PressBorderColor = Utils.HexColor(StyleDictionary["PressBorderColor"]);
                        PressTextColor = Utils.HexColor(StyleDictionary["PressTextColor"]);
                        DisabledBackColor = Utils.HexColor(StyleDictionary["DisabledBackColor"]);
                        DisabledBorderColor = Utils.HexColor(StyleDictionary["DisabledBorderColor"]);
                        DisabledForeColor = Utils.HexColor(StyleDictionary["DisabledForeColor"]);
                    }
					break;
			}
		}

		/// <summary>
		/// I make BackColor inaccessible cause we have not use of it. 
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Handling Control Enable state to detect the disability state.
		/// </summary>
		[Category("MetroSet Framework")]
		public new bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;
				_MouseMode = value ? MouseMode.Normal : MouseMode.Disabled;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the button background color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button background color in normal mouse sate.")]
		public Color NormalColor
		{
			get { return _normalColor; }
			set
			{
				_normalColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button border color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button border color in normal mouse sate.")]
		public Color NormalBorderColor
		{
			get { return _normalBorderColor; }
			set
			{
				_normalBorderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button Text color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button Text color in normal mouse sate.")]
		public Color NormalTextColor
		{
			get { return _normalTextColor; }
			set
			{
				_normalTextColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button background color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button background color in hover mouse sate.")]
		public Color HoverColor
		{
			get { return _hoverColor; }
			set
			{
				_hoverColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button border color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button border color in hover mouse sate.")]
		public Color HoverBorderColor
		{
			get { return _hoverBorderColor; }
			set
			{
				_hoverBorderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button Text color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button Text color in hover mouse sate.")]
		public Color HoverTextColor
		{
			get { return _hoverTextColor; }
			set
			{
				_hoverTextColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button background color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button background color in pushed mouse sate.")]
		public Color PressColor
		{
			get { return _pressColor; }
			set
			{
				_pressColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button border color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button border color in pushed mouse sate.")]
		public Color PressBorderColor
		{
			get { return _pressBorderColor; }
			set
			{
				_pressBorderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button Text color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button Text color in pushed mouse sate.")]
		public Color PressTextColor
		{
			get { return _pressTextColor; }
			set
			{
				_pressTextColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets BackColor used by the control while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets backcolor used by the control while disabled.")]
		public Color DisabledBackColor
		{
			get { return _disabledBackColor; }
			set
			{
				_disabledBackColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the ForeColor of the control whenever while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the forecolor of the control whenever while disabled.")]
		public Color DisabledForeColor
		{
			get { return _disabledForeColor; }
			set
			{
				_disabledForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the border color of the control while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the border color of the control while disabled.")]
		public Color DisabledBorderColor
		{
			get { return _disabledBorderColor; }
			set
			{
				_disabledBorderColor = value;
				Refresh();
			}
		}

		private bool _isDerivedStyle = true;

		/// <summary>
		/// Gets or sets the whether this control reflect to parent form style.
		/// Set it to false if you want the style of this control be independent. 
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
					 "Set it to false if you want the style of this control be independent. ")]
		public bool IsDerivedStyle
		{
			get { return _isDerivedStyle; }
			set
			{
				_isDerivedStyle = value;
				Refresh();
			}
		}

		/// <summary>
		/// Handling mouse up event of the control.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_MouseMode = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse down event of the control.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_MouseMode = MouseMode.Pushed;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse entering event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_MouseMode = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse leave event of the control.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseEnter(e);
			_MouseMode = MouseMode.Normal;
			Invalidate();
		}

	}
}