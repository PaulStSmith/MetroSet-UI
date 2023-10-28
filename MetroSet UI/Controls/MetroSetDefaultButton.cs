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
	[ToolboxBitmap(typeof(MetroSetDefaultButton), "Bitmaps.Button.bmp")]
	[Designer(typeof(MetroSetDefaultButtonDesigner))]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class MetroSetDefaultButton : Control, IMetroSetControl
	{

		/// <summary>
		/// Gets or sets the style associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the style associated with the control.")]
		public Style Style
		{
			get => StyleManager?.Style ?? _style;
			set
			{
				_style = value;
				switch (value)
				{
					case Style.Dark:
						ApplyTheme(Style.Dark);
						break;

					case Style.Custom:
						ApplyTheme(Style.Custom);
						break;

					default:
						ApplyTheme();
						break;
				}

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the Style Manager associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
		public StyleManager StyleManager
		{
			get => _styleManager;
			set
			{
				_styleManager = value;
				Invalidate();
			}
		}

		private MouseMode _state;
		private Style _style;
		private StyleManager _styleManager;

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

		public MetroSetDefaultButton()
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

			switch (_state)
			{
				case MouseMode.Normal:

					using (var bg = new SolidBrush(NormalColor))
					using (var p = new Pen(NormalBorderColor))
					using (var tb = new SolidBrush(NormalTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), Methods.SetPosition());
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
						g.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), Methods.SetPosition());
					}

					break;

				case MouseMode.Pushed:

					using (var bg = new SolidBrush(PressColor))
					using (var p = new Pen(PressBorderColor))
					using (var tb = new SolidBrush(PressTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), Methods.SetPosition());
					}

					break;

				case MouseMode.Disabled:
					using (var bg = new SolidBrush(DisabledBackColor))
					using (var p = new Pen(DisabledBorderColor))
					using (var tb = new SolidBrush(DisabledForeColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), Methods.SetPosition());
					}
					break;
			}
		}

		/// <summary>
		/// Gets or sets the style provided by the user.
		/// </summary>
		/// <param name="style">The Style.</param>
		private void ApplyTheme(Style style = Style.Light)
		{
			if (!IsDerivedStyle)
				return;

			switch (style)
			{
				case Style.Light:
					NormalColor = Color.FromArgb(238, 238, 238);
					NormalBorderColor = Color.FromArgb(204, 204, 204);
					NormalTextColor = Color.Black;
					HoverColor = Color.FromArgb(102, 102, 102);
					HoverBorderColor = Color.FromArgb(102, 102, 102);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(51, 51, 51);
					PressBorderColor = Color.FromArgb(51, 51, 51);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(204, 204, 204);
					DisabledBorderColor = Color.FromArgb(155, 155, 155);
					DisabledForeColor = Color.FromArgb(136, 136, 136);
					break;

				case Style.Dark:
					NormalColor = Color.FromArgb(32, 32, 32);
					NormalBorderColor = Color.FromArgb(64, 64, 64);
					NormalTextColor = Color.FromArgb(204, 204, 204);
					HoverColor = Color.FromArgb(170, 170, 170);
					HoverBorderColor = Color.FromArgb(170, 170, 170);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(240, 240, 240);
					PressBorderColor = Color.FromArgb(240, 240, 240);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(80, 80, 80);
					DisabledBorderColor = Color.FromArgb(109, 109, 109);
					DisabledForeColor = Color.FromArgb(109, 109, 109);
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.DefaultButtonDictionary)
						{
							if (varkey.Key == null)
							{
								return;
							}

							switch (varkey.Key)
							{
								case "NormalColor":
									NormalColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "NormalBorderColor":
									NormalBorderColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "NormalTextColor":
									NormalTextColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "HoverColor":
									HoverColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "HoverBorderColor":
									HoverBorderColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "HoverTextColor":
									HoverTextColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "PressColor":
									PressColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "PressBorderColor":
									PressBorderColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "PressTextColor":
									PressTextColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "DisabledBackColor":
									DisabledBackColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "DisabledBorderColor":
									DisabledBorderColor = Utilites.HexColor((string)varkey.Value);
									break;
								case "DisabledForeColor":
									DisabledForeColor = Utilites.HexColor((string)varkey.Value);
									break;
							}
						}
					Refresh();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
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
				if (value == false)
				{
					_state = MouseMode.Disabled;
				}
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

		/// <inheritdoc/>
        internal void OnClickInternal(EventArgs e)
        {
            base.OnClick(e);
        }

        /// <inheritdoc/>
        protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_state = MouseMode.Hovered;
			Invalidate();
		}

        /// <inheritdoc/>
        protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_state = MouseMode.Pushed;
			Invalidate();
		}

        /// <inheritdoc/>
        protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseMode.Hovered;
			Invalidate();
		}

        /// <inheritdoc/>
        protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseMode.Normal;
			Invalidate();
		}
	}
}