<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="DetailEmployee.aspx.cs" Inherits="SaveDC.ControlPanel.DetailEmployee" Title="Employee Details"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px"  class="CellLabel"  colspan="2">
               
               
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <%= hdnArea.Value%> :: <%= hdnRole.Value %> Details
                           
                        </td>
                        <td align="right" width="20%">
                            <%--<asp:ImageButton ID="ImageButton1" runat="server" SkinID="sknImgBack" Height="24"
                                             Width="24" OnClientClick="javascript:window.history.back(); return false;" />--%>
                            <asp:ImageButton ID="ImageButton1" runat="server" SkinID="sknImgBack" Height="24"
                                             Width="24" OnClick="ImageButton_Click" />
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
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="CellHeading" width="40%">
                <%= hdnRole.Value %> Details
            </td>
            <td class="ContextLinks" align="right">
            </td>
        </tr>
	
        <tr id="loginRow1" runat="server" visible="true">
            <td colspan="2" class="SubHeading" style="height: 23px">Employee Information</td>
        </tr>
        <asp:HiddenField ID="hdnArea" runat="server" />
        <asp:HiddenField ID="hdnRole" runat="server" />
	    <asp:HiddenField ID="hdnId" runat="server" />
	 
        <%--<tr>
            <td colspan="2" class="SubHeading" style="height: 23px">Profile Information</td>
        </tr>--%>
        <tr> 
            <td class="CellLabel">First Name : </td>
            <td class="CellData"><asp:Label ID="txtFName" runat="server" Text=""/></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Last Name : </td>
            <td class="CellData"><asp:Label ID="txtLName" runat="server" Text=""/></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Date Of Birth : </td>
            <td class="CellData"><asp:Label ID="txtDOB" runat="server" Text=""/></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Email Adress : </td>
            <td class="CellData"><asp:Label ID="txtEmail" runat="server" Text=""/></td>
        </tr>
        <tr> 
            <td class="CellLabel">Cell # : </td>
            <td class="CellData"><asp:Label ID="txtPhone" runat="server" Text=""/></td>
        </tr> 
        <tr id="trGender" runat="server" visible="true"> 
            <td class="CellLabel">Gender : </td>
            <td class="CellData"><asp:Label ID="txtGender" runat="server" Text=""/></td>
        </tr>
        <tr id="trCNIC" runat="server" visible="true"> 
            <td class="CellLabel">CNIC # : </td>
            <td class="CellData"><asp:Label ID="txtCNIC" runat="server" Text=""/></td>
        </tr>
        <tr id="trOccupation" runat="server" visible="true"> 
            <td class="CellLabel">Designation : </td>
            <td class="CellData"><asp:Label ID="txtDesignation" runat="server" Text=""/></td>
        </tr>
        <tr id="trQualification" runat="server" visible="true"> 
            <td class="CellLabel">Department : </td>
            <td class="CellData"><asp:Label ID="txtDepartment" runat="server" Text=""/></td>
        </tr>
        <tr> 
            <td class="CellLabel">Address : </td>
            <td class="CellData"><asp:Label ID="lblAddress" runat="server" Text=""/></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Note : </td>
            <td class="CellData"><asp:Label ID="txtNote" runat="server" Text=""/>
            </td>
        </tr>         
    </table>

</asp:Content>