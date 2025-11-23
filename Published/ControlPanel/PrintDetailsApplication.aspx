<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/Dummy.Master" CodeBehind="PrintDetailsApplication.aspx.cs"
         Inherits="SaveDC.ControlPanel.PrintDetailsApplication" Title="Print Details Application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="75%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="3" class="CellLabel">
                <table width="100%">
                    <tr>
                        <tr colspan="2">
                            <td height="80" bgcolor="ffffff" align="left">
                                <img width="600" height="63" src="../images/savedc-address.png" />
                            </td>
                        </tr>
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
        <asp:PlaceHolder ID="panPrintable" runat="server">
            <tr>
                <asp:HiddenField ID="hdnEditAppID" Value="0" runat="server" />
                <td class="CellHeading" width="40%">
                    Application Details
                </td>
                <td class="ContextLinks" colspan="3" align="right">
                    <asp:ImageButton ID="ImageButton2" AlternateText="Print" ToolTip="Print" runat="server"
                                     SkinID="sknImgPrint" Height="24" Width="24" OnClientClick="PrintForm();" />
                </td>
            </tr>
            <tr> 
                <td class="CellLabel">Application # : </td>
                <td class="CellData" colspan="3">
                    <asp:Label ID="txtApplicationNum" runat="server"  />
              
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
                    <asp:Label ID="txtStudentFName" runat="server" />
                </td>
                <td rowspan="5" width="190px">
                    <table cellpadding='0' cellspacing='0' border="0" width='100%' style='margin-bottom: 10px;'>
                        <tr>
                            <td class='profile_photo' width='162'>
                                <img class='photo' alt="" height="160" runat="server" width="150" id="imgUpload"
                                     src='../images/nophoto.gif' border='0' />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Last Name :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtStudentLName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Date of Birth :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtDOB" runat="server" />
                </td>
            </tr>
            
            
            <tr>
                <td class="CellLabel">
                    Previous Class of Child/Children :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtPrevClass" runat="server"  />
                </td>
            </tr>
        
            <tr> 
                <td class="CellLabel">Guardian Name : </td>
                <td  class="CellData" >
                    <asp:Label ID="txtGuardianName" runat="server"  />
               
                </td>
            </tr> 
        
            <tr>
                <td class="CellLabel">
                    Residence/Mailing Address :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtGAddress" runat="server"  />
                
                
                </td>
            </tr>
        
        
            <tr>
                <td colspan="3" class="SubHeading" style="height: 23px">
                    Applicant Details
                </td>
            </tr>
            <tr> 
                <td class="CellLabel">Applicant Name : </td>
                <td  class="CellData" colspan="2">
                    <asp:Label ID="txtApplicantName" runat="server"  />
                     
                
                </td>
            </tr> 
            <tr> 
                <td class="CellLabel">Applicant Contact # : </td>
                <td  class="CellData" colspan="2">
                    <asp:Label ID="txtApplicantContactNum" runat="server"  />
               
                </td>
            </tr> 
        
        
            <tr>
                <td colspan="3" class="SubHeading" style="height: 23px">
                    Application Details
                </td>
            </tr>
            <%--<tr> 
                <td class="CellLabel">Applicant # : </td>
                <td  class="CellData" colspan="2">
                    <asp:Label ID="txtApplicationNum" runat="server"  />
              
                </td>
            </tr>--%> 
        
            <tr> 
                <td class="CellLabel">Application Category : </td>
                <td  class="CellData" colspan="2">
                    <asp:Label ID="txtAppCategory" runat="server"  />
                
                
                </td>
            </tr>
        
            <tr> 
                <td class="CellLabel">Delivered By : </td>
                <td  class="CellData" colspan="2">
                    <asp:Label ID="txtAppReceivedBy" runat="server"  />
               
                </td>
            </tr> 
            <tr> 
                <td class="CellLabel">Delivery Notes : </td>
                <td  class="CellData" colspan="2" >
                    <asp:Label  ID="txtDeliveryNotes" runat="server" />
                </td>
            </tr>
            <tr> 
                <td class="CellLabel">Received On : </td>
                <td  class="CellData" colspan="2" >
                    <asp:Label  ID="txtReceivedOn" runat="server" />
                </td>
            </tr>
        
            <tr> 
                <td class="CellLabel">Referred By : </td>
                <td  class="CellData" colspan="2">
                    <asp:Label ID="txtReferedBy" runat="server"  />
               
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
                    <asp:Label ID="txtNote" runat="server"  />
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
                    <asp:Label ID="txtApplicantDO" runat="server"  />
                </td>
            </tr> 
            <tr> 
                <td class="CellLabel">Field Verification Person : </td>
                <td  class="CellData" colspan="2" >
                    <asp:Label ID="txtFieldVarificationPerson" runat="server"  />
                
                </td>
            </tr> 
            <tr> 
                <td class="CellLabel">Board Note : </td>
                <td colspan="2" class="CellData">
                    <asp:Label ID="txtBNote" runat="server"  />
                </td>
            </tr> 
            
            
        </asp:PlaceHolder>
      
    </table>
</asp:Content>