<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="AddSchool.aspx.cs" Inherits="SaveDC.ControlPanel.AddSchool" Title="Add School"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript"> </script>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" class="CellLabel" colspan="2"> 
                <asp:HiddenField ID="hdnAddEdit" value ="Add" runat="server" />
	
	
                <table width="100%">
                    <tr>
                        <td width="80%">
                            School Manager :: <%= hdnAddEdit.Value %> School
                           
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
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
       
        <tr>
            <asp:HiddenField ID="hdnEditSchoolId" Value ="0" runat="server" />
            <td class="CellHeading" width="40%"><%= hdnAddEdit.Value %> School</td>
            <td class="ContextLinks" align="right"></td>
        </tr>
	
        <%--<tr>
		<td colspan="2" class="SubHeading" style="height: 23px">Login Information</td>
	</tr>--%>
        <tr> 
            <td class="CellLabel">School Name : </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtSchoolName" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSchoolName"
                                            ErrorMessage="Please enter a School Name."></asp:RequiredFieldValidator></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Principal Name : </td>
            <td class="CellData" ><asp:TextBox CssClass="Textbox" ID="txtPrinName" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrinName"
                                            ErrorMessage="Please enter a School's Principal name."></asp:RequiredFieldValidator></td>
        </tr> 
	 
        <%--<tr>
		<td colspan="2" class="SubHeading" style="height: 23px">Profile Information</td>
	</tr>--%>
	
        <tr> 
            <td class="CellLabel">Social Organizer Name : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtSocialOrg" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSocialOrg"
                                            ErrorMessage="Please enter school's Social Organizer name."></asp:RequiredFieldValidator></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Phone Number : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtPhone" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone"
                                            ErrorMessage="Please enter a Phone number."></asp:RequiredFieldValidator></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Email Adress : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtEmail" runat="server" Columns="50"/></td>
        </tr>
        <tr> 
            <td class="CellLabel">School Address : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtAddress" runat="server" Columns="50" MaxLength="1024" Rows="4" TextMode="MultiLine"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAddress"
                                            ErrorMessage="Please enter School Address."></asp:RequiredFieldValidator></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Note : </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtNote" runat="server"  Columns="50" MaxLength="1024" Rows="4" TextMode="MultiLine"/></td>
        </tr>  
        <tr>
            <td>&nbsp;</td>
            <td class="ButtonSpacer"><asp:ImageButton runat="server" ID="btnUpdate" SkinID="sknImgBtnAddUser"  OnClick="btnUpdate_Click"/></td>
        </tr>
               
    </table>

</asp:Content>