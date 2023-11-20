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
using MetroSet.UI.Forms;
using MetroSet.UI.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetControlBox), "Bitmaps.ControlButton.bmp")]
	[Designer(typeof(MetroSetControBoxDesigner))]
	[DefaultProperty("Click")]
	[ComVisible(true)]
	public class MetroSetControlBox : MetroSetControl
	{
		private bool _maximizeBox = true;
		private Color _closeNormalForeColor;
		private Color _closeHoverForeColor;
		private Color _closeHoverBackColor;
		private Color _maximizeHoverForeColor;
		private Color _maximizeHoverBackColor;
		private Color _maximizeNormalForeColor;
		private Color _minimizeHoverForeColor;
		private Color _minimizeHoverBackColor;
		private Color _minimizeNormalForeColor;
		private Color _disabledForeColor;

		public MetroSetControlBox() : base(ControlKind.ControlBox)
		{
			SetStyle(
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			
			base.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ApplyTheme();
		}

        /// <inheritdoc/>
        protected override void ApplyThemeInternal(Style style)
        {
			switch (style)
			{
				case Style.Light:
					CloseHoverBackColor = Color.FromArgb(183, 40, 40);
					CloseHoverForeColor = Color.White;
					CloseNormalForeColor = Color.Gray;
					MaximizeHoverBackColor = Color.FromArgb(238, 238, 238);
					MaximizeHoverForeColor = Color.Gray;
					MaximizeNormalForeColor = Color.Gray;
					MinimizeHoverBackColor = Color.FromArgb(238, 238, 238);
					MinimizeHoverForeColor = Color.Gray;
					MinimizeNormalForeColor = Color.Gray;
					DisabledForeColor = Color.DimGray;
					break;

				case Style.Dark:
					CloseHoverBackColor = Color.FromArgb(183, 40, 40);
					CloseHoverForeColor = Color.White;
					CloseNormalForeColor = Color.Gray;
					MaximizeHoverBackColor = Color.FromArgb(238, 238, 238);
					MaximizeHoverForeColor = Color.Gray;
					MaximizeNormalForeColor = Color.Gray;
					MinimizeHoverBackColor = Color.FromArgb(238, 238, 238);
					MinimizeHoverForeColor = Color.Gray;
					MinimizeNormalForeColor = Color.Gray;
					DisabledForeColor = Color.Silver;
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        CloseHoverBackColor = Utils.HexColor(StyleDictionary["CloseHoverBackColor"]);
                        CloseHoverForeColor = Utils.HexColor(StyleDictionary["CloseHoverForeColor"]);
                        CloseNormalForeColor = Utils.HexColor(StyleDictionary["CloseNormalForeColor"]);
                        MaximizeHoverBackColor = Utils.HexColor(StyleDictionary["MaximizeHoverBackColor"]);
                        MaximizeHoverForeColor = Utils.HexColor(StyleDictionary["MaximizeHoverForeColor"]);
                        MaximizeNormalForeColor = Utils.HexColor(StyleDictionary["MaximizeNormalForeColor"]);
                        MinimizeHoverBackColor = Utils.HexColor(StyleDictionary["MinimizeHoverBackColor"]);
                        MinimizeHoverForeColor = Utils.HexColor(StyleDictionary["MinimizeHoverForeColor"]);
                        MinimizeNormalForeColor = Utils.HexColor(StyleDictionary["MinimizeNormalForeColor"]);
                        DisabledForeColor = Utils.HexColor(StyleDictionary["DisabledForeColor"]);
                    }
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form.")]
		public bool MaximizeBox
		{
			get { return _maximizeBox; }
			set
			{
				_maximizeBox = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.")]
		private bool _minimizeBox = true;
		public bool MinimizeBox
		{
			get { return _minimizeBox; }
			set
			{
				_minimizeBox = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Close ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Close forecolor used by the control.")]
		public Color CloseNormalForeColor
		{
			get { return _closeNormalForeColor; }
			set
			{
				_closeNormalForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Close ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Close forecolor used by the control.")]
		public Color CloseHoverForeColor
		{
			get { return _closeHoverForeColor; }
			set
			{
				_closeHoverForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Close BackColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Close backcolor used by the control.")]
		public Color CloseHoverBackColor
		{
			get { return _closeHoverBackColor; }
			set
			{
				_closeHoverBackColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Maximize ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Maximize forecolor used by the control.")]
		public Color MaximizeHoverForeColor
		{
			get { return _maximizeHoverForeColor; }
			set
			{
				_maximizeHoverForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Maximize BackColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Maximize backcolor used by the control.")]
		public Color MaximizeHoverBackColor
		{
			get { return _maximizeHoverBackColor; }
			set
			{
				_maximizeHoverBackColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Maximize ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Maximize forecolor used by the control.")]
		public Color MaximizeNormalForeColor
		{
			get { return _maximizeNormalForeColor; }
			set
			{
				_maximizeNormalForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Minimize ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Minimize forecolor used by the control.")]
		public Color MinimizeHoverForeColor
		{
			get { return _minimizeHoverForeColor; }
			set
			{
				_minimizeHoverForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Minimize BackColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Minimize backcolor used by the control.")]
		public Color MinimizeHoverBackColor
		{
			get { return _minimizeHoverBackColor; }
			set
			{
				_minimizeHoverBackColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets Minimize ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Minimize forecolor used by the control.")]
		public Color MinimizeNormalForeColor
		{
			get { return _minimizeNormalForeColor; }
			set
			{
				_minimizeNormalForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets disabled ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets disabled forecolor used by the control.")]
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
		/// I make BackColor inaccessible cause we have not use of it. 
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		private bool MinimizeHovered { get; set; }

		private bool MaximizeHovered { get; set; }

		private bool CloseHovered { get; set; }

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			using (var closeBoxState = new SolidBrush(CloseHovered ? CloseHoverBackColor : Color.Transparent))
			{
                using var f = new Font(@"Marlett", 12);
                using var tb = new SolidBrush(CloseHovered ? CloseHoverForeColor : CloseNormalForeColor);
                using var sf = new StringFormat { Alignment = StringAlignment.Center };
                g.FillRectangle(closeBoxState, new Rectangle(70, 5, 27, Height));
                g.DrawString("r", f, CloseHovered ? tb : Brushes.Gray, new Point(Width - 16, 8), sf);
            }
			using (var maximizeBoxState = new SolidBrush(MaximizeBox ? MaximizeHovered ? MaximizeHoverBackColor : Color.Transparent : Color.Transparent))
			{
                using var f = new Font(@"Marlett", 12);
                using var tb = new SolidBrush(MaximizeBox ? MaximizeHovered ? MaximizeHoverForeColor : MaximizeNormalForeColor : DisabledForeColor);
                var maxSymbol = Parent.FindForm()?.WindowState == FormWindowState.Maximized ? "2" : "1";
                using var sf = new StringFormat { Alignment = StringAlignment.Center };
                g.FillRectangle(maximizeBoxState, new Rectangle(38, 5, 24, Height));
                g.DrawString(maxSymbol, f, tb, new Point(51, 7), sf);
            }
			using (var minimizeBoxState = new SolidBrush(MinimizeBox ? MinimizeHovered ? MinimizeHoverBackColor : Color.Transparent : Color.Transparent))
			{
                using var f = new Font(@"Marlett", 12);
                using var tb = new SolidBrush(MinimizeBox ? MinimizeHovered ? MinimizeHoverForeColor : MinimizeNormalForeColor : DisabledForeColor);
                using var sf = new StringFormat { Alignment = StringAlignment.Center };
                g.FillRectangle(minimizeBoxState, new Rectangle(5, 5, 27, Height));
                g.DrawString("0", f, tb, new Point(20, 7), sf);
            }

		}

		/// <summary>
		/// Here we provide the fixed size while resizing.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Size = new Size(100, 25);
		}

		/// <summary>
		/// Handling mouse up event of the control so that we detect if cursor located in our need area.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Location.Y > 0 && e.Location.Y < (Height - 2))
			{
				if (e.Location.X > 0 && e.Location.X < 34)
				{
					Cursor = Cursors.Hand;
					MinimizeHovered = true;
					MaximizeHovered = false;
					CloseHovered = false;
				}
				else if (e.Location.X > 33 && e.Location.X < 65)
				{
					Cursor = Cursors.Hand;
					MinimizeHovered = false;
					MaximizeHovered = true;
					CloseHovered = false;
				}
				else if (e.Location.X > 64 && e.Location.X < Width)
				{
					Cursor = Cursors.Hand;
					MinimizeHovered = false;
					MaximizeHovered = false;
					CloseHovered = true;
				}
				else
				{
					Cursor = Cursors.Arrow;
					MinimizeHovered = false;
					MaximizeHovered = false;
					CloseHovered = false;
				}
			}
			Invalidate();
		}

		/// <summary>
		/// Handling mouse up event of the control so that we can perform action commands.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (CloseHovered)
            {
                if (Parent.FindForm() is MetroSetForm mf)
                    mf.Close();
            }
			else if (MinimizeHovered)
			{
				if (!MinimizeBox)
					return;
				Parent.FindForm().WindowState = FormWindowState.Minimized;
			}
			else if (MaximizeHovered)
			{
				if (MaximizeBox)
				{
					Parent.FindForm().WindowState = Parent.FindForm()?.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
				}
			}
		}

		/// <summary>
		/// Handling mouse leave event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			Cursor = Cursors.Default;
			MinimizeHovered = false;
			MaximizeHovered = false;
			CloseHovered = false;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse down event of the control.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();
		}

	}
}