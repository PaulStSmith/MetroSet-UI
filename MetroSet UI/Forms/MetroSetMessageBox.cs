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
using System.Drawing.Text;
using System.Windows.Forms;
using MetroSet.UI.Controls;
using MetroSet.UI.Extensions;

namespace MetroSet.UI.Forms
{
	public class MetroSetMessageBox : MetroSetForm
	{

		private readonly Size DefaulButtonSize;
		private MetroSetDefaultButton _okButton;
		private MetroSetDefaultButton _yesButton;
		private MetroSetDefaultButton _noButton;
		private MetroSetDefaultButton _cancelButton;
		private MetroSetDefaultButton _retryButton;
		private MetroSetDefaultButton _abortButton;
		private MetroSetDefaultButton _ignoreButton;
		private MetroSetDefaultButton _continueButton;

        /// <summary>
        /// Get or sets the parent owner.
        /// </summary>
        private Form OwnerForm { get; set; }

		/// <summary>
		/// Gets or sets the content of the message.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the title of the content
		/// </summary>
		public string Caption { get; set; }

		/// <summary>
		/// Gets or sets the MessageBoxButtons.
		/// </summary> 
		public MessageBoxButtons Buttons { get; set; }

		/// <summary>
		/// Gets or sets the MessageBoxIcon.
		/// </summary>
		public MessageBoxIcon MessageIcon { get; set; }

		/// <summary>
		/// The Constructor.
		/// </summary>
		private MetroSetMessageBox()
		{
			Font = new Font("Calibri", 14);
			ShowInTaskbar = false;
			ForeColor = Color.Transparent;
			StartPosition = FormStartPosition.CenterParent;
			DefaulButtonSize = new Size(95, 32);
			KeyPreview = true;
			ApplyTheme();
			ConfigureControls();
			AddControls();
		}

		/// <summary>
		/// Here we set the buttons properties value.
		/// </summary>
		private void ConfigureControls()
		{
            ConfigureButton(ref _okButton, "OK", DefaulButtonSize, DialogResult.OK);
            ConfigureButton(ref _yesButton, "Yes", DefaulButtonSize, DialogResult.Yes);
            ConfigureButton(ref _noButton, "No", DefaulButtonSize, DialogResult.No);
            ConfigureButton(ref _cancelButton, "Cancel", DefaulButtonSize, DialogResult.Cancel);
            ConfigureButton(ref _retryButton, "Retry", DefaulButtonSize, DialogResult.Retry);
            ConfigureButton(ref _abortButton, "Abort", DefaulButtonSize, DialogResult.Abort);
            ConfigureButton(ref _ignoreButton, "Ignore", DefaulButtonSize, DialogResult.Ignore);
            ConfigureButton(ref _continueButton, "Continue", DefaulButtonSize, DialogResult.Ignore);
        }

        /// <summary>
        /// Adding the controls just to be exist in owner but we don't need them all at the moment.
        /// </summary>
        private void AddControls()
		{
			Controls.Add(_okButton);
			Controls.Add(_yesButton);
			Controls.Add(_noButton);
			Controls.Add(_cancelButton);
			Controls.Add(_retryButton);
			Controls.Add(_abortButton);
            Controls.Add(_ignoreButton);
            Controls.Add(_continueButton);
        }

        /// <summary>
        /// Configures the specified button, with the specified parameters.
        /// </summary>
        /// <param name="button">The button to be configured.</param>
        /// <param name="text">The text of the button.</param>
        /// <param name="size">The size of the button.</param>
        /// <param name="result">The <see cref="DialogResult"/> set up at the click of the button.</param>
        private void ConfigureButton(ref MetroSetDefaultButton button, string text, Size size, DialogResult result)
		{
			button = new MetroSetDefaultButton()
			{
				Text = text,
				Size = size,
				Visible = false
			};
			button.Click += (s, e) =>
			{
				this.DialogResult = result;
			};
		}

