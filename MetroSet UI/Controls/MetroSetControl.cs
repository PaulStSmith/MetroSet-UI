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
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;
using Microsoft.Win32;

namespace MetroSet.UI.Controls
{
    /// <summary>
    /// Base class for MetroSet UI controls.
    /// </summary>
    public abstract class MetroSetControl : Control, IMetroSetControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroSetControl"/> class.
        /// </summary>
        /// <param name="kind">The kind of control.</param>
        protected MetroSetControl(ControlKind kind) : base()
        {
            Kind = kind;
        }

        protected ControlKind Kind { get; }

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
        public IDictionary<string, object> StyleDictionary => _StyleManager?.StyleDictionary(Kind);

        /// <summary>
        /// Applies the current style to the control.
        /// </summary>
        public void ApplyTheme()
        {
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
            ApplyThemeInternal(style);
            ResumeLayout();
            Refresh();
        }

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        /// <remarks>
        /// When implementing this method, DO NOT call <see cref="Control.SuspendLayout()"/>, <see cref="Control.ResumeLayout()"/>,  <see cref="Control.Refresh()"/> or <see cref="Control.Invalidate()"/>.
        /// </remarks>
        protected abstract void ApplyThemeInternal(Style style);

        /// <summary>
        ///  Determines if layout is currently suspended.
        /// </summary>
        protected bool IsLayoutSuspended
        {
            get => GetIsLayoutSuspended.Invoke(this, null) is bool b && b;
        }
        private readonly static MethodInfo GetIsLayoutSuspended = typeof(Control).GetProperty("IsLayoutSuspended", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetMethod;

        /// <inheritdoc/>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
        }

        /// <inheritdoc/>
        public override void Refresh()
        {
            if (IsLayoutSuspended)
                return;
            base.Refresh();
        }
    }
}