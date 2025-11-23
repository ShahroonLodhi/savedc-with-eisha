using System;
using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.DB
{
    public class ApprovalsDAO
    {
        private readonly Approvals approvals;

        public ApprovalsDAO()
        {
        }

        public ApprovalsDAO(Approvals oApprovals)
        {
            approvals = oApprovals;
        }

        internal Approvals[] GetApprovals()
        {
            var dbmanager = new DBManager();
            Approvals[] approvals = null;
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@StudentId", SqlDbType.Int, 0,
                                                                      this.approvals.StudentId)
                                            };
                DataSet data = dbmanager.GetDataSetProc("GetApprovals", parameters);
                if (data != null)
                {
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        approvals = new Approvals[data.Tables[0].Rows.Count];

                        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                        {
                            approvals[i] = new Approvals();
                            DataRow row = data.Tables[0].Rows[i];
                            approvals[i].ApproverName = Utils.Utils.fixNullString(row["ApproverName"]);
                            approvals[i].Status = Utils.Utils.fixNullString(row["Status"]);
                            approvals[i].Remarks = Utils.Utils.fixNullString(row["Remarks"]);
                            approvals[i].ApprovedDate = Utils.Utils.fixNullDate(row["ApprovedDate"]);
                        }
                    }
                }
                return approvals;
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

        public int AddApprovals()
        {
            var dbmanager = new DBManager();
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@StudentId", SqlDbType.Int, 0,
                                                                      approvals.StudentId),
                                                dbmanager.makeInParam("@ApprovedBy", SqlDbType.Int, 0,
                                                                      approvals.ApproverId),
                                                dbmanager.makeInParam("@StatusId", SqlDbType.Int, 0, approvals.StatusId)
                                                ,
                                                dbmanager.makeInParam("@Remarks", SqlDbType.NVarChar, 255,
                                                                      approvals.Remarks)
                                            };

                int nRet = dbmanager.RunProc("AddApproval", parameters);

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
    }
}