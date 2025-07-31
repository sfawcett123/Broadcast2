using Microsoft.Extensions.Configuration;
using SimListener;

namespace MSFSPlugin
{
    public partial class MSFSControl : PluginBase.PluginControl
    {
        const string PLUGINNAME = "MSFSPlugin";
        const string STANZA = "MSFS";
        private readonly UserControl? MSFSInfoPage = new MSFSInfo();
        private readonly Connect sim = new();
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
