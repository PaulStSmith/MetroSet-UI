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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Controls;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;
using MetroSet.UI.Native;
using Microsoft.Win32;
using static MetroSet.UI.Native.User32;

namespace MetroSet.UI.Forms
{
	[ToolboxItem(false)]
	[ToolboxBitmap(typeof(MetroSetForm), "Bitmaps.Form.bmp")]
	[DesignerCategory("Form")]
	[DefaultEvent("Load")]
	[DesignTimeVisible(false)]
	[ComVisible(true)]
	[InitializationEvent("Load")]
	public class MetroSetForm : Form, IMetroForm
	{

		protected MetroSetForm()
		{
			Utils.InitControlHandle(this);
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ContainerControl |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			
			User32 = new User32();
			Padding = new Padding(12, 70, 12, 12);
			FormBorderStyle = FormBorderStyle.None;
			_backgroundImageTransparency = 0.90f;
			base.Font = MetroSetFonts.SemiLight(13);
			DropShadowEffect = true;
			_showLeftRect = true;
			_showHeader = false;
			AllowResize = true;
		}

		/// <summary>
		/// Gets a value indicating whether or not the light theme should be used.
		/// </summary>
		public bool UseLightTheme => ((int?)Registry.CurrentUser.OpenSubKey($@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")?.GetValue("AppsUseLightTheme", 1) == 1);

        protected override void OnPaint(PaintEventArgs e)
		{

			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.InterpolationMode = InterpolationMode.High;
			e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

			using (var b = new SolidBrush(BackColor))
			{
				e.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
				if (BackgroundImage != null)
				{
					Methods.DrawImageWithTransparency(e.Graphics, BackgroundImageTransparency, BackgroundImage, ClientRectangle);
				}
			}
			if (ShowBorder)
			{
                using var p = new Pen(BorderColor, BorderThickness);
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            }

			if (ShowLeftRect)
			{
                using var b = new LinearGradientBrush(new Rectangle(0, 25, SmallRectThickness, 35), SmallLineColor1, SmallLineColor2, 90);
                using var textBrush = new SolidBrush(TextColor);
                e.Graphics.FillRectangle(b, new Rectangle(0, 40, SmallRectThickness, 35));
                e.Graphics.DrawString(Text, Font, textBrush, new Point(SmallRectThickness + 10, 46));
            }
			else
			{
				if (ShowHeader)
				{
                    using var b = new SolidBrush(HeaderColor);
                    e.Graphics.FillRectangle(b, new Rectangle(1, 1, Width - 1, HeaderHeight));
                }

				var textBrush = new SolidBrush(TextColor);
				if (ShowTitle)
				{
					switch (TextAlign)
					{
						case TextAlign.Left:
							using (var stringFormat = new StringFormat() { LineAlignment = StringAlignment.Center })
							{
								e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width, HeaderHeight), stringFormat);
							}
							break;

						case TextAlign.Center:
							using (var stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
							{
								e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 21, HeaderHeight), stringFormat);
							}
							break;

						case TextAlign.Right:
							using (var stringFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
							{
								e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 26, HeaderHeight), stringFormat);
							}
							break;
						default:
							throw new InvalidOperationException("");
					}
				}
				textBrush.Dispose();
			}
		}

		/// <summary>
		/// Gets or sets the form border color.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form bordercolor.")]
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
		/// Gets or sets the form text color.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form textcolor.")]
		public Color TextColor
		{
			get { return _textColor; }
			set
			{
				_textColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the form small line color 1.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form small line color 1.")]
		public Color SmallLineColor1
		{
			get { return _smallLineColor1; }
			set
			{
				_smallLineColor1 = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the form small line color 2.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form small line color 2.")]
		public Color SmallLineColor2
		{
			get { return _smallLineColor2; }
			set
			{
				_smallLineColor2 = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the header color.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the header color.")]
		public Color HeaderColor
		{
			get { return _headerColor; }
			set
			{
				_headerColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the width of the small rectangle on top left of the window.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the width of the small rectangle on top left of the window.")]
		public int SmallRectThickness
		{
			get { return _smallRectThickness; }
			set
			{
				_smallRectThickness = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets whether the border be shown.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets whether the border be shown.")]
		[DefaultValue(true)]
		public bool ShowBorder
		{
			get { return _showBorder; }
			set
			{
				_showBorder = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the border thickness.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the border thickness.")]
		public float BorderThickness
		{
			get { return _borderThickness; }
			set
			{
				_borderThickness = value;
				Refresh();
			}
		}

		/// <summary>Gets or sets the border style of the form.</summary>
		[DefaultValue(FormBorderStyle.None)]
		[Browsable(false)]
		private new FormBorderStyle FormBorderStyle
		{
			set
			{
				if (!Enum.IsDefined(typeof(FormBorderStyle), value))
					throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(FormBorderStyle));
				base.FormBorderStyle = FormBorderStyle.None;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a Maximize button for the form; otherwise, false. The default is true.</returns>
		[Category("WindowStyle")]
		[Browsable(false)]
		[DefaultValue(false)]
		[Description("FormMaximizeBox")]
		public new bool MaximizeBox => false;

		/// <summary>Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a Minimize button for the form; otherwise, false. The default is true.</returns>
		[Category("WindowStyle")]
		[Browsable(false)]
		[DefaultValue(false)]
		[Description("FormMinimizeBox")]
		public new bool MinimizeBox => false;

		/// <summary>
		/// Gets or sets whether the title be shown.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets whether the title be shown.")]
		public bool ShowTitle
		{
			get { return _showTitle; }
			set
			{
				_showTitle = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the title alignment.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the title alignment.")]
		public TextAlign TextAlign
		{
			get { return _textAlign; }
			set
			{
				_textAlign = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets whether show the header.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets whether show the header.")]
		public bool ShowHeader
		{
			get => _showHeader;
			set
			{
				_showHeader = value;
				if (value)
				{
					ShowLeftRect = false;
					Padding = new Padding(2, HeaderHeight + 30, 2, 2);
					Text = Text.ToUpper();
					TextColor = Color.White;
					ShowTitle = true;
					foreach (Control c in Controls)
					{
						if (c.GetType() != typeof(MetroSetControlBox))
							continue;
						c.BringToFront();
						c.Location = new Point(Width - 12, 11);
					}
				}
				else
				{
					Padding = new Padding(12, 90, 12, 12);
					ShowTitle = false;
				}
				base.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets whether the small rectangle on top left of the window be shown.
		/// </summary>
		[Category("MetroSet Framework"),
		 Description("Gets or sets whether the small rectangle on top left of the window be shown.")]
		public bool ShowLeftRect
		{
			get => _showLeftRect;
			set
			{
				_showLeftRect = value;
				if (value)
				{
					ShowHeader = false;
				}
				base.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets whether the form can be move or not.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets whether the form can be move or not."), DefaultValue(true)]
		public bool Moveable
		{
			get { return _movable; }
			set
			{
				_movable = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets whether the form use animation.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets whether the form use animation.")]
		public bool UseSlideAnimation
		{
			get { return _useSlideAnimation; }
			set
			{
				_useSlideAnimation = value;
				Refresh();
			}
		}

		[Browsable(false)]
		public new Padding Padding
		{
			get => base.Padding;
			set => base.Padding = value;
		}

		/// <summary>
		/// Gets or sets the background image transparency.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the backgroundimage transparency.")]
		public float BackgroundImageTransparency
		{
			get => _backgroundImageTransparency;
			set
			{
				if (value > 1)
					throw new Exception("The Value must be between 0-1.");

				_backgroundImageTransparency = value;
				base.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the header height.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the header height.")]
		public int HeaderHeight
		{
			get { return _headerHeight; }
			set
			{
				_headerHeight = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the background image displayed in the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the background image displayed in the control.")]
		public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

		/// <summary>
		/// Gets or sets whether the drop shadow effect apply on form.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets whether the drop shadow effect apply on form.")]
		public bool DropShadowEffect
		{
			get { return _dropShadowEffect; }
			set
			{
				_dropShadowEffect = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets whether the user be able to resize the form or not.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets whether the user be able to resize the form or not.")]
		public bool AllowResize
		{
			get { return _allowResize; }
			set
			{
				_allowResize = value;
				Refresh();
			}
		}

		/// <summary>
		/// Allows the user to resize the form at runtime.
		/// Credits : dizzy.stackoverflow
		/// </summary>
		/// <param name="message">Windows Message.</param>
		private void ResizeForm(ref Message message)
		{
			if (!AllowResize)
				return;
			var x = (int)(message.LParam.ToInt64() & 65535);
			var y = (int)((message.LParam.ToInt64() & -65536) >> 0x10);
			var point = PointToClient(new Point(x, y));

			if (point.Y >= Height - 0x10)
			{
				if (point.X >= Width - 0x10)
				{
					message.Result = (IntPtr)(IsMirrored ? 0x10 : 0x11);
					return;
				}

				if (point.X <= 0x10)
				{
					message.Result = (IntPtr)(IsMirrored ? 0x11 : 0x10);
					return;
				}
			}
			else if (point.Y <= 0x10)
			{
				if (point.X <= 0x10)
				{
					message.Result = (IntPtr)(IsMirrored ? 0xe : 0xd);
					return;
				}

				if (point.X >= Width - 0x10)
				{
					message.Result = (IntPtr)(IsMirrored ? 0xd : 0xe);
					return;
				}
			}

			if (point.Y <= 0x10)
			{
				message.Result = (IntPtr)0xc;
				return;
			}

			if (point.Y >= Height - 0x10)
			{
				message.Result = (IntPtr)0xf;
				return;
			}

			if (point.X <= 0x10)
			{
				message.Result = (IntPtr)0xa;
				return;
			}

			if (point.X >= Width - 0x10)
			{
				message.Result = (IntPtr)0xb;
			}

		}

		/// <summary>
		/// Gets or sets the style associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the style associated with the control."), DefaultValue(Style.Light)]
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
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}
				base.Invalidate();
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
				base.Invalidate();
			}
		}

		private readonly User32 User32;

		private Style _style;
		private StyleManager _styleManager;
		private bool _showLeftRect;
		private bool _showHeader;
		private float _backgroundImageTransparency;

		private Color _borderColor;
		private Color _textColor;
		private Color _smallLineColor1;
		private Color _smallLineColor2;
		private Color _headerColor;
		private int _smallRectThickness = 10;
		private bool _showBorder = true;
		private float _borderThickness = 1;
		private bool _showTitle = true;
		private TextAlign _textAlign = TextAlign.Left;
		private bool _movable = true;
		private bool _useSlideAnimation;
		private int _headerHeight = 40;
		private bool _dropShadowEffect;
		private bool _allowResize;

		/// <summary>
		/// Applies the theme to the <see cref="MetroSetForm"/>.
		/// </summary>
		/// <param name="style">The Style.</param>
		protected internal virtual void ApplyTheme(Style style = Style.Light)
		{
			_SuspendRefresh = true;
			switch (style)
			{
				case Style.Light:
					ForeColor = Color.Gray;
					BackColor = Color.White;
					BorderColor = Color.FromArgb(65, 177, 225);
					TextColor = ShowHeader ? Color.White : Color.Gray;
					SmallLineColor1 = Color.FromArgb(65, 177, 225);
					SmallLineColor2 = Color.FromArgb(65, 177, 225);
					HeaderColor = Color.FromArgb(65, 177, 225);
					break;

				case Style.Dark:
					ForeColor = Color.White;
					BackColor = Color.FromArgb(30, 30, 30);
					BorderColor = Color.FromArgb(65, 177, 225);
					SmallLineColor1 = Color.FromArgb(65, 177, 225);
					SmallLineColor2 = Color.FromArgb(65, 177, 225);
					HeaderColor = Color.FromArgb(65, 177, 225);
					TextColor = ShowHeader ? Color.Gray : Color.White;
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
						var dic = StyleManager.StyleDictionary(ControlKind.Form);
                        TextColor = Utils.HexColor(dic["TextColor"]);
                        ForeColor = Utils.HexColor(dic["ForeColor"]);
                        BorderColor = Utils.HexColor(dic["BorderColor"]);
                        HeaderColor = Utils.HexColor(dic["HeaderColor"]);
                        BackColor = Utils.HexColor(dic["BackColor"]);
                        SmallLineColor1 = Utils.HexColor(dic["SmallLineColor1"]);
                        SmallLineColor2 = Utils.HexColor(dic["SmallLineColor2"]);
                        SmallRectThickness = int.Parse((string)dic["SmallRectThickness"]);
                    }
					break;
			}
			_SuspendRefresh = false;
            Refresh();
		}

		private bool _SuspendRefresh;

		/// <inheritdoc/>
        public override void Refresh()
        {
			if (_SuspendRefresh)
                return;	
            base.Refresh();
        }

        /// <summary>
        /// Handling windows messages.
        /// </summary>
        /// <param name="message">Windows Messages</param>
        protected override void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			if ((message.Msg != _WM_NCHITTEST) | !Moveable)
				return;

			// Allow users to move the form.
			if ((int)message.Result == _HTCLIENT)
				message.Result = new IntPtr(_HTCAPTION);

			// Allow users to resize the form.
			ResizeForm(ref message);

		}

		protected override void OnHandleCreated(EventArgs e)
		{
			AutoScaleMode = AutoScaleMode.None;
			base.OnHandleCreated(e);
		}

		/// <summary>
		/// Make the drop shadow effect on form in case drop shadow property set to 'true'.
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				if (!DropShadowEffect)
					return base.CreateParams;
				var cp = base.CreateParams;
				cp.ClassStyle |= _CS_DROPSHADOW;
				return cp;
			}
		}

		/// <summary>
		/// Fade in effect on form while loading.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ApplyTheme(Style);
			if (this.StartPosition == FormStartPosition.CenterScreen)
                this.CenterToScreen();

			// https://www.codeproject.com/Articles/30255/C-Fade-Form-Effect-With-the-AnimateWindow-API-Func
			AnimateWindow(Handle, 800, AnimateWindowFlags.AW_ACTIVATE | (UseSlideAnimation ?
				  AnimateWindowFlags.AW_HOR_POSITIVE | AnimateWindowFlags.AW_SLIDE : AnimateWindowFlags.AW_BLEND));
		}

		/// <summary>
		/// Fade out effect on form while loading.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			// https://www.codeproject.com/Articles/30255/C-Fade-Form-Effect-With-the-AnimateWindow-API-Func
			if (e.Cancel == false)
			{
				AnimateWindow(Handle, 800, User32.AW_HIDE | (UseSlideAnimation ?
							  AnimateWindowFlags.AW_HOR_NEGATIVE | AnimateWindowFlags.AW_SLIDE : AnimateWindowFlags.AW_BLEND));
			}
		}

	}
}