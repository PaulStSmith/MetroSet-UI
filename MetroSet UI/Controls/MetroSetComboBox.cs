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
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetComboBox), "Bitmaps.ComoBox.bmp")]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[ComVisible(true)]
	public class MetroSetComboBox : ComboBox, IMetroSetControl
	{
        private Color _backgroundColor;
		private Color _borderColor;
		private Color _arrowColor;
		private Color _selectedItemForeColor;
		private Color _selectedItemBackColor;
		private Color _disabledBackColor;
		private Color _disabledForeColor;
		private Color _disabledBorderColor;

		public MetroSetComboBox()
		{
			SetStyle
				(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor,
				true);
			UpdateStyles();
			base.Font = MetroSetFonts.Regular(11);
			base.BackColor = Color.Transparent;
			base.AllowDrop = true;
			DrawMode = DrawMode.OwnerDrawFixed;
			ItemHeight = 20;
			CausesValidation = false;
			DropDownStyle = ComboBoxStyle.DropDownList;

			ApplyTheme();
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
        public IDictionary<string, object> StyleDictionary => _StyleManager?.StyleDictionary(ControlKind.ComboBox);

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
                    ForeColor = Color.FromArgb(20, 20, 20);
                    BackgroundColor = Color.FromArgb(238, 238, 238);
                    BorderColor = Color.FromArgb(150, 150, 150);
                    ArrowColor = Color.FromArgb(150, 150, 150);
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemForeColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    break;

                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackgroundColor = Color.FromArgb(34, 34, 34);
                    BorderColor = Color.FromArgb(110, 110, 110);
                    ArrowColor = Color.FromArgb(110, 110, 110);
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemForeColor = Color.White;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                    {
                        ForeColor = Utils.HexColor(StyleDictionary["ForeColor"]);
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        BorderColor = Utils.HexColor(StyleDictionary["BorderColor"]);
                        ArrowColor = Utils.HexColor(StyleDictionary["ArrowColor"]);
                        SelectedItemBackColor = Utils.HexColor(StyleDictionary["SelectedItemBackColor"]);
                        SelectedItemForeColor = Utils.HexColor(StyleDictionary["SelectedItemForeColor"]);
                        DisabledBackColor = Utils.HexColor(StyleDictionary["DisabledBackColor"]);
                        DisabledBorderColor = Utils.HexColor(StyleDictionary["DisabledBorderColor"]);
                        DisabledForeColor = Utils.HexColor(StyleDictionary["DisabledForeColor"]);
                    }
                    break;
            }
            ResumeLayout();
            Refresh();
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
		/// Gets or sets the form backcolor.
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
		/// Gets or sets border color used by the control
		/// </summary>
		[Category("MetroSet Framework")]
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
		/// Gets or sets arrow color used by the control
		/// </summary>
		[Category("MetroSet Framework")]
		public Color ArrowColor
		{
			get { return _arrowColor; }
			set
			{
				_arrowColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets ForeColor of the selected item used by the control
		/// </summary>
		[Category("MetroSet Framework")]
		public Color SelectedItemForeColor
		{
			get { return _selectedItemForeColor; }
			set
			{
				_selectedItemForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets BackColor of the selected item used by the control
		/// </summary>
		[Category("MetroSet Framework")]
		public Color SelectedItemBackColor
		{
			get { return _selectedItemBackColor; }
			set
			{
				_selectedItemBackColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets BackColor used by the control while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
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
		/// Gets or sets the ForeColor of the control whenever while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
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
		/// Gets or sets the border color of the control while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
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
        /// Here we draw the items.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
		{
			var g = e.Graphics;
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			if (e.Index == -1)
			{
				return;
			}

			var itemState = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using var bg = new SolidBrush(itemState ? SelectedItemBackColor : BackgroundColor);
            using var tc = new SolidBrush(itemState ? SelectedItemForeColor : ForeColor);
            using var f = new Font(Font.Name, 9);
            g.FillRectangle(bg, e.Bounds);
            g.DrawString(GetItemText(Items[e.Index]), f, tc, e.Bounds, Methods.SetPosition(StringAlignment.Near));
        }

		/// <summary>
		/// Here we draw the container.
		/// </summary>
		/// <param name="e">PaintEventArgs</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var rect = new Rectangle(0, 0, Width - 1, Height - 1);
			var downArrow = '▼';
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using var bg = new SolidBrush(Enabled ? BackgroundColor : DisabledBackColor);
            using var p = new Pen(Enabled ? BorderColor : DisabledBorderColor);
            using var s = new SolidBrush(Enabled ? ArrowColor : DisabledForeColor);
            using var tb = new SolidBrush(Enabled ? ForeColor : DisabledForeColor);
            using var f = MetroSetFonts.SemiBold(8);
            g.FillRectangle(bg, rect);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString(downArrow.ToString(), f, s, new Point(Width - 22, 8));
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.DrawString(Text, f, tb, new Rectangle(7, 0, Width - 1, Height - 1), Methods.SetPosition(StringAlignment.Near));
            g.DrawRectangle(p, rect);
        }
	}
}