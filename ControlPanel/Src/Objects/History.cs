using System.Collections;

namespace SaveDC.ControlPanel.Src.Objects
{
    public class History : IEnumerable
    {
        public string Note { get; set; }

        public int HistoryId { get; set; }

        public bool IsDoingStudy { get; set; }

        public int ClassDoingStudyIn { get; set; }

        public int ClassLeftIn { get; set; }

        public int PeriodLeftSince { get; set; }

        public string LastSchoolAttended { get; set; }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        #endregion
    }
}