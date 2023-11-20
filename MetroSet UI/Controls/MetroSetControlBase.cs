using MetroSet.UI.Components;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetroSet.UI.Controls
{
    /// <summary>
    /// Represents a base control for the MetroSet family of controls.
    /// </summary>
    public abstract class MetroSetControlBase : Control, IMetroSetControl
    {
        private Style _style;
        private StyleManager _styleManager;
        private bool _isDerivedStyle;
		private Color _disabledBackColor;
		private Color _disabledBorderColor;
		private Color _disabledForeColor;
		private Color _hoverBorderColor;
		private Color _hoverColor;
		private Color _hoverTextColor;
		private Color _normalBorderColor;

		private Color _normalColor;
		private Color _normalTextColor;
		private Color _pressBorderColor;
		private Color _pressColor;
		private Color _pressTextColor;

        /// <summary>
        /// Occurs when the user interface style changes.
        /// </summary>
        [Description("Occurs when the user interface style changes.")]
        public event EventHandler UIStyleChanged;

        /// <summary>
        /// Raises the <see cref="UIStyleChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUIStyleChanged(EventArgs e) { UIStyleChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get => StyleManager?.Style ?? _style;
            set => Utils.SetValue(ref _style, value, () => {
                OnUIStyleChanged(EventArgs.Empty);
                ApplyTheme(value);
                Refresh();
            });
        }

        /// <summary>
        /// Occurrs then the <see cref="MetroSetControlBase.StyleManager"/> property changes.
        /// </summary>
        public event EventHandler StyleManagerChanged;

        /// <summary>
        /// Raises the <see cref="StyleManagerChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnStyleManagerChanged(EventArgs e) { StyleManagerChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the Style Manager associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
        public StyleManager StyleManager
        {
            get => _styleManager;
            set => Utils.SetValue(ref _styleManager, value, () => { OnStyleManagerChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Applies the specified theme on the control.
        /// </summary>
        /// <param name="value">One of the <see cref="MetroSetControlBase.Style"/> values.</param>
        protected abstract void ApplyTheme(Style value = Style.System);

        /// <summary>
        /// Occurs when the <see cref="InheritStyle"/> property changes.
        /// </summary>
        public event EventHandler InhetiStyleChanged;

        /// <summary>
        /// Raises the <see cref="InhetiStyleChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnInhetiStyleChanged(EventArgs e) { InhetiStyleChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the whether this control reflect to parent form style.
        /// Set it to false if you want the style of this control be independent. 
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
                     "Set it to false if you want the style of this control be independent. ")]
        public bool InheritStyle
        {
            get { return _isDerivedStyle; }
            set => Utils.SetValue(ref _isDerivedStyle, value, () => { OnInhetiStyleChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.DisabledBackColor"/> property changes.
        /// </summary>
        public event EventHandler DisabledBackColorChanged;

        /// <summary>
        /// Raises the <see cref="DisabledBackColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnDisabledBackColorChanged(EventArgs e) { DisabledBackColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets BackColor used by the control while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets backcolor used by the control while disabled.")]
		public Color DisabledBackColor
        {
            get => _disabledBackColor;
            set => Utils.SetValue(ref _disabledBackColor, value, () => { OnDisabledBackColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.DisabledBorderColor"/> property changes.
        /// </summary>
        public event EventHandler DisabledBorderColorChanged;

        /// <summary>
        /// Raises the <see cref="DisabledBorderColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnDisabledBorderColorChanged(EventArgs e) { DisabledBorderColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the border color of the control while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the border color of the control while disabled.")]
		public Color DisabledBorderColor
        {
            get => _disabledBorderColor;
            set => Utils.SetValue(ref _disabledBorderColor, value, () => { OnDisabledBorderColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.DisabledForeColor"/> property changes.
        /// </summary>
        public event EventHandler DisabledForeColorChanged;

        /// <summary>
        /// Raises the <see cref="DisabledForeColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnDisabledForeColorChanged(EventArgs e) { DisabledForeColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the ForeColor of the control whenever while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the forecolor of the control whenever while disabled.")]
		public Color DisabledForeColor
        {
            get => _disabledForeColor;
            set => Utils.SetValue(ref _disabledForeColor, value, () => { OnDisabledForeColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.HoverBorderColor"/> property changes.
        /// </summary>
        public event EventHandler HoverBorderColorChanged;

        /// <summary>
        /// Raises the <see cref="HoverBorderColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnHoverBorderColorChanged(EventArgs e) { HoverBorderColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the control border color when the mouse is hovering over the control.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the control border color when the mouse is hovering over the control.")]
		public Color HoverBorderColor
        {
            get => _hoverBorderColor;
            set => Utils.SetValue(ref _hoverBorderColor, value, () => { OnHoverBorderColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.HoverColor"/> changed.
        /// </summary>
        public event EventHandler HoverColorChanged;

        /// <summary>
        /// Raises the <see cref="HoverColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnHoverColorChanged(EventArgs e) { HoverColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the control background color when the mouse is hovering over the control.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the control background color when the mouse is hovering over the control.")]
		public Color HoverColor
        {
            get => _hoverColor;
            set => Utils.SetValue(ref _hoverColor, value, () => { });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.HoverTextColor"/> property changes.
        /// </summary>
        public event EventHandler HoverTextColorChanged ;

        /// <summary>
        /// Raises the <see cref="HoverTextColorChanged "/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnHoverTextColorChanged (EventArgs e) { HoverTextColorChanged ?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the control Text color when the mouse is hovering over the control.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color when the mouse is hovering over the control.")]
		public Color HoverTextColor
        {
            get => _hoverTextColor;
            set => Utils.SetValue(ref _hoverTextColor, value, Refresh);
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.BorderColor"/> property changes.
        /// </summary>
        public event EventHandler BorderColorChanged;

        /// <summary>
        /// Raises the <see cref="BorderColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnBorderColorChanged(EventArgs e) { BorderColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the control border color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in normal mouse sate.")]
		public Color BorderColor
        {
            get => _normalBorderColor;
            set => Utils.SetValue(ref _normalBorderColor, value, () => { OnBorderColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.PressBorderColor"/> property changes.
        /// </summary>
        public event EventHandler PressBorderColorChanged;

        /// <summary>
        /// Raises the <see cref="PressBorderColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnPressBorderColorChanged(EventArgs e) { PressBorderColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the control border color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in pushed mouse sate.")]
		public Color PressBorderColor
        {
            get => _pressBorderColor;
            set => Utils.SetValue(ref _pressBorderColor, value, () => { OnPressBorderColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.PressBackColor"/> property changes.
        /// </summary>
        public event EventHandler PressBackColorChanged;

        /// <summary>
        /// Raises the <see cref="PressBackColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnPressBackColorChanged(EventArgs e) { PressBackColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the control background color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the control background color in pushed mouse sate.")]
		public Color PressBackColor
        {
            get => _pressColor;
            set => Utils.SetValue(ref _pressColor, value, () => { OnPressBackColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Occurs when the <see cref="MetroSetControlBase.PressForeColor"/> property changes.
        /// </summary>
        public event EventHandler PressForeColorChanged;

        /// <summary>
        /// Raises the <see cref="PressForeColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the data of the event.</param>
        protected virtual void OnPressForeColorChanged(EventArgs e) { PressForeColorChanged?.Invoke(this, e); Refresh(); }

        /// <summary>
        /// Gets or sets the control Text color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color in pushed mouse sate.")]
		public Color PressForeColor
        {
            get => _pressTextColor;
            set => Utils.SetValue(ref _pressTextColor, value, () => { OnPressForeColorChanged(EventArgs.Empty); });
        }

        /// <summary>
        /// Gets or seta a value indicating the paint mode of the control.
        /// </summary>
        protected PaintMode PaintMode 
        { 
            get => Enabled ? _PaintMode : PaintMode.Disabled ;
            set => _PaintMode = value;
        }
        PaintMode _PaintMode = PaintMode.Normal;

        /// <inheritdoc/>
        protected sealed override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var r = new Rectangle(0, 0, Width - 1, Height - 1);
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            switch (this.PaintMode)
            {
                case PaintMode.Normal: PaintNormal(g, r); break;
                case PaintMode.Hovered: PaintHovered(g, r); break;
                case PaintMode.Pressed: PaintPressed(g, r); break;
                case PaintMode.Disabled: PaintDisabled(g, r); break;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Paints the contol in its disabled state.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object that must be used to paint the contorl.</param>
        /// <param name="r">A <see cref="Rectangle"/> representing the area to be painted.</param>
        protected abstract void PaintDisabled(Graphics g, Rectangle r);

        /// <summary>
        /// Paints the contol in its pressed state, that is, when the user has the mouse control pressed on top of it.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object that must be used to paint the contorl.</param>
        /// <param name="r">A <see cref="Rectangle"/> representing the area to be painted.</param>
        protected abstract void PaintPressed(Graphics g, Rectangle r);

        /// <summary>
        /// Paints the contol in its hovered state.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object that must be used to paint the contorl.</param>
        /// <param name="r">A <see cref="Rectangle"/> representing the area to be painted.</param>
        protected abstract void PaintHovered(Graphics g, Rectangle r);

        /// <summary>
        /// Paints the contol in its normal state.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object that must be used to paint the contorl.</param>
        /// <param name="r">A <see cref="Rectangle"/> representing the area to be painted.</param>
        protected abstract void PaintNormal(Graphics g, Rectangle r);

        /// <inheritdoc/>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            PaintMode = PaintMode.Hovered;
            Invalidate();
        }

        /// <inheritdoc/>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            PaintMode = PaintMode.Pressed;
            Invalidate();
        }

        /// <inheritdoc/>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            PaintMode = PaintMode.Hovered;
            Invalidate();
        }

        /// <inheritdoc/>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            PaintMode = PaintMode.Normal;
            Invalidate();
        }
    }
}
