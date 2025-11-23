using System;
using System.IO;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailsStudent : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation
                var oValidator = new Validator();
                oValidator.ValidateRequest(Request);
                oValidator.ValidateUserPageAccess(SaveDCSession.UserAccessLevel,
                                                  new[]
                                                      {
                                                          UserAccessLevels.SuperAdmin, UserAccessLevels.Admin,
                                                          UserAccessLevels.Operator, UserAccessLevels.Donor
                                                      });

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator ||
                    SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                {
                    trApprovalRemarks.Visible = false;
                    trBtns.Visible = false;
                }

                // get form/query string values.
                int nEditStudentId = Utils.fixNullInt(Request.QueryString["StudentId"]);

                // load edit user details.
                if (nEditStudentId > 0)
                {
                    var oStudent = new Student();
                    oStudent.StudentId = nEditStudentId;
                    var oStudentManager = new StudentManager(oStudent);
                    oStudent = oStudentManager.Load();
                    txtStudentFName.Text = oStudent.FirstName;
                    txtStudentLName.Text = oStudent.LastName;
                    txtEduLevel.Text = oStudent.EducationalLevel;
                    txtDOB.Text = oStudent.DateOfBirth == null || oStudent.DateOfBirth == DateTime.MinValue
                                      ? DateTime.Now.Date.ToString("dd/MM/yyyy")
                                      : oStudent.DateOfBirth.ToString("dd/MM/yyyy");
                    txtNote.Text = oStudent.Note;
                    hdnEditStudentId.Value = nEditStudentId.ToString();
                    if (oStudent.StatusId == 3 || oStudent.StatusId == 6)
                    {
                        trApprovalRemarks.Visible = false;
                        trBtns.Visible = false;
                    }


                    string szFileUniqueName = oStudent.ImageGUID;
                    string szServerPath = Server.MapPath(SaveDCConstants.ProfileSnapUploadPath) + szFileUniqueName +
                                          ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        imgUpload.Src = SaveDCConstants.ProfileSnapUploadPath + szFileUniqueName + ".jpg";
                    }

                    // set history info.
                    var ohistory = new History();
                    ohistory.HistoryId = oStudent.HistoryId;
                    ohistory = new HistoryManager(ohistory).Load();
                    txtIsDoingStudy.Text = ohistory.IsDoingStudy ? "Yes" : "No";

                    if (ohistory.IsDoingStudy)
                    {
                        classLeft1.Parent.Controls.Remove(classLeft1);
                        classLeft2.Parent.Controls.Remove(classLeft2);
                    }
                    else
                    {
                        classIn.Parent.Controls.Remove(classIn);
                    }

                    try
                    {
                        var oclass = new Class_();
                        oclass.ClassId = ohistory.ClassDoingStudyIn;
                        var classManager = new ClassManager(oclass);
                        txtClassDoingStudyIn.Text = classManager.Load().ClassName;

                        oclass.ClassId = ohistory.ClassLeftIn;
                        classManager = new ClassManager(oclass);
                        txtClassLeftIn.Text = classManager.Load().ClassName;
                    }
                    catch
                    {
                    }

                    txtPeriodLeftSince.Text = ohistory.PeriodLeftSince.ToString();
                    txtLastSchoolAttended.Text = ohistory.LastSchoolAttended;
                    txtHNote.Text = ohistory.Note;

                    #region History

                    if (oStudent.FamilyId <= 0)
                    {
                        PlaceHolderFamily.Parent.Controls.Remove(PlaceHolderFamily);
                    }
                    else
                    {
                        PlaceHolderFamilyNorec.Parent.Controls.Remove(PlaceHolderFamilyNorec);

                        var oFamily = new Family();
                        oFamily.FamilyId = oStudent.FamilyId;
                        var oFamilyManager = new FamilyManager(oFamily);
                        oFamily = oFamilyManager.Load();

                        txtDisplayName.Text = oFamily.DisplayName;
                        txtFatherName.Text = oFamily.FatherName;
                        txtFatherOccupation.Text = oFamily.FatherOccu;
                        txtFatherCNIC.Text = oFamily.FatherCNIC;
                        txtFatherAge.Text = oFamily.FatherAge.ToString();
                        txtMotherName.Text = oFamily.MotherName;
                        txtMotherAge.Text = oFamily.MotherAge.ToString();
                        txtMotherOccupation.Text = oFamily.MotherOccu;
                        txtMotherCNIC.Text = oFamily.MotherCNIC;

                        txtIsFatherAlive.Text = oFamily.IsFatherAlive ? "Yes" : "No";
                        txtIsMotherAlive.Text = oFamily.IsMotherAlive ? "Yes" : "No";

                        if (oFamily.IsFatherAlive)
                            family_4.Parent.Controls.Remove(family_4);
                        if (oFamily.IsMotherAlive)
                            family_5.Parent.Controls.Remove(family_5);

                        txtIsParentsDivorced.Text = oFamily.IsDivorced ? "Yes" : "No";

                        if (!oFamily.IsDivorced)
                        {
                            family_11.Parent.Controls.Remove(family_11);
                            family_12.Parent.Controls.Remove(family_12);
                            family_13.Parent.Controls.Remove(family_13);
                        }

                        txtDivorcedPeriod.Text = oFamily.DivorcedPeriod.ToString();
                        txtQardian.Text = oFamily.Gardian.ToString() == "1" ? "Father" : "Mother";
                        txtDAddress.Text = oFamily.FatherLocation;

                        txtMaleMembers.Text = oFamily.MaleMembers.ToString();
                        txtFemleMembers.Text = oFamily.FemaleMembers.ToString();
                        txtMemberDetails.Text = oFamily.MemberDetail;

                        txtIsOwnHouse.Text = oFamily.IsHouseOwner ? "Yes" : "No";
                        txtHouseArea.Text = oFamily.HouseArea.ToString();
                        txtRooms.Text = oFamily.HouseRooms.ToString();
                        txtLivingPeriod.Text = oFamily.LivingPeriod.ToString();
                        txtIncome.Text = oFamily.FamilyIncome.ToString();
                        txtPermanentAddress.Text = oFamily.PermResAddress;
                        txtCurrentAddress.Text = oFamily.CurResAddress;
                        txtLandline.Text = oFamily.LandlineNumber;
                        txtCell.Text = oFamily.CellNumber;
                        txtFamilyNote.Text = oFamily.Note;
                    }

                    #endregion

                    #region Varification

                    if (oStudent.Varification.VarificationDate == null ||
                        oStudent.Varification.VarificationDate == DateTime.MinValue)
                    {
                        PlaceHolderVarification.Parent.Controls.Remove(PlaceHolderVarification);
                    }
                    else
                    {
                        PlaceHolderVarificationNorec.Parent.Controls.Remove(PlaceHolderVarificationNorec);

                        var oUser = new User();
                        oUser.UserID = oStudent.Varification.VarifiedBy;
                        var oUserManager = new UserManager(oUser);
                        oUser = oUserManager.Load();

                        txtVarifiedBy.Text = oUser.UserName;
                        txtVDate.Text = oStudent.Varification.VarificationDate.ToShortDateString();
                        txtVRemarks.Text = oStudent.Varification.Remarks;
                        txtIsVarified.Text = oStudent.Varification.IsVarified ? "Yes" : "No";
                    }

                    #endregion

                    var approval = new Approvals();
                    approval.StudentId = nEditStudentId;
                    var oApprovalManager = new ApprovalsManager(approval);
                    Approvals[] approvals = oApprovalManager.GetApprovals();

                    Repeater1.DataSource = approvals;
                    Repeater1.DataBind();
                }
                hdnEditStudentId.Value = nEditStudentId.ToString();
                RenderError(Request.QueryString["status"]);
            }
        }

        private void RenderError(string szErrorCode)
        {
            if (string.IsNullOrEmpty(szErrorCode))
            {
                lblError.Text = "";
                return;
            }

            string szErrorDesc = Utils.GetMessageText(szErrorCode);
            if (!szErrorCode.EndsWith("1"))
                lblError.CssClass = "FailureMessage";
            else
                lblError.CssClass = "SuccessMessage";

            lblError.Text = szErrorDesc;
        }

        protected void btnApprove_Click(object sender, ImageClickEventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            AddApprove(3);
        }

        protected void btnReject_Click(object sender, ImageClickEventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            AddApprove(4);
        }

        private void AddApprove(int nStatusId)
        {
            // get the edit user id.
            int szCurStudentId = 0;
            int.TryParse(hdnEditStudentId.Value, out szCurStudentId);

            var approvals = new Approvals();
            approvals.Remarks = txtApprovRemarks.Text;
            approvals.StatusId = nStatusId;
            approvals.ApproverId = SaveDCSession.UserId;
            approvals.StudentId = szCurStudentId;

            var oApprovalManager = new ApprovalsManager(approvals);
            int nStatus = oApprovalManager.Save();
            if (nStatus > 0)
            {
                Response.Redirect("ListStudents.aspx?status=5031161&StudentId=" + szCurStudentId);
                //  user updated successfully.
            }
            else if (nStatus == 0)
            {
                Response.Redirect("DetailsStudent.aspx?status=5031190&StudentId=" + szCurStudentId);
            }
            else if (nStatus == -3)
            {
                Response.Redirect("DetailsStudent.aspx?status=5031180&StudentId=" + szCurStudentId);
            }
            else if (nStatus == -2)
            {
                Response.Redirect("DetailsStudent.aspx?status=5031200&StudentId=" + szCurStudentId);
            }
        }
    }
}