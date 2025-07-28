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
            Description = "A plugin for testing purposes.";
            Icon = Properties.Resources.red;
        }
    }
}
