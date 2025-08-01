using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPlugin 
{
    public class APIPlugin : PluginBase.PluginControl
    {
        #region Constants
        public new const string Name = "APIPlugin";
        public new const string Description = "Plugin to Present simulator data as an API service.";
        public new const string Stanza = "API";
        #endregion

        #region IPLUGIN Overrides
        public override IConfigurationSection? Configuration { get; set; }
        #endregion

        #region Constructors
        public APIPlugin() : base(Stanza)
        {
            Icon = Properties.Resources.red;
        }
        #endregion
    }
}
