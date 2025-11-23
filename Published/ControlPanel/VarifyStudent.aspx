<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="VarifyStudent.aspx.cs" Inherits="SaveDC.ControlPanel.VarifyStudent" Title="Verify Student"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript"> </script>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2"  class="CellLabel" >
	
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Student Manager :: List Students :: Verify Student
                        </td>
                        <td align="right" width="20%">
                            <asp:ImageButton ID="ImageButton2" runat="server" SkinID="sknImgBack"  Height="24"
                                             Width="24" OnClientClick = "javascript:window.history.back(); return false;"/>
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
            <asp:HiddenField ID="hdnEditStudentId" Value ="0" runat="server" />
            <td class="CellHeading" width="40%">Verify Student</td>
            <td class="ContextLinks" align="right"></td>
        </tr>
	
        <tr> 
            <td class="CellLabel">Information Provided by the Student is Correct & Authentic? : </td>
            <td class="CellData"> <asp:RadioButtonList ID="rbIsVarified" runat="server" CellPadding="1" CellSpacing="1"
                                                       RepeatDirection="Horizontal">
                                      <asp:ListItem Selected="True" Value="True">Yes</asp:ListItem>
                                      <asp:ListItem Value="False">No</asp:ListItem>
                                  </asp:RadioButtonList></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Your Remarks : </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtNote" runat="server" Columns="50" MaxLength="1024"
                             Rows="4" TextMode="MultiLine" />
            </td>
        </tr>  
        <tr>
            <td>&nbsp;</td>
            <td class="ButtonSpacer"><asp:ImageButton runat="server" ID="btnUpdate" SkinID="sknImgBtnAddUser"  OnClick="btnUpdate_Click"/></td>
        </tr>
               
    </table>

</asp:Content>