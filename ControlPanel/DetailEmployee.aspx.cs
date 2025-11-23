using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailEmployee : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // get form/query string values.
                int nEditUserId = Utils.fixNullInt(Request.QueryString["EmployeeId"]);

                // load edit user details.
                if (nEditUserId > 0)
                {
                    var oEmployee = new Employee();
                    oEmployee.EmployeeID = nEditUserId;
                    var oEmployeeManager = new EmployeeManager(oEmployee);
                    oEmployee = oEmployeeManager.Load();
                    hdnId.Value = nEditUserId.ToString();

                    hdnArea.Value = "Employee Manager";
                    hdnRole.Value = "Employee";
                    
                    txtFName.Text = oEmployee.FirstName;
                    txtLName.Text = oEmployee.LastName;
                    txtEmail.Text = oEmployee.EmailAddress;
                    txtPhone.Text = oEmployee.PhoneNumber;
                    txtNote.Text = oEmployee.Notes;
                    lblAddress.Text = oEmployee.Address;

                    if (oEmployee.Gender.Equals("M"))
                        txtGender.Text = "Male";
                    else if(oEmployee.Gender.Equals("F"))
                        txtGender.Text = "Female";
                    txtCNIC.Text = oEmployee.CNIC;
                    txtDesignation.Text = oEmployee.Designation;
                    txtDepartment.Text = oEmployee.Department;
                    if (!String.IsNullOrEmpty(Convert.ToString(oEmployee.DOB)))
                    {
                        txtDOB.Text = Convert.ToDateTime(oEmployee.DOB).ToString("dd/MM/yyyy");
                    }
                    else
                        txtDOB.Text = String.Empty;
                    

                }
            }
        }

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ListEmployee.aspx");
        }

    }
}