using System;
using System.Collections;

namespace SaveDC.ControlPanel.Src.Objects
{
    public class Approvals : IEnumerable
    {
        public string Status { get; set; }
        public int StatusId { get; set; }
        public int ApproverId { get; set; }
        public int StudentId { get; set; }
        public string ApproverName { get; set; }
        public string Remarks { get; set; }
        public DateTime ApprovedDate { get; set; }

        public string ApprovedDateShortString
        {
            get { return ApprovedDate.ToShortDateString(); }
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        #endregion
    }
}