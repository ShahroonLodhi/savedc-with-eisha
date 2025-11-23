using System;
using System.IO;
using System.Web;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class AddFamily : Page
    {
        public AddFamily()
        {
            PreInit += AddFamily_PreInit;
            Load += AddFamily_Load;
        }

        protected void AddFamily_Load(object sender, EventArgs e)
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
                                                          UserAccessLevels.Operator
                                                      });
                oValidator = null;
                
                // Operator can now edit Family based on new Feedback 1/22/2022
                //if (SaveDCSession.UserAccessLevel != UserAccessLevels.Operator)
                //{
                    // get form/query string values.
                    int nEditFamilyId = Utils.fixNullInt(Request.QueryString["FamilyId"]);

                    // load edit user details.
                    if (nEditFamilyId > 0)
                    {
                        var oFamily = new Family();
                        oFamily.FamilyId = nEditFamilyId;
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

                        rbIsFatherAlive.SelectedValue = oFamily.IsFatherAlive ? "1" : "0";
                        rbIsMotherAlive.SelectedValue = oFamily.IsMotherAlive ? "1" : "0";

                        rbIsParentsDivorced.SelectedValue = oFamily.IsDivorced ? "1" : "0";
                        txtDivorcedPeriod.Text = oFamily.DivorcedPeriod.ToString();
                        rbQardian.SelectedValue = oFamily.Gardian.ToString(); // 1 ; father, 0 ; mother
                        txtDAddress.Text = oFamily.FatherLocation;

                        txtMaleMembers.Text = oFamily.MaleMembers.ToString();
                        txtFemleMembers.Text = oFamily.FemaleMembers.ToString();
                        txtMemberDetails.Text = oFamily.MemberDetail;

                        rbIsOwnHouse.SelectedValue = oFamily.IsHouseOwner ? "1" : "0";
                        txtHouseArea.Text = oFamily.HouseArea.ToString();
                        txtRooms.Text = oFamily.HouseRooms.ToString();
                        txtLivingPeriod.Text = oFamily.LivingPeriod.ToString();
                        txtIncome.Text = oFamily.FamilyIncome.ToString();
                        txtPermanentAddress.Text = oFamily.PermResAddress;
                        txtCurrentAddress.Text = oFamily.CurResAddress;
                        txtLandline.Text = oFamily.LandlineNumber;
                        txtCell.Text = oFamily.CellNumber;
                        txtNote.Text = oFamily.Note;

                        SetBillsInformation(oFamily.Bill1GUID, "1");
                        SetBillsInformation(oFamily.Bill2GUID, "2");
                        SetCertiInformation(oFamily.Cert1GUID, "1");
                        SetCertiInformation(oFamily.Cert2GUID, "2");

                        hdnEditFamilyId.Value = nEditFamilyId.ToString();
                        hdnAddEdit.Value = "Edit";
                    }
                //}
                RenderError(Request.QueryString["status"]);
            }
        }

        private void SetBillsInformation(string szBillGUID, string szBillNum)
        {
            string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + szBillGUID + ".jpg";
            if (File.Exists(szServerPath))
            {
                if (szBillNum == "1")
                {
                    hdnOldBill1.Value = szBillGUID;
                    hdnNewBill1.Value = szBillGUID;
                }
                else
                {
                    hdnOldBill2.Value = szBillGUID;
                    hdnNewBill2.Value = szBillGUID;
                }
            }
        }

        private void SetCertiInformation(string szCertGuid, string szNum)
        {
            string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + szCertGuid + ".jpg";
            if (File.Exists(szServerPath))
            {
                if (szNum == "1")
                {
                    hdnOldCerti1.Value = szCertGuid;
                    hdnNewCerti1.Value = szCertGuid;
                }
                else
                {
                    hdnOldCerti2.Value = szCertGuid;
                    hdnNewCerti2.Value = szCertGuid;
                }
            }
        }

        private void AddFamily_PreInit(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["modalwin"]) && Request.QueryString["modalwin"] == "1")
                MasterPageFile = "~/ControlPanel/Dummy.master";
        }

        private string UploadFileAndReturnName(HttpPostedFile postedFile)
        {
            try
            {
                HttpPostedFile hpf = postedFile;
                if (hpf.ContentLength > 0)
                {
                    if (IsValidImageFile(Path.GetFileName(hpf.FileName)))
                    {
                        string szFileGUID = Guid.NewGuid().ToString();
                        hpf.SaveAs(Server.MapPath(SaveDCConstants.BillsUploadPath) + szFileGUID + ".jpg");
                        return szFileGUID;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            // ajax did not work on the sever so to minimize code changes assigning new file name to hdn control.
            string szBill1FileName = UploadFileAndReturnName(familybill1.PostedFile);
            if (!string.IsNullOrEmpty(szBill1FileName))
            {
                hdnNewBill1.Value = szBill1FileName;
            }
            string szBill2FileName = UploadFileAndReturnName(familybill2.PostedFile);
            if (!string.IsNullOrEmpty(szBill2FileName))
            {
                hdnNewBill2.Value = szBill2FileName;
            }
            string szCert1FileName = UploadFileAndReturnName(certificateFile1.PostedFile);
            if (!string.IsNullOrEmpty(szCert1FileName))
            {
                hdnNewCerti1.Value = szCert1FileName;
            }
            string szCert2FileName = UploadFileAndReturnName(certificateFile2.PostedFile);
            if (!string.IsNullOrEmpty(szCert2FileName))
            {
                hdnNewCerti2.Value = szCert2FileName;
            }

            // get the edit user id.
            int nEditFamilyId = 0;
            int.TryParse(hdnEditFamilyId.Value, out nEditFamilyId);

            // fill the studnet informations.
            var oFamily = new Family();
            oFamily.DisplayName = txtDisplayName.Text;
            oFamily.FatherName = txtFatherName.Text;
            oFamily.FatherOccu = txtFatherOccupation.Text;
            oFamily.FatherCNIC = txtFatherCNIC.Text;
            oFamily.FatherAge = Utils.fixNullInt(txtFatherAge.Text);
            oFamily.MotherName = txtMotherName.Text;
            oFamily.MotherAge = Utils.fixNullInt(txtMotherAge.Text);
            oFamily.MotherOccu = txtMotherOccupation.Text;
            oFamily.MotherCNIC = txtMotherCNIC.Text;

            oFamily.IsFatherAlive = rbIsFatherAlive.SelectedValue == "1" ? true : false;
            oFamily.IsMotherAlive = rbIsMotherAlive.SelectedValue == "1" ? true : false;

            oFamily.IsDivorced = rbIsParentsDivorced.SelectedValue == "1" ? true : false;
            oFamily.DivorcedPeriod = Utils.fixNullInt(txtDivorcedPeriod.Text);
            oFamily.Gardian = Utils.fixNullInt(rbQardian.SelectedValue); // 1 = father, 0 = mother
            oFamily.FatherLocation = txtDAddress.Text;

            oFamily.MaleMembers = Utils.fixNullInt(txtMaleMembers.Text);
            oFamily.FemaleMembers = Utils.fixNullInt(txtFemleMembers.Text);
            oFamily.MemberDetail = txtMemberDetails.Text;

            oFamily.IsHouseOwner = rbIsOwnHouse.SelectedValue == "1" ? true : false;
            oFamily.HouseArea = Utils.fixNullInt(txtHouseArea.Text);
            oFamily.HouseRooms = Utils.fixNullInt(txtRooms.Text);
            oFamily.LivingPeriod = Utils.fixNullInt(txtLivingPeriod.Text);
            oFamily.FamilyIncome = Utils.fixNullInt(txtIncome.Text);
            oFamily.PermResAddress = txtPermanentAddress.Text;
            oFamily.CurResAddress = txtCurrentAddress.Text;
            oFamily.LandlineNumber = txtLandline.Text;
            oFamily.CellNumber = txtCell.Text;
            oFamily.Note = txtNote.Text;
            oFamily.FamilyId = nEditFamilyId;

            oFamily.Bill1GUID = hdnNewBill1.Value;
            oFamily.Bill2GUID = hdnNewBill2.Value;
            oFamily.Cert1GUID = hdnNewCerti1.Value;
            oFamily.Cert2GUID = hdnNewCerti2.Value;

            var oFamilyManager = new FamilyManager(oFamily);

            int nStatus = oFamilyManager.Save();
            if (nStatus > 0)
            {
                // delete older files.
                try
                {
                    // bill1
                    if (!string.IsNullOrEmpty(hdnOldBill1.Value) && hdnOldBill1.Value != hdnNewBill1.Value)
                    {
                        string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + hdnOldBill1.Value +
                                              ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            File.Delete(szServerPath);
                        }
                    }
                    // bill2
                    if (!string.IsNullOrEmpty(hdnOldBill2.Value) && hdnOldBill2.Value != hdnNewBill2.Value)
                    {
                        string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + hdnOldBill2.Value +
                                              ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            File.Delete(szServerPath);
                        }
                    }
                    // cert 1
                    if (!string.IsNullOrEmpty(hdnOldCerti1.Value) && hdnOldCerti1.Value != hdnNewCerti1.Value)
                    {
                        string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + hdnOldCerti1.Value +
                                              ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            File.Delete(szServerPath);
                        }
                    }
                    // cert 2
                    if (!string.IsNullOrEmpty(hdnOldCerti2.Value) && hdnOldCerti2.Value != hdnNewCerti2.Value)
                    {
                        string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + hdnOldCerti2.Value +
                                              ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            File.Delete(szServerPath);
                        }
                    }
                }
                catch
                {
                }

                if (MasterPageFile.Contains("Dummy"))
                {
                    string szResponseStr = "<script type=\"text/javascript\">window.opener.AddNewFamily(\"" +
                                           oFamily.DisplayName + "\",\"" +
                                           oFamily.FamilyId + "\"); window.close(); </script>";
                    Response.Write(szResponseStr);
                    Response.End();
                }
                else
                {
                    if (nEditFamilyId > 0)
                        Response.Redirect("ListFamily.aspx?status=5041021"); //  user updated successfully.
                    else
                        Response.Redirect("ListFamily.aspx?status=5041011");
                }
            }
            else if (nStatus == 0)
            {
                Response.Redirect("AddFamily.aspx?status=5041010");
            }
            else if (nStatus == -1)
            {
                Response.Redirect("AddFamily.aspx?status=5041000");
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

        //protected void ProcessUpload1(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        //{
        //    //if (AsyncFileUpload1.HasFile &&  IsValidImageFile(AsyncFileUpload1.FileName))
        //    //{
        //    //    string szFileUniqueName = Guid.NewGuid().ToString();
        //    //    string szServerPath = Server.MapPath("../Images/UtilityBills/") + szFileUniqueName + ".jpg";
        //    //    AsyncFileUpload1.SaveAs(szServerPath);

        //    //    ProcessUpload("1", szFileUniqueName, hdnNewBill1.ClientID );

        //    //  //  RegisterClientScriptBlock("spanhide" + szBillNum, szScript);
        //    //}
        // }

        //protected void ProcessUpload2(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        //{
        //    //if (AsyncFileUpload2.HasFile && IsValidImageFile(AsyncFileUpload2.FileName))
        //    //{
        //    //    string szFileUniqueName = Guid.NewGuid().ToString();
        //    //    string szServerPath = Server.MapPath("../Images/UtilityBills/") + szFileUniqueName + ".jpg";
        //    //    AsyncFileUpload2.SaveAs(szServerPath);

        //    //    ProcessUpload("2", szFileUniqueName, hdnNewBill2.ClientID);
        //    //}
        //}

        private void ProcessUpload(string szBillNum, string szFileUniqueName, string szValueControlId)
        {
            //string szScript = "top.document.getElementById('browseBill" + szBillNum + "').style.display = 'none';"+
            //"top.document.getElementById('viewBill" + szBillNum + "' ).style.display  = '';" +
            //" top.document.getElementById('" + szValueControlId + "').value='" + szFileUniqueName + "'; ";


            //ScriptManager.RegisterClientScriptBlock(PlaceHolder1, PlaceHolder1.GetType(), "a1",
            //    "top.document.getElementById('viewlink" + szBillNum + "').href='../Images/UtilityBills/" + szFileUniqueName + ".jpg';  " + szScript,
            //    true);

            //   RegisterClientScriptBlock("spanhide" + szBillNum + "", szScript);
        }

        private bool IsValidImageFile(string szFileName)
        {
            szFileName = szFileName.ToLower();
            if (szFileName.EndsWith(".gif"))
                return true;
            if (szFileName.EndsWith(".jpg"))
                return true;
            if (szFileName.EndsWith(".jpeg"))
                return true;
            if (szFileName.EndsWith(".tiff"))
                return true;
            if (szFileName.EndsWith(".png"))
                return true;
            return false;
        }
    }
}