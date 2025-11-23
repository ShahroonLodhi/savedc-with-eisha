using System;

namespace SaveDC.ControlPanel.Src.Exceptions
{
    /// <summary>
    /// Summary description for ApplicationConnectionException.
    /// </summary>
    public class ApplicationConnectionException : ApplicationException
    {
        /// <summary>
        /// Class attributes
        /// </summary>

        #region ATTRIBUTES
        private const string ERROR_MESSGE =
            "An error occurred while establishing connection with the database server. This may be due to some connection problem or data base server is not accessible. For more help please contact system administrator.";

        #endregion

        /// <summary>
        /// Custom Constructor
        /// </summary>
        /// <param name="message"></param>

        #region CUESTOM CONSTRUCTOR 1
        public ApplicationConnectionException(string message) : base(message)
        {
            RaiseException();
        }

        #endregion

        /// <summary>
        /// Custom Constructor
        /// </summary>

        #region CUESTOM CONSTRUCTOR 2
        public ApplicationConnectionException() : base(ERROR_MESSGE)
        {
            RaiseException();
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