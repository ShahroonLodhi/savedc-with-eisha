using System;
using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.DB
{
    public class ClassDAO
    {
        private readonly Class_ class_;

        public ClassDAO()
        {
        }

        public ClassDAO(Class_ oClass_)
        {
            class_ = oClass_;
        }

        public Class_[] GetClasses()
        {
            var dbmanager = new DBManager();
            Class_[] classess = null;
            try
            {
                DataSet data = dbmanager.GetDataSetProc("GetClassesList");
                if (data != null)
                {
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        classess = new Class_[data.Tables[0].Rows.Count];

                        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                        {
                            classess[i] = new Class_();
                            DataRow row = data.Tables[0].Rows[i];
                            classess[i].ClassId = Utils.Utils.fixNullInt(row["ClassId"]);
                            classess[i].ClassName = Utils.Utils.fixNullString(row["ClassName"]);
                        }
                    }
                }
                return classess;
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return null;
            }
            finally
            {
                dbmanager = null;
            }
        }

        public Class_ LoadClass(int classID)
        {
            try
            {
                var dbmanager = new DBManager();
                SqlParameter[] sqlparameter = {dbmanager.makeInParam("@ClassId", SqlDbType.Int, 0, classID)};

                SqlDataReader sqldatareader = dbmanager.GetDataReaderProc("LoadClass", sqlparameter);
                if (sqldatareader.Read())
                {
                    class_.ClassId = classID;
                    class_.ClassName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("ClassName")));

                    sqldatareader.Close();

                    return class_;
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
    }
}