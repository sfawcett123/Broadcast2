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
        private int Port { get; set; } = DEFAULT_PORT;
        private string Server { get; set; } = DEFAULT_SERVER;


        private static System.Timers.Timer? aTimer = null ;
        private IConfigurationSection? _configuration;
        #endregion

        public override IConfigurationSection? Configuration
        {
            get => _configuration;
            set
            {
                if (value == null)
                {
                    Debug.WriteLine($"Setting configuration to Defaults");
                    this.SamplingRate = DEFAULT_SAMPLE_RATE; // Default value
                    this.Port = DEFAULT_PORT;
                    this.Server = DEFAULT_SERVER;
                    return;
                }

                _configuration = value;

                // Read the configuration values from the configuration section.
                int SamplingRate = int.Parse(_configuration["sample"] ?? DEFAULT_SAMPLE_RATE.ToString());    
                string Server = _configuration["server"] ?? DEFAULT_SERVER;
                int Port = int.Parse(_configuration["port"] ?? DEFAULT_PORT.ToString() );

                // Update the properties if they have changed.
                if (this.SamplingRate != SamplingRate)
                {
                    // If the sampling rate has changed, reset the timer.
                    Debug.WriteLine($"Setting configuration for {Name} with sampling rate: {this.SamplingRate} ms");
                    this.SamplingRate = SamplingRate;
                    SetTimer( _connection?.IsConnected() ?? false );
                }

                if( this.Port != Port || this.Server != Server )
                {
                    // If the server or port has changed, reset the connection.
                    Debug.WriteLine($"Changing Redis connection to {this.Server}:{this.Port}");
                    this.Port = Port;   
                    this.Server = Server;
                    Connect(); // Reconnect with the new server and port
                }

            }
        }

        public void Connect()
        {
            if (RedisInfoPage is not null) RedisInfoPage.Url = $"redis://{this.Server}:{this.Port}";
            _connection?.Dispose();
            _connection = new Connection(this.Server, this.Port);
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

            if ( connected == false && aTimer?.Interval != DEFAULT_SCAN_RATE)
            {
                rate = DEFAULT_SCAN_RATE; // Default reconnection rate
                Debug.WriteLine($"Setting timer with interval: {rate} ms");
            }

            if (aTimer != null)
            {
                // If the timer is already running, stop it.
                aTimer.Stop();
                aTimer.Dispose();
            }

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(rate);
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
            if (_connection == null || _connection.IsConnected() == false )
            {
                Debug.WriteLine($"Attempting connecting to Redis at {Server}:{Port}");
                try
                {
                    Connect();
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
