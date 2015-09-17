namespace WindowsFormsApplication1
{
    partial class DesignWindow
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1327, 644);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(322, 185);
            this.button1.TabIndex = 0;
            this.button1.Text = "Draw Gears";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // DesignWindow
            // 
            this.ClientSize = new System.Drawing.Size(1661, 841);
            this.Controls.Add(this.button1);
            this.Name = "DesignWindow";
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.Button button1;
    }
}