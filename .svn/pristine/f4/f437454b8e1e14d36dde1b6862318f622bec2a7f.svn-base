using SaveDC.ControlPanel.Src.Exceptions;

namespace SaveDC.ControlPanel.Src.Configurations
{
    public class ApplicationCofiguration : Configuration
    {
        #region CLASS ATTRIBUTES

        private string errorLogFilePath;
        private string messageFilePath;

        #endregion

        #region DEFAULT CONSTRUCTOR

        public ApplicationCofiguration()
        {
            LoadConfiguration();
        }

        #endregion

        #region LOAD CONFIGURATION

        private void LoadConfiguration()
        {
            string szLogNodeValue = ReadConfigKey("ErrorLogFilePath");
            if (string.IsNullOrEmpty(szLogNodeValue))
            {
                //Throw Custom exception class
                throw new ApplicationParameterInValidException("ErrorLogFilePath");
            }
            errorLogFilePath = szLogNodeValue;


            string szMessageNodeValue = ReadConfigKey("MessageFilePath");
            if (string.IsNullOrEmpty(szMessageNodeValue))
            {
                //Throw Custom exception class
                throw new ApplicationParameterInValidException("MessageFilePath");
            }
            messageFilePath = szMessageNodeValue;
        }

        # endregion

        #region Class Services

        public string ErrorLogFilePath
        {
            get { return errorLogFilePath; }
        }

        public string MessageFilePath
        {
            get { return messageFilePath; }
        }

        #endregion
    }
}