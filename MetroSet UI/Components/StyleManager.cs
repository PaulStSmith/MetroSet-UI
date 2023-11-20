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
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Interfaces;

namespace MetroSet.UI.Components
{
    public enum ControlKind
    {
        Button,
        DefaultButton,
        Label,
        LinkLabel,
        TextBox,
        RichTextBox,
        ComboBox,
        Form,
        Badge,
        Divider,
        CheckBox,
        RadioButton,
        SwitchBox,
        ToolTip,
        Numeric,
        Ellipse,
        Tile,
        ProgressBar,
        ControlBox,
        TabControl,
        ScrollBar,
        Panel,
        TrackBar,
        ContextMenuStrip,
        ListBox,
        DataGrid
    }

    [DefaultProperty("Style")]
	[Designer(typeof(StyleManagerDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(StyleManager), "Style.bmp")]
	public class StyleManager : Component
	{
		public StyleManager()
		{
			_style = Style.Light;
			if (_customTheme == null)
			{
				var themeFile = Properties.Settings.Default.ThemeFile;
				_customTheme = File.Exists(themeFile) ? themeFile : ThemeFilePath(themeFile);
			}
		}

		/// <summary>
		/// The Method to update the form with the style manager style and it's controls.
		/// </summary>
		private void UpdateForm()
		{
			switch (MetroForm)
			{
				case IMetroForm form:
					form.Style = Style;
					form.StyleManager = this;
					break;
			}

			if (MetroForm.Controls.Count > 0)
				UpdateControls(MetroForm.Controls);

			MetroForm.Invalidate();
		}

		/// <summary>
		/// The Method to update controls with the style manager style.
		/// </summary>
		private void UpdateControls(Control.ControlCollection controls)
		{

			foreach (Control ctrl in controls)
			{
				var control = ctrl as IMetroSetControl;
				if (control != null && CustomTheme != null)
				{
					control.Style = Style;
					control.StyleManager = this;
				}
				if (control is TabControl tabControl)
				{
					foreach (TabPage c in tabControl.TabPages)
					{
						if (c is IMetroSetControl)
						{
							control.Style = Style;
							control.StyleManager = this;
						}
						UpdateControls(c.Controls);
					}
				}

				foreach (Control child in ctrl.Controls)
				{
					if (child is not IMetroSetControl)
						continue;
					((IMetroSetControl)child).Style = Style;
					((IMetroSetControl)child).StyleManager = this;

				}
			}
		}

		/// <summary>
		/// The Method to apply the style manager style to the added controls.
		/// </summary>
		private void ControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control is IMetroSetControl control)
			{
				if (!control.IsCustomStyle)
					return;
				control.Style = Style;
				control.StyleManager = this;
			}
			else
			{
				UpdateForm();
			}
		}

		private Style _style;
		private string _customTheme;

		/// <summary>
		/// Gets or sets the form (MetroForm) to Apply themes for.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the form (MetroForm) to Apply themes for.")]
		public Form MetroForm
		{
			get => _metroForm;
			set
			{
				if (_metroForm != null || value == null)
					return;

				_metroForm = value;
				_metroForm.ControlAdded += ControlAdded;
				UpdateForm();
			}
		}
		private Form _metroForm;

		/// <summary>
		/// Gets or sets the style for the button.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the style.")]
		public Style Style
		{
			get => _style;
			set
			{
				_style = value;
				UpdateForm();
			}
		}

		/// <summary>
		/// Gets or sets the custom theme file controls.
		/// </summary>
		[Editor(typeof(FileNamesEditor), typeof(UITypeEditor)), Category("MetroSet Framework"), Description("Gets or sets the custom theme file.")]
		public string CustomTheme
		{
			get => _customTheme;
			set
			{
				if (Style == Style.Custom)
				{
					Properties.Settings.Default.ThemeFile = value;
					Properties.Settings.Default.Save();
					ControlProperties(value);
				}
				_customTheme = value;
			}
		}

		/// <summary>
		/// The Method to execute the FileNamesEditor and open the dialog of importing the custom theme.
		/// </summary>
		public void OpenTheme()
		{
			Style = Style.Custom;
			using var ofd = new OpenFileDialog { Filter = @"Xml File (*.xml)|*.xml" };
			if (ofd.ShowDialog() != DialogResult.OK)
				return;
			CustomTheme = ofd.FileName;
		}

		/// <summary>
		/// The Method for setting the custom theme up.
		/// </summary>
		/// <param name="path">The Custom theme file path.</param>
		public void SetTheme(string path)
		{
			Style = Style.Custom;
			CustomTheme = path;
		}

		/// <summary>
		/// The Method to write the them file from resources to templates folder.
		/// </summary>
		/// <param name="str">the theme content</param>
		/// <returns>The Sorted theme path in templates folder.</returns>
		private string ThemeFilePath(string str)
		{
			var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\ThemeFile.xml"}";
			File.WriteAllText(path, str);
			return path;
		}

