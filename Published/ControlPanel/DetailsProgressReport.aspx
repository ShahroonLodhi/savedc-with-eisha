<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="DetailsProgressReport.aspx.cs" Inherits="SaveDC.ControlPanel.DetailsProgressReport" Title="Progress Report Details"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript"> </script>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" class="CellLabel"  colspan="3">
	   
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Student Manager :: List Students :: Progress Reports :: Progress Report Details
                           
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
            <td class="CellHeading" width="40%">Progress Report Details</td>
            <td class="ContextLinks" align="right"  colspan="2"></td>
        </tr>
        <tr>
            <td class="CellLabel">
                Report Month :
            </td>
            <td class="CellData" colspan="2">
                <asp:Label ID="lblMonth" runat="server" Text="Label"></asp:Label>
            </td>
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
	 
        <asp:Repeater ID="Repeater1" runat="server" 
                      onitemdatabound="Repeater1_ItemDataBound">
            <HeaderTemplate>
                <tr>
                    <td class="SubHeading" style="height: 23px">
                        Particular
                    </td>
                    <td class="SubHeading" style="height: 23px">
                        Details
                    </td>
                    <td class="SubHeading" style="height: 23px">
                        Remarks
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="CellLabel">
                        <asp:HiddenField ID="hdnReportParticularId" runat="server" Value = '<%# DataBinder.Eval(Container.DataItem, "ReportParticularId") %>' />
                        <asp:HiddenField ID="hdnPrevRemarks" runat="server" Value = '<%# DataBinder.Eval(Container.DataItem, "RemarksId") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "ParticularDesc") %>  :
                    </td>
                    <td class="CellData">
                        <asp:Label ID='txtNotes' runat="server" Text ='<%# DataBinder.Eval(Container.DataItem, "Notes") %>' />
                    </td>
                    <td class="CellData">
                        <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="SubHeading" colspan="4" style="height: 23px">
                Report Provided by School
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Scanned Report :
            </td>
            <td class="CellData" colspan="2">
                <a href="#" runat ="server"  id="ViewReport">View Report</a>
            </td>
        </tr>
        <tr>
            <td class="SubHeading" colspan="4" style="height: 23px">
                Report Remarks
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