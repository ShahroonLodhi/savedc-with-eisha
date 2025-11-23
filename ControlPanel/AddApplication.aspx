<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  EnableEventValidation="False" CodeBehind="AddApplication.aspx.cs" Inherits="SaveDC.ControlPanel.AddApplication" Title="Add Application"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <link type="text/css" href="../js/css/ui-lightness/jquery-ui-1.8.18.custom.css" rel="stylesheet" />	
    <script type="text/javascript" src="../js/jquery-1.7.1.min.js"> </script>
    <script type="text/javascript" src="../js/jquery-ui-1.8.18.custom.min.js"> </script>
    <script type="text/javascript" language="javascript">

        function RemoveCertificate1() {

            document.getElementById('browseCert1').style.display = '';
            document.getElementById('viewCert1').style.display = 'none';
            document.getElementById("<%= hdnNewCerti1.ClientID %>").value = '';
        }

        function RemoveCertificate2() {

            document.getElementById('browseCert2').style.display = '';
            document.getElementById('viewCert2').style.display = 'none';
            document.getElementById("<%= hdnNewCerti2.ClientID %>").value = '';
        }


        function StartupScriptCert() {

            var ViewHTML = "window.open('_REPLACEURL?modalwin=1', 'View', 'left=100,top=30,screenX=100,screenY=30, height=550,width=840,toolbar=no,directories=no,status=no,menubar=no,modal=yes,scrollbars=yes');";

            // certificates
            var certNewVal1 = document.getElementById("<%= hdnOldCerti1.ClientID %>").value;
            if (certNewVal1) {
                document.getElementById('browseCert1').style.display = 'none';
                document.getElementById('viewCert1').style.display = '';
                //  document.getElementById('viewLinkCerti1').href = '../Uploads/UtilityBills/' + certNewVal1 + '.jpg';
                document.getElementById('viewLinkCerti1').setAttribute("onclick", ViewHTML.replace("_REPLACEURL", '../Uploads/UtilityBills/' + certNewVal1 + '.jpg'));
            } else {
                document.getElementById('browseCert1').style.display = '';
                document.getElementById('viewCert1').style.display = 'none';
            }

            // certificates
            var certNewVal2 = document.getElementById("<%= hdnOldCerti2.ClientID %>").value;
            if (certNewVal2) {
                document.getElementById('browseCert2').style.display = 'none';
                document.getElementById('viewCert2').style.display = '';
                //document.getElementById('viewLinkCerti2').href = '../Uploads/UtilityBills/' + certNewVal2 + '.jpg';
                document.getElementById('viewLinkCerti2').setAttribute("onclick", ViewHTML.replace("_REPLACEURL", '../Uploads/UtilityBills/' + certNewVal2 + '.jpg'));
            } else {
                document.getElementById('browseCert2').style.display = '';
                document.getElementById('viewCert2').style.display = 'none';
            }


        }


        $(document).ready(function() {
            $(".showCalender").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                yearRange: "-50:+0"
            });
            $(".showCalender").datepicker("option", "showAnim", "slideDown");

