<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="DonorMonthlyExpReport.aspx.cs" Inherits="SaveDC.ControlPanel.DonorMonthlyExpReport" Title="Donor Monthly Exp Report"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2"  class="CellLabel" >
            
                
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Reports :: Monthly Expense Report
                           
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
            <td class="CellHeading" width="60%">
                Monthly Expense Report
            </td>
            <td class="ContextLinks" align="right">
                <%--Total Donors:
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>--%>&nbsp;<%--<a id="context" href= "ListUsers.aspx">[ List Users ]</a> <a id="context" href= "AddUsers.aspx">[ Add Users ]</a>--%>
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                <table width="100%" border="0" id="tbDataFound" runat="server">
                    <tr>
                        <td width="100%">
                        
                            <asp:DataGrid ID="dgUsers" runat="server" AutoGenerateColumns="False" Width="100%"
                                          SkinID="StanderdGrid" OnItemDataBound="dgUsers_Databound">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Expense Month
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "ExpenseMonthFormatted") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Expense Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Balnace" runat="server" Value = '<%# DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                            <%# DataBinder.Eval(Container.DataItem, "AmountAbs") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Total Students
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "TotalStudent") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                            
                            <uc1:DataPager ID="pagerApps" runat="server" PageIndex="1" RecordsPerPage="100" TotalRecords="0" />
                        </td>
                    </tr>
                </table>

                <script type="text/javascript" language="javascript"> </script>

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