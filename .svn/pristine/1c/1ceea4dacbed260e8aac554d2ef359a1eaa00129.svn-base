using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class ListStudentEstimations : Page
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
                                                          UserAccessLevels.Operator
                                                      });

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                {
                    btnDelEstimation.Visible = false;
                    btnEditEstimation.Visible = false;
                    btnAssignSchool.Visible = false;
                }

                ListEstimations();
                RenderError(Request.QueryString["status"]);
            }
        }

        private void ListEstimations()
        {
            int nStudentId = SaveDCSession.StudentId;

            var oStudent = new Student();
            oStudent.StudentId = nStudentId;
            var oStudentManager = new StudentManager(oStudent);
            oStudent = oStudentManager.Load();
            lblStdName.Text = oStudent.FirstName + " " + oStudent.LastName;
            hdnSelectedSchool.Value = oStudent.SchoolId.ToString();
            var oCommon = new Common();
            DataSet oSqlDataSet = oCommon.LoadStudentSchoolEstimations(nStudentId);

            dgEstimations.DataSource = oSqlDataSet;
            dgEstimations.DataBind();

            // hide/show grid if no rec found
            if (oSqlDataSet == null || oSqlDataSet.Tables[0].Rows.Count == 0)
            {
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
                // set the total
                lblTotal.Text = 0.ToString();
            }
            else
            {
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
                // set the total
                lblTotal.Text = oSqlDataSet.Tables[0].Rows.Count.ToString();
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

        protected void dgEstimations_Databound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string curSchoolId = Utils.fixNullString(((HiddenField) e.Item.FindControl("hdnCurSchoolId")).Value);
                if (hdnSelectedSchool.Value == curSchoolId)
                {
                    e.Item.Style.Add("background-color", "red");
                    e.Item.Style.Add("color", "white");
                }
            }
        }

        protected void btnEditEstimation_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurEstimationId = hdnEstimationID.Value;
            Response.Redirect("AddStudentEstimation.aspx?EstimationId=" + szCurEstimationId);
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            string szCurEstimationId = hdnEstimationID.Value;
            Response.Redirect("DetailsStudentEstimation.aspx?EstimationId=" + szCurEstimationId);
        }

        protected void btnDelEstimation_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurEstimationId = hdnEstimationID.Value;

            string szStatus = "5031230";
            var oCommon = new Common();
            int nStatus = oCommon.DeleteStudentSchoolEstimation(szCurEstimationId);
            if (nStatus > 0)
            {
                szStatus = "5031231";
            }

            Response.Redirect("ListStudentEstimations.aspx?status=" + szStatus);
        }

        protected void btnAssignSchool_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szStatus = "";
            string szCurStudentId = SaveDCSession.StudentId.ToString();
            string szCurEstimationId = hdnEstimationID.Value;
            var oCommon = new Common();
            int nStatus = oCommon.AssignSchool(szCurStudentId, szCurEstimationId);
            if (nStatus > 0)
            {
                szStatus = "5031121";
                Response.Redirect("ListStudents.aspx?status=" + szStatus);
            }
            if (nStatus == -2)
            {
                szStatus = "5031240";
                Response.Redirect("ListStudentEstimations.aspx?status=" + szStatus);
            }
            else
            {
                szStatus = "5031120";
                Response.Redirect("ListStudentEstimations.aspx?status=" + szStatus);
            }
        }
    }
}