//            $("#<%= txtReceivedOn.ClientID %>").datepicker({
//                changeMonth: true,
//                changeYear: true
//            });
//            $("#<%= txtReceivedOn.ClientID %>").datepicker("option", "showAnim", "slideDown");
        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <asp:HiddenField ID="hdnAddEdit" Value="Add" runat="server" />
            <td height="20px" class="CellLabel" colspan="3">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Application Manager ::
                            <%= hdnAddEdit.Value %>
                            New Application
                        </td>
                        <td align="right" width="20%">
                            <asp:ImageButton ID="ImageButton1" runat="server" SkinID="sknImgBack" Height="24"
                                             Width="24" OnClientClick="javascript:window.history.back(); return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20px">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <asp:HiddenField ID="hdnEditAppID" Value="0" runat="server" />
            <td class="CellHeading" width="40%">
                <%= hdnAddEdit.Value %>
                New Application
            </td>
            <td class="ContextLinks" colspan="2" align="right">
             
            </td>
        </tr>
        <tr>
            <td colspan="3" class="SubHeading" style="height: 23px">
                Student Profile
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                First Name :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtStudentFName" runat="server" Columns="50" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStudentFName"
                                            ErrorMessage="Please enter a First Name."></asp:RequiredFieldValidator>
            </td>
            <td rowspan="5" Width = "190px">
                <table cellpadding='0' border="0" cellspacing='0' width='100%' style='margin-bottom: 10px;'>
                    <tr>
                        <asp:HiddenField runat ="server"  ID="hdnOldProfileImage" Value = ""/>
                        <asp:HiddenField runat ="server"  ID="hdnProfileImage" Value = ""/>
                        <asp:HiddenField runat ="server"  ID="hdnProfileImageURL" Value = "../images/nophoto.gif"/>
                        <td class='profile_photo' width='162'>
                            <img class='photo' height="160" width="150" id="imgUpload" src='<%= hdnProfileImageURL.Value %>' border='0' />
                        </td>
                    </tr>
                </table>
                <table cellpadding='0' cellspacing='0' width='100%' style='margin-bottom: 10px;'>
                    <tr>
                        <td align="center">
                            <input type="file" accept="image/*" width='162px' id="userprofile" runat ="server"/>
                            <%--<asp:asyncfileupload id="AsyncFileUpload1" runat="server" Width = "190px"
                                    onuploadedcomplete="ProcessUpload" throbberid="spanUploading" />
                                <span id="spanUploading" runat="server">Uploading...</span>--%>
                        </td>
                    </tr>
                </table>
                (select any image like jpg, gif, png etc...)
            </td>
        </tr> 
        <tr> 
            <td class="CellLabel">Last Name : </td>
            <td  class="CellData" ><asp:TextBox CssClass="Textbox" ID="txtStudentLName" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStudentLName"
                                            ErrorMessage="Please enter a Last name."></asp:RequiredFieldValidator></td>
        </tr> 
	 
	
        <tr> 
            <td class="CellLabel">Date of Birth : </td>
            <td  class="CellData">
                <asp:TextBox CssClass="Textbox showCalender" ID="txtDOB" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDOB"
                                            ErrorMessage="Please enter Date of Birth." Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDOB" 
                    ErrorMessage="Please enter valid Date of Birth. e.g. 15/12/2001" 
                  
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                    Display="Dynamic"></asp:RegularExpressionValidator><br />(valid date format is dd/mm/yyyy. e.g. 15/12/2001)
            </td>
        </tr> 
        
        <tr>
            <td class="CellLabel">
                Previous Class of Child/Children :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtPrevClass" runat="server" Columns="50" MaxLength="1024"
                             Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        
        <tr> 
            <td class="CellLabel">Guardian Name : </td>
            <td  class="CellData" ><asp:TextBox CssClass="Textbox" ID="txtGuardianName" runat="server" Columns="50"/>
            </td>
        </tr> 
        
        <tr>
            <td class="CellLabel">
                Residence/Mailing Address :
            </td>
            <td class="CellData" colspan="2" >
                <asp:TextBox CssClass="Textbox" ID="txtGAddress" runat="server" Columns="50" MaxLength="1024"
                             Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        
        
        
        
        
        <tr>
            <td colspan="3" class="SubHeading" style="height: 23px">
                Applicant Details
            </td>
        </tr>
        <tr> 
            <td class="CellLabel">Applicant Name : </td>
            <td  class="CellData" colspan="2" ><asp:TextBox CssClass="Textbox" ID="txtApplicantName" runat="server" Columns="50"/>
             
            </td>
        </tr> 
        <tr> 
            <td class="CellLabel">Applicant Contact # : </td>
            <td  class="CellData" colspan="2" ><asp:TextBox CssClass="Textbox" ID="txtApplicantContactNum" runat="server" Columns="50"/>
            </td>
        </tr> 
        
        
        <tr>
            <td colspan="3" class="SubHeading" style="height: 23px">
                Application Details
            </td>
        </tr>
        <tr> 
            <td class="CellLabel">Application # : </td>
            <td  class="CellData" colspan="2" ><asp:TextBox CssClass="Textbox" ID="txtApplicationNum" runat="server" Columns="50"/>
            </td>
        </tr> 
        
        <tr> 
            <td class="CellLabel">Application Category : </td>
            <td  class="CellData" colspan="2" >
                <asp:RadioButtonList ID="rbAppCategory" runat="server" CellPadding="1" CellSpacing="1"
                                     RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="Orphan ">Orphan</asp:ListItem>
                    <asp:ListItem  Value="Neglected">Neglected</asp:ListItem>
                    <asp:ListItem  Value="Neglected">Divorced</asp:ListItem>
                    <asp:ListItem  Value="Other">Other</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        
        <tr> 
            <td class="CellLabel">Delivered By : </td>
            <td  class="CellData" colspan="2" >
                <asp:RadioButtonList ID="rbAppReceivedBy" runat="server" CellPadding="1" CellSpacing="1"
                                     RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="Post ">Post</asp:ListItem>
                    <asp:ListItem  Value="Hand">Hand</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr> 
        <tr>
            <td class="CellLabel">
                Delivery Notes :
            </td>
            <td colspan="2" class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtDeliveryNotes" runat="server" Columns="50" MaxLength="1024"
                             Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        
        
        <tr> 
            <td class="CellLabel">Received On : </td>
            <td  class="CellData" colspan="2" >
                <asp:TextBox CssClass="Textbox showCalender" ID="txtReceivedOn" runat="server" Columns="50"/>
            </td>
        </tr>
        
        <tr> 
            <td class="CellLabel">Referred By : </td>
            <td  class="CellData" colspan="2" ><asp:TextBox CssClass="Textbox" ID="txtReferedBy" runat="server" Columns="50"/>
            </td>
        </tr> 
        
        
        <tr>
            <td colspan="3" class="SubHeading" style="height: 23px">
                Supporting Documents
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Death Certificate :
            </td>
            <td class="CellData" colspan="2">
                <asp:HiddenField runat="server" ID="hdnOldCerti1" Value="" />
                <asp:HiddenField runat="server" ID="hdnNewCerti1" Value="" />
                <span id="viewCert1" ><a  href="javascript:void(0);"  id="viewLinkCerti1">View Certificate</a> | <a
                                                                                                                     href="#" onclick=" RemoveCertificate1(); return false; ">Remove Certificate</a> </span>
                <span id="browseCert1">
                    <input accept="image/*" type="file" id="certificateFile1" runat ="server"/>
                </span>
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Divorce Certificate :
            </td>
            <td class="CellData" colspan="2">
                <asp:HiddenField runat="server" ID="hdnOldCerti2" Value="" />
                <asp:HiddenField runat="server"  ID="hdnNewCerti2" Value="" />
                 
                <span id="viewCert2"  ><a  href="javascript:void(0);"  id="viewLinkCerti2">View Certificate</a> | <a href="#" onclick=" RemoveCertificate2(); return false; ">
                                                                                                                      Remove Certificate</a> </span><span id="browseCert2">
                                                                                                                                                        <input accept="image/*" type="file" id="certificateFile2" runat ="server"/>
                                                                                                                                                    </span>
            </td>
        </tr>
        
        <tr>
            <td colspan="3" class="SubHeading" style="height: 23px">
                Additional Details (if any)
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Note :
            </td>
            <td colspan="2" class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtNote" runat="server" Columns="50" MaxLength="1024"
                             Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        
        <tr>
            <td colspan="3" class="SubHeading" style="height: 23px">
                Official Use
            </td>
        </tr>
        <tr> 
            <td class="CellLabel">Social Organizer Name : </td>
            <td  class="CellData" colspan="2" >
                <asp:TextBox CssClass="Textbox" ID="txtApplicantDO" runat="server" Columns="50"/>
            </td>
        </tr> 
        <tr> 
            <td class="CellLabel">Field Verification Person : </td>
            <td  class="CellData" colspan="2" >
                <asp:TextBox CssClass="Textbox" ID="txtFieldVarificationPerson" runat="server" Columns="50"/>
            </td>
        </tr> 
        <tr> 
            <td class="CellLabel">Board Note : </td>
            <td colspan="2" class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtBNote" runat="server" Columns="50" MaxLength="1024"
                             Rows="4" TextMode="MultiLine" />
            </td>
        </tr> 
          
          
      
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="ButtonSpacer">
                <asp:ImageButton runat="server" ID="btnUpdate" SkinID="sknImgBtnAddUser" OnClick="btnUpdate_Click" />
            </td>
        </tr>

        <script language="javascript" type="text/javascript">

            StartupScriptCert();
        </script>

    </table>

</asp:Content>