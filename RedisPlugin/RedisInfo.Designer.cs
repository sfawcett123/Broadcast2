namespace RedisPlugin
{
    partial class RedisInfo
    {
        public string Url { get => url.Text; set => this.url.Text = value; }
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new System.Windows.Forms.PictureBox();
            url = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.green;
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.Location = new System.Drawing.Point(79, 15);
            pictureBox1.Name = "Redis";
            pictureBox1.Size = new System.Drawing.Size(272, 250);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // url
            // 
            url.BorderStyle = System.Windows.Forms.BorderStyle.None;
            url.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            url.Location = new System.Drawing.Point(25, 292);
            url.Name = "url";
            url.ReadOnly = true;
            url.Size = new System.Drawing.Size(401, 26);
            url.TabIndex = 1;
            url.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RedisInfo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(url);
            Controls.Add(pictureBox1);
            Name = "RedisInfo";
            Size = new System.Drawing.Size(448, 450);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox url;
    }
}
