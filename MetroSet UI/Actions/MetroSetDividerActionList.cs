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

using System.ComponentModel;
using System.ComponentModel.Design;
using MetroSet.UI.Components;
using MetroSet.UI.Controls;
using MetroSet.UI.Enums;

namespace MetroSet.UI.Actions
{
    internal class MetroSetDividerActionList : DesignerActionList
    {
        private readonly MetroSetDivider _metroSetDivider;

        public MetroSetDividerActionList(IComponent component) : base(component)
        {
            _metroSetDivider = (MetroSetDivider)component;
        }

        public Style Style
        {
            get => _metroSetDivider.Style;
            set => _metroSetDivider.Style = value;
        }

        public StyleManager StyleManager
        {
            get => _metroSetDivider.StyleManager;
            set => _metroSetDivider.StyleManager = value;
        }

        public DividerStyle Orientation
        {
            get => _metroSetDivider.Orientation;
            set => _metroSetDivider.Orientation = value;
        }

        public int Thickness
        {
            get => _metroSetDivider.Thickness;
            set => _metroSetDivider.Thickness = value;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new()
            {
            new DesignerActionHeaderItem("MetroSet Framework"),
            new DesignerActionPropertyItem("StyleManager", "StyleManager", "MetroSet Framework", "Gets or sets the stylemanager for the control."),
            new DesignerActionPropertyItem("Style", "Style", "MetroSet Framework", "Gets or sets the style."),

            new DesignerActionHeaderItem("Appearance"),
            new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Gets or sets Orientation of the control."),
            new DesignerActionPropertyItem("Thickness", "Thickness", "Appearance", "Gets or sets the divider thickness."),
            };

            return items;
        }
    }
}