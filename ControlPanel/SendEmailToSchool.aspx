<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="SendEmailToSchool.aspx.cs" Inherits="SaveDC.ControlPanel.SendEmailToSchool" Title="Send Email to School"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
                        
            <td colspan="2" class="CellLabel" height="20px" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            School Manager :: Send Email
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
            <asp:HiddenField ID="hdnSchoolId" Value="0" runat="server" />
            <td class="CellHeading" width="40%">
                Send Email to <i><%= hdnSchoolName.Value %></i>
            </td>
            <td class="ContextLinks" align="right">
             
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Send Email From :
            </td>
            <td class="CellData">
                <asp:DropDownList ID="txtEmailFrom" runat="server">
                    <asp:ListItem Value="accounts@save-dc.org">accounts@save-dc.org</asp:ListItem>
                    <asp:ListItem Value="info@save-dc.org">info@save-dc.org</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                School Email :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtEmailAddress" runat="server" Columns="50"/>
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                CC :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtCC" runat="server" Columns="50"/>
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Subject :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtSubject" runat="server" Columns="50"/>
            </td>
        </tr>
        <%--<tr id="balanceRow" runat="server">
            <td class="CellLabel">
                Current Balance :
            </td>
            <td class="CellData">
                <asp:HiddenField ID="txtBalanceHidden" runat="server"/>
                <asp:Label ID="txtBalance" runat="server"/>&nbsp;&nbsp;<asp:CheckBox ID="includeBalance" AutoPostBack="true" OnCheckedChanged="btnIncludeBalance_Click" Checked="false" runat="server"/> Include in Email
            </td>
        </tr>--%>
        <tr>
            <td class="CellLabel">
                Message :
            </td>
            <td class="CellData">
                Dear <i><%= hdnSchoolName.Value %></i>,<br />
                <asp:TextBox CssClass="Textbox" ID="txtMessage" runat="server" Width="90%" Columns="50" MaxLength="1048576"
                             Rows="20" TextMode="MultiLine" />
                <br />Regards,
                <br />SAVE DC Email Service
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