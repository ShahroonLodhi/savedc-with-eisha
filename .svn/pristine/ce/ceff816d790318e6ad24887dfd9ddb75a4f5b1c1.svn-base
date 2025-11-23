using System;
using SaveDC.ControlPanel.Src.DB;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.Managers
{
    public class ApprovalsManager
    {
        private readonly Approvals approvals;

        public ApprovalsManager(Approvals approvals)
        {
            this.approvals = approvals;
        }

        public Approvals[] GetApprovals()
        {
            return (new ApprovalsDAO(approvals)).GetApprovals();
        }

        public int Save()
        {
            try
            {
                var approvalsDAO = new ApprovalsDAO(approvals);
                return approvalsDAO.AddApprovals();
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return 0;
            }
        }
    }
}