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
using MetroSet.UI.Animates;
using MetroSet.UI.Components;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetSwitchBox), "Bitmaps.Switch.bmp")]
	[Designer(typeof(MetroSetSwitchDesigner))]
	[DefaultEvent("SwitchedChanged")]
	[DefaultProperty("Switched")]
	[ComVisible(true)]
	public class MetroSetSwitchBox : MetroSetControl, IDisposable
	{
		private bool _switched;
		private int _switchLocation;
		private readonly IntAnimate _animator;
		private Enums.CheckState _checkState;
		private Color _borderColor;
		private Color _checkColor;
		private Color _disabledBorderColor;
		private Color _disabledCheckColor;
		private Color _disabledUnCheckColor;
		private Color _backgroundColor;
		private Color _symbolColor;
		private Color _unCheckColor;

		public MetroSetSwitchBox() : base(ControlKind.SwitchBox)
		{
			SetStyle(
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			base.Cursor = Cursors.Hand;
			
			_animator = new IntAnimate();
			_animator.Setting(100, 0, 132);
			_animator.Update = (alpha) =>
			{
				_switchLocation = alpha;
				Invalidate(false);
			};
			ApplyTheme();
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
					ForeColor = Color.Black;
					BackColor = Color.White;
					BorderColor = Color.FromArgb(165, 159, 147);
					DisabledBorderColor = Color.FromArgb(205, 205, 205);
					SymbolColor = Color.FromArgb(92, 92, 92);
					UnCheckColor = Color.FromArgb(155, 155, 155);
					CheckColor = Color.FromArgb(65, 177, 225);
					DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
					DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
					break;

				case Style.Dark:
					ForeColor = Color.FromArgb(170, 170, 170);
					BackColor = Color.FromArgb(30, 30, 30);
					BorderColor = Color.FromArgb(155, 155, 155);
					DisabledBorderColor = Color.FromArgb(85, 85, 85);
					SymbolColor = Color.FromArgb(92, 92, 92);
					UnCheckColor = Color.FromArgb(155, 155, 155);
					CheckColor = Color.FromArgb(65, 177, 225);
					DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
					DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
					break;

				case Style.Custom:

					if (StyleManager != null)
					{
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        BorderColor = Utils.HexColor(StyleDictionary["BorderColor"]);
                        DisabledBorderColor = Utils.HexColor(StyleDictionary["DisabledBorderColor"]);
                        SymbolColor = Utils.HexColor(StyleDictionary["SymbolColor"]);
                        UnCheckColor = Utils.HexColor(StyleDictionary["UnCheckColor"]);
                        CheckColor = Utils.HexColor(StyleDictionary["CheckColor"]);
                        DisabledUnCheckColor = Utils.HexColor(StyleDictionary["DisabledUnCheckColor"]);
                        DisabledCheckColor = Utils.HexColor(StyleDictionary["DisabledCheckColor"]);
                    }
					break;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			var rect = new Rectangle(1, 1, 56, 20);

			var rect2 = new Rectangle(3, 3, 52, 16);

            using var backBrush = new SolidBrush(BackgroundColor);
            using var checkback = new SolidBrush(Enabled ? Switched ? CheckColor : UnCheckColor : Switched ? DisabledCheckColor : DisabledUnCheckColor);
            using var checkMarkBrush = new SolidBrush(SymbolColor);
            using var p = new Pen(Enabled ? BorderColor : DisabledBorderColor, 2);
            g.FillRectangle(backBrush, rect);
            g.FillRectangle(checkback, rect2);
            g.DrawRectangle(p, rect);
            g.FillRectangle(checkMarkBrush, new Rectangle((Convert.ToInt32(rect.Width * (_switchLocation / 180.0))), 0, 16, 22));
        }

		public delegate void SwitchedChangedEventHandler(object sender);

		public event SwitchedChangedEventHandler SwitchedChanged;

		/// <summary>
		/// Here we will handle the checking state in runtime.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Switched = !Switched;
			Invalidate();
		}

		/// <summary>
		/// Here we will set the limited height for the control to avoid high and low of the text drawing.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Size = new Size(58, 22);
			Invalidate();
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

		/// <summary>
		/// Specifies the state of a control, such as a check box, that can be checked, unchecked.
		/// </summary>
		[Browsable(false)]
		public Enums.CheckState CheckState
		{
			get { return _checkState; }
			set
			{
				_checkState = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control is checked.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the control is checked.")]
		public bool Switched
		{
			get => _switched;
			set
			{
				if (_switched == value) 
					return;
				_switched = value;
				SwitchedChanged?.Invoke(this);
				_animator.Reverse(!value);
				CheckState = value != true ? Enums.CheckState.Unchecked : Enums.CheckState.Checked;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the border color.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the border color.")]
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
		/// Gets or sets the Checked backColor.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the Checkd backColor.")]
		public Color CheckColor
		{
			get { return _checkColor; }
			set
			{
				_checkColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the border color while the control disabled.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the border color while the control disabled.")]
		public Color DisabledBorderColor
		{
			get { return _disabledBorderColor; }
			set
			{
				_disabledBorderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the CheckedBackColor while disabled.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the CheckdBackColor while disabled.")]
		public Color DisabledCheckColor
		{
			get { return _disabledCheckColor; }
			set
			{
				_disabledCheckColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the Unchecked BackColor while disabled.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the Un-Checkd BackColor while disabled.")]
		public Color DisabledUnCheckColor
		{
			get { return _disabledUnCheckColor; }
			set
			{
				_disabledUnCheckColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets fore color used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets forecolor used by the control.")]
		public override Color ForeColor { get; set; }

		/// <summary>
		/// I make back color inaccessible cause I want it to be just transparent and I used another property for the same job in following properties. 
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets the control back color.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the control backcolor.")]
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

		/// <summary>
		/// Gets or sets the color of the check symbol.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the color of the check symbol.")]
		public Color SymbolColor
		{
			get { return _symbolColor; }
			set
			{
				_symbolColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the Unchecked backColor.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the Un-Checkd backColor.")]
		public Color UnCheckColor
		{
			get { return _unCheckColor; }
			set
			{
				_unCheckColor = value;
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
		/// Disposing Methods.
		/// </summary>
		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}