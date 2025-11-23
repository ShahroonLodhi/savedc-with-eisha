using System;
using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.DB
{
    public class UserDAO
    {
        private readonly User user;

        public UserDAO()
        {
        }

        public UserDAO(User oUser)
        {
            user = oUser;
        }

        public Int32 RecordCount { get; set; }

        public User[] GetUsers(int nPageNo, int nPageSize)
        {
            var dbmanager = new DBManager();
            RecordCount = 0;
            User[] users = null;
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@UserName", SqlDbType.NVarChar, 250,
                                                                      user.UserName),
                                                dbmanager.makeInParam("@UserRoleId", SqlDbType.Int, 0, user.UserRoleID),
                                                dbmanager.makeInParam("@PageNo", SqlDbType.Int, 0, nPageNo),
                                                dbmanager.makeInParam("@PageSize", SqlDbType.Int, 0, nPageSize),
                                                dbmanager.makeOutParam("@TotalRecord", SqlDbType.Int, 4)
                                            };


                DataSet data = dbmanager.GetDataSetProc("GetUsersList", parameters);

                if (data != null)
                {
                    RecordCount = Utils.Utils.fixNullInt(parameters[4].Value);
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        users = new User[data.Tables[0].Rows.Count];

                        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                        {
                            users[i] = new User();
                            DataRow row = data.Tables[0].Rows[i];
                            users[i].UserID = Utils.Utils.fixNullInt(row["UserId"]);
                            users[i].UserName = Utils.Utils.fixNullString(row["UserName"]);
                            users[i].UserPassword = Utils.Utils.fixNullString(row["Password"]);
                            users[i].FirstName = Utils.Utils.fixNullString(row["FirstName"]);
                            users[i].LastName = Utils.Utils.fixNullString(row["LastName"]);
                            users[i].EmailAddress = Utils.Utils.fixNullString(row["Email"]);
                            users[i].PhoneNumber = Utils.Utils.fixNullString(row["PhoneNum"]);
                            users[i].UserRoleID = Utils.Utils.fixNullInt(row["UserRoleId"]);
                            users[i].UserRoleName = Utils.Utils.fixNullString(row["RoleName"]);
                            users[i].IsSuspended = Utils.Utils.fixNullBool(row["IsSuspended"]);
                            users[i].Country = Utils.Utils.fixNullString(row["Country"]);
                            users[i].Address = Utils.Utils.fixNullString(row["Address"]);
                            users[i].LastSMSDate = Utils.Utils.fixNullString(row["LastSMSDate"]);
                        }
                    }
                }
                else
                    RecordCount = 0;

                return users;
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

        public DataSet GetUsersinDS(int nPageNo, int nPageSize)
        {
            var dbmanager = new DBManager();
            RecordCount = 0;
            User[] users = null;
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@UserName", SqlDbType.NVarChar, 250,
                                                                      user.UserName),
                                                dbmanager.makeInParam("@UserRoleId", SqlDbType.Int, 0, user.UserRoleID),
                                                dbmanager.makeInParam("@PageNo", SqlDbType.Int, 0, nPageNo),
                                                dbmanager.makeInParam("@PageSize", SqlDbType.Int, 0, nPageSize),
                                                dbmanager.makeOutParam("@TotalRecord", SqlDbType.Int, 4)
                                            };


                DataSet data = dbmanager.GetDataSetProc("GetUsersList", parameters);
                return data;
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

        public User LoadUser(int userID)
        {
            try
            {
                var dbmanager = new DBManager();
                SqlParameter[] sqlparameter = {
                                                  dbmanager.makeInParam("@UserID", SqlDbType.Int, 0, userID)
                                              };

                SqlDataReader sqldatareader = dbmanager.GetDataReaderProc("LoadUser", sqlparameter);
                if (sqldatareader.Read())
                {
                    user.UserID = userID;
                    user.UserName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("UserName")));
                    user.UserPassword =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Password")));

                    user.UserRoleID =
                        Utils.Utils.fixNullInt(sqldatareader.GetValue(sqldatareader.GetOrdinal("UserRoleId")));
                    user.UserRoleName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("RoleName")));
                    user.FirstName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("FirstName")));
                    user.LastName =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("LastName")));
                    user.EmailAddress =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Email")));
                    user.PhoneNumber =
                        Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("PhoneNum")));
                    user.Gender = Utils.Utils.fixNullBool(sqldatareader.GetValue(sqldatareader.GetOrdinal("Gender")));
                    user.CNIC = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("CNIC")));
                    user.RecevingDate = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("RecevingDate")));
                    user.Occupation = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Occupation")));
                    user.Qualification = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Qualification")));
                    user.Notes = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Note")));
                    user.IsSuspended =
                        Utils.Utils.fixNullBool(sqldatareader.GetValue(sqldatareader.GetOrdinal("IsSuspended")));
                    user.Country = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Country")));
                    user.Address = Utils.Utils.fixNullString(sqldatareader.GetValue(sqldatareader.GetOrdinal("Address")));

                    sqldatareader.Close();

                    return user;
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

        public int AddUser()
        {
            var dbmanager = new DBManager();
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@UserId", SqlDbType.Int, 0, user.UserID),
                                                dbmanager.makeInParam("@UserName", SqlDbType.NVarChar, 255,
                                                                      user.UserName),
                                                dbmanager.makeInParam("@Password", SqlDbType.NVarChar, 255,
                                                                      user.UserPassword),
                                                dbmanager.makeInParam("@UserRoleId", SqlDbType.Int, 0, user.UserRoleID),
                                                dbmanager.makeInParam("@FirstName", SqlDbType.NVarChar, 255,
                                                                      user.FirstName),
                                                dbmanager.makeInParam("@LastName", SqlDbType.NVarChar, 255,
                                                                      user.LastName),
                                                dbmanager.makeInParam("@Email", SqlDbType.NVarChar, 255,
                                                                      user.EmailAddress),
                                                dbmanager.makeInParam("@PhoneNum", SqlDbType.NVarChar, 255,
                                                                      user.PhoneNumber),
                                                dbmanager.makeInParam("@Gender", SqlDbType.Bit, 0,
                                                                      user.Gender),
                                                dbmanager.makeInParam("@CNIC", SqlDbType.NVarChar, 255,
                                                                      user.CNIC),
                                                dbmanager.makeInParam("@RecevingDate", SqlDbType.SmallDateTime, 255,
                                                                      (user.RecevingDate == "")? Convert.DBNull : Convert.ToDateTime(user.RecevingDate)),
                                                dbmanager.makeInParam("@Occupation", SqlDbType.NVarChar, 255,
                                                                      user.Occupation),
                                                dbmanager.makeInParam("@Qualification", SqlDbType.NVarChar, 255,
                                                                      user.Qualification),
                                                dbmanager.makeInParam("@Note", SqlDbType.NVarChar, 255, user.Notes),
                                                dbmanager.makeInParam("@IsSuspended", SqlDbType.NVarChar, 255,
                                                                      user.IsSuspended),
                                                dbmanager.makeInParam("@Country", SqlDbType.NVarChar, 255, user.Country)
                                                ,
                                                dbmanager.makeInParam("@Address", SqlDbType.NVarChar, 255, user.Address)
                                            };

                int nRet = dbmanager.RunProc("AddUser", parameters);

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

        public bool LoginUser()
        {
            var dbmanager = new DBManager();
            try
            {
                //Update Member information to the Database
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@UserName", SqlDbType.NVarChar, 50, user.UserName)
                                                ,
                                                dbmanager.makeInParam("@UserPassword", SqlDbType.NVarChar, 50,
                                                                      user.UserPassword)
                                            };

                SqlDataReader sqldatareader = dbmanager.GetDataReaderProc("ValidateUser", parameters);

                // if successfull then save the userid to load details later
                if (sqldatareader.Read())
                {
                    user.UserID = Utils.Utils.fixNullInt(sqldatareader.GetValue(sqldatareader.GetOrdinal("UserId")));
                    return true;
                }
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

        public bool UserExists()
        {
            var dbmanager = new DBManager();
            try
            {
                //Update Member information to the Database
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@UserName", SqlDbType.NVarChar, 50, user.UserName)
                                                ,
                                                dbmanager.makeInParam("@EmailAddress", SqlDbType.NVarChar, 250,
                                                                      user.EmailAddress)
                                            };

                int nRet = dbmanager.RunProc("nmMemberExists", parameters);

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

        public bool UpdateUser()
        {
            var dbmanager = new DBManager();
            try
            {
                //Update Member information to the Database
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@UserID", SqlDbType.Int, 0, user.UserID),
                                                dbmanager.makeInParam("@UserPassword", SqlDbType.NVarChar, 50,
                                                                      user.UserPassword),
                                                dbmanager.makeInParam("@EmailAddress", SqlDbType.NVarChar, 100,
                                                                      user.EmailAddress)
                                            };

                int nRet = dbmanager.RunProc("UpdateUser", parameters);

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

        public bool AddRemarks(string remarks)
        {
            var dbmanager = new DBManager();
            try
            {
                //Update Member information to the Database
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@DonorId", SqlDbType.Int, 0, user.UserID),
                                                dbmanager.makeInParam("@Remarks", SqlDbType.NVarChar, 1024,
                                                                      remarks),
                                                dbmanager.makeInParam("@PostedBy", SqlDbType.Int, 0,
                                                                      user.RemarksPostedBy),
                                            };

                int nRet = dbmanager.RunProc("AddRemarks", parameters);

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

        public bool AddAnnualFee(int fee, int month, int year)
        {
            var dbmanager = new DBManager();
            try
            {
                //Update Member information to the Database
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@MemberId", SqlDbType.Int, 0, user.UserID),
                                                dbmanager.makeInParam("@AnnualFee", SqlDbType.Int, 0,
                                                                      fee),
                                                dbmanager.makeInParam("@PostedOn", SqlDbType.DateTime, 0,
                                                                      Convert.ToDateTime(month.ToString() + " " + year.ToString())),
                                                dbmanager.makeInParam("@PostedBy", SqlDbType.Int, 0,
                                                                      user.RemarksPostedBy),
                                            };

                int nRet = dbmanager.RunProc("AddAnnualFee", parameters);

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

        public bool AddNote(string notes)
        {
            var dbmanager = new DBManager();
            try
            {
                //Update Member information to the Database
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@DonorId", SqlDbType.Int, 0, user.UserID),
                                                dbmanager.makeInParam("@Notes", SqlDbType.NVarChar, 1024,
                                                                      notes),
                                                dbmanager.makeInParam("@PostedBy", SqlDbType.Int, 0,
                                                                      user.RemarksPostedBy),
                                            };

                int nRet = dbmanager.RunProc("AddNote", parameters);

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

        public bool DeleteUser()
        {
            var dbmanager = new DBManager();
            try
            {
                SqlParameter[] parameters = {
                                                dbmanager.makeInParam("@UserId", SqlDbType.Int, 0, user.UserID)
                                            };

                int nRet = dbmanager.RunProc("DeleteUser", parameters);

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

        internal User Load()
        {
            return LoadUser(user.UserID);
        }
    }
}