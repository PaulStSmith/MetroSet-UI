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
	internal class MetroSetSwitchActionList : DesignerActionList
	{
		private readonly MetroSetSwitchBox _metroSetSwitch;

		public MetroSetSwitchActionList(IComponent component) : base(component)
		{
			_metroSetSwitch = (MetroSetSwitchBox)component;
		}

		public Style Style
		{
			get => _metroSetSwitch.Style;
			set => _metroSetSwitch.Style = value;
		}

		public StyleManager StyleManager
		{
			get => _metroSetSwitch.StyleManager;
			set => _metroSetSwitch.StyleManager = value;
		}

		public string Text
		{
			get => _metroSetSwitch.Text;
			set => _metroSetSwitch.Text = value;
		}

		public bool Switched
		{
			get => _metroSetSwitch.Switched;
			set => _metroSetSwitch.Switched = value;
		}

		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection
		{
			new DesignerActionHeaderItem("MetroSet Framework"),
			new DesignerActionPropertyItem("StyleManager", "StyleManager", "MetroSet Framework", "Gets or sets the stylemanager for the control."),
			new DesignerActionPropertyItem("Style", "Style", "MetroSet Framework", "Gets or sets the style."),

			new DesignerActionHeaderItem("Appearance"),
			new DesignerActionPropertyItem("Text", "Text", "Appearance", "Gets or sets the The text associated with the control."),
			new DesignerActionPropertyItem("Switched", "Switched", "Appearance", "Gets or sets a value indicating whether the control is switched."),
		};

			return items;
		}
	}
}