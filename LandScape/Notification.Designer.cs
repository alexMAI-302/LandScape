namespace LandScape
{
    partial class Notification
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
            this.hint_label = new System.Windows.Forms.Label();
            this.OK_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hint_label
            // 
            this.hint_label.AutoSize = true;
            this.hint_label.Location = new System.Drawing.Point(13, 13);
            this.hint_label.Name = "hint_label";
            this.hint_label.Size = new System.Drawing.Size(29, 13);
            this.hint_label.TabIndex = 0;
            this.hint_label.Text = "Hint:";
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(195, 93);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 23);
            this.OK_button.TabIndex = 1;
            this.OK_button.Text = "Ok";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 128);
            this.ControlBox = false;
            this.Controls.Add(this.OK_button);
            this.Controls.Add(this.hint_label);
            this.Name = "Notification";
            this.Text = "Notification";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label hint_label;
        private System.Windows.Forms.Button OK_button;

    }
}