using System;

namespace SaveDC.ControlPanel.Src.Exceptions
{
    /// <summary>
    /// This class is a custom defined exception class, Raised when some property value
    /// is missing or invalid
    /// Author: Asim 10 - 2007
    /// </summary>
    public class ApplicationParameterInValidException : ApplicationException
    {
        private const string CONTACT_MESSGE = "Please contact your system administrator for further details.";

        /// <summary>
        /// Class attributes
        /// </summary>

        #region ATTRIBUTES
        private readonly string message;

        #endregion

        /// <summary>
        /// Custom Constructor
        /// <param name="missingProperty">missing property name</param>
        /// </summary>

        #region CUESTOM CONSTRUCTOR 1
        public ApplicationParameterInValidException(string missingProperty)
        {
            message = "The application property '" + missingProperty.ToUpper() +
                      "' is missing or contains an invalid entry.";
            message += CONTACT_MESSGE;
            RaiseException();
        }

        #endregion

        /// <summary>
        /// Custom Constructor
        /// </summary>

        #region CUESTOM CONSTRUCTOR 2
        public ApplicationParameterInValidException()
        {
            message = "Some or any of the application property is invalid.";
            message += CONTACT_MESSGE;
            RaiseException();
        }

        #endregion

        /// <summary>
        /// Exception overridden method "Message"
        /// </summary>

        #region Overridden method of Exception "Message"
        public override string Message
        {
            get { return message; }
        }

        #endregion

        /// <summary>
        /// Private method for raising an exception  
        /// </summary>

        #region RAISE EXCEPTION
        private void RaiseException()
        {
            throw this;
        }

        #endregion
    }
}