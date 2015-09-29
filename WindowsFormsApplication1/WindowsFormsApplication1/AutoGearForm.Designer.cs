namespace WindowsFormsApplication1
{
    partial class AutoGearForm
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
            this.LengthText = new System.Windows.Forms.TextBox();
            this.WidthText = new System.Windows.Forms.TextBox();
            this.HeightText = new System.Windows.Forms.TextBox();
            this.DiametralText = new System.Windows.Forms.TextBox();
            this.TorqueInText = new System.Windows.Forms.TextBox();
            this.TorqueOutText = new System.Windows.Forms.TextBox();
            this.DesignButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LengthText
            // 
            this.LengthText.Location = new System.Drawing.Point(150, 102);
            this.LengthText.Name = "LengthText";
            this.LengthText.Size = new System.Drawing.Size(100, 20);
            this.LengthText.TabIndex = 0;
            this.LengthText.Text = "Length";
            // 
            // WidthText
            // 
            this.WidthText.Location = new System.Drawing.Point(150, 149);
            this.WidthText.Name = "WidthText";
            this.WidthText.Size = new System.Drawing.Size(100, 20);
            this.WidthText.TabIndex = 1;
            this.WidthText.Text = "Width";
            // 
            // HeightText
            // 
            this.HeightText.Location = new System.Drawing.Point(150, 193);
            this.HeightText.Name = "HeightText";
            this.HeightText.Size = new System.Drawing.Size(100, 20);
            this.HeightText.TabIndex = 2;
            this.HeightText.Text = "Height";
            // 
            // DiametralText
            // 
            this.DiametralText.Location = new System.Drawing.Point(150, 248);
            this.DiametralText.Name = "DiametralText";
            this.DiametralText.Size = new System.Drawing.Size(100, 20);
            this.DiametralText.TabIndex = 3;
            this.DiametralText.Text = "Diametral Pitch";
            // 
            // TorqueInText
            // 
            this.TorqueInText.Location = new System.Drawing.Point(150, 299);
            this.TorqueInText.Name = "TorqueInText";
            this.TorqueInText.Size = new System.Drawing.Size(100, 20);
            this.TorqueInText.TabIndex = 4;
            this.TorqueInText.Text = "Torque In";
            // 
            // TorqueOutText
            // 
            this.TorqueOutText.Location = new System.Drawing.Point(150, 348);
            this.TorqueOutText.Name = "TorqueOutText";
            this.TorqueOutText.Size = new System.Drawing.Size(100, 20);
            this.TorqueOutText.TabIndex = 5;
            this.TorqueOutText.Text = "Torque Out";
            // 
            // DesignButton
            // 
            this.DesignButton.Location = new System.Drawing.Point(321, 208);
            this.DesignButton.Name = "DesignButton";
            this.DesignButton.Size = new System.Drawing.Size(170, 98);
            this.DesignButton.TabIndex = 6;
            this.DesignButton.Text = "Design Gear Train";
            this.DesignButton.UseVisualStyleBackColor = true;
            this.DesignButton.Click += new System.EventHandler(this.DesignButton_Click);
            // 
            // AutoGearForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 785);
            this.Controls.Add(this.DesignButton);
            this.Controls.Add(this.TorqueOutText);
            this.Controls.Add(this.TorqueInText);
            this.Controls.Add(this.DiametralText);
            this.Controls.Add(this.HeightText);
            this.Controls.Add(this.WidthText);
            this.Controls.Add(this.LengthText);
            this.Name = "AutoGearForm";
            this.Text = "AutoGearForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LengthText;
        private System.Windows.Forms.TextBox WidthText;
        private System.Windows.Forms.TextBox HeightText;
        private System.Windows.Forms.TextBox DiametralText;
        private System.Windows.Forms.TextBox TorqueInText;
        private System.Windows.Forms.TextBox TorqueOutText;
        private System.Windows.Forms.Button DesignButton;
    }
}