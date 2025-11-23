<%@ Page Language="C#" Theme="Default" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SaveDC.ControlPanel.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <title>SaveDC :: Control Panel Login </title>
    </head>
    <body>
        <form id="form1" runat="server">
            <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="80" align="left">
                        <asp:Image ID="imgCPTitle" runat="server" SkinID="sknImgCPTitle" />
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle" height="100%">
                        <table width="350" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <TD align="right" width="250" colspan="2">
                                    <asp:Image ID="imgLoginTitle" runat="server" SkinID="sknImgLoginTitle" />
                                </TD>
                            </tr> 
		             
                            <TR> 
                                <TD width="100" align="left" style="BORDER-RIGHT: silver 1px solid; PADDING-BOTTOM: 15px; PADDING-Left: 15px;">
                                    Username : 
                                </TD>
                                <TD style="BORDER-RIGHT: silver 1px solid; PADDING-BOTTOM: 15px; PADDING-Left: 15px;"> 
                                    <asp:TextBox TextMode="SingleLine" ID="loginName" runat="server" Width="150" Columns="20"/>
                                    <asp:RequiredFieldValidator id="reqLoginName" runat="server" ControlToValidate="loginName" ErrorMessage="*" Display="dynamic">*</asp:RequiredFieldValidator>
                                </TD>
                            </TR>
                            <TR> 
                                <TD width="100" align="left" style="BORDER-RIGHT: silver 1px solid; PADDING-BOTTOM: 15px; PADDING-Left: 15px;">
                                    Password : 
                                </TD>
                                <TD style="BORDER-RIGHT: silver 1px solid; PADDING-BOTTOM: 15px; PADDING-Left: 15px;"> 
                                    <asp:TextBox TextMode="Password" ID="loginPassword" runat="server" Width="150" Columns="20"/>
                                    <asp:RequiredFieldValidator id="reqPassword" runat="server" ControlToValidate="loginpassword" ErrorMessage="*" Display="dynamic">*</asp:RequiredFieldValidator>
                                </TD>
                            </TR>
                            <TR> 
                                <TD align="right" colspan="2" style="BORDER-RIGHT: silver 1px solid; PADDING-right: 65px;">
                                    <asp:ImageButton runat="server" ID="btnLogin" SkinID="sknImgBtnLogin" OnClick="btnLogin_Click" />
                        
                                </TD>
                            </TR>
                            <tr>
                                <TD align="right" width="250" colspan="2" align="right" style="PADDING-right: 65px;">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                                </TD>
                            </tr> 
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="20">&nbsp;</td>
                </tr>
            </table>
        </form>
    </body>
</html>