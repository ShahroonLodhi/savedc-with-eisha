<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"
         CodeBehind="ListUsers.aspx.cs" Inherits="SaveDC.ControlPanel.ListUsers"
         Title="List Users" %>

<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function ValidateAction(action) {
            if (action == 'DELETE') {
                if (confirm("Are you sure, you want to delete this user?"))
                    return true;
                else
                    return false;
            }

        }

        function SelectOne(rdo, gridName) {
            /* Getting an array of all the "INPUT" controls on the form.*/
            all = document.getElementsByTagName("input");
            for (i = 0; i < all.length; i++) {
                if (all[i].type == "radio")/*Checking if it is a radio button*/ {
                    /*I have added '__ctl' ASP.NET adds '__ctl' to all 
                    the controls of DataGrid.*/
                    var count = all[i].id.indexOf(gridName);
                    if (count != -1) {
                        all[i].checked = false;
                    }
                }
            }
            rdo.checked = true; /* Finally making the clicked radio button CHECKED */
            document.getElementById('<%= hdnUserID.ClientID %>').value = rdo.value;
        }

        function SelectFirst(gridName) {
            /* Getting an array of all the "INPUT" controls on the form.*/
            all = document.getElementsByTagName("input");
            for (i = 0; i < all.length; i++) {
                if (all[i].type == "radio")/*Checking if it is a radio button*/ {
                    var count = all[i].id.indexOf("rowbtn");
                    if (count != -1) {
                        {
                            SelectOne(all[i], gridName);
                            break;
                        }
                    }
                }
            }
        }
    </script>

    <head>
        <title>List Users</title>
        <style>
            .mGrid td+td {   
                text-align:center !important;
            }
        </style>
    </head>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            User Manager :: List Users
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
            <td height="20px" colspan="2">This is User Manager. Here all of your users operating this system are listed with their brief details like the First Name, Last Name, Phone Number, Email Address and its Role either he/she is Admin or Operator.
                If you want to have a detail view of the user then select the appropriate user and click on User Details button.<br /><br />
                You can also filter specific user in the listing through Username.
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
                                Username:
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
                [ <a href="AddUsers.aspx">Add User</a> ]
                <asp:LinkButton runat="server" OnClick="btnExportToExcel_Click" Style="color:black">[ Export To Excel ]</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="CellHeading" width="60%">User Manager</td>
            <td class="ContextLinks" align="right">Total Users:
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>&nbsp;<%--<a id="context" href= "ListUsers.aspx">[ List Users ]</a> <a id="context" href= "AddUsers.aspx">[ Add Users ]</a>--%>
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                <table width="100%" border="0" id="tbDataFound" runat="server">
                    <tr>
                        <td width="85%" valign="top">
                            <asp:HiddenField runat="server" ID="hdnUserID" Value="" />
                            <asp:DataGrid ID="dgUsers" runat="server" AutoGenerateColumns="False" Width="100%"
                                          SkinID="StanderdGrid" OnItemDataBound="dgUsers_Databound" CssClass="mGrid" >
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton onclick="SelectOne(this,'dgUsers')" ID="rowbtn" name="rowbtn" runat="server"
                                                             Value='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Username
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "UserName") %>--%>
                                            <%# ConvertToTitleCase(Eval("UserName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            First Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "FirstName") %>--%>
                                            <%# ConvertToTitleCase(Eval("FirstName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Last Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "LastName") %>--%>
                                            <%# ConvertToTitleCase(Eval("LastName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Phone #
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PhoneNumber") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Email Address
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "EmailAddress") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="18%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            User Role
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "UserRoleName") %>
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
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnDetail" runat="server" Width="100%" Text="User Details" OnClick="btnDetail_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnEditUser" runat="server" Width="100%" Text="Edit User" OnClick="btnEditUser_Click" />
                                    </td>
                                </tr>
                                <tr ><td>
                                         <asp:Button ID="btnSendEmail" runat="server" Width="100%" Text="Send Email" OnClick="btnSendEmail_Click" />
                                     </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnEmailsHistory" runat="server" Width="100%" Text="Emails History" OnClick="btnEmailsHistory_Click" />
                                     </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnSendSms" runat="server" Width="100%" Text="Send SMS" OnClick="btnSendSMS_Click" />
                                     </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnSmsHistory" runat="server" Width="100%" Text="SMS History" OnClick="btnSMSHistory_Click" />
                                     </td></tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnDelUser" runat="server" Width="100%" Text="Delete User" OnClientClick="return ValidateAction('DELETE');"
                                                    OnClick="btnDelUser_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <script type="text/javascript" language="javascript">
                    SelectFirst('dgUsers');
                </script>

                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>
                            No User Found.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>