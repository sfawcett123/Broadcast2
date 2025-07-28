using Microsoft.Extensions.Configuration;

namespace PluginBase
{
    public class PluginEventArgs
    {
        public string Name { get; set; } = string.Empty;
    }
    public  interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Stanza { get; set; }
        IConfigurationSection? Configuration { get; set; }

        public event EventHandler DataRecieved;
    }
}
