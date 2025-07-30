using Microsoft.Extensions.Configuration;
using PluginBase;
using System.Globalization;

namespace MSFSPlugin
{
    public partial class MSFSControl : PluginBase.PluginControl
    {
        const string PLUGINNAME = "MSFSPlugin";
        const string STANZA = "MSFS";
        private readonly UserControl? MSFSInfoPage = new MSFSInfo();
        public override UserControl? InfoPage { get => MSFSInfoPage; }
        public override IConfigurationSection? Configuration { get; set; }

        public MSFSControl() : base(STANZA)
        {
            InitializeComponent();
            Name = PLUGINNAME;
            Description = "Connect to Microsoft Flight Simulator 2024.";
            Icon = Properties.Resources.red;
        }
    }
}
