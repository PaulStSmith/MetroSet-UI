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
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Controls
{

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetBadge), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroSetBadgeDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    public class MetroSetBadge : MetroSetControl
    {

        private MouseMode _MouseMode;
        private BadgeAlign _badgeAlignment;
        private string _badgeText;
        private Color _normalColor;
        private Color _normalBorderColor;
        private Color _normalTextColor;
        private Color _hoverColor;
        private Color _hoverBorderColor;
        private Color _hoverTextColor;
        private Color _pressColor;
        private Color _pressBorderColor;
        private Color _pressTextColor;
        private Color _disabledBackColor;
        private Color _disabledForeColor;
        private Color _disabledBorderColor;
        private Color _normalBadgeColor;
        private Color _normalBadgeTextColor;
        private Color _hoverBadgeColor;
        private Color _hoverBadgeTextColor;
        private Color _pressBadgeColor;
        private Color _pressBadgeTextColor;

        public MetroSetBadge() : base(ControlKind.Badge)
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            base.Font = MetroSetFonts.Light(10);
            base.BackColor = Color.Transparent;
            _badgeAlignment = BadgeAlign.TopRight;
            _badgeText = "3";
            
            ApplyTheme();
        }

        /// <summary>
        /// Gets or sets the badge alignment associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the badge alignment associated with the control.")]
        public BadgeAlign BadgeAlignment
        {
            get { return _badgeAlignment; }
            set
            {
                _badgeAlignment = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the badge text associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the badge text associated with the control.")]
        public string BadgeText
        {
            get { return _badgeText; }
            set
            {
                _badgeText = value;
                Refresh();
            }
        }

        /// <summary>
        /// Handling Control Enable state to detect the disability state.
        /// </summary>
        [Category("MetroSet Framework")]
        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                if (value == false)
                {
                    _MouseMode = MouseMode.Disabled;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the control background color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control background color in normal mouse sate.")]
        public Color NormalColor
        {
            get { return _normalColor; }
            set
            {
                _normalColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control border color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control border color in normal mouse sate.")]
        public Color NormalBorderColor
        {
            get { return _normalBorderColor; }
            set
            {
                _normalBorderColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control Text color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control Text color in normal mouse sate.")]
        public Color NormalTextColor
        {
            get { return _normalTextColor; }
            set
            {
                _normalTextColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control background color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control background color in hover mouse sate.")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set
            {
                _hoverColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control border color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control border color in hover mouse sate.")]
        public Color HoverBorderColor
        {
            get { return _hoverBorderColor; }
            set
            {
                _hoverBorderColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control Text color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control Text color in hover mouse sate.")]
        public Color HoverTextColor
        {
            get { return _hoverTextColor; }
            set
            {
                _hoverTextColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control background color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control background color in pushed mouse sate.")]
        public Color PressColor
        {
            get { return _pressColor; }
            set
            {
                _pressColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control border color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control border color in pushed mouse sate.")]
        public Color PressBorderColor
        {
            get { return _pressBorderColor; }
            set
            {
                _pressBorderColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control Text color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control Text color in pushed mouse sate.")]
        public Color PressTextColor
        {
            get { return _pressTextColor; }
            set
            {
                _pressTextColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets backcolor used by the control while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets backcolor used by the control while disabled.")]
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
        /// Gets or sets the forecolor of the control whenever while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the forecolor of the control whenever while disabled.")]
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
        [Description("Gets or sets the border color of the control while disabled.")]
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
        /// Gets or sets the Badge background color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge background color in normal mouse sate.")]
        public Color NormalBadgeColor
        {
            get { return _normalBadgeColor; }
            set
            {
                _normalBadgeColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Badge Text color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge Text color in normal mouse sate.")]
        public Color NormalBadgeTextColor
        {
            get { return _normalBadgeTextColor; }
            set
            {
                _normalBadgeTextColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Badge background color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge background color in hover mouse sate.")]
        public Color HoverBadgeColor
        {
            get { return _hoverBadgeColor; }
            set
            {
                _hoverBadgeColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Badge Text color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge Text color in hover mouse sate.")]
        public Color HoverBadgeTextColor
        {
            get { return _hoverBadgeTextColor; }
            set
            {
                _hoverBadgeTextColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Badge background color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge background color in pushed mouse sate.")]
        public Color PressBadgeColor
        {
            get { return _pressBadgeColor; }
            set
            {
                _pressBadgeColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Badge Text color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge Text color in pushed mouse sate.")]
        public Color PressBadgeTextColor
        {
            get { return _pressBadgeTextColor; }
            set
            {
                _pressBadgeTextColor = value;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Rectangle r;
            Rectangle badge;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (BadgeAlignment)
            {
                case BadgeAlign.Topleft:
                    r = new Rectangle(18, 18, Width - 21, Height - 21);
                    badge = new Rectangle(5, 5, 29, 29);
                    break;

                case BadgeAlign.TopRight:
                    r = new Rectangle(0, 18, Width - 18, Height - 21);
                    badge = new Rectangle(Width - 35, 1, 29, 29);
                    break;

                case BadgeAlign.BottmLeft:
                    r = new Rectangle(18, 0, Width - 19, Height - 18);
                    badge = new Rectangle(1, Height - 35, 29, 29);
                    break;

                case BadgeAlign.BottomRight:
                    r = new Rectangle(0, 0, Width - 19, Height - 18);
                    badge = new Rectangle(Width - 35, Height - 35, 29, 29);
                    break;
                default:
                    throw new InvalidOperationException($@"The allowed values for {nameof(BadgeAlignment)} are: {BadgeAlign.Topleft}, {BadgeAlign.TopRight}, {BadgeAlign.BottomRight}, and  {BadgeAlign.BottmLeft}.");
            }

            switch (_MouseMode)
            {
                case MouseMode.Normal:

                    using (var bg = new SolidBrush(NormalColor))
                    using (var p = new Pen(NormalBorderColor))
                    using (var tb = new SolidBrush(NormalTextColor))
                    using (var bdgBrush = new SolidBrush(NormalBadgeColor))
                    using (var bdgtxtBrush = new SolidBrush(NormalBadgeTextColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, Methods.SetPosition());
                        SmoothingType(g);
                        g.FillEllipse(bdgBrush, badge);
                        g.DrawString(BadgeText, Font, bdgtxtBrush, badge, Methods.SetPosition());
                    }

                    break;

                case MouseMode.Hovered:

                    Cursor = Cursors.Hand;
                    using (var bg = new SolidBrush(HoverColor))
                    using (var p = new Pen(HoverBorderColor))
                    using (var tb = new SolidBrush(HoverTextColor))
                    using (var bdgBrush = new SolidBrush(HoverBadgeColor))
                    using (var bdgtxtBrush = new SolidBrush(HoverBadgeTextColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, Methods.SetPosition());
                        SmoothingType(g);
                        g.FillEllipse(bdgBrush, badge);
                        g.DrawString(BadgeText, Font, bdgtxtBrush, badge, Methods.SetPosition());
                    }

                    break;

                case MouseMode.Pushed:

                    using (var bg = new SolidBrush(PressColor))
                    using (var p = new Pen(PressBorderColor))
                    using (var tb = new SolidBrush(PressTextColor))
                    using (var bdgBrush = new SolidBrush(PressBadgeColor))
                    using (var bdgtxtBrush = new SolidBrush(PressBadgeTextColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, Methods.SetPosition());
                        SmoothingType(g);
                        g.FillEllipse(bdgBrush, badge);
                        g.DrawString(BadgeText, Font, bdgtxtBrush, badge, Methods.SetPosition());
                    }

                    break;

                case MouseMode.Disabled:

                    using (var bg = new SolidBrush(DisabledBackColor))
                    using (var p = new Pen(DisabledBorderColor))
                    using (var tb = new SolidBrush(DisabledForeColor))
                    using (var bdgBrush = new SolidBrush(PressBadgeColor))
                    using (var bdgtxtBrush = new SolidBrush(PressBadgeTextColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, Methods.SetPosition());
                        SmoothingType(g);
                        g.FillEllipse(bdgBrush, badge);
                        g.DrawString(BadgeText, Font, bdgtxtBrush, badge, Methods.SetPosition());
                    }

                    break;
            }
        }

        /// <inheritdoc/>
        protected override void ApplyThemeInternal(Style style)
        {
            switch (style)
            {
                case Style.Light:
                    NormalColor = Color.FromArgb(238, 238, 238);
                    NormalBorderColor = Color.FromArgb(204, 204, 204);
                    NormalTextColor = Color.Black;
                    HoverColor = Color.FromArgb(102, 102, 102);
                    HoverBorderColor = Color.FromArgb(102, 102, 102);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(51, 51, 51);
                    PressBorderColor = Color.FromArgb(51, 51, 51);
                    PressTextColor = Color.White;
                    NormalBadgeColor = Color.FromArgb(65, 177, 225);
                    NormalBadgeTextColor = Color.White;
                    HoverBadgeColor = Color.FromArgb(85, 187, 245);
                    HoverBadgeTextColor = Color.White;
                    PressBadgeColor = Color.FromArgb(45, 147, 205);
                    PressBadgeTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    break;

                case Style.Dark:
                    NormalColor = Color.FromArgb(32, 32, 32);
                    NormalBorderColor = Color.FromArgb(64, 64, 64);
                    NormalTextColor = Color.FromArgb(204, 204, 204);
                    HoverColor = Color.FromArgb(170, 170, 170);
                    HoverBorderColor = Color.FromArgb(170, 170, 170);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(240, 240, 240);
                    PressBorderColor = Color.FromArgb(240, 240, 240);
                    PressTextColor = Color.White;
                    NormalBadgeColor = Color.FromArgb(65, 177, 225);
                    NormalBadgeTextColor = Color.White;
                    HoverBadgeColor = Color.FromArgb(85, 187, 245);
                    HoverBadgeTextColor = Color.White;
                    PressBadgeColor = Color.FromArgb(45, 147, 205);
                    PressBadgeTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                    {
                        NormalColor = Utils.HexColor(StyleDictionary["NormalColor"]);
                        NormalBorderColor = Utils.HexColor(StyleDictionary["NormalBorderColor"]);
                        NormalTextColor = Utils.HexColor(StyleDictionary["NormalTextColor"]);
                        HoverColor = Utils.HexColor(StyleDictionary["HoverColor"]);
                        HoverBorderColor = Utils.HexColor(StyleDictionary["HoverBorderColor"]);
                        HoverTextColor = Utils.HexColor(StyleDictionary["HoverTextColor"]);
                        PressColor = Utils.HexColor(StyleDictionary["PressColor"]);
                        PressBorderColor = Utils.HexColor(StyleDictionary["PressBorderColor"]);
                        PressTextColor = Utils.HexColor(StyleDictionary["PressTextColor"]);
                        NormalBadgeColor = Utils.HexColor(StyleDictionary["NormalBadgeColor"]);
                        NormalBadgeTextColor = Utils.HexColor(StyleDictionary["NormalBadgeTextColor"]);
                        HoverBadgeColor = Utils.HexColor(StyleDictionary["HoverBadgeColor"]);
                        HoverBadgeTextColor = Utils.HexColor(StyleDictionary["HoverBadgeTextColor"]);
                        PressBadgeColor = Utils.HexColor(StyleDictionary["PressBadgeColor"]);
                        PressBadgeTextColor = Utils.HexColor(StyleDictionary["PressBadgeTextColor"]);
                        DisabledBackColor = Utils.HexColor(StyleDictionary["DisabledBackColor"]);
                        DisabledBorderColor = Utils.HexColor(StyleDictionary["DisabledBorderColor"]);
                        DisabledForeColor = Utils.HexColor(StyleDictionary["DisabledForeColor"]);
                    }
                    break;
            }
        }

        /// <summary>
        /// Handling mouse up event of the control.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _MouseMode = MouseMode.Hovered;
            Invalidate();
        }

        /// <summary>
        /// Handling mouse down event of the control.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _MouseMode = MouseMode.Pushed;
            Invalidate();
        }

        /// <summary>
        /// Handling mouse entering event of the control.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _MouseMode = MouseMode.Hovered;
            Invalidate();
        }

        /// <summary>
        /// Handling mouse leave event of the control.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);
            _MouseMode = MouseMode.Normal;
            Invalidate();
        }

        /// <summary>
        /// Sets the smoothingmode the the specific graphics.
        /// </summary>
        /// <param name="e">Graphics to Set the effect.</param>
        /// <param name="state">state of smoothingmode.</param>
        private static void SmoothingType(Graphics e, SmoothingMode state = SmoothingMode.AntiAlias)
        {
            e.SmoothingMode = state;
        }

    }
}