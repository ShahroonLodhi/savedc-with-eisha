<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="SendSMSToSchool.aspx.cs" Inherits="SaveDC.ControlPanel.SendSMSToSchool" Title="Send SMS to School"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
                        
            <td colspan="2" class="CellLabel" height="20px" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            School Manager :: Send SMS
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
            <asp:HiddenField ID="hdnSchoolName" Value="School" runat="server" />
            <asp:HiddenField ID="hdnPrincipalName" Value="" runat="server" />
            <asp:HiddenField ID="hdnSchoolId" Value="0" runat="server" />
            <td class="CellHeading" width="40%">
                Send SMS to <i><%= hdnSchoolName.Value %></i>
            </td>
            <td class="ContextLinks" align="right">
             
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Principal Phone # :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtPhoneNum" runat="server" Columns="50"/>
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Message :
            </td>
            <td class="CellData">
                Dear <i><%= hdnPrincipalName.Value %></i>,<br />
                <asp:TextBox CssClass="Textbox" ID="txtMessage" runat="server" Columns="50" MaxLength="1024"
                             Rows="5" TextMode="MultiLine" />
                <br />Plz do not reply to this SMS.
                <br />Regards,
                <br />SAVE DC SMS Service
                <br />http://save-dc.org
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