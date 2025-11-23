using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Configurations;

namespace SaveDC.ControlPanel.Src.DB
{
    /// <summary>
    /// This class is a wrapper on the NuestraMusicaDAO class
    /// Its Provides its method after performing some managing tasks
    /// </summary>
    public class DBManager
    {
        #region CLASS ATTRIBUTES

        private readonly SaveDCDAO conn;

        #endregion

        /// <summary>
        /// Default Constructor 
        /// </summary>

        #region DEFAULT CONSTRUCTOR
        public DBManager()
        {
            //Getting configuration obj and passing it to NuestraMusicaDAO class 
            Configuration config = new DBConfiguration();
            conn = new SaveDCDAO(config);
        }

//emd method

        #endregion

        /// <summary>
        /// Run stored procedure.
        /// </summary>
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="prams">Stored procedure params.</param>
        /// <returns type="int" >Stored procedure return value.</returns>

        #region RUN PROCEDURE
        public int RunProc(string procName, SqlParameter[] prams)
        {
            return conn.RunProc(procName, prams);
        }

        #endregion

        /// <summary>
        /// Make input param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>

        #region MAKE IN PARAMETER
        public SqlParameter makeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return conn.makeInParam(ParamName, DbType, Size, Value);
        }

        #endregion

        /// <summary>
        /// Make out param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <returns>New parameter.</returns>

        #region MAKE OUT PARAMETER
        public SqlParameter makeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return conn.makeOutParam(ParamName, DbType, Size);
        }

        #endregion

        /// <summary>
        /// Get the data reader form the data base by executing the mentioned 
        /// Prcedure and its params
        /// </summary>
        /// <remarks>
        /// This is a wrapper method on the connection class
        /// </remarks> 
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="dataReader">Return result of procedure.</param>
        /// 

        #region GET DATA READER WITH PARAMS
        public SqlDataReader GetDataReaderProc(string procName, SqlParameter[] prams)
        {
            return conn.GetDataReaderProc(procName, prams);
        }

        #endregion

        /// <summary>
        /// Get the data reader form the data base by executing the mentioned 
        /// Prcedure
        /// </summary>
        /// <remarks>
        /// This is a wrapper method on the connection class
        /// </remarks> 
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="dataReader">Return result of procedure.</param>
        /// 

        #region GET DATA READER WITH OUT PARAMS
        public SqlDataReader GetDataReaderProc(string procName)
        {
            return conn.GetDataReaderProc(procName);
        }

        #endregion

        public int ExecuteNonQuery(string strSQL)
        {
            return conn.ExecuteNonQuery(strSQL);
        }

        public string ExecuteScalar(string strSQL)
        {
            return conn.ExecuteScalar(strSQL);
        }

        public SqlDataReader GetDataReader(string Query)
        {
            return conn.GetDataReader(Query);
        }

        #region GET DATA SET WITH PARAMS

        public DataSet GetDataSetProc(string procName, SqlParameter[] param)
        {
            return conn.GetDataSetProc(procName, param);
        }

        #endregion

        #region GET DATA SET WITHOUT  PARAMS

        public DataSet GetDataSet(string strSQL)
        {
            return conn.GetDataSet(strSQL);
        }

        public DataSet GetDataSetProc(string procName)
        {
            return conn.GetDataSetProc(procName);
        }

        public DataSet GetDataSetProc(string procName, string datatableName)
        {
            return conn.GetDataSetProc(procName, datatableName);
        }

        public DataSet GetDataSetProc(string procName, string datatableName, SqlParameter[] param)
        {
            return conn.GetDataSetProc(procName, datatableName, param);
        }

        #endregion
    }
}