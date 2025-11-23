<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="GetSchoolExpenseReportDetail.aspx.cs" Inherits="SaveDC.ControlPanel.GetSchoolExpenseReportDetail" Title="Get Monthly Expense Report Detail"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2"  class="CellLabel" >
             
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Reports :: Monthly Expense Report :: Details
                           
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
        <tr>
            <td colspan="2" height="10px" align="right">
             
            </td>
        </tr>
        <tr>
            <td class="CellHeading" width="60%">
                Monthly Expense Report for <i><asp:Label ID="lblMonth" runat="server" Text="0"></asp:Label></i>
            </td>
            <td class="ContextLinks" align="right">
                Total Expenses:
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>&nbsp;<%--<a id="context" href= "ListUsers.aspx">[ List Users ]</a> <a id="context" href= "AddUsers.aspx">[ Add Users ]</a>--%>
                <asp:LinkButton runat="server" OnClick="btnExportToExcel_Click" Style="color:white">[ Export To Excel ]</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                <table width="100%" border="0" id="tbDataFound" runat="server">
                    <tr>
                        <td width="100%">
                            <asp:HiddenField runat="server" ID="hdnExpMonth" Value="" />
                            <asp:DataGrid ID="dgSchools" runat="server" AutoGenerateColumns="False" Width="100%"
                                          SkinID="StanderdGrid" OnItemDataBound="dgSchools_Databound">
                                <Columns>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Payee Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "SchoolName") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Expense Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "ExpenseType")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Expense Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "TotalExpenses") %>--%>
                                            <%# ConvertAmounToPkrCurrency(Eval("TotalExpenses")) %>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Payment Mode
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PaymentMode") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Bank
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "BankName") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Posted On
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PaymentDate") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateColumn>
                                    
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                            
                            <uc1:DataPager ID="pagerApps" runat="server" PageIndex="1" RecordsPerPage="100" TotalRecords="0" />
                        </td>
                    </tr>
                </table>


                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>
                            No Expense Found.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>