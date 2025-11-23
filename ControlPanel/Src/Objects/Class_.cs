using System.Collections;

namespace SaveDC.ControlPanel.Src.Objects
{
    public class Class_ : IEnumerable
    {
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        #endregion
    }
}