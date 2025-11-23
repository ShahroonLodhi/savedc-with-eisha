using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class VarifyStudent : Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            if (!Page.IsPostBack)
            {
                // get form/query string values.
                int nEditStudentId = Utils.fixNullInt(Request.QueryString["StudentId"]);

                var oStudent = new Student();
                oStudent.StudentId = nEditStudentId;
                var oStudentManager = new StudentManager(oStudent);
                oStudent = oStudentManager.Load();

                // show error if varified and varified by other operator.
                if (oStudentManager.IsVarified() && oStudent.Varification.VarifiedBy != SaveDCSession.UserId)
                {
                    Response.Redirect("ListStudents.aspx?status=5031140");
                }
                else
                {
                    if (oStudent.Varification != null)
                    {
                        rbIsVarified.SelectedValue = oStudent.Varification.IsVarified.ToString();
                        txtNote.Text = oStudent.Varification.Remarks;
                    }
                }
                hdnEditStudentId.Value = nEditStudentId.ToString();

                RenderError(Request.QueryString["status"]);
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            var oStudent = new Student();
            oStudent.StudentId = int.Parse(hdnEditStudentId.Value);
            oStudent.Varification.IsVarified = Utils.fixNullBool(rbIsVarified.SelectedValue);
            oStudent.Varification.Remarks = txtNote.Text;
            oStudent.Varification.VarifiedBy = SaveDCSession.UserId;

            var oStudentManager = new StudentManager(oStudent);

            bool isEdit = false;
            if (oStudentManager.IsVarified())
                isEdit = true;

            if (oStudentManager.Varify())
            {
                if (!isEdit)
                    Response.Redirect("ListStudents.aspx?status=5031141");
                else
                    Response.Redirect("ListStudents.aspx?status=5031151");
            }
            else
            {
                Response.Redirect("VarifyStudent.aspx?status=5031170&StudentId=" + oStudent.StudentId);
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