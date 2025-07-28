using Microsoft.Extensions.Configuration;
using PluginBase;
using System;
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
        const int DEFAULT_SCAN_RATE   = 5000; // Default reconnection rate in milliseconds
        const int DEFAULT_PORT = 6379;
        const string DEFAULT_SERVER = "localhost";

        private readonly RedisInfo? RedisInfoPage = new RedisInfo();
        public override UserControl? InfoPage { get => RedisInfoPage; }

        private Connection? _connection = null;
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
                int oldSamplingRate = SamplingRate;
                string oldServer = _configuration["server"] ?? DEFAULT_SERVER;
                string oldPort = _configuration["port"] ?? DEFAULT_PORT.ToString();

                SamplingRate = int.Parse(_configuration["sample"] ?? DEFAULT_SAMPLE_RATE.ToString());
                Debug.WriteLine($"Setting configuration for {Name} with sampling rate: {SamplingRate} ms");
                
                string server = _configuration["server"] ?? DEFAULT_SERVER;
                string port = _configuration["port"] ?? DEFAULT_PORT.ToString();

                if(RedisInfoPage is not null ) RedisInfoPage.Url=  $"redis://{server}:{port}" ;

                if (oldSamplingRate != SamplingRate)
                {
                    // If the sampling rate has changed, reset the timer.
                    SetTimer( _connection?.IsConnected() ?? false );
                }

                if( oldPort != port || oldServer != server )
                {
                    // If the server or port has changed, reset the connection.
                    Debug.WriteLine($"Changing Redis connection to {server}:{port}");
                    _connection?.Dispose();
                    _connection = new Connection(server, int.Parse(port));
                }

            }
        }

        public RedisPlugin() : base(STANZA)
        {
            Name = PLUGINNAME;
            Description = "Connect to a REDIS Cache.";
            Icon = Properties.Resources.red;
            SetTimer( false );
        }

        #region Private Methods
        private void SetTimer(bool connected)
        {
            int rate = SamplingRate;

            if ( connected is false)
            {
                SamplingRate = DEFAULT_SCAN_RATE; // Default reconnection rate
            }


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
            // This method is called when the timer elapses.
            // So when the timer ticks we will send some test data
            if (_connection == null || !_connection.IsConnected())
            {
                string server = _configuration?["server"] ?? DEFAULT_SERVER;
                string port = _configuration?["port"] ?? DEFAULT_PORT.ToString();
                Debug.WriteLine($"Re-Connecting to Redis at {server}:{port}");
                try
                {
                    _connection?.Dispose(); // Dispose of the old connection if it exists
                    _connection = new Connection(server, int.Parse(port));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error with redis connection: {ex.Message}");
                } 
            }
            OnDataRecieved(new PluginEventArgs()
            {
                Icon = _connection?.IsConnected() ?? false ? Properties.Resources.green : Properties.Resources.red,
                Name = Name
            });

            SetTimer(_connection?.IsConnected() ?? false);
        }
        #endregion
    }
}
