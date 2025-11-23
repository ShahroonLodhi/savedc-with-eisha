<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="ListAttachments.aspx.cs" Inherits="SaveDC.ControlPanel.ListAttachments" Title="List Attachments"%>
<%@ Register Src="Controls/AdvDataPager.ascx" TagName="DataPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        function ValidateAction(action) {
            if (action == 'DELETE') {
                if (confirm("Are you sure, you want to delete this attachment?"))
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
                    var count = all[i].id.indexOf(gridName);
                    if (count != -1) {
                        all[i].checked = false;
                    }
                }
            }
            rdo.checked = true; /* Finally making the clicked radio button CHECKED */
            document.getElementById('<%= hdnReportID.ClientID %>').value = rdo.value;
            //alert(document.getElementById("ctl00_ContentPlaceHolder1_hdnReportID").value);
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

//     SelectFirst('dgReports');
</script>
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2"  class="CellLabel" >

                <table width="100%">
                    <tr>
                        <td width="80%">
                            Student Manager :: List Students :: Student Attachments
                           
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
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
	
        <!---<tr>
            <td colspan="1" class="CellLabel">
                <table id="Table1" runat="server" width="100%">
                    <form name="frmsearch" method="post" id="frmsearch">
                        <tr>
                            <td width="40%">
                                Report Year:
                            </td>
                            <td valign="middle">
                                <asp:TextBox CssClass="Textbox" ID="txtYearName" runat="server" />
                            </td>
                            <td width="70%" align="left">
                                <asp:ImageButton ID="searchbtn" runat="server" ImageUrl="search.png" Height="40"
                                                 Width="40" OnClick="searchbtn_Click" />
                            </td>
                        </tr>
                    </form>
                </table>
            </td>
        </tr>--->
        <tr id="trAddReport" runat="server">
            <td height="10px" colspan ="2" align="right">
                [ <a href="AddAttachment.aspx">Add Attachment</a> ]
            </td>
        </tr>
	
        <tr>
            <td class="CellHeading" width="60%">Manage Attachments for 
                <asp:Label ID="lblStdName" style="font-style: italic" runat="server" Text=""></asp:Label></td>
            <td class="ContextLinks" align="right">Total Attachments: 
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>&nbsp; <%--<a id="context" href= "ListReports.aspx">[ List Reports ]</a> <a id="context" href= "AddReport.aspx">[ Add Report ]</a>--%>
            </td>
        </tr>
        <tr> 
            <td class="CellLabel" colspan="2">
	  
                <asp:HiddenField runat="server" ID="hdnReportID" Value ="" />
                <table width="100%" border = 0 id="tbDataFound" runat="server">
                    <tr>
                        <td width="85%"  valign="top">
        	 
                            <asp:DataGrid ID="dgReports" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" OnItemDataBound="dgReports_Databound" >
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton onclick="SelectOne(this,'dgReports')" ID="rowbtn" name="rowbtn" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ReportId") %>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Attachments
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <a target="_blank" onmouseover="this.style.cursor='pointer'" style="background:none;border:0;color:#0000FF" onclick="window.open('../Uploads/Attachments/<%# DataBinder.Eval(Container.DataItem, "ReportGUID") %>.jpg?modalwin=1', 'View', 'left=100,top=30,screenX=100,screenY=30, height=550,width=840,toolbar=no,directories=no,status=no,menubar=no,modal=yes,scrollbars=yes');">View Attachment</a>
                                        </ItemTemplate>
                                        <ItemStyle Width="17%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Added By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "UserName") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="17%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Added On
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "AddedOn")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="17%" />
                                    </asp:TemplateColumn>	
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                           
                            <uc1:DataPager ID="pagerApps" runat="server" PageIndex="1" RecordsPerPage="100" TotalRecords="0" />
                        </td>
                        <td valign="top">
                            <table >
                                <tr><td >
                                        <asp:Button ID="btnDetail" runat="server"  Width="100%" Text="Attachment Details" OnClick="btnDetail_Click" />
                                    </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnEditReport" runat="server" Width="100%" Text="Edit Attachment" OnClick="btnEditReport_Click" />
                                     </td></tr>
                                <tr><td>
                                        <asp:Button ID="btnDelReport" runat="server"  Width="100%" Text="Delete Attachment"  OnClick="btnDelReport_Click" 
                                                    OnClientClick ="return ValidateAction('DELETE')"/></td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
	        
                <script type="text/javascript" language="javascript">
                    SelectFirst('dgReports');

                </script>
          
                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No Attachment Found.</td>
                    </tr>
                </table>		
            </td>
        </tr> 
    </table>
</asp:Content>