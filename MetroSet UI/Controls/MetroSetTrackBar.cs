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
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetTrackBar), "Bitmaps.Slider.bmp")]
	[Designer(typeof(MetroSetTrackBarDesigner))]
	[DefaultProperty("Value")]
	[DefaultEvent("Scroll")]
	[ComVisible(true)]
	public class MetroSetTrackBar : MetroSetControl
	{
		private bool _variable;
		private Rectangle _track;
		private int _maximum;
		private int _minimum;
		private int _value;
		private int _currentValue;

		private Color _valueColor;
		private Color _handlerColor;
		private Color _backgroundColor;
		private Color _disabledValueColor;
		private Color _disabledBackColor;
		private Color _disabledBorderColor;
		private Color _disabledHandlerColor;

		public MetroSetTrackBar() : base(ControlKind.TrackBar)
		{
			SetStyle(
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			_maximum = 100;
			_minimum = 0;
			_value = 0;
			_currentValue = Convert.ToInt32(Value / (double)(Maximum) - (2 * Width));
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
					HandlerColor = Color.FromArgb(180, 180, 180);
					BackgroundColor = Color.FromArgb(205, 205, 205);
					ValueColor = Color.FromArgb(65, 177, 225);
					DisabledBackColor = Color.FromArgb(235, 235, 235);
					DisabledValueColor = Color.FromArgb(205, 205, 205);
					DisabledHandlerColor = Color.FromArgb(196, 196, 196);
					UpdateProperties();
					break;

				case Style.Dark:
					HandlerColor = Color.FromArgb(143, 143, 143);
					BackgroundColor = Color.FromArgb(90, 90, 90);
					ValueColor = Color.FromArgb(65, 177, 225);
					DisabledBackColor = Color.FromArgb(80, 80, 80);
					DisabledValueColor = Color.FromArgb(109, 109, 109);
					DisabledHandlerColor = Color.FromArgb(90, 90, 90);
					UpdateProperties();
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        HandlerColor = Utils.HexColor(StyleDictionary["HandlerColor"]);
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        ValueColor = Utils.HexColor(StyleDictionary["ValueColor"]);
                        DisabledBackColor = Utils.HexColor(StyleDictionary["DisabledBackColor"]);
                        DisabledValueColor = Utils.HexColor(StyleDictionary["DisabledValueColor"]);
                        DisabledHandlerColor = Utils.HexColor(StyleDictionary["DisabledHandlerColor"]);
                    }
					break;
			}
		}

		private void UpdateProperties()
		{
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;

			Cursor = Cursors.Hand;

            using var bg = new SolidBrush(Enabled ? BackgroundColor : DisabledBackColor);
            using var v = new SolidBrush(Enabled ? ValueColor : DisabledValueColor);
            using var vc = new SolidBrush(Enabled ? HandlerColor : DisabledHandlerColor);
            g.FillRectangle(bg, new Rectangle(0, 6, Width, 4));
            if (_currentValue != 0)
                g.FillRectangle(v, new Rectangle(0, 6, _currentValue, 4));
            g.FillRectangle(vc, _track);
        }

		/// <summary>
		/// Gets or sets the upper limit of the range this TrackBar is working with.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the upper limit of the range this TrackBar is working with.")]
		public int Maximum
		{
			get => _maximum;
			set
			{
				_maximum = value;
				RenewCurrentValue();
				MoveTrack();
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the lower limit of the range this TrackBar is working with.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the lower limit of the range this TrackBar is working with.")]
		public int Minimum
		{
			get => _minimum;
			set
			{
				if (!(value < 0))
				{
					_minimum = value;
					RenewCurrentValue();
					MoveTrack();
					Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets a numeric value that represents the current position of the scroll box on the track bar.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a numeric value that represents the current position of the scroll box on the track bar.")]
		public int Value
		{
			get => _value;
			set
			{
				if (value != _value)
				{
					_value = value;
					RenewCurrentValue();
					MoveTrack();
					Invalidate();
					Scroll?.Invoke(this);
				}
			}
		}

		[Browsable(false)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets the value color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework"), Description(" Gets or sets the value color in normal mouse sate.")]
		public Color ValueColor
		{
			get { return _valueColor; }
			set
			{
				_valueColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the handler color.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the handler color.")]
		public Color HandlerColor
		{
			get { return _handlerColor; }
			set
			{
				_handlerColor = value;
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
		/// Gets or sets the value of the control whenever while disabled
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the value of the control whenever while disabled.")]
		public Color DisabledValueColor
		{
			get { return _disabledValueColor; }
			set
			{
				_disabledValueColor = value;
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

		/// <summary>
		/// Gets or sets the handler color while the control disabled.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the handler color while the control disabled.")]
		public Color DisabledHandlerColor
		{
			get { return _disabledHandlerColor; }
			set
			{
				_disabledHandlerColor = value;
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

		public event ScrollEventHandler Scroll;
		public delegate void ScrollEventHandler(object sender);

		/// <summary>
		/// Handling mouse move event so that we can handle the thumb value.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_variable && e.X > -1 && e.X < Width + 1)
			{
				Value = Minimum + (int)Math.Round((double)(Maximum - Minimum) * e.X / Width);
			}
			base.OnMouseMove(e);
		}

		/// <summary>
		/// Handling mouse down event so that we can put the thumb in clicked state.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && Height > 0)
			{
				RenewCurrentValue();
				_track = new Rectangle(_currentValue, 0, 6, 16);
				_variable = new Rectangle(_currentValue, 0, 6, 16).Contains(e.Location);
			}
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Handling mouse up event.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			_variable = false;
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Handling key press event so that we can change the track value by keys.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
			{
				if (Value != 0)
				{
					Value -= 1;
				}

			}
			else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
			{
				if (Value != Maximum)
				{
					Value += 1;
				}

			}
			base.OnKeyDown(e);
		}

		/// <summary>
		/// Handling the height and value of the track while resizing the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			RenewCurrentValue();
			MoveTrack();
			Height = 16;
			Invalidate();
			base.OnResize(e);
		}

		/// <summary>
		/// The Method to provide the track value.
		/// </summary>
		private void MoveTrack()
		{
			_track = new Rectangle(_currentValue, 0, 6, 16);
		}

		/// <summary>
		/// The Method to renew the value of the track.
		/// </summary>
		public void RenewCurrentValue()
		{
			_currentValue = Convert.ToInt32(Math.Round((double)(Value - Minimum) / (Maximum - Minimum) * (Width - 6)));
			Invalidate();
		}

	}
}