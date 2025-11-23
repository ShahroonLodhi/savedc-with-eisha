<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Dummy.Master" AutoEventWireup="true"
         CodeBehind="PrintDetailsSchool.aspx.cs" Inherits="SaveDC.ControlPanel.PrintDetailsSchool"
         Title="Print Details School" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="CellHeading" width="25%">
                School Details
            </td>
            <td class="ContextLinks" align="right">
            </td>
        </tr>
       
        <tr>
            <td class="CellLabel">
                School Name :
            </td>
            <td class="CellData">
                <asp:Label ID="txtSchoolName" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Principal Name :
            </td>
            <td class="CellData">
                <asp:Label ID="txtPrinName" runat="server" Text="" />
            </td>
        </tr>
        <%--<tr>
		<td colspan="2" class="SubHeading" style="height: 23px">Profile Information</td>
	</tr>--%>
        <tr>
            <td class="CellLabel">
                Social Organizer Name :
            </td>
            <td class="CellData">
                <asp:Label ID="txtSocialOrg" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Phone Number :
            </td>
            <td class="CellData">
                <asp:Label ID="txtPhone" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Email Adress :
            </td>
            <td class="CellData">
                <asp:Label ID="txtEmail" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                School Address :
            </td>
            <td class="CellData">
                <asp:Label ID="txtAddress" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Note :
            </td>
            <td class="CellData">
                <asp:Label ID="txtNote" runat="server" Text="" />
            </td>
        </tr>
    </table>
</asp:Content>