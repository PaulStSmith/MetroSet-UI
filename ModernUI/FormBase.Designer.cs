using Microsoft.Win32;

namespace ModernUI
{
    partial class FormBase
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mnuMain = new MenuStrip();
            pnlButtons = new Panel();
            btnHelp = new Button();
            btnMinimize = new Button();
            btnMaximize = new Button();
            btnClose = new Button();
            picIcon = new PictureBox();
            pnlIcon = new Panel();
            pnlMenu = new Panel();
            pnlLayout = new Panel();
            pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            pnlIcon.SuspendLayout();
            pnlMenu.SuspendLayout();
            pnlLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mnuMain
            // 
            mnuMain.Dock = DockStyle.Fill;
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Size = new Size(100, 32);
            mnuMain.TabIndex = 5;
            mnuMain.Text = "menuStrip1";
            mnuMain.MouseDown += FormHeader_MouseDown;
            mnuMain.MouseMove += FormHeader_MouseMove;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnHelp);
            pnlButtons.Controls.Add(btnMinimize);
            pnlButtons.Controls.Add(btnMaximize);
            pnlButtons.Controls.Add(btnClose);
            pnlButtons.Dock = DockStyle.Right;
            pnlButtons.Location = new Point(136, 0);
            pnlButtons.Margin = new Padding(0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(200, 36);
            pnlButtons.TabIndex = 5;
            // 
            // btnHelp
            // 
            btnHelp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnHelp.CausesValidation = false;
            btnHelp.Dock = DockStyle.Right;
            btnHelp.FlatAppearance.BorderSize = 0;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Font = new Font("Segoe MDL2 Assets", 7F, FontStyle.Regular, GraphicsUnit.Point);
            btnHelp.Location = new Point(0, 0);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(50, 36);
            btnHelp.TabIndex = 5;
            btnHelp.TabStop = false;
            btnHelp.Text = "";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnMinimize.CausesValidation = false;
            btnMinimize.Dock = DockStyle.Right;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Font = new Font("Segoe MDL2 Assets", 7F, FontStyle.Regular, GraphicsUnit.Point);
            btnMinimize.Location = new Point(50, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(50, 36);
            btnMinimize.TabIndex = 6;
            btnMinimize.TabStop = false;
            btnMinimize.Text = "";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximize
            // 
            btnMaximize.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnMaximize.CausesValidation = false;
            btnMaximize.Dock = DockStyle.Right;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.Font = new Font("Segoe MDL2 Assets", 7F, FontStyle.Regular, GraphicsUnit.Point);
            btnMaximize.Location = new Point(100, 0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(50, 36);
            btnMaximize.TabIndex = 4;
            btnMaximize.TabStop = false;
            btnMaximize.Text = "";
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += btnMaximize_Click;
            // 
            // btnClose
            // 
            btnClose.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnClose.CausesValidation = false;
            btnClose.Dock = DockStyle.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe MDL2 Assets", 7F, FontStyle.Regular, GraphicsUnit.Point);
            btnClose.Location = new Point(150, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(50, 36);
            btnClose.TabIndex = 3;
            btnClose.TabStop = false;
            btnClose.Text = "";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            btnClose.MouseEnter += btnClose_MouseEnter;
            btnClose.MouseLeave += btnClose_MouseLeave;
            // 
            // picIcon
            // 
            picIcon.Dock = DockStyle.Fill;
            picIcon.Location = new Point(0, 0);
            picIcon.Margin = new Padding(0);
            picIcon.Name = "picIcon";
            picIcon.Padding = new Padding(2);
            picIcon.Size = new Size(36, 36);
            picIcon.TabIndex = 5;
            picIcon.TabStop = false;
            picIcon.Click += picIcon_Click;
            picIcon.MouseDown += FormHeader_MouseDown;
            picIcon.MouseMove += FormHeader_MouseMove;
            // 
            // pnlIcon
            // 
            pnlIcon.Controls.Add(picIcon);
            pnlIcon.Dock = DockStyle.Left;
            pnlIcon.Location = new Point(0, 0);
            pnlIcon.Margin = new Padding(0);
            pnlIcon.Name = "pnlIcon";
            pnlIcon.Size = new Size(36, 36);
            pnlIcon.TabIndex = 6;
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(mnuMain);
            pnlMenu.Dock = DockStyle.Top;
            pnlMenu.Location = new Point(36, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(100, 32);
            pnlMenu.TabIndex = 0;
            // 
            // pnlLayout
            // 
            pnlLayout.Controls.Add(pnlMenu);
            pnlLayout.Controls.Add(pnlIcon);
            pnlLayout.Controls.Add(pnlButtons);
            pnlLayout.Dock = DockStyle.Top;
            pnlLayout.Location = new Point(0, 0);
            pnlLayout.Margin = new Padding(0);
            pnlLayout.Name = "pnlLayout";
            pnlLayout.Size = new Size(336, 36);
            pnlLayout.TabIndex = 7;
            // 
            // FormBase
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(336, 220);
            Controls.Add(pnlLayout);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormBase";
            Text = "FormBase";
            pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            pnlIcon.ResumeLayout(false);
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            pnlLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlButtons;
        private Button btnHelp;
        private Button btnMinimize;
        private Button btnMaximize;
        private Button btnClose;
        private PictureBox picIcon;
        private MenuStrip mnuMain;
        private Panel pnlIcon;
        private Panel pnlMenu;
        private Panel pnlLayout;
    }
}