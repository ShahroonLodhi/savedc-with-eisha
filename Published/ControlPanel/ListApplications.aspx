<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="ListApplications.aspx.cs" Inherits="SaveDC.ControlPanel.ListApplications" Title="List Applications"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        function ValidateAction(action) {
            //  if (action == 'DELETE') {
            if (confirm("Are you sure, you want to " + action + " this application?"))
                return true;
            else
                return false;
            //  }
        }

        function SelectOne(rdo, gridName) {
            /* Getting an array of all the "INPUT" controls on the form.*/
            all = document.getElementsByTagName("input");
            for (i = 0; i < all.length; i++) {
                if (all[i].type == "radio")/*Checking if it is a radio button*/ {
                    var count = all[i].id.indexOf(gridName);
                    if (count != -1) {
                        all[i].checked = false;
                    }
                }
            }
            rdo.checked = true; /* Finally making the clicked radio button CHECKED */
            document.getElementById('<%= hdnAppID.ClientID %>').value = rdo.value;
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
        <title>List Applications</title>
        <style>
            .mGrid td+td {   
                text-align:center !important;
            }
        </style>
    </head>
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td  colspan="2" class="CellLabel" height="20px" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Application Manager :: List New Applications
                        </td>
                        <td align="right" width="20%">
                            <asp:ImageButton ID="ImageButton1" runat="server" SkinID="sknImgBack"  Height="24"
                                             Width="24" OnClientClick = "javascript:window.history.back(); return false;"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20px" colspan="2">This is New Applications Manager. Here you can view all of your new applications with their details. If you want to have a detail view of the new application then select the appropriate application and click on Application Details button.<br /><br />You can also filter specific application in the listing through Student Name.</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td colspan="1" class="CellLabel">
                <table id="Table1" runat="server" border="0" width="100%">
              <form name="frmsearch" method="post" id="frmsearch">
                        <tr>
                            <td width="40%">
                                Student Name:
                            </td>
                            <td valign="middle">
                                <asp:TextBox CssClass="Textbox" ID="txtStudentName" runat="server" />
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
	
        <tr id="trAddStudent" runat ="server">
            <td colspan="2" height="10px" align="right">
                                                  [ <a href="AddApplication.aspx" >Add New Application</a> ]
                <asp:LinkButton runat="server" OnClick="btnExportToExcel_Click" Style="color:black">[ Export To Excel ]</asp:LinkButton>
                <%--<asp:Button ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click" Text="Export To Excel" />--%>
           </td>
        </tr>
        <tr>
            <td class="CellHeading" width="60%">Application Manager</td>
            <td class="ContextLinks" align="right">Total New Applications: 
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr> 
            <td class="CellLabel" colspan="2">
                <table width="100%" border = 0 id="tbDataFound" runat="server">
                    <tr>
                        <td valign="top" width="85%" >
                            <asp:HiddenField runat="server" ID="hdnAppID" Value ="" />
                            <asp:DataGrid ID="dgStudents" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" CssClass="mGrid" >
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton onclick="SelectOne(this,'dgStudents');" ID="rowbtn" name="rowbtn" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ApplicationId") %>'/>
                                            <%--       <%# DataBinder.Eval(Container.DataItem, "StudentName")%>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateColumn>	
				    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Student Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "StdFirstName") %>&nbsp;<%# DataBinder.Eval(Container.DataItem, "StdLastName")%>--%>
                                            <%# ConvertToTitleCase(Eval("StdFirstName").ToString())%>&nbsp;<%# ConvertToTitleCase(Eval("StdLastName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                             Guardian Name 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<%# Eval("GuardianName")%>--%>
                                            <%# ConvertToTitleCase(Eval("GuardianName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Applicant Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
						                <%--<%# DataBinder.Eval(Container.DataItem, "ApplicantName")%>--%>
						                <%# ConvertToTitleCase(Eval("ApplicantName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>		
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Referred By
                                        </HeaderTemplate>
                                        <ItemTemplate>
						                    <%# ConvertToTitleCase(Eval("ReferredBy").ToString())%>
                                           <%--<%# DataBinder.Eval(Container.DataItem, "ReferredBy")%>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Category
                                        </HeaderTemplate>
                                        <ItemTemplate>
						
                                           <%# DataBinder.Eval(Container.DataItem, "ApplicationCategory")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Receiving Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "ReceivedOnDate1")%>  
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
                                        <asp:Button ID="btnDetail" runat="server" Width="100%" Text="Application Details" OnClick="btnDetailStudent_Click" />
                                    </td>
                                </tr>
                                <tr id="trEditStudent" runat="server">
                                    <td>
                                        <asp:Button ID="btnEditStudent" runat="server" Width="100%" Text="Edit Application" OnClick="btnEditStudent_Click" />
                                    </td>
                                </tr>
                                
                                <tr id="trDisconStd" runat="server">
                                    <td>
                                        <asp:Button ID="btnDisconStudent" runat="server" Width="100%" Text="Move to Students"  OnClientClick="return ValidateAction('move')"
                                                    OnClick="btnMoveApplication_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnDelStudent" runat="server" OnClientClick="return ValidateAction('delete')"
                                                    Width="100%" Text="Delete Application" OnClick="btnDelStudent_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
	        
                <script type="text/javascript" language="javascript">
                    SelectFirst('dgStudents');

                </script>
          
                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No New Application Found.</td>
                    </tr>
                </table>		
            </td>
        </tr> 
    </table>
</asp:Content>