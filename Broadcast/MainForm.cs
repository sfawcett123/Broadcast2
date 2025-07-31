using Microsoft.Extensions.Configuration;
using PluginBase;
using System.Diagnostics;

namespace Broadcast
{
    public partial class MainForm : Form
    {
        public MainForm(IConfigurationRoot Configuration )
        {
            InitializeComponent();

            StartUp StartUp = new();
            StartUp.ShowDialog(Configuration , this);
        }
        public void PluginControl_DataReceived(object? sender, PluginEventArgs e)
        {
            if (sender is PluginControl c)
            {
               if( e.Icon is not null ) c.Icon = e.Icon;
            }
        }
        public void PluginControl_Click(object? sender, EventArgs e)
        {
            if (sender is PluginControl c)
            {
                panel.Controls.Clear();
                panel.Controls.Add(c.InfoPage ?? new UserControl());
                panel.Size = c.InfoPage?.Size ?? new Size(300, 300);
            }
        }
    }
}
