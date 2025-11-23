using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class AddStudentEstimation : Page
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
                int nEstimationId = Utils.fixNullInt(Request.QueryString["EstimationId"]);

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                {
                    nEstimationId = 0;
                }

                if (nEstimationId > 0)
                {
                    hdnAddEdit.Value = "Edit";
                }
                int nStudentId = SaveDCSession.StudentId;

                var oCommon = new Common();
                SqlDataReader oSqlData = oCommon.LoadStudentSchoolEstimationDetails(nEstimationId);

                Repeater1.DataSource = oSqlData;
                Repeater1.DataBind();

                var oSchoolManager = new SchoolManager(new School());
                School[] schools = oSchoolManager.GetSchools();
                if (schools == null || schools.Length <= 0)
                {
                    var noRecFound = new ListItem();
                    noRecFound.Value = "0";
                    noRecFound.Text = "No School Found.";
                    ddSchools.Items.Add(noRecFound);
                }
                else
                {
                    ddSchools.DataSource = schools;
                    ddSchools.DataValueField = "SchoolId";
                    ddSchools.DataTextField = "SchoolName";
                    ddSchools.DataBind();
                    ddSchools.SelectedValue = oCommon.GetSchoolIdByEstimationId(nEstimationId).ToString();
                }

                var oStudent = new Student();
                oStudent.StudentId = nStudentId;
                var oStudentManager = new StudentManager(oStudent);
                oStudent = oStudentManager.Load();
                lblStdName.Text = oStudent.FirstName + " " + oStudent.LastName;

                hdnEditExtimationId.Value = nEstimationId.ToString();
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

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            int nStudentId = SaveDCSession.StudentId;
            // get the edit user id.
            int nEditEstimationId = 0;
            int.TryParse(hdnEditExtimationId.Value, out nEditEstimationId);
            bool bIsEdit = nEditEstimationId > 0;


            var oCommon = new Common();
            int nStatus =
                nEditEstimationId =
                oCommon.AddStudentSchoolEstimation(nEditEstimationId, nStudentId, int.Parse(ddSchools.SelectedValue));

            foreach (RepeaterItem estimationCategory in Repeater1.Items)
            {
                int categoryId = int.Parse(((HiddenField) estimationCategory.FindControl("hdnEstimationCateID")).Value);
                decimal estimatedAmount = decimal.Parse(((TextBox) estimationCategory.FindControl("txtEstimation")).Text);
                decimal actualAmount = decimal.Parse(((TextBox) estimationCategory.FindControl("txtActual")).Text);

                oCommon.AddStudentSchoolEstimationDetail(nEditEstimationId, categoryId, estimatedAmount, actualAmount);
            }

            // save student info

            if (nStatus > 0)
            {
                if (bIsEdit)
                    Response.Redirect("ListStudentEstimations.aspx?status=5031221"); //  
                else
                    Response.Redirect("ListStudentEstimations.aspx?status=5031211");
            }
            else
            {
                Response.Redirect("AddStudentEstimation.aspx?status=5031210&EstimationId=" +
                                  nEditEstimationId.ToString());
            }
        }
    }
}