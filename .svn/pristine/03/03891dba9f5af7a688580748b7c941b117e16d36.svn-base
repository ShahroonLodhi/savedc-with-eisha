using SaveDC.ControlPanel.Src.Exceptions;

namespace SaveDC.ControlPanel.Src.Configurations
{
    public class DBConfiguration : Configuration
    {
        #region CLASS ATTRIBUTES

        private string catalog;
        private string connectionString;
        private string password;
        private string serverName;
        private string userId;

        #endregion

        #region DEFAULT CONSTRUCTOR

        public DBConfiguration()
        {
            //Loading the configuration 
            loadConfiguration();
        }

        #endregion

        #region LOAD CONFIGURATION

        private void loadConfiguration()
        {
            if (ReadConfigKey("DbServer") == null || ReadConfigKey("DbServer") == "")
            {
                //Throw Custom exception class
                throw new ApplicationParameterInValidException("DbServer");
            }
            serverName = ReadConfigKey("DbServer");


            if (ReadConfigKey("Catalog") == null || ReadConfigKey("Catalog") == "")
            {
                //Throw Custom exception class
                throw new ApplicationParameterInValidException("Catalog");
            }
            catalog = ReadConfigKey("Catalog");

            if (ReadConfigKey("UID") == null || ReadConfigKey("UID") == "")
            {
                //Throw Custom exception class
                throw new ApplicationParameterInValidException("UID");
            }
            userId = ReadConfigKey("UID");

            if (ReadConfigKey("PWD") == null)
            {
                //Throw Custom exception class
                throw new ApplicationParameterInValidException("PWD");
            }
            password = ReadConfigKey("PWD");

            ///Creating the connection string ;
            connectionString = "Data Source=" + serverName + ";";
            connectionString += "UID=" + userId + ";";
            connectionString += "Pwd=" + password + ";";
            connectionString += "Initial Catalog=" + catalog + ";";
        }

        # endregion

        #region Class Services

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public string Server
        {
            get { return serverName; }
        }

        public string Catalog
        {
            get { return catalog; }
        }

        public string UserId
        {
            get { return userId; }
        }

        public string Password
        {
            get { return password; }
        }

        #endregion
    }
}