        /// <summary>
        /// Gets the style dictionary for the specified control.
        /// </summary>
        /// <param name="kind">The kind of dictionary to get.</param>
        /// <returns>The dictionary for the specified control.</returns>
        /// <exception cref="NotSupportedException">Thrown if the specified control is not supported.</exception>
        public IDictionary<string, object> StyleDictionary(ControlKind kind) => kind switch
		{
			ControlKind.Button => ButtonDictionary,
			ControlKind.DefaultButton => DefaultButtonDictionary,
			ControlKind.Label => LabelDictionary,
			ControlKind.LinkLabel => LinkLabelDictionary,
			ControlKind.TextBox => TextBoxDictionary,
			ControlKind.RichTextBox => RichTextBoxDictionary,
			ControlKind.ComboBox => ComboBoxDictionary,
			ControlKind.Form => FormDictionary,
			ControlKind.Badge => BadgeDictionary,
			ControlKind.Divider => DividerDictionary,
			ControlKind.CheckBox => CheckBoxDictionary,
			ControlKind.RadioButton => RadioButtonDictionary,
			ControlKind.SwitchBox => SwitchBoxDictionary,
			ControlKind.ToolTip => ToolTipDictionary,
			ControlKind.Numeric => NumericDictionary,
			ControlKind.Ellipse => EllipseDictionary,
			ControlKind.Tile => TileDictionary,
			ControlKind.ProgressBar => ProgressDictionary,
			ControlKind.ControlBox => ControlBoxDictionary,
			ControlKind.TabControl => TabControlDictionary,
			ControlKind.ScrollBar => ScrollBarDictionary,
			ControlKind.Panel => PanelDictionary,
			ControlKind.TrackBar => TrackBarDictionary,
			ControlKind.ContextMenuStrip => ContextMenuDictionary,
			ControlKind.ListBox => ListBoxDictionary,
			ControlKind.DataGrid => DataGridDictionary,
			_ => throw new NotSupportedException()
		};

		/// <summary>
		/// The Button properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ButtonDictionary = new();

		/// <summary>
		/// The DefaultButton properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> DefaultButtonDictionary = new();

		/// <summary>
		/// The Label properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> LabelDictionary = new();

		/// <summary>
		/// The LinkLabel properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> LinkLabelDictionary = new();

		/// <summary>
		/// The TextBox properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> TextBoxDictionary = new();

		/// <summary>
		/// The RichTextBox properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> RichTextBoxDictionary = new();

		/// <summary>
		/// The ComboBox properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ComboBoxDictionary = new();

		/// <summary>
		/// The Form properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> FormDictionary = new();

		/// <summary>
		/// The Badge properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> BadgeDictionary = new();

		/// <summary>
		/// The Divider properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> DividerDictionary = new();

		/// <summary>
		/// The CheckBox properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> CheckBoxDictionary = new();

		/// <summary>
		/// The RadioButton properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> RadioButtonDictionary = new();

		/// <summary>
		/// The Switch properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> SwitchBoxDictionary = new();

		/// <summary>
		/// The ToolTip properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ToolTipDictionary = new();

		/// <summary>
		/// The ToolTip properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> NumericDictionary = new();

		/// <summary>
		/// The ToolTip properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> EllipseDictionary = new();

		/// <summary>
		/// The Tile properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> TileDictionary = new();

		/// <summary>
		/// The Tile properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ProgressDictionary = new();

		/// <summary>
		/// The ControlBox properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ControlBoxDictionary = new();

		/// <summary>
		/// The TabControl properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> TabControlDictionary = new();

		/// <summary>
		/// The ScrollBar properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ScrollBarDictionary = new();

		/// <summary>
		/// The Panel properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> PanelDictionary = new();

		/// <summary>
		/// The TrackBar properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> TrackBarDictionary = new();

		/// <summary>
		/// The ContextMenuStrip properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ContextMenuDictionary = new();

		/// <summary>
		/// The ListBox properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> ListBoxDictionary = new();

		/// <summary>
		/// The ListBox properties from custom theme will be stored into this dictionary.
		/// </summary>
		private Dictionary<string, object> DataGridDictionary = new();

		private void Clear()
		{
			ButtonDictionary.Clear();
			DefaultButtonDictionary.Clear();
			FormDictionary.Clear();
			LabelDictionary.Clear();
			TextBoxDictionary.Clear();
			LabelDictionary.Clear();
			LinkLabelDictionary.Clear();
			BadgeDictionary.Clear();
			DividerDictionary.Clear();
			CheckBoxDictionary.Clear();
			RadioButtonDictionary.Clear();
			SwitchBoxDictionary.Clear();
			ToolTipDictionary.Clear();
			RichTextBoxDictionary.Clear();
			ComboBoxDictionary.Clear();
			NumericDictionary.Clear();
			EllipseDictionary.Clear();
			TileDictionary.Clear();
			ProgressDictionary.Clear();
			ControlBoxDictionary.Clear();
			TabControlDictionary.Clear();
			ScrollBarDictionary.Clear();
			PanelDictionary.Clear();
			TrackBarDictionary.Clear();
			ContextMenuDictionary.Clear();
			ListBoxDictionary.Clear();
			DataGridDictionary.Clear();
		}

