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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetPanel), "Bitmaps.Panel.bmp")]
	[ComVisible(true)]
	public class MetroSetPanel : Panel, IMetroSetControl
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
					case Style.Light:
						ApplyTheme();
						break;

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
			set { _styleManager = value; Invalidate(); }
		}

		private Style _style;
		private StyleManager _styleManager;

		private int _borderThickness = 1;
		private Color _borderColor;
		private Color _backgroundColor;

		public MetroSetPanel()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			BorderStyle = BorderStyle.None;
			UpdateStyles();
			
			ApplyTheme();
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
					BorderColor = Color.FromArgb(150, 150, 150);
					BackgroundColor = Color.White;
					UpdateProperties();
					break;

				case Style.Dark:
					BorderColor = Color.FromArgb(110, 110, 110);
					BackgroundColor = Color.FromArgb(30, 30, 30);
					UpdateProperties();
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.LabelDictionary)
						{
							switch (varkey.Key)
							{
								case "BorderColor":
									BorderColor = Utilites.HexColor((string)varkey.Value);
									break;

								case "BackColor":
									BackgroundColor = Utilites.HexColor((string)varkey.Value);
									break;

								default:
									return;
							}
						}
					UpdateProperties();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
			}
		}

		private void UpdateProperties()
		{
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var r = new Rectangle(BorderThickness, BorderThickness, Width - (BorderThickness * 2 + 1), Height - ((BorderThickness * 2) + 1));

			using (var bg = new SolidBrush(BackgroundColor))
			{
				using (var p = new Pen(BorderColor, BorderThickness))
				{
					g.FillRectangle(bg, r);
					g.DrawRectangle(p, r);
				}
			}

		}

		/// <summary>
		/// Gets the background color.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets the foreground color.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor => Color.Transparent;

		/// <summary>
		/// Gets or sets the border style.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new BorderStyle BorderStyle;

		/// <summary>
		/// Gets or sets the border thickness the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the border thickness the control.")]
		public int BorderThickness
		{
			get { return _borderThickness; }
			set
			{
				_borderThickness = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets BorderColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets bordercolor used by the control.")]
		public Color BorderColor
		{
			get { return _borderColor; }
			set
			{
				_borderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets BackColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets backcolor used by the control.")]
		[DisplayName("BackColor")]
		public Color BackgroundColor
		{
			get { return _backgroundColor; }
			set
			{
				_backgroundColor = value;
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

	}
}