using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace TestPlugin.Plugins
{
    public class TestPlugin : PluginBase.PluginControl
    {
        #region Constants
        const string PLUGINNAME = "TestPlugin";
        const string STANZA = "Test";
        #endregion

        #region IPLUGIN Overrides
        public override IConfigurationSection? Configuration { get; set; }
        #endregion

        #region Constructors
        public TestPlugin() :base(STANZA)
        {
            Name = PLUGINNAME;
            Description = "A plugin for testing purposes.";
            //Icon = Properties.Resources.red;

        }
        #endregion


    }
}
