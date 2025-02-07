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
	public class MetroSetTrackBarActionList : DesignerActionList
	{
		private readonly MetroSetTrackBar _metroSetTrackBar;

		public MetroSetTrackBarActionList(IComponent component) : base(component)
		{
			_metroSetTrackBar = (MetroSetTrackBar)component;
		}

		public Style Style
		{
			get => _metroSetTrackBar.Style;
			set => _metroSetTrackBar.Style = value;
		}

		public StyleManager StyleManager
		{
			get => _metroSetTrackBar.StyleManager;
			set => _metroSetTrackBar.StyleManager = value;
		}

		public int Maximum
		{
			get => _metroSetTrackBar.Maximum;
			set => _metroSetTrackBar.Maximum = value;
		}

		public int Minimum
		{
			get => _metroSetTrackBar.Minimum;
			set => _metroSetTrackBar.Minimum = value;
		}

		public int Value
		{
			get => _metroSetTrackBar.Value;
			set => _metroSetTrackBar.Value = value;
		}

		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection
		{
			new DesignerActionHeaderItem("MetroSet Framework"),
			new DesignerActionPropertyItem("StyleManager", "StyleManager", "MetroSet Framework", "Gets or sets the stylemanager for the control."),
			new DesignerActionPropertyItem("Style", "Style", "MetroSet Framework", "Gets or sets the style."),

			new DesignerActionHeaderItem("Informations"),
			new DesignerActionPropertyItem("ThemeName", "ThemeName", "Informations", "Gets or sets the The Theme name associated with the theme."),
			new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Informations", "Gets or sets the The Author name associated with the theme."),

			new DesignerActionHeaderItem("Appearance"),
			new DesignerActionPropertyItem("Maximum", "Maximum", "Appearance", "Gets or sets the upper limit of the range this TrackBar is working with."),
			new DesignerActionPropertyItem("Minimum", "Minimum", "Appearance", "Gets or sets the lower limit of the range this TrackBar is working with."),
			new DesignerActionPropertyItem("Value", "Value", "Appearance", "Gets or sets a numeric value that represents the current position of the scroll box on the track bar."),
		};

			return items;
		}
	}
}