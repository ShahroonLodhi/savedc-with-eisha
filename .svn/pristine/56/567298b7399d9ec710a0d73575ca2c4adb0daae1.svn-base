using System;
using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.DB
{
    public class HistoryDAO
    {
        private readonly History history;
        private string searchMode;
        private string searchText;

        public HistoryDAO()
        {
        }

        public HistoryDAO(string SearchText, string SearchMode)
        {
            searchText = SearchText;
            searchMode = SearchMode;
        }

        public HistoryDAO(History oHistory)
        {
            history = oHistory;
        }

        public History LoadHistory(int historyID)
        {
            try
            {
                var dbmanager = new DBManager();
                SqlParameter[] sqlparameter = {
                                                  dbmanager.makeInParam("@HistoryID", SqlDbType.Int, 0, historyID)
                                              };

                SqlDataReader sqldatareader = dbmanager.GetDataReaderProc("LoadHistory", sqlparameter);
                if (sqldatareader.Read())
                {
                    history.HistoryId = historyID;
                    history.IsDoingStudy =
                        Utils.Utils.fixNullBool(sqldatareader.GetValue(sqldatareader.GetOrdinal("IsDoingStudy")));
                    history.ClassDoingStudyIn =
                        Utils.Utils.fixNullInt(sqldatareader.GetValue(sqldatareader.GetOrdinal("ClassDoingStudyIn")));
                    history.ClassLeftIn =
                        Utils.Utils.fixNullInt(sqldatareader.GetValue(sqldatareader.GetOrdinal("ClassLeftIn")));
                    history.PeriodLeftSince =
                        Utils.Utils.fixNullInt(sqldatareader.GetValue(sqldatareader.GetOrdinal("PeriodLeftSince")));
                    history.LastSchoolAttended =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("LastSchoolAttended")));
                    history.Note = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Note")));

                    sqldatareader.Close();

                    return history;
                }
                sqldatareader.Close();

                return null;
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return null;
            }
        }

        public int AddHistory()
        {
            var dbmanager = new DBManager();
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@HistoryId", SqlDbType.Int, 0, history.HistoryId)
                                                ,
                                                dbmanager.makeInParam("@IsDoingStudy", SqlDbType.Bit, 0,
                                                                      history.IsDoingStudy),
                                                dbmanager.makeInParam("@ClassDoingStudyIn", SqlDbType.Int, 0,
                                                                      history.ClassDoingStudyIn),
                                                dbmanager.makeInParam("@ClassLeftIn", SqlDbType.Int, 0,
                                                                      history.ClassLeftIn),
                                                dbmanager.makeInParam("@PeriodLeftSince", SqlDbType.Int, 0,
                                                                      history.PeriodLeftSince),
                                                dbmanager.makeInParam("@LastSchoolAttended", SqlDbType.NVarChar, 255,
                                                                      history.LastSchoolAttended),
                                                dbmanager.makeInParam("@Note", SqlDbType.NVarChar, 255, history.Note),
                                            };

                int nRet = dbmanager.RunProc("AddHistory", parameters);

                return nRet;
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return 0;
            }
            finally
            {
                dbmanager = null;
            }
        }

        //public History[] GetHistorys()
        //{
        //    DBManager dbmanager = new DBManager();
        //    History[] historys = null;
        //    try
        //    {
        //        SqlParameter[] parameters = { 
        //            dbmanager.makeInParam("@SearchText", System.Data.SqlDbType.NVarChar, 250, searchText),
        //             dbmanager.makeInParam("@SearchMode", System.Data.SqlDbType.NVarChar, 250, searchMode)
        //                   };
        //        DataSet data = dbmanager.GetDataSetProc("GetHistorysList", parameters);
        //        if (data != null)
        //        {
        //            if (data.Tables[0].Rows.Count > 0)
        //            {
        //                historys = new History[data.Tables[0].Rows.Count];

        //                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        //                {
        //                    historys[i] = new History();
        //                    DataRow row = data.Tables[0].Rows[i];
        //                    historys[i].HistoryId = Utils.Utils.fixNullInt(row["HistoryId"]);
        //                    historys[i].IsDoingStudy = Utils.Utils.fixNullBool(row["IsDoingStudy"]);
        //                    historys[i].ClassDoingStudyIn = Utils.Utils.fixNullInt(row["ClassDoingStudyIn"]);
        //                    historys[i].ClassLeftIn = Utils.Utils.fixNullInt(row["ClassLeftIn"]);
        //                    historys[i].PeriodLeftSince = Utils.Utils.fixNullInt(row["PeriodLeftSince"]);
        //                    historys[i].LastSchoolAttended = Utils.Utils.fixNullString(row["LastSchoolAttended"]);
        //                    historys[i].Note = Utils.Utils.fixNullString(row["Note"]);
        //                }
        //            }
        //        }
        //        return historys;
        //    }
        //    catch (Exception e)
        //    {

        //        Utils.Utils.LogErrorToFile(e);
        //        return null;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }

        //}

        //public bool DeleteHistory()
        //{
        //    DBManager dbmanager = new DBManager();
        //    try
        //    {
        //        System.Data.SqlClient.SqlParameter[] parameters = { 
        //            dbmanager.makeInParam("@HistoryId", System.Data.SqlDbType.Int, 0, history.HistoryId)
        //        };

        //        int nRet = dbmanager.RunProc("DeleteHistory", parameters);

        //        if (nRet > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }
        //}

        //internal bool HistoryExists()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}
    }
}