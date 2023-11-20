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
	[ToolboxBitmap(typeof(MetroSetTile), "Bitmaps.Button.bmp")]
	[Designer(typeof(MetroSetTileDesigner))]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class MetroSetTile : MetroSetControl
	{

		private MouseMode _MouseMode;
		private TileAlign _textAlign;
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

		public MetroSetTile() : base(ControlKind.Tile)
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
			var r = new Rectangle(1, 1, Width - 2, Height - 2);
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			StringFormat sf = TileAlign switch
			{
				TileAlign.BottmLeft => new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far },
				TileAlign.BottomRight => new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far },
				TileAlign.Topleft => new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near },
				TileAlign.TopRight => new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near },
				TileAlign.TopCenter => new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near },
				TileAlign.BottomCenter => new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far },
				_ => throw new InvalidOperationException()
			};
            switch (_MouseMode)
			{
				case MouseMode.Normal:
					{
                        using var p = new Pen(NormalBorderColor, 2);
                        using var tb = new SolidBrush(NormalTextColor);
                        if (BackgroundImage != null)
                        {
                            g.DrawImage(BackgroundImage, r);
                        }
                        else
                        {
							using var bg = new SolidBrush(NormalColor);
                            g.FillRectangle(bg, r);
                            g.DrawRectangle(p, r);
                        }
                        g.DrawString(Text, Font, tb, r, sf);
                    }
					break;
				case MouseMode.Hovered:
					{
						Cursor = Cursors.Hand;
                        using var p = new Pen(HoverBorderColor, 2);
                        using var tb = new SolidBrush(HoverTextColor);
                        if (BackgroundImage != null)
                        {
                            g.DrawImage(BackgroundImage, r);
                        }
                        else
                        {
                            using var bg = new SolidBrush(HoverColor);
                            g.FillRectangle(bg, r);
                        }
                        g.DrawString(Text, Font, tb, r, sf);
                        g.DrawRectangle(p, r);
                    }
					break;
				case MouseMode.Pushed:
					{
                        using var p = new Pen(PressBorderColor, 2);
                        using var tb = new SolidBrush(PressTextColor);
                        if (BackgroundImage != null)
                        {
                            g.DrawImage(BackgroundImage, r);
                        }
                        else
                        {
                            using var bg = new SolidBrush(PressColor);
                            g.FillRectangle(bg, r);
                        }
                        g.DrawString(Text, Font, tb, r, sf);
                        g.DrawRectangle(p, r);
                    }
					break;

				case MouseMode.Disabled:
					{
                        using var p = new Pen(DisabledBorderColor);
                        using var tb = new SolidBrush(DisabledForeColor);
                        using var bg = new SolidBrush(DisabledBackColor);
                        g.FillRectangle(bg, r);
                        g.DrawString(Text, Font, tb, r, sf);
                        g.DrawRectangle(p, r);
                    }
					break;
				default:
					throw new InvalidOperationException();
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
					HoverColor = Color.FromArgb(65, 177, 225);
					HoverBorderColor = Color.FromArgb(230, 230, 230);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(65, 177, 225);
					PressBorderColor = Color.FromArgb(65, 177, 225);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(204, 204, 204);
					DisabledBorderColor = Color.FromArgb(155, 155, 155);
					DisabledForeColor = Color.FromArgb(136, 136, 136);
					break;

				case Style.Dark:
					NormalColor = Color.FromArgb(65, 177, 225);
					NormalBorderColor = Color.FromArgb(65, 177, 225);
					NormalTextColor = Color.White;
					HoverColor = Color.FromArgb(65, 177, 225);
					HoverBorderColor = Color.FromArgb(102, 102, 102);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(65, 177, 225);
					PressBorderColor = Color.FromArgb(51, 51, 51);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(80, 80, 80);
					DisabledBorderColor = Color.FromArgb(109, 109, 109);
					DisabledForeColor = Color.FromArgb(109, 109, 109);
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
		/// Gets the background color.
		/// </summary>
		[Browsable(false)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets a value indicating whether the control can respond to user interaction.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the control can respond to user interaction.")]
		public new bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;
				if (value == false)
				{
					_MouseMode = MouseMode.Disabled;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the BackgroundImage associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the BackgroundImage associated with the control.")]
		public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

		/// <summary>
		/// Gets or sets the TileAlign associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the TileAlign associated with the control.")]
		[DefaultValue(TileAlign.BottmLeft)]
		public TileAlign TileAlign
		{
			get { return _textAlign; }
			set
			{
				_textAlign = value;
				Refresh();
			}
		}
		/// <summary>
		/// Gets or sets the control background color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control background color in normal mouse sate.")]
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
		/// Gets or sets the control border color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in normal mouse sate.")]
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
		/// Gets or sets the control Text color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color in normal mouse sate.")]
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
		/// Gets or sets the control background color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control background color in hover mouse sate.")]
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
		/// Gets or sets the control border color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in hover mouse sate.")]
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
		/// Gets or sets the control Text color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color in hover mouse sate.")]
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
		/// Gets or sets the control background color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control background color in pushed mouse sate.")]
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
		/// Gets or sets the control border color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in pushed mouse sate.")]
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
		/// Gets or sets the control Text color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color in pushed mouse sate.")]
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
		/// Gets or sets back color used by the control while disabled.
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
		/// Gets or sets the fore color of the control whenever while disabled.
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
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_MouseMode = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse leave event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseEnter(e);
			_MouseMode = MouseMode.Normal;
			Invalidate();
		}

	}

}