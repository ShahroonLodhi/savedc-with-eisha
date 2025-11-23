<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="GetExpenseReport.aspx.cs" Inherits="SaveDC.ControlPanel.GetExpenseReport" Title="Get Expense Report"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <head>
        <title>Get Expense Report</title>
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
                            Reports :: Expense Report
                           
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
                                Payee Name:
                            </td>
                            <td valign="middle">
                                <asp:TextBox CssClass="Textbox" ID="txtSchoolName" runat="server" />
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
        <tr id="fullLinks" runat="server">
            <td colspan="2" height="10px" align="right">
                [ <a href="GetDonationReport.aspx">General Donation Report</a> ] [ <a href="GetSchoolDonationReport.aspx">Monthly Donation Report</a> ] [ <font color ="gray">General Expense Report</font> ] [ <a href="GetSchoolExpenseReport.aspx">Monthly Expense Report</a> ] [ <a href="GetSMSReport.aspx">SMS Report</a> ] 
            </td>
        </tr>
        <tr id="shortLinks" runat="server">
            <td colspan="2" height="10px" align="right">
                [ <font color ="gray">General Expense Report</font> ] [ <a href="GetSchoolExpenseReport.aspx">Monthly Expense Report</a> ] [ <a href="GetSMSReport.aspx">SMS Report</a> ] 
            </td>
        </tr>
        <tr>
            <td class="CellHeading" width="60%">
                Expense Report
            </td>
            <td class="ContextLinks" align="right">
                Total Expense Heads:
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
                            <asp:DataGrid ID="dgSchools" runat="server" AutoGenerateColumns="False" Width="100%"
                                          SkinID="StanderdGrid" OnItemDataBound="dgSchools_Databound" CssClass="mGrid">
                                <Columns>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Payee Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "PayeeName") %>--%>
                                            <%# ConvertToTitleCase(Eval("PayeeName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Expense Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "ExpenseType") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Expense Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "TotalExpenses") %>--%>
                                            <%# ConvertAmounToPkrCurrency(Eval("TotalExpenses")) %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="ListMonthlyExpenses.aspx?MonthName=<%# DataBinder.Eval(Container.DataItem, "PayeeName") %>">View Details by Entry</a>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="ListMonthlyExpenses.aspx?MonthName=<%# DataBinder.Eval(Container.DataItem, "PayeeName") %>">View Details by Category</a>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                    <%--<asp:TemplateColumn>
                                        <HeaderTemplate>
                                           Total Students
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "StudentsCount")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>--%>
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
                            No Expese Found.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>