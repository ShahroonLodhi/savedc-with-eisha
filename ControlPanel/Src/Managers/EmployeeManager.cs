using System;
using System.Data;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.DB;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel.Src.Managers
{
    public class EmployeeManager
    {
        private Employee employee;

        public EmployeeManager(Employee employee)
        {
            this.employee = employee;
        }

        public Int32 RecordCount { get; set; }

        //public User[] GetUsers(int nPageNo, int nPageSize)
        //{
        //    var userdao = new UserDAO(user);
        //    User[] users = userdao.GetUsers(nPageNo, nPageSize);
        //    RecordCount = userdao.RecordCount;
        //    return users;
        //}

        public DataTable GetEmployees()
        {
            DataTable ds = new DataTable();
            var common = new Common();
            ds.Load(common.GetEmployees());
            return ds;
        }


        public Employee Load()
        {
            try
            {
                employee = new EmployeeDAO(employee).LoadEmployee(employee.EmployeeID);
                return employee;
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
                var employeeDAO = new EmployeeDAO(employee);
                return employeeDAO.AddEmployee();
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return 0;
            }
        }

        //public bool Exists()
        //{
        //    try
        //    {
        //        return new EmployeeDAO(employee).EmployeeExists();
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}

        //public bool Login()
        //{
        //    try
        //    {
        //        var userDto = new UserDAO(user);
        //        bool status = userDto.LoginUser();
        //        if (status)
        //        {
        //            user = userDto.Load();
        //        }
        //        return status;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}

        //public bool Update()
        //{
        //    try
        //    {
        //        var employeeDAO = new EmployeeDAO(employee);
        //        return employeeDAO.UpdateEmployee();
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}

        //public bool AddRemarks(string remarks)
        //{
        //    try
        //    {
        //        var userDAO = new UserDAO(user);
        //        return userDAO.AddRemarks(remarks);
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}

        //public bool AddAnnualFee(int fee, int month, int year)
        //{
        //    try
        //    {
        //        var userDAO = new UserDAO(user);
        //        return userDAO.AddAnnualFee(fee, month, year);
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}

        //public bool AddNote(string notes)
        //{
        //    try
        //    {
        //        var userDAO = new UserDAO(user);
        //        return userDAO.AddNote(notes);
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}

        //public void LoadSession()
        //{
        //    SaveDCSession.UserId = user.UserID;
        //    SaveDCSession.UserName = user.UserName;
        //    SaveDCSession.UserRoleId = user.UserRoleID.ToString();
        //    SaveDCSession.UserRoleName = user.UserRoleName;

        //    if (user.UserRoleName.ToLower() == "superadmin")
        //        SaveDCSession.UserAccessLevel = UserAccessLevels.SuperAdmin;
        //    else if (user.UserRoleName.ToLower() == "admin")
        //        SaveDCSession.UserAccessLevel = UserAccessLevels.Admin;
        //    else if (user.UserRoleName.ToLower() == "operator")
        //        SaveDCSession.UserAccessLevel = UserAccessLevels.Operator;
        //    else if (user.UserRoleName.ToLower() == "donor")
        //        SaveDCSession.UserAccessLevel = UserAccessLevels.Donor;
        //}

        public bool Delete()
        {
            try
            {
                var employeeDAO = new EmployeeDAO(employee);
                return employeeDAO.DeleteEmployee();
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return false;
            }
        }
    }
}