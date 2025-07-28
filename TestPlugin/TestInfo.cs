using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPlugin
{
    internal class TestInfo : UserControl
    {
        public TestInfo()
        {
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            pictureBox2 = new PictureBox();
            InitializeComponent();
        }
        private void InitializeComponent()
        {

            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.green;
            pictureBox1.Location = new System.Drawing.Point(18, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(167, 160);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            textBox1.Location = new System.Drawing.Point(15, 188);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(514, 41);
            textBox1.TabIndex = 1;
            textBox1.Text = "Test Plugin";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.red;
            pictureBox2.Location = new System.Drawing.Point(353, 14);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(167, 160);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // RedisInfo
            // 
            Controls.Add(pictureBox2);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Name = "RedisInfo";
            Size = new System.Drawing.Size(544, 450);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
            // Initialization code for the UserControl can be added here.
            // This is typically where you would set up the UI elements.
        }
        private readonly PictureBox pictureBox1;
        private readonly TextBox textBox1;
        private readonly PictureBox pictureBox2;
    }
}
