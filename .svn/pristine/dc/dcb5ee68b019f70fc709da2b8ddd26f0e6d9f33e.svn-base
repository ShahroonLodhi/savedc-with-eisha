using System;
using System.Configuration;

namespace SaveDC.ControlPanel.Src.Configurations
{
    /// <summary>
    /// Summary description for Configuratiopn.
    /// </summary>
    public class Configuration
    {
        public string ReadConfigKey(string szKey)
        {
            try
            {
                string szKeyValue = "";
                if (ConfigurationManager.AppSettings[szKey] == null || ConfigurationManager.AppSettings[szKey] == "")
                    szKeyValue = null;
                else
                    szKeyValue = Convert.ToString(ConfigurationManager.AppSettings[szKey]);

                return szKeyValue;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}