		/// <summary>
		/// Reads the theme file and put elements properties to dictionaries.
		/// </summary>
		/// <param name="path">The File path.</param>
		private void ControlProperties(string path)
		{
			// We clear every dictionary for avoid the "the key is already exist in dictionary" exception.

			Clear();

			// Here we refill the dictionaries with information we get in custom theme.

			FormDictionary = GetValues(path, "Form");

			ButtonDictionary = GetValues(path, "Button");

			DefaultButtonDictionary = GetValues(path, "DefaultButton");

			LabelDictionary = GetValues(path, "Label");

			LinkLabelDictionary = GetValues(path, "LinkLabel");

			BadgeDictionary = GetValues(path, "Badge");

			DividerDictionary = GetValues(path, "Divider");

			CheckBoxDictionary = GetValues(path, "CheckBox");

			RadioButtonDictionary = GetValues(path, "RadioButton");

			SwitchBoxDictionary = GetValues(path, "SwitchBox");

			ToolTipDictionary = GetValues(path, "ToolTip");

			TextBoxDictionary = GetValues(path, "TextBox");

			RichTextBoxDictionary = GetValues(path, "RichTextBox");

			ComboBoxDictionary = GetValues(path, "ComboBox");

			NumericDictionary = GetValues(path, "Numeric");

			EllipseDictionary = GetValues(path, "Ellipse");

			TileDictionary = GetValues(path, "Tile");

			ProgressDictionary = GetValues(path, "Progress");

			ControlBoxDictionary = GetValues(path, "ControlBox");

			TabControlDictionary = GetValues(path, "TabControl");

			ScrollBarDictionary = GetValues(path, "ScrollBar");

			PanelDictionary = GetValues(path, "Panel");

			TrackBarDictionary = GetValues(path, "TrackBar");

			ContextMenuDictionary = GetValues(path, "ContextMenu");

			ListBoxDictionary = GetValues(path, "ListBox");

			DataGridDictionary = GetValues(path, "DataGrid");

			ThemeDetailsReader(path);

			UpdateForm();

		}

		/// <summary>
		/// The Method get the custom theme name and author.
		/// </summary>
		/// <param name="path">The Path of the custom theme file.</param>
		private void ThemeDetailsReader(string path)
		{
			foreach (var item in GetValues(path, "Theme"))
			{
				switch (item.Key)
				{
					case "Name":
						ThemeName = item.Value.ToString();
						break;
					case "Author":
						ThemeAuthor = item.Value.ToString();
						break;
				}
			}
		}

        /// <summary>
        /// Gets or sets the the name of the theme.
        /// </summary>
        public string ThemeName { get; protected internal set; }

        /// <summary>
        /// Gets or sets the the author's name associated with the theme.
        /// </summary>
		public string ThemeAuthor { get; protected internal set; }

		/// <summary>
		/// The Method to load the custom xml theme file and add a childnodes from a specific node into a dectionary. 
		/// </summary>
		/// <param name="path">The Path of custom theme file (XML file).</param>
		/// <param name="nodename">The Node name to get the childnodes from.</param>
		/// <returns>The Dictionary of childnodes names and values of a specific node.</returns>
		private Dictionary<string, object> GetValues(string path, string nodename)
		{
			try
			{
				var dict = new Dictionary<string, object>();
				var doc = new XmlDocument();
				if (File.Exists(path))
					doc.Load(path);
				if (doc.DocumentElement == null)
				{ return null; }
				var xmlNode = doc.SelectSingleNode($"/MetroSetTheme/{nodename}");
				if (xmlNode == null)
					return dict;
				foreach (XmlNode node in xmlNode.ChildNodes)
					dict.Add(node.Name, node.InnerText);

				return dict;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Dialog Type For Opening the theme.
		/// </summary>
		public class FileNamesEditor : UITypeEditor
		{
			private OpenFileDialog _ofd;

			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.Modal;
			}

			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				if (context == null || provider == null)
					return base.EditValue(context, provider, value);
				var editorService =
					(IWindowsFormsEditorService)
					provider.GetService(typeof(IWindowsFormsEditorService));
				if (editorService == null)
					return base.EditValue(context, provider, value);
				_ofd = new OpenFileDialog
				{
					Filter = @"Xml File (*.xml)|*.xml",
				};
				return _ofd.ShowDialog() == DialogResult.OK ? _ofd.FileName : base.EditValue(context, provider, value);
			}
		}

	}
}