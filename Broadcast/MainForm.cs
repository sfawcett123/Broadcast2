using Microsoft.Extensions.Configuration;
using PluginBase;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace Broadcast
{
    public partial class MainForm : Form
    {
        public MainForm(IConfigurationRoot Configuration )
        {
            InitializeComponent();

            LoadControls( Configuration );
        }
        private void LoadControls( IConfigurationRoot Configuration)
        {
            Debug.WriteLine($"Loading plugins from {Configuration["plugins"]}");

            string[] pluginPaths = Directory.GetFiles(Configuration["plugins"] ?? "./plugins", "*.dll");
            foreach (var control in Program.ReadDlls(pluginPaths, Configuration))
            {
                Debug.WriteLine($"Adding control {control.Name} Configuration {control.Stanza}");
                flowLayoutPanel1.Controls.Add(control);
                control.Click += PluginControl_Click;
                control.DataRecieved += PluginControl_DataReceived;
            }

            toolStripStatusLabel.Text = $"Loaded {pluginPaths.Length} plugins.";
        }
        private void PluginControl_DataReceived(object? sender, EventArgs e)
        {
            if (sender is PluginControl c)
            {
                Debug.WriteLine($"Data Recieved from {c.Name}");
            }
        }
        private void PluginControl_Click(object? sender, EventArgs e)
        {
            if (sender is PluginControl c)
            {
                Debug.WriteLine($"Mouse Click from {c.Name}");
            }
        }
    }
}
