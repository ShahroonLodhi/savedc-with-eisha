using System;
using System.IO;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailsFamily : Page
    {
        public DetailsFamily()
        {
            PreInit += Page_PreInit;
            Load += Page_Load;
        }

        private void Page_PreInit(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["modalwin"]) && Request.QueryString["modalwin"] == "1")
                MasterPageFile = "~/ControlPanel/Dummy.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // hide unrelated data for model win.
                if (!string.IsNullOrEmpty(Request.QueryString["modalwin"]) && Request.QueryString["modalwin"] == "1")
                    trBredCrum.Visible = false;

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

                    string szViewHTML =
                        "window.open('{0}?modalwin=1', 'View', 'left=100,top=30,screenX=100,screenY=30, height=550,width=840,toolbar=no,directories=no,status=no,menubar=no,modal=yes,scrollbars=yes');";
                    string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + oFamily.Bill1GUID + ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        Viewbill1.Attributes.Add("onclick",
                                                 string.Format(szViewHTML,
                                                               SaveDCConstants.BillsUploadPath + oFamily.Bill1GUID +
                                                               ".jpg"));
                    }
                    else
                    {
                        Viewbill1.Parent.Controls.Remove(Viewbill1);
                    }
                    szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + oFamily.Bill2GUID + ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        Viewbill2.Attributes.Add("onclick",
                                                 string.Format(szViewHTML,
                                                               SaveDCConstants.BillsUploadPath + oFamily.Bill2GUID +
                                                               ".jpg"));
                    }
                    else
                    {
                        Viewbill2.Parent.Controls.Remove(Viewbill2);
                    }
                    // cert 1
                    szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + oFamily.Cert1GUID + ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        viewCert1.Attributes.Add("onclick",
                                                 string.Format(szViewHTML,
                                                               SaveDCConstants.BillsUploadPath + oFamily.Cert1GUID +
                                                               ".jpg"));
                    }
                    else
                    {
                        viewCert1.Parent.Controls.Remove(viewCert1);
                    }
                    // cert 2
                    szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + oFamily.Cert2GUID + ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        viewCert2.Attributes.Add("onclick",
                                                 string.Format(szViewHTML,
                                                               SaveDCConstants.BillsUploadPath + oFamily.Cert2GUID +
                                                               ".jpg"));
                    }
                    else
                    {
                        viewCert2.Parent.Controls.Remove(viewCert2);
                    }
                }
            }
        }
    }
}