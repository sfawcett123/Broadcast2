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
        string Stanza { get;}
        System.Drawing.Image Icon { get; set; }
        IConfigurationSection? Configuration { get; set; }
        UserControl? InfoPage { get; }

        public event EventHandler DataRecieved;
    }
}
