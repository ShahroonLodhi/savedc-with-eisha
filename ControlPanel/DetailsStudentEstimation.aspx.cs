using System;
using System.Data.SqlClient;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailsStudentEstimation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // load edit user details.
                int nEstimationId = Utils.fixNullInt(Request.QueryString["EstimationId"]);
                int nStudentId = SaveDCSession.StudentId;

                var oCommon = new Common();
                SqlDataReader oSqlData = oCommon.LoadStudentSchoolEstimationDetails(nEstimationId);

                Repeater1.DataSource = oSqlData;
                Repeater1.DataBind();

                lblSchool.Text = oCommon.GetSchoolNameById(oCommon.GetSchoolIdByEstimationId(nEstimationId));

                var oStudent = new Student();
                oStudent.StudentId = nStudentId;
                var oStudentManager = new StudentManager(oStudent);
                oStudent = oStudentManager.Load();
                lblStdName.Text = oStudent.FirstName + " " + oStudent.LastName;

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

            string szErrorDesc = "";
            if (szErrorCode == "03")
            {
                szErrorDesc = "Error occured while adding estimation.";
                lblError.CssClass = "FailureMessage";
            }

            lblError.Text = szErrorDesc;
        }
    }
}