<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="ListSchools.aspx.cs" Inherits="SaveDC.ControlPanel.ListSchools" Title="List Schools"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function ValidateAction(action) {
            if (action == 'DELETE') {
                if (confirm("Are you sure, you want to delete this school?"))
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
            document.getElementById('<%= hdnSchoolID.ClientID %>').value = rdo.value;
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
        <title>List Schools</title>
        <style>
            .mGrid td+td {   
                text-align:center !important;
            }
        </style>
    </head>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2" class="CellLabel" >
                <table width="100%">
                    <tr>
                        <td width="80%">
                            School Manager :: List Schools
     	     
                           
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
            <td height="20px" colspan="2">This is School Manager. Here all of your schools are listed with their brief details like the Principal Name, Social Organizer, School Phone and number of your students studying in.
                If you want to have a detail view of the school then select the appropriate school and click on School Details button.<br /><br />
                You can also filter specific school in the listing through School Name.
            </td>
        </tr>
        <tr>
            <td colspan="2"> 
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
	
        <tr>
            <td colspan="1" class="CellLabel">
                <table id="Table1" runat="server"   width="100%" >
                    <form name="frmsearch" method="post" id="frmsearch">
                        <tr> 
			  
                            <td width="40%">School Name: </td>
                            <td  valign ="middle"><asp:TextBox CssClass="Textbox" ID="txtSchoolName" runat="server" /> 
                                <%--<a href="#" onclick="__doPostBack('btnSearch', '');"> <span style="color: #0099cc">Search</span></a>--%>  
                   
                            </td>
                            <td width="70%" align = "left"><asp:ImageButton ID="searchbtn" runat ="server" ImageUrl="search.png" height="40" width="40" OnClick="searchbtn_Click" /></td>
                            <%--  <td width="20%">   <a href="#"><img  src="../images/search.png" /></a>
		        </td>--%>
		       
                        </tr>
                    </form>
                </table>
            </td>
        </tr>
	
        <tr><td colspan="2" height="10px" align="right">
                [ <a href="AddSchool.aspx">Add School</a> ]
            <asp:LinkButton runat="server" OnClick="btnExportToExcel_Click" Style="color:black">[ Export To Excel ]</asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnShowHideList" OnClick="btnShowHideList_Click" Style="color:black">[ Show Discontinue List ]</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="CellHeading" width="60%">School Manager</td>
            <td class="ContextLinks" align="right">Total Schools: 
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>&nbsp; <%--<a id="context" href= "ListSchools.aspx">[ List Schools ]</a> <a id="context" href= "AddSchool.aspx">[ Add School ]</a>--%>
            </td>
        </tr>
        <tr> 
            <td class="CellLabel" colspan="2">
                <table width="100%" border = 0 id="tbDataFound" runat="server">
                    <tr style="line-height: 30px;">
                        <td width="85%"  valign="top">
                            <asp:HiddenField runat="server" ID="hdnSchoolID" Value ="" />
                            <asp:DataGrid ID="dgSchools" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" OnItemDataBound="dgSchools_Databound" CssClass="mGrid">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton onclick="SelectOne(this,'dgSchools')" ID="rowbtn" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SchoolID") %>'/>
                                            <%--  <%# DataBinder.Eval(Container.DataItem, "SchoolName")%>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateColumn>	
				    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            School Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--    <asp:HiddenField ID="hdnSchoolID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"SchoolID") %>' />--%>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "SchoolName") %>--%>
                                            <%# ConvertToTitleCase(Eval("SchoolName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="27%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Principal Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "PrincipalName") %>--%>
                                            <%# ConvertToTitleCase(Eval("PrincipalName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="17%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            S. Organizer
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "SocialOrganizerName") %>--%>
                                            <%# ConvertToTitleCase(Eval("SocialOrganizerName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="16%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Phone No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PhoneNumber") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateColumn>				
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Email Address
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "EmailAddress") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="16%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Total Ss
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnSchoolStdCount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TotalStudents") %>' />
                                            <%# DataBinder.Eval(Container.DataItem, "TotalStudents") %>
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
                            <table >
                                <tr><td>
                                        <asp:Button ID="btnDetails" runat="server" Width="100%" Text="School Details" OnClick="btnDetails_Click" />
                                    </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnEditSchool" runat="server" Width="100%" Text="Edit School" OnClick="btnEditSchool_Click" />
                                     </td></tr>
        	  
                                <tr ><td>
                                         <asp:Button ID="btnSendSms" runat="server" Width="100%" Text="Send SMS" OnClick="btnSendSMS_Click" />
                                     </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnSendEmail" runat="server" Width="100%" Text="Send Email" OnClick="btnSendEmail_Click" />
                                     </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnSmsHistory" runat="server" Width="100%" Text="SMS History" OnClick="btnSMSHistory_Click" />
                                     </td></tr>
        	  
                                <tr><td>
                                        <asp:Button ID="btnDelSchool" runat="server"  Width="100%" Text="Delete School"  OnClientClick="return ValidateAction('DELETE');"  OnClick="btnDelSchool_Click" /></td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <script type="text/javascript" language="javascript">
                    SelectFirst('dgSchools');

                </script>
          
                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No School Found.</td>
                    </tr>
                </table>		
            </td>
        </tr> 
    </table>
</asp:Content>