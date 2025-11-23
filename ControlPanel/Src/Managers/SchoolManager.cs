using System;
using System.Data;
using SaveDC.ControlPanel.Src.DB;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.Managers
{
    public class SchoolManager
    {
        #region Declarations

        private readonly School school;
        private int nUpdatedRecordID;

        #endregion

        public SchoolManager(School school)
        {
            this.school = school;
        }

        public Int32 RecordCount { get; set; }

        public School[] GetSchools()
        {
            var schoolDao = new SchoolDAO(school);
            School[] list = schoolDao.GetSchools(0, 0);
            RecordCount = schoolDao.RecordCount;
            return list;
        }

        public DataSet GetSchoolsInDS()
        {
            var schoolDao = new SchoolDAO(school);
            DataSet list = schoolDao.GetSchoolsInDS(0, 0);
            return list;
        }

        

        public School[] GetSchools(int nPageNo, int nPageSize)
        {
            var schoolDao = new SchoolDAO(school);
            School[] list = schoolDao.GetSchools(nPageNo, nPageSize);
            RecordCount = schoolDao.RecordCount;
            return list;
        }

        public School Load()
        {
            try
            {
                return new SchoolDAO(school).LoadSchool(school.SchoolID);
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return null;
            }
        }

        public int Save()
        {
            try
            {
                var schoolDAO = new SchoolDAO(school);
                return schoolDAO.AddSchool();
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return 0;
            }
        }

        public bool Delete()
        {
            try
            {
                var schoolDAO = new SchoolDAO(school);
                return schoolDAO.DeleteSchool();
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return false;
            }
        }
    }
}