using Microsoft.Extensions.Configuration;
using PluginBase;
using System.Configuration;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RedisPlugin
{
    public class RedisPlugin : PluginBase.PluginControl
    {
        public override string Description => "Plugin to read and write to a redis cache";
        public override IConfigurationSection? Configuration { get; set; }

        public RedisPlugin()
        {
            Name = "Redis Plugin";
            Stanza = "RedisPlugin";
            BackgroundImage = Properties.Resources.red;
        }
    }
}
