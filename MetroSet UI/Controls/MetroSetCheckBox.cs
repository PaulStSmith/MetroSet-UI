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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MetroSet.UI.Animates;
using MetroSet.UI.Components;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;
using MetroSet.UI.Native;

namespace MetroSet.UI.Controls
{

	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetCheckBox), "Bitmaps.CheckBox.bmp")]
	[Designer(typeof(MetroSetCheckBoxDesigner))]
	[DefaultEvent("CheckedChanged")]
	[DefaultProperty("Checked")]
	public class MetroSetCheckBox : MetroSetControl, IDisposable
	{

		private bool _checked;
		private readonly IntAnimate _animator;
		private SignStyle _signStyle = SignStyle.Sign;
		private Enums.CheckState _checkState;
		private Color _backgroundColor;
		private Color _borderColor;
		private Color _disabledBorderColor;
		private Color _checkSignColor;

		public MetroSetCheckBox() : base(ControlKind.CheckBox)
		{
			SetStyle(
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			base.Font = MetroSetFonts.Light(10);
			base.Cursor = Cursors.Hand;
			base.BackColor = Color.Transparent;
			_animator = new IntAnimate();
			_animator.Setting(100, 0, 255);
            _animator.Update = (alpha) => base.Invalidate();
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
					BackgroundColor = Color.White;
					BorderColor = Color.FromArgb(155, 155, 155);
					DisabledBorderColor = Color.FromArgb(205, 205, 205);
					CheckSignColor = Color.FromArgb(65, 177, 225);
					break;

				case Style.Dark:
					ForeColor = Color.FromArgb(170, 170, 170);
					BackgroundColor = Color.FromArgb(30, 30, 30);
					BorderColor = Color.FromArgb(155, 155, 155);
					DisabledBorderColor = Color.FromArgb(85, 85, 85);
					CheckSignColor = Color.FromArgb(65, 177, 225);
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        ForeColor = Utils.HexColor(StyleDictionary["ForeColor"]);
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        BorderColor = Utils.HexColor(StyleDictionary["BorderColor"]);
                        DisabledBorderColor = Utils.HexColor(StyleDictionary["DisabledBorderColor"]);
                        CheckSignColor = Utils.HexColor(StyleDictionary["CheckColor"]);
                        SignStyle = Enum.Parse<SignStyle>((string)StyleDictionary["CheckedStyle"]);
                    }
					break;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			var rect = new Rectangle(0, 0, 16, 15);
			var alpha = _animator.Value;

            using var backBrush = new SolidBrush(Enabled ? BackgroundColor : Color.FromArgb(238, 238, 238));
            using var checkMarkPen = new Pen(Enabled ? Checked || _animator.Active ? Color.FromArgb(alpha, CheckSignColor) : BackgroundColor : Color.FromArgb(alpha, DisabledBorderColor), 2);
            using var checkMarkBrush = new SolidBrush(Enabled ? Checked || _animator.Active ? Color.FromArgb(alpha, CheckSignColor) : BackgroundColor : DisabledBorderColor);
            using var p = new Pen(Enabled ? BorderColor : DisabledBorderColor);
            using var sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            using var tb = new SolidBrush(ForeColor);
            g.FillRectangle(backBrush, rect);
            g.DrawRectangle(Enabled ? p : checkMarkPen, rect);
            DrawSymbol(g, checkMarkPen, checkMarkBrush);
            g.DrawString(Text, Font, tb, new Rectangle(19, 2, Width, Height - 4), sf);
        }

		private void DrawSymbol(Graphics g, Pen pen, SolidBrush solidBrush)
		{
			if (solidBrush == null)
				throw new ArgumentNullException(nameof(solidBrush));
			if (SignStyle == SignStyle.Sign)
			{
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.DrawLines(pen, new[]
				{
					new Point(3, 7),
					new Point(7, 10),
					new Point(13, 3)
				});
				g.SmoothingMode = SmoothingMode.None;
			}
			else
			{
				g.FillRectangle(solidBrush, new Rectangle(3, 3, 11, 10));
			}
		}

		public event CheckedChangedEventHandler CheckedChanged;

		public delegate void CheckedChangedEventHandler(object sender);

		/// <summary>
		/// Here we will handle the checking state in runtime.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Checked = !Checked;
			base.Invalidate();
		}

		/// <summary>
		/// Here we will set the limited height for the control to avoid high and low of the text drawing.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 16;
			base.Invalidate();
		}

		/// <summary>
		/// Here we set the mouse hand smooth.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == User32.WM_SETCURSOR)
			{
				User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
				m.Result = IntPtr.Zero;
				return;
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control is checked.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the control is checked.")]
		public bool Checked
		{
			get => _checked;
			set
			{
				if (_checked == value)
					return;

				_checked = value;
				CheckedChanged?.Invoke(this);
				_animator.Reverse(!value);
				CheckState = value ? Enums.CheckState.Checked : Enums.CheckState.Unchecked;
				base.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the the sign style of check.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the the sign style of check.")]
		public SignStyle SignStyle
		{
			get { return _signStyle; }
			set
			{
				_signStyle = value;
				Refresh();
			}
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
		/// Gets or sets ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form forecolor.")]
		public override Color ForeColor { get; set; }

		/// <summary>
		/// I make BackColor inaccessible cause I want it to be just transparent and I used another property for the same job in following properties. 
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets the form BackColor.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form backcolor.")]
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
		/// Gets or sets the color of the check symbol.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the color of the check symbol.")]
		public Color CheckSignColor
		{
			get { return _checkSignColor; }
			set
			{
				_checkSignColor = value;
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