using System;
using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.DB
{
    public class EmployeeDAO
    {
        private readonly Employee employee;

        public EmployeeDAO()
        {
        }

        public EmployeeDAO(Employee oEmployee)
        {
            employee = oEmployee;
        }

        public Int32 RecordCount { get; set; }

        public Employee LoadEmployee(int employeeID)
        {
            try
            {
                var dbmanager = new DBManager();
                SqlParameter[] sqlparameter = {
                                                  dbmanager.makeInParam("@EmployeeID", SqlDbType.Int, 0, employeeID)
                                              };

                SqlDataReader sqldatareader = dbmanager.GetDataReaderProc("prGetEmployeeByID", sqlparameter);
                if (sqldatareader.Read())
                {
                    employee.EmployeeID = employeeID;
                    employee.EmployeeName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("EmployeeName")));
                    employee.FirstName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("FirstName")));
                    employee.LastName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("LastName")));
                    employee.EmailAddress =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Email")));
                    employee.PhoneNumber =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("PhoneNumber")));
                    employee.Gender = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Gender")));
                    employee.DOB = Utils.Utils.fixNullDate(sqldatareader.GetValue(sqldatareader.GetOrdinal("DOB")));
                    employee.CNIC = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("CNIC")));
                    employee.Notes = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Notes")));
                    employee.Designation = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Designation")));
                    employee.Department = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Department")));
                    employee.Address = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Address")));

                    sqldatareader.Close();

                    return employee;
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

        public int AddEmployee()
        {
            var dbmanager = new DBManager();
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@EmployeeId", SqlDbType.Int, 0, employee.EmployeeID),
                                                dbmanager.makeInParam("@EmployeeName", SqlDbType.VarChar, 255,
                                                                      employee.FirstName + " " + employee.LastName),
                                                dbmanager.makeInParam("@FirstName", SqlDbType.VarChar, 255,
                                                                      employee.FirstName),
                                                dbmanager.makeInParam("@LastName", SqlDbType.VarChar, 255,
                                                                      employee.LastName),
                                                dbmanager.makeInParam("@Email", SqlDbType.VarChar, 500,
                                                                      employee.EmailAddress),
                                                dbmanager.makeInParam("@DOB", SqlDbType.Date, 500,
                                                                      employee.DOB),
                                                dbmanager.makeInParam("@PhoneNumber", SqlDbType.VarChar, 255,
                                                                      employee.PhoneNumber),
                                                dbmanager.makeInParam("@Gender", SqlDbType.VarChar, 1,
                                                                      employee.Gender),
                                                dbmanager.makeInParam("@CNIC", SqlDbType.VarChar, 50,
                                                                      employee.CNIC),
                                                dbmanager.makeInParam("@Department", SqlDbType.VarChar, 255,
                                                                      employee.Department),
                                                dbmanager.makeInParam("@Designation", SqlDbType.VarChar, 255,
                                                                      employee.Designation),
                                                dbmanager.makeInParam("@Note", SqlDbType.VarChar, int.MaxValue, employee.Notes),
                                                dbmanager.makeInParam("@Address", SqlDbType.VarChar, int.MaxValue, employee.Address)
                                            };

                int nRet = dbmanager.RunProc("prAddEmployee", parameters);

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


        //public bool UserExists()
        //{
        //    var dbmanager = new DBManager();
        //    try
        //    {
        //        //Update Member information to the Database
        //        SqlParameter[] parameters = {
        //                                        dbmanager.makeInParam("@UserName", SqlDbType.NVarChar, 50, user.UserName)
        //                                        ,
        //                                        dbmanager.makeInParam("@EmailAddress", SqlDbType.NVarChar, 250,
        //                                                              user.EmailAddress)
        //                                    };

        //        int nRet = dbmanager.RunProc("nmMemberExists", parameters);

        //        if (nRet > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }
        //}

        //public bool UpdateUser()
        //{
        //    var dbmanager = new DBManager();
        //    try
        //    {
        //        //Update Member information to the Database
        //        SqlParameter[] parameters = {
        //                                        dbmanager.makeInParam("@UserID", SqlDbType.Int, 0, user.UserID),
        //                                        dbmanager.makeInParam("@UserPassword", SqlDbType.NVarChar, 50,
        //                                                              user.UserPassword),
        //                                        dbmanager.makeInParam("@EmailAddress", SqlDbType.NVarChar, 100,
        //                                                              user.EmailAddress)
        //                                    };

        //        int nRet = dbmanager.RunProc("UpdateUser", parameters);

        //        if (nRet > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }
        //}

        //public bool AddRemarks(string remarks)
        //{
        //    var dbmanager = new DBManager();
        //    try
        //    {
        //        //Update Member information to the Database
        //        SqlParameter[] parameters = {
        //                                        dbmanager.makeInParam("@DonorId", SqlDbType.Int, 0, user.UserID),
        //                                        dbmanager.makeInParam("@Remarks", SqlDbType.NVarChar, 1024,
        //                                                              remarks),
        //                                        dbmanager.makeInParam("@PostedBy", SqlDbType.Int, 0,
        //                                                              user.RemarksPostedBy),
        //                                    };

        //        int nRet = dbmanager.RunProc("AddRemarks", parameters);

        //        if (nRet > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }
        //}

        //public bool AddAnnualFee(int fee, int month, int year)
        //{
        //    var dbmanager = new DBManager();
        //    try
        //    {
        //        //Update Member information to the Database
        //        SqlParameter[] parameters = {
        //                                        dbmanager.makeInParam("@MemberId", SqlDbType.Int, 0, user.UserID),
        //                                        dbmanager.makeInParam("@AnnualFee", SqlDbType.Int, 0,
        //                                                              fee),
        //                                        dbmanager.makeInParam("@PostedOn", SqlDbType.DateTime, 0,
        //                                                              Convert.ToDateTime(month.ToString() + " " + year.ToString())),
        //                                        dbmanager.makeInParam("@PostedBy", SqlDbType.Int, 0,
        //                                                              user.RemarksPostedBy),
        //                                    };

        //        int nRet = dbmanager.RunProc("AddAnnualFee", parameters);

        //        if (nRet > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }
        //}

        //public bool AddNote(string notes)
        //{
        //    var dbmanager = new DBManager();
        //    try
        //    {
        //        //Update Member information to the Database
        //        SqlParameter[] parameters = {
        //                                        dbmanager.makeInParam("@DonorId", SqlDbType.Int, 0, user.UserID),
        //                                        dbmanager.makeInParam("@Notes", SqlDbType.NVarChar, 1024,
        //                                                              notes),
        //                                        dbmanager.makeInParam("@PostedBy", SqlDbType.Int, 0,
        //                                                              user.RemarksPostedBy),
        //                                    };

        //        int nRet = dbmanager.RunProc("AddNote", parameters);

        //        if (nRet > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }
        //}

        //public int GetUserIdByName()
        //{
        //    DBManager dbmanager = new DBManager();
        //    try
        //    {
        //        System.Data.SqlClient.SqlParameter[] parameters = { 
        //            dbmanager.makeInParam("@UserID", System.Data.SqlDbType.Int, 0, user.UserName)
        //              };

        //        SqlDataReader reader = dbmanager.GetDataReaderProc("GetUserIdByName", parameters);

        //        while(reader.Read())
        //        reader["UserId"]
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //    finally
        //    {
        //        dbmanager = null;
        //    }
        //}

        public bool DeleteEmployee()
        {
            var dbmanager = new DBManager();
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@EmployeeId", SqlDbType.Int, 0, employee.EmployeeID)
                                            };

                int nRet = dbmanager.RunProc("prDeleteEmployeeById", parameters);

                if (nRet > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return false;
            }
            finally
            {
                dbmanager = null;
            }
        }

        internal Employee Load()
        {
            return LoadEmployee(employee.EmployeeID);
        }
    }
}