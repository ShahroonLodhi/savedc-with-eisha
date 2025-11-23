<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="DetailsAttachment.aspx.cs" Inherits="SaveDC.ControlPanel.DetailsAttachment" Title="Attachment Details"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" class="CellLabel"  colspan="3">
	   
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Student Manager :: List Students :: Attachment Details
                           
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
            <td colspan="3">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
       
        <tr>
            <td class="CellHeading" width="40%">Attachment Details</td>
            <td class="ContextLinks" align="right"  colspan="2"></td>
        </tr>
        <tr>
            <td class="CellLabel">Student Name : </td>
            <td class="CellData"  colspan="2">
                <asp:Label ID="lblStdName" runat="server" Text="Label"></asp:Label></td>
        </tr> 
        <tr> 
            <td class="CellLabel">School Name : </td>
            <td class="CellData" colspan="2"> 
                <asp:Label ID="lblSchoolName" runat="server" Text="Label"></asp:Label></td>
        </tr> 
	 
        <tr>
            <td class="SubHeading" colspan="4" style="height: 23px">
                Attachment Submitted by Admin
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Scanned Attachment :
            </td>
            <td class="CellData" colspan="2">
                <a href="#" runat ="server"  id="ViewReport">View Attachment</a>
            </td>
        </tr>
        <tr>
            <td class="SubHeading" colspan="4" style="height: 23px">
                Attachment Remarks
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Added By:
            </td>
            <td class="CellData" colspan="2"> 
                <asp:Label ID="addedBy" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Remarks:
            </td>
            <td class="CellData" colspan="2"> 
                <asp:Label ID="txtRemarks" runat="server" Text=""></asp:Label>
              
            </td>
        </tr>
    </table>

</asp:Content>