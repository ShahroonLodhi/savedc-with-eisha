using System;
using System.Collections;

namespace SaveDC.ControlPanel.Src.Objects
{
    public class Varification
    {
        public bool IsVarified { get; set; }
        public string Remarks { get; set; }
        public int VarifiedBy { get; set; }
        public DateTime VarificationDate { get; set; }
    }

    public class Student : IEnumerable
    {
        private string note;

        public Student()
        {
            Varification = new Varification();
        }

        public Varification Varification { get; set; }

        public string ClassName { set; get; }
        public string FamilyName { set; get; }
        public string SchoolName { set; get; }
        public string DonorName { set; get; }
        public string StatusName { set; get; }
        public string ImageGUID { set; get; }
        public bool IsVarificationExists { set; get; }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EducationalLevel { get; set; }

        public int StatusId { get; set; }

        public int ClassId { get; set; }

        public string Notes
        {
            get { return note; }
            set { note = value; }
        }

        public int HistoryId { get; set; }

        public int FamilyId { get; set; }

        public int SchoolId { get; set; }

        public int DonorId { get; set; }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        #endregion
    }
}