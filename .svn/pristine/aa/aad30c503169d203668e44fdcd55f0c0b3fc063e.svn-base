using System;
using System.Collections;

namespace SaveDC.ControlPanel.Src.Objects
{
    public class Balance : IEnumerable
    {
        private DateTime fundDate;

        public decimal ABL { get; set; }

        public decimal FBL { get; set; }

        public string FundDateShortString
        {
            get { return fundDate.ToString("dd MMM yyyy"); }
        }

        public DateTime FundDate
        {
            get { return fundDate; }
            set { fundDate = value; }
        }

        public string FundedOnDateString { get; set; }

        public int BalanceID { get; set; }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        #endregion
    }

    public class Fund : IEnumerable
    {
        private DateTime fundDate;

        public string DonorName { get; set; }

        public string Note { get; set; }

        public decimal FundAmount { get; set; }

        public string Attachment { get; set; }

        public string FundDateShortString
        {
            get { return fundDate.ToString("dd MMM yyyy"); }
        }


        public DateTime FundDate
        {
            get { return fundDate; }
            set { fundDate = value; }
        }

        public string FundedOnDateString { get; set; }

        public int DonorID { get; set; }

        public int FundID { get; set; }

        public int FundPostedBy { get; set; }
        public string FundPostedName { get; set; }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        #endregion
    }
}