<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="AddRemarksForDonor.aspx.cs" Inherits="SaveDC.ControlPanel.AddRemarksForDonor" Title="Add Remarks for Donor"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
                        
            <td colspan="2" class="CellLabel" height="20px" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <%= hdnRole.Value %> :: Add Remarks
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
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <asp:HiddenField ID="hdnRole" runat="server" />
            <asp:HiddenField ID="hdnDonorName" Value="School" runat="server" />
            <asp:HiddenField ID="hdnDonorId" Value="0" runat="server" />
            <td class="CellHeading" width="40%">
                Add Remarks for <i><%= hdnDonorName.Value %></i>
            </td>
            <td class="ContextLinks" align="right">
             
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Remarks :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtMessage" runat="server" Columns="50" MaxLength="1024"
                             Rows="5" TextMode="MultiLine" />
            </td>
        </tr>
       
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="ButtonSpacer">
                <asp:ImageButton runat="server" ID="btnUpdate" SkinID="sknImgBtnAddUser" OnClick="btnUpdate_Click" />
            </td>
        </tr>
    </table>

</asp:Content>