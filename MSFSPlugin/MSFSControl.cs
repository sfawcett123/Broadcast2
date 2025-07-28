using Microsoft.Extensions.Configuration;
using PluginBase;

namespace MSFSPlugin
{
    public partial class MSFSControl : PluginBase.PluginControl
    {
        const string PLUGINNAME = "MSFSPlugin";
        const string STANZA = "MSFS";
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
