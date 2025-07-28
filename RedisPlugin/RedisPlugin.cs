using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Forms;

namespace RedisPlugin
{
    public class RedisPlugin : PluginBase.PluginControl
    {
        const string PLUGINNAME = "RedisPlugin";
        const string STANZA = "Redis";
        const int DEFAULT_SAMPLE_RATE = 2000;

        private readonly RedisInfo? RedisInfoPage = new RedisInfo();
        public override UserControl? InfoPage { get => RedisInfoPage; }
        #region Private Attributes
        private int SamplingRate { get; set; } = DEFAULT_SAMPLE_RATE; // Default sampling rate
        private System.Timers.Timer? aTimer;
        private IConfigurationSection? _configuration;
        #endregion

        public override IConfigurationSection? Configuration
        {
            get => _configuration;
            set
            {
                if (value == null)
                {
                    SamplingRate = DEFAULT_SAMPLE_RATE; // Default value
                    Debug.WriteLine($"Setting configuration to Defaults");
                    return;
                }

                _configuration = value;
                SamplingRate = int.Parse(_configuration["sample"] ?? DEFAULT_SAMPLE_RATE.ToString());
                Debug.WriteLine($"Setting configuration for {Name} with sampling rate: {SamplingRate} ms");
                int oldSamplingRate = SamplingRate;
             
                string server = _configuration["server"] ?? "localhost";
                string port = _configuration["port"] ?? "6379";

                if(RedisInfoPage is not null ) RedisInfoPage.Url=  $"redis://{server}:{port}" ;

                if (oldSamplingRate != SamplingRate)
                {
                    // If the sampling rate has changed, reset the timer.
                    SetTimer();
                }
            }
        }

        public RedisPlugin() : base(STANZA)
        {
            Name = PLUGINNAME;
            Description = "Connect to a REDIS Cache.";
            Icon = Properties.Resources.red;
            SetTimer();
        }

        #region Private Methods
        private void SetTimer()
        {
            Debug.WriteLine($"Setting timer with interval: {SamplingRate} ms");

            if (aTimer != null)
            {
                // If the timer is already running, stop it.
                aTimer.Stop();
                aTimer.Dispose();
            }

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(SamplingRate);
            // Hook up the Elapsed event for the timer. 
            // This event will be raised when the timer interval elapses.
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        #endregion

        #region Event Handlers
        private void OnTimedEvent(object? source, EventArgs e)
        {
            // So when the timer ticks we will send some test data
            OnDataRecieved(e);
        }
        #endregion
    }
}
