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
	[ToolboxBitmap(typeof(MetroSetProgressBar), "Bitmaps.Progress.bmp")]
	[Designer(typeof(MetroSetProgressBarDesigner))]
	[DefaultEvent("ValueChanged")]
	[DefaultProperty("Value")]
	[ComVisible(true)]
	public class MetroSetProgressBar : MetroSetControl
	{
        private int _value;
        private int _currentValue;

        private int _maximum = 100;
        private int _minimum;
        private ProgressOrientation _orientation = ProgressOrientation.Horizontal;
        private Color _backgroundColor;
        private Color _borderColor;
        private Color _progressColor;
        private Color _disabledProgressColor;
        private Color _disabledBackColor;
        private Color _disabledBorderColor;
        
		public MetroSetProgressBar() : base(ControlKind.ProgressBar)
		{
			SetStyle(
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			
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
					ProgressColor = Color.FromArgb(65, 177, 225);
					BorderColor = Color.FromArgb(238, 238, 238);
					BackgroundColor = Color.FromArgb(238, 238, 238);
					DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
					DisabledBorderColor = Color.FromArgb(238, 238, 238);
					DisabledBackColor = Color.FromArgb(238, 238, 238);
					break;

				case Style.Dark:
					ProgressColor = Color.FromArgb(65, 177, 225);
					BackgroundColor = Color.FromArgb(38, 38, 38);
					BorderColor = Color.FromArgb(38, 38, 38);
					DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
					DisabledBackColor = Color.FromArgb(38, 38, 38);
					DisabledBorderColor = Color.FromArgb(38, 38, 38);
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        ProgressColor = Utils.HexColor(StyleDictionary["ProgressColor"]);
                        BorderColor = Utils.HexColor(StyleDictionary["BorderColor"]);
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        DisabledBackColor = Utils.HexColor(StyleDictionary["DisabledBackColor"]);
                        DisabledBorderColor = Utils.HexColor(StyleDictionary["DisabledBorderColor"]);
                        DisabledProgressColor = Utils.HexColor(StyleDictionary["DisabledProgressColor"]);
                    }
					break;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using var bg = new SolidBrush(Enabled ? BackgroundColor : DisabledBackColor);
            using var p = new Pen(Enabled ? BorderColor : DisabledBorderColor);
            using var ps = new SolidBrush(Enabled ? ProgressColor : DisabledProgressColor);
            g.FillRectangle(bg, rect);
            if (_currentValue != 0)
            {
                switch (Orientation)
                {
                    case ProgressOrientation.Horizontal:
                        g.FillRectangle(ps, new Rectangle(0, 0, _currentValue - 1, Height - 1));
                        break;
                    case ProgressOrientation.Vertical:
                        g.FillRectangle(ps, new Rectangle(0, Height - _currentValue, Width - 1, _currentValue - 1));
                        break;
                    default:
						throw new InvalidOperationException();
                }
            }
            g.DrawRectangle(p, rect);
        }

		/// <summary>
		/// Gets or sets the current position of the progressbar.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the current position of the progressbar.")]
		public int Value
		{
			get => _value < 0 ? 0 : _value;
			set
			{
				if (value > Maximum)
				{
					value = Maximum;
				}
				_value = value;
				RenewCurrentValue();
				ValueChanged?.Invoke(this);
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the maximum value of the progressbar.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the maximum value of the progressbar.")]
		public int Maximum
		{
			get { return _maximum; }
			set
			{
				_maximum = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the minimum value of the progressbar.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the minimum value of the progressbar.")]
		public int Minimum
		{
			get { return _minimum; }
			set
			{
				_minimum = value;
				Refresh();
			}
		}

		[Browsable(false)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets the minimum value of the progressbar.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the minimum value of the progressbar.")]
		public ProgressOrientation Orientation
		{
			get { return _orientation; }
			set
			{
				_orientation = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the control BackColor.
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
		/// Gets or sets the progress color of the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the progress color of the control.")]
		public Color ProgressColor
		{
			get { return _progressColor; }
			set
			{
				_progressColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the Progress color of the control whenever while disabled
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the progresscolor of the control whenever while disabled.")]
		public Color DisabledProgressColor
		{
			get { return _disabledProgressColor; }
			set
			{
				_disabledProgressColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets disabled BackColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets disabled backcolor used by the control.")]
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

		public event ValueChangedEventHandler ValueChanged;
		public delegate void ValueChangedEventHandler(object sender);

		/// <summary>
		/// Here we handle the current value.
		/// </summary>
		private void RenewCurrentValue()
		{
			if (Orientation == ProgressOrientation.Horizontal)
				_currentValue = (int)Math.Round((Value - Minimum) / (double)(Maximum - Minimum) * (Width - 1));
			else
				_currentValue = Convert.ToInt32(Value / (double)Maximum * Height - 1);
		}

	}
}