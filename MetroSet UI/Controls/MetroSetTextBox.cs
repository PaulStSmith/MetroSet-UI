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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet.UI.Components;
using MetroSet.UI.Design;
using MetroSet.UI.Enums;
using MetroSet.UI.Extensions;
using MetroSet.UI.Interfaces;
using MetroSet.UI.Native;

namespace MetroSet.UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetTextBox), "Bitmaps.TextBox.bmp")]
	[Designer(typeof(MetroSetTextBoxDesigner))]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class MetroSetTextBox : MetroSetControl
	{
		private HorizontalAlignment _textAlign;
		private int _maxLength;
		private bool _readOnly;
		private bool _useSystemPasswordChar;
		private string _watermarkText;
		private Image _image;
		private MouseMode _MouseMode;
		private AutoCompleteSource _autoCompleteSource;
		private AutoCompleteMode _autoCompleteMode;
		private AutoCompleteStringCollection _autoCompleteCustomSource;
		private bool _multiline;
		private string[] _lines;
		private Color _borderColor;
		private Color _hoverColor;
		private Color _disabledForeColor;
		private Color _disabledBackColor;
		private Color _disabledBorderColor;

		private readonly TextBox _textBox = new();

		public MetroSetTextBox() : base(ControlKind.TextBox)
		{
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.UserPaint, true);
			UpdateStyles();
			Font = MetroSetFonts.Regular(10);
			Defaults();
			ApplyTheme();
			if (!Multiline)
				Size = new Size(135, 30);
		}

		private void Defaults()
		{
			_watermarkText = string.Empty;
			_useSystemPasswordChar = false;
			_readOnly = false;
			_maxLength = 32767;
			_textAlign = HorizontalAlignment.Left;
			_MouseMode = MouseMode.Normal;
			_autoCompleteMode = AutoCompleteMode.None;
			_autoCompleteSource = AutoCompleteSource.None;
			_lines = null;
			_multiline = false;
			_textBox.Multiline = _multiline;
			_textBox.Cursor = Cursors.IBeam;
			_textBox.BackColor = BackColor;
			_textBox.ForeColor = ForeColor;
			_textBox.BorderStyle = BorderStyle.None;
			_textBox.Location = new Point(7, 8);
			_textBox.Font = Font;
			_textBox.UseSystemPasswordChar = UseSystemPasswordChar;
			if (Multiline)
				_textBox.Height = Height - 11;
			else
				Height = _textBox.Height + 11;

			_textBox.MouseHover += HandleMouseHover;
			_textBox.Leave += HandleLeave;
			_textBox.Enter += HandleEnter;
			_textBox.KeyDown += HandleKeyDown;
			_textBox.TextChanged += HandleTextChanged;
			_textBox.KeyPress += HandleKeyPress;

		}

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
			if (_textBox.Enabled = Enabled)
			{
				_textBox.BackColor = DisabledBackColor;
				_textBox.ForeColor = DisabledForeColor;
			}
			else
			{
                _textBox.BackColor = BackColor;
                _textBox.ForeColor = ForeColor;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var rect = new Rectangle(0, 0, Width - 1, Height - 1);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			if (Enabled)
			{
                using var bg = new SolidBrush(BackColor);
                g.FillRectangle(bg, rect);
				using var p = _MouseMode switch
				{
                    MouseMode.Normal => new Pen(BorderColor),
                    MouseMode.Pushed => new Pen(HoverColor),
                    MouseMode.Hovered => new Pen(HoverColor),
                    _ => new Pen(BorderColor)
                };
				g.DrawRectangle(p, rect);
            }
			else
			{
                using var bg = new SolidBrush(DisabledBackColor);
                using var p = new Pen(DisabledBorderColor);
                g.FillRectangle(bg, rect);
                g.DrawRectangle(p, rect);
            }
			if (Image != null)
			{
				_textBox.Location = new Point(31, 4);
				_textBox.Width = Width - 60;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.DrawImage(Image, new Rectangle(7, 6, 18, 18));
			}
			else
			{
				_textBox.Location = new Point(7, 4);
				_textBox.Width = Width - 10;
			}
		}

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        protected override void ApplyThemeInternal(Style style)
        {
			switch (style)
			{
				case Style.Light:
					ForeColor = Color.FromArgb(20, 20, 20);
					BackColor = Color.FromArgb(238, 238, 238);
					HoverColor = Color.FromArgb(102, 102, 102);
					BorderColor = Color.FromArgb(155, 155, 155);
					DisabledBackColor = Color.FromArgb(204, 204, 204);
					DisabledBorderColor = Color.FromArgb(155, 155, 155);
					DisabledForeColor = Color.FromArgb(136, 136, 136);
					break;

				case Style.Dark:
					ForeColor = Color.FromArgb(204, 204, 204);
					BackColor = Color.FromArgb(34, 34, 34);
					HoverColor = Color.FromArgb(65, 177, 225);
					BorderColor = Color.FromArgb(110, 110, 110);
					DisabledBackColor = Color.FromArgb(80, 80, 80);
					DisabledBorderColor = Color.FromArgb(109, 109, 109);
					DisabledForeColor = Color.FromArgb(109, 109, 109);
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
                        ForeColor = Utils.HexColor(StyleDictionary["ForeColor"]);
                        BackColor = Utils.HexColor(StyleDictionary["BackColor"]);
                        HoverColor = Utils.HexColor(StyleDictionary["HoverColor"]);
                        BorderColor = Utils.HexColor(StyleDictionary["BorderColor"]);
                        WatermarkText = (string)StyleDictionary["WatermarkText"];
                        DisabledBackColor = Utils.HexColor(StyleDictionary["DisabledBackColor"]);
                        DisabledBorderColor = Utils.HexColor(StyleDictionary["DisabledBorderColor"]);
                        DisabledForeColor = Utils.HexColor(StyleDictionary["DisabledForeColor"]);
                    }
					break;
			}
		}

		public new event EventHandler TextChanged;
		public event KeyPressEventHandler KeyPressed;
		public new event EventHandler Leave;

		/// <summary>
		/// Handling textbox leave event and raising the same event here.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void HandleLeave(object sender, EventArgs e)
		{
			base.OnMouseLeave(e);
			Leave?.Invoke(sender, e);
		}

		public void HandleKeyPress(object sender, KeyPressEventArgs e)
		{
			KeyPressed?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// Handling mouse leave event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			_MouseMode = MouseMode.Normal;
			base.OnMouseLeave(e);
		}

		/// <summary>
		/// Handling mouse up event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_MouseMode = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse entering event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_MouseMode = MouseMode.Pushed;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse hover event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			_MouseMode = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling the mouse hover event on text box control.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void HandleMouseHover(object sender, EventArgs e)
		{
			base.OnMouseHover(e);
			Invalidate();
		}

		/// <summary>
		/// Raises the Control.Resize event.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			//if (!Multiline)
			//{
			_textBox.Size = new Size(Width - 10, Height - 10);
			//}
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			_textBox.Focus();
		}

		/// <summary>
		/// Raises the Control.Enter event.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void HandleEnter(object sender, EventArgs e)
		{
			base.OnMouseEnter(e);
			Invalidate();
		}

		/// <summary>
		/// Handling Keydown event of text box control.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">KeyEventArgs</param>
		private void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
				e.SuppressKeyPress = true;
			if (!e.Control || e.KeyCode != Keys.C)
				return;
			_textBox.Copy();
			e.SuppressKeyPress = true;
		}

		/// <summary>
		/// An System.EventArgs that contains the event data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTextChanged(object sender, EventArgs e)
		{
			Text = _textBox.Text;
			TextChanged?.Invoke(this, e);
			Invalidate();
		}

		/// <summary>
		/// override the control creating , here we add the base textbox to the main control.
		/// </summary>
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (!Controls.Contains(_textBox))
				Controls.Add(_textBox);
		}

		/// <summary>
		/// Appends text to the current text of a text box.
		/// </summary>
		/// <param name="text"></param>
		public void AppendText(string text)
		{
			_textBox?.AppendText(text);
		}

		/// <summary>
		/// Undoes the last edit operation in the text box.
		/// </summary>
		public void Undo()
		{
			if (_textBox == null)
				return;
			if (_textBox.CanUndo)
			{
				_textBox.Undo();
			}
		}

		/// <summary>
		/// Retrieves the line number from the specified character position within the text of the control.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetLineFromCharIndex(int index)
		{
			return _textBox?.GetLineFromCharIndex(index) ?? 0;
		}

		/// <summary>
		/// Retrieves the location within the control at the specified character index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Point GetPositionFromCharIndex(int index)
		{
			return _textBox.GetPositionFromCharIndex(index);
		}

		/// <summary>
		/// Retrieves the index of the character nearest to the specified location.
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		public int GetCharIndexFromPosition(Point pt)
		{
			return _textBox?.GetCharIndexFromPosition(pt) ?? 0;
		}

		/// <summary>
		/// Clears information about the most recent operation from the undo buffer of the text box.
		/// </summary>
		public void ClearUndo()
		{
			_textBox?.ClearUndo();
		}

		/// <summary>
		/// Copies the current selection in the text box to the Clipboard.
		/// </summary>
		public void Copy()
		{
			_textBox?.Copy();
		}

		/// <summary>
		/// Moves the current selection in the text box to the Clipboard.
		/// </summary>
		public void Cut()
		{
			_textBox?.Cut();
		}

		/// <summary>
		/// Selects all text in the text box.
		/// </summary>
		public void SelectAll()
		{
			_textBox?.SelectAll();
		}

		/// <summary>
		/// Specifies that the value of the TextBoxBase.SelectionLength property is zero so that no characters are selected in the control.
		/// </summary>
		public void DeselectAll()
		{
			_textBox?.DeselectAll();
		}

		/// <summary>
		/// Replaces the current selection in the text box with the contents of the Clipboard.
		/// </summary>
		/// <param name="clipFormat"></param>
		public void Paste(string clipFormat)
		{
			_textBox?.Paste(clipFormat);
		}

		/// <summary>
		/// Selects a range of text in the text box.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="length"></param>
		public void Select(int start, int length)
		{
			_textBox?.Select(start, length);
		}

		/// <summary>
		/// Gets the border style.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BorderStyle BorderStyle => BorderStyle.None;

		/// <summary>
		/// Gets or sets how text is aligned in a TextBox control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets how text is aligned in a TextBox control.")]
		public HorizontalAlignment TextAlign
		{
			get => _textAlign;
			set
			{
				_textAlign = value;
				var text = _textBox;
				if (text != null)
				{
					text.TextAlign = value;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets how text is aligned in a TextBox control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets how text is aligned in a TextBox control.")]
		public int MaxLength
		{
			get => _maxLength;
			set
			{
				_maxLength = value;
				if (_textBox != null)
				{
					_textBox.MaxLength = value;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the background color of the control.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the background color of the control.")]
		public override Color BackColor
		{
			get => base.BackColor;
			set
			{
                base.BackColor = value;
				_textBox.BackColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the color of the control whenever hovered.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the color of the control whenever hovered.")]
		public Color HoverColor
		{
			get => _hoverColor;
			set
			{
				_hoverColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the border color of the control.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the border color of the control.")]
		public Color BorderColor
		{
			get => _borderColor;
			set
			{
				_borderColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the foreground color of the control.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the foreground color of the control.")]
		[Browsable(false)]
		public override Color ForeColor
		{
			get => base.ForeColor;
			set
			{
				base.ForeColor = value;
				_textBox.ForeColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether text in the text box is read-only.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether text in the text box is read-only.")]
		public bool ReadOnly
		{
			get => _readOnly;
			set
			{
				_readOnly = value;
				if (_textBox != null)
				{
					_textBox.ReadOnly = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.")]
		public bool UseSystemPasswordChar
		{
			get => _useSystemPasswordChar;
			set
			{
				_useSystemPasswordChar = value;
				if (_textBox != null)
				{
					_textBox.UseSystemPasswordChar = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this is a multiline TextBox control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether this is a multiline TextBox control.")]
		public bool Multiline
		{
			get => _multiline;
			set
			{
				_multiline = value;
				if (_textBox == null)
				{ return; }
				_textBox.Multiline = value;
				if (value)
				{
					_textBox.Height = Height - 10;
				}
				else
				{
					Height = _textBox.Height + 10;
				}
			}
		}

		/// <summary>
		/// Gets or sets the background image.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage => null;

		/// <summary>
		/// Gets or sets the current text in the TextBox.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the current text in the TextBox.")]
		public override string Text
		{
			get => _textBox.Text;
			set
			{
				base.Text = value;
				if (_textBox != null)
				{
					_textBox.Text = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the text in the TextBox while being empty.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the text in the TextBox while being empty.")]
		public string WatermarkText
		{
			get => _watermarkText;
			set
			{
				_watermarkText = value;
				User32.SendMessage(_textBox.Handle, 5377, 0, value);
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the image of the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the image of the control.")]
		public Image Image
		{
			get => _image;
			set
			{
				_image = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value specifying the source of complete strings used for automatic completion.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
		public AutoCompleteSource AutoCompleteSource
		{
			get => _autoCompleteSource;
			set
			{
				_autoCompleteSource = value;
				if (_textBox != null)
				{
					_textBox.AutoCompleteSource = value;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value specifying the source of complete strings used for automatic completion.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get => _autoCompleteCustomSource;
			set
			{
				_autoCompleteCustomSource = value;
				if (_textBox != null)
				{
					_textBox.AutoCompleteCustomSource = value;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets an option that controls how automatic completion works for the TextBox.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
		public AutoCompleteMode AutoCompleteMode
		{
			get => _autoCompleteMode;
			set
			{
				_autoCompleteMode = value;
				if (_textBox != null)
				{
					_textBox.AutoCompleteMode = value;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the font of the text displayed by the control.")]
		public sealed override Font Font
		{
			get => base.Font;
			set
			{
				base.Font = value;
				if (_textBox == null)
					return;
				_textBox.Font = value;
				_textBox.Location = new Point(5, 5);
				_textBox.Width = Width - 8;
				if (!Multiline)
					Height = _textBox.Height + 11;
			}
		}

		/// <summary>
		/// Gets or sets the lines of text in the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the lines of text in the control.")]
		public string[] Lines
		{
			get => _lines;
			set
			{
				_lines = value;
				if (_textBox != null)
					_textBox.Lines = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the ContextMenuStrip associated with this control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the ContextMenuStrip associated with this control.")]
		public override ContextMenuStrip ContextMenuStrip
		{
			get => base.ContextMenuStrip;
			set
			{
				base.ContextMenuStrip = value;
				if (_textBox == null)
					return;
				_textBox.ContextMenuStrip = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the forecolor of the control whenever while disabled
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the forecolor of the control whenever while disabled.")]
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
		/// Gets or sets disabled backcolor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets disabled backcolor used by the control.")]
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
		/// Gets or sets the border color while the control disabled.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the border color while the control disabled.")]
		public Color DisabledBorderColor
		{
			get { return _disabledBorderColor; }
			set
			{
				_disabledBorderColor = value;
				Refresh();
			}
		}

		private bool _isDerivedStyle = true;

		/// <summary>
		/// Gets or sets the whether this control reflect to parent form style.
		/// Set it to false if you want the style of this control be independent. 
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
					 "Set it to false if you want the style of this control be independent. ")]
		public bool IsDerivedStyle
		{
			get { return _isDerivedStyle; }
			set
			{
				_isDerivedStyle = value;
				Refresh();
			}
		}

	}
}