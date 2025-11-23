<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="SendMarkettingSMS.aspx.cs" Inherits="SaveDC.ControlPanel.SendMarkettingSMS" Title="Send Marketting SMS"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
                        
            <td colspan="2" class="CellLabel" height="20px" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            SMS Manager :: Send Bulk SMS
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
            <td class="CellHeading" width="40%">
                Send Bulk SMS
            </td>
            <td class="ContextLinks" align="right">
               
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Browse Recipient File:
            </td>
            <td class="CellData">
                <input type="file" id="filePhoneNums" runat ="server"/>
                <br />(select recipient file containing phone numbers)
                <br /><i>the sample file format is as below</i>
                <br />Haider,+920000000000<br />Sohail,+920000000000<br />Azhar,+440000000000
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Phone No. :
            </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtPhoneNum" runat="server" Columns="50"/>
                <cc2:MaskedEditExtender
            ID="MaskedEditExtender2"
            Enabled ="false"
            runat="server"
            TargetControlID="txtPhoneNum"
            Mask="+\929999999999"
            MessageValidatorTip="true"
            OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError"
            MaskType="None"
            ClearMaskOnLostFocus="false"
            ErrorTooltipEnabled="True" />
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Message :
            </td>
            <td class="CellData">
                Dear <i>Member</i>,<br />
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