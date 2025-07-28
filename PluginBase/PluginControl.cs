using Microsoft.Extensions.Configuration;

namespace PluginBase
{
    public partial class PluginControl : UserControl, IPlugin
    {
        private readonly ToolTip toolTip1 = new();
        private string _name = "TestPlugin";
        private string _stanza = "Plugins";

        public PluginControl()
        {
            InitializeComponent();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this, "No value set");
            this.BackColor = Color.LightGray;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        #region Interface Implementation
        public new string Name
        {
            set
            {
                toolTip1.SetToolTip(this, value);
                _name = value;
            }
            get { return _name; }
        }

        public virtual string Description => throw new NotImplementedException();

        public virtual IConfigurationSection? Configuration
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public string Stanza { 
            get { return _stanza; }
            set { _stanza = value; } 
        }

        public event EventHandler? DataRecieved;

        protected virtual void OnDataRecieved(EventArgs e)
        {
            DataRecieved?.Invoke(this, e);
        }
        #endregion
    }
}
