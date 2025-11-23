<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="ListUserEmails.aspx.cs" Inherits="SaveDC.ControlPanel.ListUserEmails" Title="List User Emails"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2" class="CellLabel" >
                <table width="100%">
                    <tr>
                        <td width="80%">
                            User Manager :: List User Emails
     	     
                           
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
            <td height="20px" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2"> 
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
	
      
       
        <tr>
            <td class="CellHeading" width="60%">List Sent Emails for <i><%= hdnDonorName.Value %></i></td>
            <td class="ContextLinks" align="right">Total Sent Emails: 
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>&nbsp; 
            </td>
        </tr>
        <tr> 
            <td class="CellLabel" colspan="2">
                <table width="100%" border = 0 id="tbDataFound" runat="server">
                    <tr>
                        <td width="100%"  valign="top">
                            <asp:HiddenField runat="server" ID="hdnSchoolID" Value ="" />
                            <asp:HiddenField runat="server" ID="hdnDonorName" Value ="" />
                            <asp:DataGrid ID="dgSchools" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" OnItemDataBound="dgSchools_Databound" >
                                <Columns>
                                   
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            CC
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "CC") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Subject
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Subject") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Body
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Body").ToString().Replace("\r\n", "<br>") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="60%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Sent On
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DateSent") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateColumn>	
                                    
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
          
                            <%-- <br />--%>
                            <uc1:DataPager ID="pagerApps" runat="server" PageIndex="1" RecordsPerPage="100" TotalRecords="0" />
                        </td>
                        <td valign="top">
                         
                        </td>
                    </tr>
                </table>
               
          
                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No Sent Email Found.</td>
                    </tr>
                </table>		
            </td>
        </tr> 
    </table>
</asp:Content>