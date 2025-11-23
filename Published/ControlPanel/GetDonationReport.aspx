<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="GetDonationReport.aspx.cs" Inherits="SaveDC.ControlPanel.GetDonationReport" Title="Get Donation Report"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <head>
        <title>Get Donation Report</title>
        <style>
            .mGrid td {   
                text-align:center !important;
            }
        </style>
    </head>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2"  class="CellLabel" >
            
                
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Reports :: Donation Report
                           
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
            <td colspan="1" class="CellLabel">
                <table id="Table1" runat="server" width="100%">
                    <form name="frmsearch" method="post" id="frmsearch">
                        <tr>
                            <td width="40%">
                                Donor Name:
                            </td>
                            <td valign="middle">
                                <asp:TextBox CssClass="Textbox" ID="txtUserName" runat="server" />
                            </td>
                            <td width="70%" align="left">
                                <asp:ImageButton ID="searchbtn" runat="server" ImageUrl="search.png" Height="40"
                                                 Width="40" OnClick="searchbtn_Click" />
                            </td>
                        </tr>
                    
                    </form>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="10px" align="right">
                [ <font color ="gray">General Donation Report</font> ] [ <a href="GetSchoolDonationReport.aspx">Monthly Donation Report</a> ] [ <a href="GetExpenseReport.aspx">General Expense Report</a> ] [ <a href="GetSchoolExpenseReport.aspx">Monthly Expense Report</a> ] [ <a href="GetSMSReport.aspx">SMS Report</a> ] 
            </td>
        </tr>
        <tr>
            <td class="CellHeading" width="60%">
                Donation Report
            </td>
            <td class="ContextLinks" align="right">
                Total Donors:
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>&nbsp;<%--<a id="context" href= "ListUsers.aspx">[ List Users ]</a> <a id="context" href= "AddUsers.aspx">[ Add Users ]</a>--%>
                <asp:LinkButton runat="server" OnClick="btnExportToExcel_Click" Style="color:white">[ Export To Excel ]</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                <table width="100%" border="0" id="tbDataFound" runat="server">
                    <tr>
                        <td width="100%">
                            <asp:HiddenField runat="server" ID="hdnUserID" Value="" />
                            <asp:DataGrid ID="dgUsers" runat="server" AutoGenerateColumns="False" Width="100%"
                                          SkinID="StanderdGrid" OnItemDataBound="dgUsers_Databound" CssClass="mGrid">
                                <Columns>
                                    <asp:TemplateColumn visible="false">
                                        <HeaderTemplate>
                                            Donor Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "DonorName") %>--%>
                                            <%# ConvertToTitleCase(Eval("DonorName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Full Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "FirstName") %> <%# DataBinder.Eval(Container.DataItem, "LastName") %>--%>
                                            <%# ConvertToTitleCase(Eval("FirstName").ToString())%> <%# ConvertToTitleCase(Eval("LastName").ToString())%> 
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Balance
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Balnace" runat="server" Value = '<%# ConvertAmounToPkrCurrency(Eval("Balnace")) %>' />
                                            <%--<%# DataBinder.Eval(Container.DataItem, "Balnace") %>--%>
                                            <%# ConvertAmounToPkrCurrency(Eval("Balnace")) %>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Actions">
                                        <HeaderTemplate>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkSendEmail" runat="server" NavigateUrl='<%# String.Format("~/ControlPanel/SendEmailToDonor.aspx?DonorId={0}&return=reports", DataBinder.Eval(Container.DataItem,"DonorID")) %>'>Send Email</asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Actions">
                                        <HeaderTemplate>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkSendSMS" runat="server" NavigateUrl='<%# String.Format("~/ControlPanel/SendSMSToDonor.aspx?DonorId={0}&return=reports", DataBinder.Eval(Container.DataItem,"DonorID")) %>'>Send SMS</asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Total Donations
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# ConvertAmounToPkrCurrency(Eval("TotalDonations")) %>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "TotalDonations") %>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Total Expenses
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# ConvertAmounToPkrCurrency(Eval("AmountUsed")) %>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "AmountUsed") %>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Total Students
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "TotalStudents") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateColumn>
                                    
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                            
                            <%-- <br />--%>
                            <uc1:DataPager ID="pagerApps" runat="server" PageIndex="1" RecordsPerPage="100" TotalRecords="0" />
                        </td>
                    </tr>
                </table>

                <script type="text/javascript" language="javascript"> </script>

                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>
                            No Donor Found.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>