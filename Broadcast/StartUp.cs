using Microsoft.Extensions.Configuration;
using PluginBase;
using System.Diagnostics;
using System.Reflection;

namespace Broadcast
{
 
    partial class StartUp : Form
    {
        public void ShowDialog(IConfigurationRoot Configuration , MainForm parent)
        {
            this.Show();
            this.Refresh();
            textBox.Clear();
            textBox.AppendLine("Starting up Broadcast");
            if (Configuration is not null)
            {
                string[] pluginPaths = Directory.GetFiles(Configuration["plugins"] ?? "./plugins", "*.dll");
                foreach (var control in ReadDlls(pluginPaths, Configuration , textBox))
                {
                    textBox.AppendLine(  $"Adding control {control.Name} Configuration {control.Stanza}") ;
                    parent.flowLayoutPanel1.Controls.Add(control);
                    control.Click += parent.PluginControl_Click;
                    control.DataRecieved += parent.PluginControl_DataReceived;
                }
            }
            this.Close();  
        }

        public StartUp()
        {
            InitializeComponent();
            this.Text = String.Format("Broadcast Start up");
        }

        static Assembly LoadPlugin(string relativePath, TextBox tb)
        {
            try
            {
                PluginLoadContext loadContext = new(relativePath);
                return loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(relativePath));
            }
            catch (Exception ex)
            {
                tb.AppendLine($"Error loading plugin {relativePath}: {ex.Message}");
                return null!;
            }
        }
        static public IEnumerable<PluginControl> ReadDlls(string[] pluginPaths, IConfigurationRoot Configuration , TextBox tb)
        {
            List<PluginControl> commands = [];

            if (pluginPaths == null || pluginPaths.Length == 0)
            {
                tb.AppendLine("No plugins specified.");
                return commands;
            }
            foreach (string relativePath in pluginPaths)
            {
                Assembly assembly = LoadPlugin(relativePath, tb);
                if (assembly != null)
                {
                    commands.AddRange(CreateCommands(assembly , tb));
                    foreach (IPlugin command in commands)
                    {
                        command.Configuration = Configuration.GetSection(command.Stanza);
                    }
                }
            }
            return commands;
        }
        static List<PluginControl> CreateCommands(Assembly assembly , TextBox tb)
        {
            List<PluginControl> commands = [];

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    tb.AppendLine($"Found type: {type.FullName} which implements IPlugin");

                    // Ensure the instance is not null and handle potential nullability issues
                    if (Activator.CreateInstance(type) is PluginControl instance)
                    {
                        commands.Add(instance);
                    }
                    else
                    {
                        tb.AppendLine($"Failed to create an instance of type: {type.FullName}");
                    }
                }
            }

            return commands;
        }

    }

    public static class WinFormsExtensions
    {
        public static void AppendLine(this TextBox source, string value)
        {
            Debug.WriteLine(value);

            if (source.Text.Length == 0)
                source.Text = value;
            else
                source.AppendText("\r\n" + value);

        }
    }
}
