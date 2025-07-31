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

    }
}