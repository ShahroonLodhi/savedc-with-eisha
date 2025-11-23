<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="ListStudentEstimations.aspx.cs" Inherits="SaveDC.ControlPanel.ListStudentEstimations" Title="List Student Estimations"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        function ValidateAction(action) {
            if (action == 'DELETE') {
                if (confirm("Are you sure, you want to delete this estimation?"))
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
            document.getElementById('<%= hdnEstimationID.ClientID %>').value = rdo.value;
            //alert(document.getElementById("ctl00_ContentPlaceHolder1_hdnEstimationID").value);
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

//     SelectFirst('dgEstimations');
</script>
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="2" class="CellLabel" >
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Student Manager :: List Students :: Student Estimations

     	     
                           
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
	

        <tr>
            <td colspan="2" height="10px" align="right">
                [ <a href="AddStudentEstimation.aspx">Add Student Estimation</a> ]
            </td>
        </tr>
	
        <tr>
            <td class="CellHeading" width="60%">Manager School Estimations for 
                <asp:Label ID="lblStdName" style="font-style: italic"  runat="server" Text=""></asp:Label></td>
            <td class="ContextLinks" align="right">Total Estimations: 
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>&nbsp; <%--<a id="context" href= "ListEstimations.aspx">[ List Estimations ]</a> <a id="context" href= "AddEstimation.aspx">[ Add Estimation ]</a>--%>
            </td>
        </tr>
        <tr> 
            <td class="CellLabel" colspan="2">
                <asp:HiddenField ID="hdnSelectedSchool" runat="server" />
                <asp:HiddenField runat="server" ID="hdnEstimationID" Value ="" />
                <table width="100%" border = 0 id="tbDataFound" runat="server">
                    <tr>
                        <td width="85%" valign="top">
        	 
                            <asp:DataGrid ID="dgEstimations" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" OnItemDataBound="dgEstimations_Databound" >
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton onclick="SelectOne(this,'dgEstimations')" ID="rowbtn" name="rowbtn" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "EstimationId") %>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateColumn>	
				    
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            School Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnCurSchoolId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SchoolId") %>'/>
                                            <%# DataBinder.Eval(Container.DataItem, "SchoolName") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Contact Number
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PhoneNum") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="17%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Total Estimated Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "TotalEstimatedAmount") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="17%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Total Actual Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "TotalActualAmount") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateColumn>				
				   
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
          
                        </td>
                        <td valign="top">
                            <table >
                                <tr><td >
                                        <asp:Button ID="btnDetail" runat="server"  Width="100%" Text="Estimation Details" OnClick="btnDetail_Click" />
                                    </td></tr>
                                <tr ><td>
                                         <asp:Button ID="btnEditEstimation" runat="server" Width="100%" Text="Edit Estimation" OnClick="btnEditEstimation_Click" />
                                     </td></tr>
                                <tr><td >
                                        <asp:Button ID="btnAssignSchool" runat="server"  Width="100%" Text="Assign School" OnClick="btnAssignSchool_Click" />
                                    </td></tr>
                                <tr><td>
                                        <asp:Button ID="btnDelEstimation" runat="server"  Width="100%" 
                                                    Text="Delete Estimation" OnClientClick ="return ValidateAction('DELETE')" 
                                                    onclick="btnDelEstimation_Click" /></td></tr>
                  
                            </table>
                        </td>
                    </tr>
                </table>
	        
                <script type="text/javascript" language="javascript">
                    SelectFirst('dgEstimations');

                </script>
          
                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No Estimation Found.</td>
                    </tr>
                </table>		
            </td>
        </tr> 
    </table>
</asp:Content>