using System;
using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Exceptions;

namespace SaveDC.ControlPanel.Src.DB
{
    public class SaveDCDAO
    {
        /// <summary>
        /// Properties of the class 
        /// </summary>

        #region CLASS ATTRIBUTES
        private readonly Configuration config;

        private SqlConnection sqlConn;

        #endregion

        /// <summary>
        /// class properties
        /// </summary>
        /// <summary>
        /// Custom Constructor
        /// </summary>
        /// <param name="config">takes the Configuration obj</param>
        /// 

        #region CUSTOM CONSTRUCTOR
        public SaveDCDAO(Configuration config)
        {
            this.config = config;
        }

        #endregion

        /// <summary>
        /// Run stored procedure.
        /// </summary>
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="prams">Stored procedure params.</param>
        /// <returns>Stored procedure return value of type int.</returns>

        #region RUN ROCEDURE
        public int RunProc(string procName, SqlParameter[] prams)
        {
            //Oppening the connection
            Open();
            //Creating commad
            SqlCommand cmd = CreateCommand(procName, prams);
            //Executing 
            cmd.ExecuteNonQuery();
            //Closing Connection
            Close();
            //returning value
            return (int) cmd.Parameters["ReturnValue"].Value;
        }

        #endregion

        /// <summary>
        /// This method opens the connection with the DB
        /// </summary>

        #region OPEN CONNECTION
        private void Open()
        {
            /* open connection */
            if (sqlConn == null)
            {
                sqlConn = new SqlConnection(((DBConfiguration) config).ConnectionString);
                try
                {
                    sqlConn.Open();
                }
                catch (Exception)
                {
                    throw new ApplicationConnectionException();
                }
            }
        }

        #endregion

        /// <summary>
        /// Close the connection.
        /// </summary>

        #region CLOSE CONNECTION
        public void Close()
        {
            if (sqlConn != null)
            {
                sqlConn.Close();
                sqlConn.Dispose();
                sqlConn = null;
            }
        }

        #endregion

        /// <summary>
        /// Create command object used to call stored procedure.
        /// </summary>
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="prams">Params to stored procedure.</param>
        /// <returns>Command object.</returns>

        #region CREATE COMMAND
        public SqlCommand CreateCommand(string szQuery)
        {
            // make sure connection is open
            Open();
            var cmd = new SqlCommand(szQuery, sqlConn);

            cmd.CommandTimeout = 1000;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            //command = new SqlCommand( sprocName, new SqlConnection( ConfigManager.DALConnectionString ) );
            var cmd = new SqlCommand(procName, sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            // add proc parameters
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }

            // return param
            cmd.Parameters.Add(
                new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                                 ParameterDirection.ReturnValue, false, 0, 0,
                                 string.Empty, DataRowVersion.Default, null));

            return cmd;
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
        /// 

        #region MAKE IN PARAM
        public SqlParameter makeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
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
        /// 

        #region MAKE OUT PARAM
        public SqlParameter makeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        #endregion

        /// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
        /// 

        #region MAKE PARAM
        private SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction,
                                       object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }

        #endregion

        /// <summary>
        /// Get the data reader form the data base by executing the mentioned 
        /// Prcedure and its params
        /// </summary>
        /// <remarks>
        /// The data reader is a connected object so it is the responsibility of the
        /// user to close it explicitely 
        /// </remarks> 
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="dataReader">Return result of procedure.</param>
        /// 

        #region GET DATA READER WITH PARAMS
        public SqlDataReader GetDataReaderProc(string procName, SqlParameter[] prams)
        {
            //opening the conmnection 
            Open();

            SqlCommand cmd = CreateCommand(procName, prams);
            //getting the reader 
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        /// <summary>
        /// Get the data reader form the data base by executing the mentioned 
        /// Prcedure
        /// </summary>
        /// <remarks>
        /// The data reader is a connected object so it is the responsibility of the
        /// user to close it explicitely 
        /// </remarks> 
        /// <param name="procName">Name of stored procedure.</param>
        /// <param name="dataReader">Return result of procedure.</param>
        /// 

        #region GET DATA READER WITH OUT PARAMS
        public SqlDataReader GetDataReaderProc(string procName)
        {
            //opening the conmnection 
            Open();
            SqlCommand cmd = CreateCommand(procName, null);
            //executing the proc
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        public DataSet GetDataSet(string strSql)
        {
            Open();
            SqlCommand cmd = CreateCommand(strSql);
            var adapter = new SqlDataAdapter(cmd);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset;
        }

        #region Get Dataset

        /// <summary>
        /// Create Dataset With Parameters
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public DataSet GetDataSetProc(string procName, SqlParameter[] prams)
        {
            //opening the conmnection 
            Open();
            SqlCommand cmd = CreateCommand(procName, prams);
            var adapter = new SqlDataAdapter(cmd);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            Close();
            return dataset;
        }

        /// <summary>
        /// return dataset without parammeter 
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public DataSet GetDataSetProc(string procName)
        {
            Open();
            SqlCommand cmd = CreateCommand(procName, null);
            var adapter = new SqlDataAdapter(cmd);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            Close();
            return dataset;
        }

        public DataSet GetDataSetProc(string procName, string datatableName)
        {
            Open();
            SqlCommand cmd = CreateCommand(procName, null);
            var adapter = new SqlDataAdapter(cmd);
            var dataset = new DataSet();
            adapter.Fill(dataset, datatableName);
            Close();
            return dataset;
        }

        public DataSet GetDataSetProc(string procName, string datatableName, SqlParameter[] prams)
        {
            //opening the conmnection 
            Open();
            SqlCommand cmd = CreateCommand(procName, prams);
            var adapter = new SqlDataAdapter(cmd);
            var dataset = new DataSet();
            adapter.Fill(dataset, datatableName);
            Close();
            return dataset;
        }

        #endregion

        #region Query Function

        public int ExecuteNonQuery(string strSQL)
        {
            Open();
            SqlCommand sqlCmd = CreateCommand(strSQL);
            int RowsEffected = sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            return RowsEffected;
        }


        public string ExecuteScalar(string strSQL)
        {
            Open();
            SqlCommand sqlCmd = CreateCommand(strSQL);
            string szReturnVal = "";
            if (sqlCmd.ExecuteScalar() != DBNull.Value)
            {
                szReturnVal = Convert.ToString(sqlCmd.ExecuteScalar());
            }
            sqlCmd.Dispose();
            return szReturnVal;
        }


        public SqlDataReader GetDataReader(string Query)
        {
            Open();
            SqlDataReader dataReader = null;
            SqlCommand cmd = CreateCommand(Query);
            dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dataReader;
        }

        #endregion
    }

//end class
}