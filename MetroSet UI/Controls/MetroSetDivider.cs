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
	[ToolboxBitmap(typeof(MetroSetDivider), "Bitmaps.Divider.bmp")]
	[Designer(typeof(MetroSetDividerDesigner))]
	[DefaultProperty("Orientation")]
	[ComVisible(true)]
	public class MetroSetDivider : MetroSetControl
	{
		private DividerStyle _orientation;
		private int _thickness;

		public MetroSetDivider() : base(ControlKind.Divider)
		{
			SetStyle(
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			
			ApplyTheme();
			Orientation = DividerStyle.Horizontal;
		}

        protected override void ApplyThemeInternal(Style style)
        {
			switch (style)
			{
				case Style.Light:
					Thickness = 1;
					ForeColor = Color.Black;
					break;

				case Style.Dark:
					Thickness = 1;
					ForeColor = Color.FromArgb(170, 170, 170);
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        Orientation = Enum.Parse<DividerStyle>((string)StyleDictionary["Orientation"]);
                        Thickness = (int)StyleDictionary["Thickness"];
                        ForeColor = Utils.HexColor(StyleDictionary["ForeColor"]);
                    }
					break;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
            using var p = new Pen(ForeColor, Thickness);
            if (Orientation == DividerStyle.Horizontal)
                g.DrawLine(p, 0, Thickness, Width, Thickness);
            else
                g.DrawLine(p, Thickness, 0, Thickness, Height);
        }

		/// <summary>
		/// Gets or sets the style associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Orientation of the control.")]
		public DividerStyle Orientation
		{
			get { return _orientation; }
			set
			{
				_orientation = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the divider thickness.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the divider thickness.")]
		public int Thickness
		{
			get { return _thickness; }
			set
			{
				_thickness = value;
				Refresh();
			}
		}

		/// <summary>
		/// I make BackColor inaccessible cause I want it to be just transparent and I used another property for the same job in following properties. 
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form forecolor.")]
		public override Color ForeColor { get; set; }

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
		/// Here we handle the width and height while resizing.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (Orientation == DividerStyle.Horizontal)
			{
				Height = Thickness + 3;
			}
			else
			{
				Width = Thickness + 3;
			}
		}

	}
}