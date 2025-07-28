using Microsoft.Extensions.Configuration;
using PluginBase;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace TestPlugin
{
    public class TestPlugin : PluginBase.PluginControl
    {
        public override string Description => "A plugin for testing purposes.";
        public override IConfigurationSection? Configuration { get; set; }

        private System.Timers.Timer? aTimer;

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent ;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public TestPlugin()
        {
            Name = "Test Plugin";
            Stanza = "TestPlugin";
            BackgroundImage = Properties.Resources.red;
            SetTimer();
        }

        private void OnTimedEvent(object source, EventArgs e)
        {
            // So when the timer ticks we will send some test data
            OnDataRecieved( e );
        }
    }
}
