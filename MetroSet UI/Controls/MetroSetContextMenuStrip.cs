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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;
using Microsoft.Win32;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetContextMenuStrip), "Bitmaps.ContextMenu.bmp")]
	[DefaultEvent("Opening")]
	[ComVisible(true)]
	public class MetroSetContextMenuStrip : ContextMenuStrip, IMetroSetControl
	{
		private ToolStripItemClickedEventArgs _clickedEventArgs;

		public MetroSetContextMenuStrip()
		{
			
			ApplyTheme();
			Renderer = new MetroSetToolStripRender();
		}

        /// <summary>
        /// Gets a value indicating whether or not the light theme should be used.
        /// </summary>
        public static bool UseLightTheme => Utils.UseLightTheme;

        /// <inheritdoc />
        public Style Style
        {
            get => (Style)(_style ?? StyleManager?.Style);
            set
            {
                if (_style != value)
                    ApplyTheme((Style)(_style = value));
            }
        }
        private Style? _style = UseLightTheme ? Style.Light : Style.Dark;

        /// <inheritdoc />
        public StyleManager StyleManager
        {
            get => _StyleManager;
            set
            {
                if (value != _StyleManager)
                {
                    _StyleManager = value;
                    Refresh();
                }
            }
        }
        private StyleManager _StyleManager;

        /// <inheritdoc />
        public bool IsCustomStyle
        {
            get => _IsCustomStyle;
            set
            {
                if (value == _IsCustomStyle)
                    return;
                _IsCustomStyle = value;
                Refresh();
            }
        }
        private bool _IsCustomStyle = false;

		/// <summary>
		/// Gets the style dictionary for the control.
		/// </summary>
		public IDictionary<string, object> StyleDictionary => _StyleManager?.StyleDictionary(ControlKind.ContextMenuStrip);

        /// <summary>
        /// Applies the current style to the control.
        /// </summary>
        public void ApplyTheme()
        {
            if (IsCustomStyle)
                return;
            ApplyTheme(Style);
        }

        /// <summary>
        /// Applies the specified style to the control.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        public void ApplyTheme(Style style)
        {
            if (IsCustomStyle)
                return;
            SuspendLayout();
            switch (style)
			{
				case Style.Light:
					ForegroundColor = Color.FromArgb(170, 170, 170);
					BackgroundColor = Color.White;
					ArrowColor = Color.Gray;
					SelectedItemBackColor = Color.FromArgb(65, 177, 225);
					SelectedItemColor = Color.White;
					SeparatorColor = Color.LightGray;
					DisabledForeColor = Color.Silver;
					break;

				case Style.Dark:
					ForegroundColor = Color.FromArgb(170, 170, 170);
					BackgroundColor = Color.FromArgb(30, 30, 30);
					ArrowColor = Color.Gray;
					SelectedItemBackColor = Color.FromArgb(65, 177, 225);
					SelectedItemColor = Color.White;
					SeparatorColor = Color.Gray;
					DisabledForeColor = Color.Silver;
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        ForeColor = Utils.HexColor(StyleDictionary["ForeColor"]);
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        ArrowColor = Utils.HexColor(StyleDictionary["ArrowColor"]);
                        SeparatorColor = Utils.HexColor(StyleDictionary["SeparatorColor"]);
                        SelectedItemColor = Utils.HexColor(StyleDictionary["SelectedItemColor"]);
                        SelectedItemBackColor = Utils.HexColor(StyleDictionary["SelectedItemBackColor"]);
                        DisabledForeColor = Utils.HexColor(StyleDictionary["DisabledForeColor"]);
                    }
					break;
			}
			ResumeLayout();
			Refresh();
		}

		/// <summary>
		/// Gets or sets ForegroundColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets forecolor used by the control.")]
		[DisplayName("ForeColor")]
		public static Color ForegroundColor { get; set; }

		/// <summary>
		/// Gets or sets BackgroundColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets backcolor used by the control.")]
		[DisplayName("BackColor")]
		public static Color BackgroundColor { get; set; }

		/// <summary>
		/// Gets or sets separator color used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets separator color used by the control.")]
		public static Color SeparatorColor { get; set; }

		/// <summary>
		/// Gets or sets Arrow color used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets arrowcolor used by the control.")]
		public static Color ArrowColor { get; set; }

		/// <summary>
		/// Gets or sets SelectedItem color used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets selecteditem color used by the control.")]
		public static Color SelectedItemColor { get; set; }

		/// <summary>
		/// Gets or sets SelectedItem BackColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets selecteditem backcolor used by the control.")]
		public static Color SelectedItemBackColor { get; set; }

		/// <summary>
		/// Gets or sets disabled forecolor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets disabled forecolor used by the control.")]
		public static Color DisabledForeColor { get; set; }

		public static new Font Font => MetroSetFonts.UIRegular(10);

		public event ClickedEventHandler Clicked;
		public delegate void ClickedEventHandler(object sender);

		/// <summary>
		/// Here we handle whenever and item clicked.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			if ((e.ClickedItem == null) || e.ClickedItem is ToolStripSeparator)
				return;
			if (ReferenceEquals(e, _clickedEventArgs))
				OnItemClicked(e);
			else
			{
				_clickedEventArgs = e;
				Clicked?.Invoke(this);
			}
		}

		/// <summary>
		/// Here we handle mouse hover event.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			Cursor = Cursors.Hand;
			base.Invalidate();
		}

		/// <summary>
		/// Here we handle mouse up event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Cursor = Cursors.Hand;
			base.Invalidate();
		}

		private sealed class MetroSetToolStripRender : ToolStripProfessionalRenderer
		{

			/// <summary>
			/// Here we draw item text.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				var textRect = new Rectangle(25, e.Item.ContentRectangle.Y, e.Item.ContentRectangle.Width - (24 + 16), e.Item.ContentRectangle.Height - 4);
                using var b = new SolidBrush(e.Item.Enabled ? e.Item.Selected ? SelectedItemColor : ForegroundColor : DisabledForeColor);
                e.Graphics.DrawString(e.Text, Font, b, textRect);
            }

			/// <summary>
			/// Here we draw toolstrip background.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
			{
				base.OnRenderToolStripBackground(e);
				e.Graphics.Clear(BackgroundColor);
			}

			/// <summary>
			/// Here we draw toolstrip menu item background.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				e.Graphics.InterpolationMode = InterpolationMode.High;
				e.Graphics.Clear(BackgroundColor);
				var r = new Rectangle(0, e.Item.ContentRectangle.Y - 2, e.Item.ContentRectangle.Width + 4, e.Item.ContentRectangle.Height + 3);
                using var b = new SolidBrush(e.Item.Selected && e.Item.Enabled ? SelectedItemBackColor : BackgroundColor);
                e.Graphics.FillRectangle(b, r);
            }

			protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
			{
				//MyBase.OnRenderImageMargin(e)
				//I Make above line comment which makes users to be able to add images to ToolStrips
			}

			/// <summary>
			/// Here we draw toolstrip separators.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
			{
                using var p = new Pen(SeparatorColor);
                e.Graphics.DrawLine(p, new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2), new Point(e.Item.Bounds.Right - 5, e.Item.Bounds.Height / 2));
            }

			/// <summary>
			/// Here we draw the toolstrip arrows.
			/// </summary>
			/// <param name="e">ToolStripArrowRenderEventArgs</param>
			protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
			{
				var arrowX = e.ArrowRectangle.X + e.ArrowRectangle.Width / 2;
				var arrowY = e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2;
				var arrowPoints = new[]
				{
				new Point(arrowX - 5, arrowY - 5),
				new Point(arrowX, arrowY),
				new Point(arrowX - 5, arrowY + 5)
				};

                using var arrowBrush = new SolidBrush(e.Item.Enabled ? ArrowColor : DisabledForeColor);
                e.Graphics.FillPolygon(arrowBrush, arrowPoints);
            }

		}

	}
}