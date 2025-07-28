using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace TestPlugin
{
    public class TestPlugin : PluginBase.PluginControl
    {
        #region Constants
        const string PLUGINNAME = "TestPlugin";
        const string STANZA = "Test";
        private readonly UserControl? RedisInfoPage = new TestInfo();
        #endregion

        #region IPLUGIN Overrides
        public override UserControl? InfoPage { get => RedisInfoPage;  }
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