		/// <summary>
		/// When the user just provides the content of message to appear.
		/// </summary>
		/// <param name="owner">The Form that messagebox will be showed from.</param>
		/// <param name="content">The Content of the message.</param>
		/// <returns>The MessageBox with just the content and an ok button.</returns>
		public static DialogResult Show(MetroSetForm owner, string content)
		{
			return Show(owner, content, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
		}

		/// <summary>
		///  When the user provides the content of message and the message title to appear.
		/// </summary>
		/// <param name="owner">The Form that messagebox will be showed from.</param>
		/// <param name="content">The Content of the message.</param>
		/// <param name="caption">The MesageBox title.</param>
		/// <returns>The MessageBox with the content and title and an ok button.</returns>
		public static DialogResult Show(MetroSetForm owner, string content, string caption)
		{
			return Show(owner, content, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
		}

		/// <summary>
		/// When the user provides the content of message and the message title and also which type of buttons to appear.
		/// </summary>
		/// <param name="owner">The Form that messagebox will be showed from.</param>
		/// <param name="content">The Content of the message.</param>
		/// <param name="caption">The MesageBox title.</param>
		/// <param name="buttons">The Type of buttons to appear.</param>
		/// <returns>The MessageBox with the content and title and provided button(s) type.</returns>
		public static DialogResult Show(MetroSetForm owner, string content, string caption, MessageBoxButtons buttons)
		{
			return Show(owner, content, caption, buttons, MessageBoxIcon.None);
		}

		/// <summary>
		/// When the user provides the content of message and the message title and also which type message and buttons to appear.
		/// </summary>
		/// <param name="owner">The Form that messagebox will be showed from.</param>
		/// <param name="content">The Content of the message.</param>
		/// <param name="caption">The MesageBox title.</param>
		/// <param name="buttons">The Type of buttons to appear.</param>
		/// <param name="icon">The MessageBox type.</param>
		/// <returns>The MessageBox with the content and title and provided button(s) and type.</returns>
		public static DialogResult Show(MetroSetForm owner, string content, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			const string message = @"MetroSetMessageBox requires a form, use 'this' as the first parameter in the place you use MetroSetMessageBox.";
			var msgBox = new MetroSetMessageBox
			{
				OwnerForm = owner ?? throw new ArgumentNullException(nameof(owner), message),
				Content = content,
				Caption = caption,
				Buttons = buttons,
				Size = new Size(owner.Width - 2, (owner.Height / 3) - 1),
				Location = new Point(owner.Location.X, (owner.Height / 2) - 1)
			};

			if (icon == MessageBoxIcon.Hand)
                msgBox.BackColor = Color.FromArgb(210, 50, 45);
			else if (icon == MessageBoxIcon.Asterisk)
                msgBox.BackColor = Color.FromArgb(60, 180, 218);
			else if (icon == MessageBoxIcon.Question)
                msgBox.BackColor = Color.FromArgb(50, 65, 120);
			else if (icon == MessageBoxIcon.Exclamation)
                msgBox.BackColor = Color.FromArgb(237, 156, 40);
			else // (icon == MessageBoxIcon.None || icon == MessageBoxIcon.Asterisk || icon == MessageBoxIcon.Hand)
                msgBox.BackgroundColor = Color.White;

			msgBox.MessageIcon = icon;
            msgBox.ForeColor = msgBox.BackColor.GetContrastingColor();
            msgBox.BorderColor = msgBox.BackColor.AdjustLuminance(-50);

            return msgBox.ShowDialog();
		}

		private void SetupButton(MetroSetDefaultButton button, int position)
		{
			button.Location = new Point(Width - DefaulButtonSize.Width * position - 10 * position, Height - 45); ;
			button.Visible = true;
		}

		private void SetupButtons(MetroSetDefaultButton btn)
		{
			SetupButton(btn, 1);
			Button1 = btn;
		}
        private MetroSetDefaultButton Button1;

        private void SetupButtons(MetroSetDefaultButton btn2, MetroSetDefaultButton btn1)
        {
            SetupButtons(btn1);
            SetupButton(btn2, 2);
            Button2 = btn2;
        }
        private MetroSetDefaultButton Button2;

        private void SetupButtons(MetroSetDefaultButton btn3, MetroSetDefaultButton btn2, MetroSetDefaultButton btn1)
		{
			SetupButtons(btn1, btn2);
            SetupButton(btn3, 3);
            Button3 = btn3;
        }
        private MetroSetDefaultButton Button3;

        /// <summary>
        /// Here we handle the user provided buttons appearance.
        /// </summary>
        /// <returns>The MessageBox with provided buttons.</returns>
        protected new DialogResult ShowDialog()
		{
            switch (Buttons)
			{
				case MessageBoxButtons.OKCancel:
					SetupButtons(_okButton, _cancelButton);
					break;

                case MessageBoxButtons.AbortRetryIgnore:
                    SetupButtons(_abortButton, _retryButton, _ignoreButton);
                    break;

                case MessageBoxButtons.YesNo:
					SetupButtons(_yesButton, _noButton);
					break;

				case MessageBoxButtons.YesNoCancel:
					SetupButtons(_yesButton, _noButton, _cancelButton);
					break;

                case MessageBoxButtons.RetryCancel:
                    SetupButtons(_retryButton, _cancelButton);
                    break;

                case MessageBoxButtons.CancelTryContinue:
                    SetupButtons(_cancelButton, _retryButton, _continueButton);
                    break;

                default:
                    SetupButtons(_okButton);
                    break;
			}
			return base.ShowDialog();
		}

        /// <inheritdoc/>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
			if (e.KeyChar == '\r')
				Button1.OnClickInternal(EventArgs.Empty);
			if (e.KeyChar == '\x1B')
                (Button2 ?? Button1).OnClickInternal(EventArgs.Empty);
            base.OnKeyPress(e);
        }

		/// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
		{
			var G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            var lft = ClientRectangle.X + 104;
			var rgt = ClientRectangle.Width - lft - 10;
            using var bgBrush = new SolidBrush(BackColor);
			using var fgBrush = new SolidBrush(ForeColor == Color.Transparent ? Utilites.GetContrastingColor(BackColor) : ForeColor);
            using var borderPen = new Pen(BorderColor, 3);
            G.FillRectangle(bgBrush, ClientRectangle);
            var bmp = Utilites.GetMessageBoxIcon(this.MessageIcon);
			if (bmp != null)
				G.DrawImage(bmp, new Point(10, ClientRectangle.Y + 10));
			else
			{
				lft -= 94;
				rgt += 94;
			}
			G.DrawString(Caption, GlobalFont.SemiBold((float)(Font.Size * 1.5)), fgBrush, new RectangleF(lft, ClientRectangle.Y + 10, rgt, 40));
			G.DrawString(Content, Font, fgBrush, new RectangleF(lft, ClientRectangle.Y + 50, rgt, ClientRectangle.Height - 60));
            G.DrawRectangle(borderPen, ClientRectangle);
        }
	}
}
