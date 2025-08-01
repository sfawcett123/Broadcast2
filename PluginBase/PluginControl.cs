using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace PluginBase
{
    public partial class PluginControl : UserControl, IPlugin
    {
        private readonly ToolTip pluginToolTip = new();
        private string _name = "BasePlugin";
        private string _description = "Base Plugin With No Functionality";
        private Image _icon = Properties.Resources.initial;      

        public PluginControl( string stanza = "" )
        {
            InitializeComponent();

            Stanza = stanza;

            pluginToolTip.ShowAlways = true;
            pluginToolTip.SetToolTip(this, "No value set");
            Icon = Properties.Resources.initial;
            BackgroundImage = Icon;
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(100, 100);
            InfoPage = new InfoPage();
        }

        #region Interface Implementation
        public new string Name
        {
            set
            {
                pluginToolTip.SetToolTip(this, value);
                _name = value;
            }
            get { return _name; }
        }

        public System.Drawing.Image Icon { 
            set { _icon = value;
                Size = new Size(100, 100);
                BackgroundImage = value;
                BackgroundImageLayout = ImageLayout.Stretch;
                 
            }
            get { return _icon; }
        }

        public string Description { get { return _description; }
                                    set { _description = value; }
                                  }
        public string Stanza { get; } 
        public virtual UserControl? InfoPage { get; protected set; }    
        public virtual IConfigurationSection? Configuration
        {
            get {
                Debug.WriteLine("Configuration property set in base class, but not implemented.");
                return null;
            }

            set {
                Debug.WriteLine("Configuration property set in base class, but not implemented.");
            }
        }
        #endregion

        public event EventHandler<PluginEventArgs>? DataRecieved;
        protected virtual void OnDataRecieved(PluginEventArgs e)
        {
            DataRecieved?.Invoke(this, e);
        }
      
    }
}
