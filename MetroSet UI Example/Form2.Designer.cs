namespace MetroSet.UI_Example
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new System.Windows.Forms.Button();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            metroSetButton1 = new UI.Controls.MetroSetButton();
            metroSetSetToolTip1 = new UI.Components.MetroSetSetToolTip();
            checkBox1 = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(90, 123);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(137, 106);
            button1.TabIndex = 0;
            button1.Text = "button1";
            toolTip1.SetToolTip(button1, "Test");
            button1.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            toolTip1.ToolTipTitle = "Normal Tooltip";
            // 
            // metroSetButton1
            // 
            metroSetButton1.DisabledBackColor = System.Drawing.Color.FromArgb(120, 65, 177, 225);
            metroSetButton1.SetDisabledBorderColor(System.Drawing.Color.FromArgb(120, 65, 177, 225));
            metroSetButton1.DisabledForeColor = System.Drawing.Color.Gray;
            metroSetButton1.Font = new System.Drawing.Font("Segoe WP Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            metroSetButton1.HoverBorderColor = System.Drawing.Color.FromArgb(95, 207, 255);
            metroSetButton1.HoverColor = System.Drawing.Color.FromArgb(95, 207, 255);
            metroSetButton1.HoverTextColor = System.Drawing.Color.White;
            metroSetButton1.InheritStyle = true;
            metroSetButton1.Location = new System.Drawing.Point(261, 123);
            metroSetButton1.Name = "metroSetButton1";
            metroSetButton1.BorderColor = System.Drawing.Color.FromArgb(65, 177, 225);
            metroSetButton1.ForeColor = System.Drawing.Color.FromArgb(65, 177, 225);
            metroSetButton1.ForeColor = System.Drawing.Color.White;
            metroSetButton1.PressBorderColor = System.Drawing.Color.FromArgb(35, 147, 195);
            metroSetButton1.PressBackColor = System.Drawing.Color.FromArgb(35, 147, 195);
            metroSetButton1.PressForeColor = System.Drawing.Color.White;
            metroSetButton1.Size = new System.Drawing.Size(137, 106);
            metroSetButton1.Style = UI.Enums.Style.System;
            metroSetButton1.StyleManager = null;
            metroSetButton1.TabIndex = 1;
            metroSetButton1.Text = "metroSetButton1";
            metroSetSetToolTip1.SetToolTip(metroSetButton1, "Metro ToolTip");
            // 
            // metroSetSetToolTip1
            // 
            metroSetSetToolTip1.BackColor = System.Drawing.Color.White;
            metroSetSetToolTip1.BorderColor = System.Drawing.Color.FromArgb(204, 204, 204);
            metroSetSetToolTip1.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            metroSetSetToolTip1.InheritStyle = true;
            metroSetSetToolTip1.OwnerDraw = true;
            metroSetSetToolTip1.Style = UI.Enums.Style.System;
            metroSetSetToolTip1.StyleManager = null;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(120, 256);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(107, 28);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(checkBox1);
            Controls.Add(metroSetButton1);
            Controls.Add(button1);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private UI.Components.MetroSetSetToolTip metroSetSetToolTip1;
        private UI.Controls.MetroSetButton metroSetButton1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}