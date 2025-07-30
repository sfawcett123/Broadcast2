using Microsoft.Extensions.Configuration;
using PluginBase;  
using System.Diagnostics;
using System.Reflection;

namespace Broadcast
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
  
        [STAThread]
        static void Main()
        {
            IConfigurationRoot Configuration;

            var builder = new ConfigurationBuilder()
                    .SetBasePath( Directory.GetCurrentDirectory())
                    .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables(); 

            Configuration = builder.Build();
      
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm( Configuration ));
        }
        static public IEnumerable<PluginControl> ReadDlls(string[] pluginPaths, IConfigurationRoot Configuration)
        {
            List<PluginControl> commands = [];

            if (pluginPaths == null || pluginPaths.Length == 0)
            {
                Debug.WriteLine("No plugins specified.");
                return commands;
            }
            foreach (string relativePath in pluginPaths)
            {
                Assembly assembly = LoadPlugin(relativePath);

                commands.AddRange(CreateCommands(assembly));
                foreach (IPlugin command in commands)
                {
                    command.Configuration = Configuration.GetSection(command.Stanza);
                }
            }
            return commands;
        }
        static Assembly LoadPlugin(string relativePath)
        { 
            PluginLoadContext loadContext = new(relativePath);
            return loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(relativePath));
        }
        static List<PluginControl> CreateCommands(Assembly assembly)
        {
            List<PluginControl> commands = [];

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    Debug.WriteLine($"Found type: {type.FullName} which implements IPlugin");

                    // Ensure the instance is not null and handle potential nullability issues
                    if (Activator.CreateInstance(type) is PluginControl instance)
                    {
                        commands.Add(instance);
                    }
                    else
                    {
                        Debug.WriteLine($"Failed to create an instance of type: {type.FullName}");
                    }
                }
            }

            return commands;
        }
    }
}