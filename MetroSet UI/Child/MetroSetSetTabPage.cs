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

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Child
{
	[Designer(typeof(MetroSetTabPageDesigner))]
	public class MetroSetSetTabPage : TabPage, IMetroSetControl
	{

		/// <summary>
		/// Gets or sets the style associated with the control.
		/// </summary>
		[Browsable(false)]
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
		[Browsable(false)]
		[Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
		public StyleManager StyleManager
		{
			get => _styleManager;
			set { _styleManager = value; Invalidate(); }
		}

		private Style _style;
		private StyleManager _styleManager;

		public MetroSetSetTabPage()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			base.Font = MetroSetFonts.Light(10);
			UpdateStyles();
			ApplyTheme();
		}

		/// <summary>
		/// Gets or sets the style provided by the user.
		/// </summary>
		/// <param name="style">The Style.</param>
		private void ApplyTheme(Style style = Style.Light)
		{
			if (!IsCustomStyle)
				return;

			switch (style)
			{
				case Style.Light:
					BaseColor = Color.White;
					UpdateProperties();
					break;

				case Style.Dark:
					BaseColor = Color.FromArgb(32, 32, 32);
					UpdateProperties();
					break;
			}
		}

		/// <summary>
		/// Updating properties after changing in style.
		/// </summary>
		public void UpdateProperties()
		{
			Invalidate();
		}

		[Browsable(false)]
		public new Color BackColor { get; set; } = Color.Transparent;

		// I don't want to re-create the following properties for specific reason but for helping
		// the users to find usage properties easily under MetroSet Framework category in property grid.

		[Category("MetroSet Framework")]
		public override string Text { get; set; }

		[Category("MetroSet Framework")]
		public new Font Font { get; set; }

		[Category("MetroSet Framework")]
		public new int ImageIndex { get; set; }

		[Category("MetroSet Framework")]
		public new string ImageKey { get; set; }

		[Category("MetroSet Framework")]
		public new string ToolTipText { get; set; }

		[Category("MetroSet Framework")]
		[Bindable(false)]
		public Color BaseColor { get; set; }

		private bool _isDerivedStyle = true;

		/// <summary>
		/// Gets or sets the whether this control reflect to parent form style.
		/// Set it to false if you want the style of this control be independent. 
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the whether this control reflect to parent form style. \n " +
					 "Set it to false if you want the style of this control be independent. ")]
		public bool IsCustomStyle
		{
			get { return _isDerivedStyle; }
			set
			{
				_isDerivedStyle = value;
				Refresh();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			using (var bg = new SolidBrush(BaseColor))
			{
				g.FillRectangle(bg, ClientRectangle);
			}
		}

	}
}