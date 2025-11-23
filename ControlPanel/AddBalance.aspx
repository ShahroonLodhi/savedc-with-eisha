<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="AddBalance.aspx.cs" Inherits="SaveDC.ControlPanel.AddBalance" Title="Add Balance"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript"> </script>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" class="CellLabel" colspan="2"> 
                <asp:HiddenField ID="hdnAddEdit" value ="Add" runat="server" />
	
	
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Bank Balance :: <%= hdnAddEdit.Value %> Balance
                           
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
            <td class="CellHeading" width="40%"><%= hdnAddEdit.Value %> Balance</td>
            <td class="ContextLinks" align="right"></td>
        </tr>
	
        <%--<tr>
		<td colspan="2" class="SubHeading" style="height: 23px">Login Information</td>
	</tr>--%>
        <tr> 
            <td class="CellLabel">Allied Bank Ltd : </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtABL" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtABL"
                                            ErrorMessage="Please enter a balance amount."></asp:RequiredFieldValidator><asp:RangeValidator
                                                                                                                            ID="RangeValidator1" runat="server" 
                                                                                                                            ErrorMessage="Please enter a value greater then 0." 
                                                                                                                            ControlToValidate="txtABL" Display="Dynamic" MaximumValue="999999999999" 
                                                                                                                            MinimumValue="1"></asp:RangeValidator>
            </td>
        </tr> 
        <tr> 
            <td class="CellLabel">Faisal Bank Ltd : </td>
            <td class="CellData" ><asp:TextBox CssClass="Textbox" ID="txtFBL" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFBL"
                                            ErrorMessage="Please enter a balance amount."></asp:RequiredFieldValidator><asp:RangeValidator
                                                                                                                            ID="RangeValidator2" runat="server" 
                                                                                                                            ErrorMessage="Please enter a value greater then 0." 
                                                                                                                            ControlToValidate="txtFBL" Display="Dynamic" MaximumValue="999999999999" 
                                                                                                                            MinimumValue="1"></asp:RangeValidator>
            </td>
        </tr> 
	 
        <%--<tr>
		<td colspan="2" class="SubHeading" style="height: 23px">Profile Information</td>
	</tr>--%>
	
        <tr>
            <td>&nbsp;</td>
            <td class="ButtonSpacer"><asp:ImageButton runat="server" ID="btnUpdate" SkinID="sknImgBtnAddUser"  OnClick="btnUpdate_Click"/></td>
        </tr>
               
    </table>

</asp:Content>