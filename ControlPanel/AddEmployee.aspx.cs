using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;
using System.Web.Security;

namespace SaveDC.ControlPanel
{
    public partial class AddEmployee : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // page validation
            var oValidator = new Validator();
            oValidator.ValidateRequest(Request);
            oValidator.ValidateUserPageAccess(SaveDCSession.UserAccessLevel,
                                              new[] {UserAccessLevels.SuperAdmin, UserAccessLevels.Admin, UserAccessLevels.Operator});
            oValidator = null;

            if (!Page.IsPostBack)
            {
                // get form/query string values.
                int nEditUserId = Utils.fixNullInt(Request.QueryString["EmployeeId"]);
                hdnArea.Value = "Employee Manager";
                hdnRole.Value = "Employee";

                // load edit user details.
                
                if (nEditUserId > 0)
                {
                    var oEmployee = new Employee();
                    oEmployee.EmployeeID = nEditUserId;
                    var oEmployeeManager = new EmployeeManager(oEmployee);
                    oEmployee = oEmployeeManager.Load();

                    // txtUserName.Text = oUser.UserName;
                    //Control nameParent = txtUserName.Parent;
                    //nameParent.Controls.Remove(txtUserName);
                    //nameParent.Controls.Remove(RFV_txtUserName);

                    //var oCommon = new Common();
                    //nameParent.Controls.Add(oCommon.CreateTextLabel("txtUserName", oUser.UserName));
                    txtFirstName.Text = oEmployee.FirstName;
                    txtLastName.Text = oEmployee.LastName;
                    txtEmail.Text = oEmployee.EmailAddress;
                    txtPhone.Text = oEmployee.PhoneNumber;
                    txtNote.Text = oEmployee.Notes;
                    txtAddress.Text = oEmployee.Address;
                    txtDesignation.Text = oEmployee.Designation;
                    txtDepartment.Text = oEmployee.Department;
                    txtDOB.Text = Convert.ToDateTime(oEmployee.DOB).ToString("dd/MM/yyyy");
                    txtCNIC.Text = oEmployee.CNIC;

                    if (oEmployee.Gender.Equals("M"))
                        txtGenderM.Checked = true;
                    else
                        txtGenderF.Checked = true;
                    
                    hdnAddEdit.Value = "Edit";
                    hdnEditUserId.Value = nEditUserId.ToString();
                }
                

                RenderError(Request.QueryString["status"]);
            }
        }

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ListEmployee.aspx");
        }


        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            var oEmployee = new Employee();
            //oUser.UserName = (hdnRole.Value.ToLower() == "member") ? Membership.GeneratePassword(12, 1) : txtUserName.Text;
            //oUser.UserPassword = (hdnRole.Value.ToLower() == "member") ? Membership.GeneratePassword(12, 1) : txtUserpassword.Text;
            ////txtConfirmpassword.Text = oUser.UserPassword = txtConfirmpassword.Text;

            //oUser.UserRoleID = (hdnRole.Value.ToLower() == "donor") ? 4 : (hdnRole.Value.ToLower() == "member") ? 5 : Utils.fixNullInt(comboUserRoles.SelectedValue);
            oEmployee.EmployeeName = txtFirstName.Text + " " + txtLastName.Text;
            oEmployee.FirstName = txtFirstName.Text;
            oEmployee.LastName = txtLastName.Text;
            oEmployee.EmailAddress = txtEmail.Text;
            oEmployee.PhoneNumber = txtPhone.Text;
            oEmployee.Notes = txtNote.Text;
            oEmployee.Address = txtAddress.Text;
            if (txtGenderM.Checked)
            {
                oEmployee.Gender = "M";
            }
            else
            {
                oEmployee.Gender = "F";
            }
            if (txtCNIC.Text.Equals("_____-_______-_"))
            {
                oEmployee.CNIC = String.Empty;
            }
            else
            {
                oEmployee.CNIC = txtCNIC.Text;
            }
            oEmployee.DOB = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", null);
            oEmployee.Designation = txtDesignation.Text;
            oEmployee.Department = txtDepartment.Text;

            int nEditUserId = 0;
            int.TryParse(hdnEditUserId.Value, out nEditUserId);
            oEmployee.EmployeeID = nEditUserId;
            var oEmployeeManager = new EmployeeManager(oEmployee);
            int nStatus = oEmployeeManager.Save();

            if (nStatus > 0)
            {
                if (nEditUserId > 0)
                    Response.Redirect("ListEmployee.aspx?status=5011241"); //  user updated successfully.
                else
                    Response.Redirect("ListEmployee.aspx?status=5011261");
            }
            else if (nStatus == 0)
            {
                Response.Redirect("AddEmployee.aspx?status=5011260");
            }
            else if (nStatus == -1)
            {
                Response.Redirect("AddEmployee.aspx?status=5011250");
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
    }
}