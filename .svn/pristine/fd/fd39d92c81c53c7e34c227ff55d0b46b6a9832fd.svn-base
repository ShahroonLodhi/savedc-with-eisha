using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using SaveDC.ControlPanel.Src.Configurations;

namespace SaveDC.ControlPanel.Src.Utils
{
    public class Utils
    {
        public const string APPLICATION_LOG_FILE_NAME = "ApplicationErrorLog.txt";
        public const string MESSAGE_FILE_NAME = "MessagesDetail.xml";
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

        // Define supported KEY characters divided into groups.
        // You can add (or remove) characters to (from) these groups.
        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "123456789";
        private static string PASSWORD_CHARS_SPECIAL = @"!@#$%^()=[]{}";

        public static string GetMessageText(string messageCode)
        {
            try
            {
                string defaultPath = (new ApplicationCofiguration().MessageFilePath + "\\" + MESSAGE_FILE_NAME);
                var oDoc = new XmlDocument();
                oDoc.Load(defaultPath);
                return oDoc.SelectSingleNode("//Message[@id = '" + messageCode + "']").InnerText;
            }
            catch
            {
            }
            return "Error details not found.";
        }

        public static void LogErrorToFile(Exception e)
        {
            string defaultPath = (new ApplicationCofiguration().ErrorLogFilePath + "\\" + APPLICATION_LOG_FILE_NAME);

            string message = "";

            message = "\n\r**************************************" + DateTime.Now.ToString() +
                      "*****************************************";
            message = message + "\n\rAn exception occured.\n\rThe original error message was: '" + e.Message + "'.";
            message = message + "\n\rThe error source was: '" + e.Source + "'.";
            message = message + "\n\rThe stack trace was:" + e.StackTrace;
            message = message +
                      "\n\r****************************************************************************************";

            try
            {
                var fileInfo = new FileInfo(defaultPath);
                if (!fileInfo.Exists)
                    fileInfo.Create();
                StreamWriter writer = fileInfo.AppendText();
                writer.WriteLine(message);
                writer.Close();
            }
            catch (Exception)
            {
            }

            if (e.GetType().Name.Equals("ApplicationConnectionException"))
            {
                //  throw e;
            }
        }

        public static string fixNullString(Object objValue)
        {
            try
            {
                if (objValue == null)
                {
                    return "";
                }
                Type t = objValue.GetType();
                if (t.Name.Equals("DBNull"))
                {
                    return "";
                }
                else
                {
                    return objValue.ToString().Trim();
                }
            }
            catch
            {
                return "";
            }
        }

        public static int fixNullInt(Object objValue)
        {
            try
            {
                if (objValue == null)
                {
                    return 0;
                }
                Type t = objValue.GetType();
                if (t.Name.Equals("DBNull"))
                {
                    return 0;
                }
                else if (objValue.ToString() == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(objValue.ToString());
                }
            }
            catch
            {
                return 0;
            }
        }

        public static bool fixNullBool(Object objValue)
        {
            if (objValue == null)
            {
                return false;
            }
            Type t = objValue.GetType();
            if (t.Name.Equals("DBNull"))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(objValue.ToString());
            }
        }

        public static char fixNullChar(Object objValue)
        {
            if (objValue == null)
            {
                return ' ';
            }
            Type t = objValue.GetType();
            if (t.Name.Equals("DBNull"))
            {
                return ' ';
            }
            else
            {
                return Convert.ToChar(objValue.ToString());
            }
        }

        public static DateTime fixNullDate(Object objValue)
        {
            try
            {
                if (objValue == null)
                {
                    return Convert.ToDateTime(null);
                }

                Type t = objValue.GetType();
                if (t.Name.Equals("DBNull"))
                {
                    return Convert.ToDateTime(null);
                }
                else
                {
                    return Convert.ToDateTime(objValue.ToString());
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static decimal fixNullDecimal(Object objValue)
        {
            try
            {
                if (objValue == null)
                    return 0;

                Type t = objValue.GetType();

                if (t.Name.Equals("DBNull"))
                    return 0;
                else
                    return Convert.ToDecimal(objValue.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static double fixNulldouble(Object objValue)
        {
            if (objValue == null)
                return 0;

            Type t = objValue.GetType();

            if (t.Name.Equals("DBNull"))
                return 0;
            else
                return Convert.ToDouble(objValue.ToString().Trim());
        }

        //Random Alpha Numeric Key Generation code.

        // Define default min and max KEY lengths.

        /// <summary>
        /// Generates a random Alpha numeric KEY.
        /// </summary>
        /// <returns>
        /// Randomly generated Alpha numeric KEY.
        /// </returns>
        /// <remarks>
        /// The length of the generated Alpha numeric KEY will be determined at
        /// random. It will be no shorter than the minimum default and
        /// no longer than maximum default.
        /// </remarks>
        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                            DEFAULT_MAX_PASSWORD_LENGTH);
        }

        /// <summary>
        /// Generates a random Alpha numeric KEY of the exact length.
        /// </summary>
        /// <param name="length">
        /// Exact Alpha numeric KEY length.
        /// </param>
        /// <returns>
        /// Randomly generated Alpha numeric KEY.
        /// </returns>
        public static string Generate(int length)
        {
            return Generate(length, length);
        }

        /// <summary>
        /// Generates a random Alpha numeric KEY.
        /// </summary>
        /// <param name="minLength">
        /// Minimum Alpha numeric KEY length.
        /// </param>
        /// <param name="maxLength">
        /// Maximum Alpha numeric KEY length.
        /// </param>
        /// <returns>
        /// Randomly generated Alpha numeric KEY.
        /// </returns>
        /// <remarks>
        /// The length of the generated Alpha numeric KEY will be determined at
        /// random and it will fall with the range determined by the
        /// function parameters.
        /// </remarks>
        public static string Generate(int minLength, int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported Alpha numeric KEY characters
            // grouped by types. You can remove character groups from this
            // array, but doing so will weaken the Alpha numeric KEY strength.
            var charGroups = new[]
                                 {
                                     PASSWORD_CHARS_LCASE.ToCharArray(),
                                     PASSWORD_CHARS_UCASE.ToCharArray(),
                                     PASSWORD_CHARS_NUMERIC.ToCharArray(),
                                     PASSWORD_CHARS_SPECIAL.ToCharArray()
                                 };

            // Use this array to track the number of unused characters in each
            // character group.
            var charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            var leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            var randomBytes = new byte[4];

            // Generate 4 random bytes.
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                       randomBytes[1] << 16 |
                       randomBytes[2] << 8 |
                       randomBytes[3];

            // Now, this is real randomization.
            var random = new Random(seed);

            // This array will hold Alpha numeric KEY characters.
            char[] szKey = null;

            // Allocate appropriate memory for the Alpha numeric KEY.
            if (minLength < maxLength)
                szKey = new char[random.Next(minLength, maxLength + 1)];
            else
                szKey = new char[minLength];

            // Index of the next character to be added to Alpha numeric KEY.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate Alpha numeric KEY characters one at a time.
            for (int i = 0; i < szKey.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                                                         lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the Alpha numeric KEY.
                szKey[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                        charGroups[nextGroupIdx].Length;
                    // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                            charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                    // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                            leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert Alpha numeric KEY characters into a string and return the result.
            return new string(szKey);
        }

        public static string ReadConfigKey(string szKey)
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