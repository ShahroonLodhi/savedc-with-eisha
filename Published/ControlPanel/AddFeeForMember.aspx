<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="AddFeeForMember.aspx.cs" Inherits="SaveDC.ControlPanel.AddFeeForMember" Title="Add Fee for Member"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
                        
            <td colspan="2" class="CellLabel" height="20px" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <%= hdnRole.Value %> :: Add Annual Fee
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
                Add Annual Fee for <i><%= hdnDonorName.Value %></i>
            </td>
            <td class="ContextLinks" align="right">
             
            </td>
        </tr>
        <tr  id="school2">
            <td class="CellLabel">
                For Month :
            </td>
            <td class="CellData">
                <asp:DropDownList ID="ddMonths" runat="server">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddYear" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Fee Amount :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtMessage" Text="500" runat="server"/>
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