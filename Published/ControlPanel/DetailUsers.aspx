<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="DetailUsers.aspx.cs" Inherits="SaveDC.ControlPanel.DetailUsers" Title="User Details"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px"  class="CellLabel"  colspan="2">
               
               
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <%= hdnArea.Value%> :: <%= hdnRole.Value %> Details
                           
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
            <td class="CellHeading" width="40%">
                <%= hdnRole.Value %> Details
            </td>
            <td class="ContextLinks" align="right">
            </td>
        </tr>
	
        <tr id="loginRow1" runat="server" visible="true">
            <td colspan="2" class="SubHeading" style="height: 23px">Login Information</td>
        </tr>
        <asp:HiddenField ID="hdnArea" runat="server" />
        <asp:HiddenField ID="hdnRole" runat="server" />
        <tr id="loginRow2" runat="server" visible="true"> 
            <td class="CellLabel">Login/Display Name : </td>
            <td class="CellData">
                <asp:Label ID="txtUserName" runat="server" Text=""/>
            </td>
        </tr> 
	    <asp:HiddenField ID="hdnId" runat="server" />
	 
        <tr>
            <td colspan="2" class="SubHeading" style="height: 23px">Profile Information</td>
        </tr>
	
        <tr id="trRoles" runat="server"> 
            <td class="CellLabel">User Role : </td>
            <td class="CellData">  <asp:Label ID="txtUserRoles" runat="server" Text=""/>
            </td>
        </tr> 
        <tr> 
            <td class="CellLabel">First Name : </td>
            <td class="CellData"><asp:Label ID="txtFName" runat="server" Text=""/></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Last Name : </td>
            <td class="CellData"><asp:Label ID="txtLName" runat="server" Text=""/></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Email Adress : </td>
            <td class="CellData"><asp:Label ID="txtEmail" runat="server" Text=""/></td>
        </tr>
        <tr> 
            <td class="CellLabel">Cell # : </td>
            <td class="CellData"><asp:Label ID="txtPhone" runat="server" Text=""/></td>
        </tr> 
        <tr id="trGender" runat="server" visible="false"> 
            <td class="CellLabel">Gender : </td>
            <td class="CellData"><asp:Label ID="txtGender" runat="server" Text=""/></td>
        </tr>
        <tr id="trCNIC" runat="server" visible="false"> 
            <td class="CellLabel">CNIC # : </td>
            <td class="CellData"><asp:Label ID="txtCNIC" runat="server" Text=""/></td>
        </tr>
        <tr id="trRD" runat="server" visible="false"> 
            <td class="CellLabel">Receiving Date : </td>
            <td class="CellData"><asp:Label ID="txtReceivingDate" runat="server" Text=""/></td>
        </tr>
        <tr id="trOccupation" runat="server" visible="false"> 
            <td class="CellLabel">Occupation : </td>
            <td class="CellData"><asp:Label ID="txtOccupation" runat="server" Text=""/></td>
        </tr>
        <tr id="trQualification" runat="server" visible="false"> 
            <td class="CellLabel">Qualification : </td>
            <td class="CellData"><asp:Label ID="txtQualification" runat="server" Text=""/></td>
        </tr>
        <tr> 
            <td class="CellLabel">Address : </td>
            <td class="CellData"><asp:Label ID="lblAddress" runat="server" Text=""/></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Country : </td>
            <td class="CellData"><asp:Label ID="lblCountry" runat="server" Text=""/></td>
        </tr> 
	
        <tr> 
            <td class="CellLabel">Note : </td>
            <td class="CellData"><asp:Label ID="txtNote" runat="server" Text=""/>
            </td>
        </tr>  
        
        <tr>
            <td class="SubHeading" style="height: 23px">Remarks</td>
            <td align="right" ><a href="AddRemarksForDonor.aspx?MemberId=<%= hdnId.Value %>">Add Remarks</a></td>
        </tr>
	    <tr> 
            <td class="CellLabel" colspan="2">
                <table width="100%" border = 0 id="tbDataFound" runat="server">
                    <tr>
                        <td width="100%"  valign="top">
                            <asp:DataGrid ID="dgSchools" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" OnItemDataBound="dgSchools_Databound" >
                                <Columns>
                                   
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Remarks") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="60%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Posted By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PostedBy")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Posted On
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DatePosted")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateColumn>	
                                    
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                        </td>
                        
                    </tr>
                </table>
          
                <table id="tbNoDataFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No Remarks Found.</td>
                    </tr>
                </table>		
            </td>
        </tr>
        
        <tr id="loginRow5" runat="server" visible="true">
            <td class="SubHeading" style="height: 23px">Annual Fee</td>
            <td align="right" ><a href="AddFeeForMember.aspx?MemberId=<%= hdnId.Value %>">Add Annual Fee</a></td>
        </tr>
	    <tr id="loginRow6" runat="server" visible="true"> 
            <td class="CellLabel" colspan="2">
                <table width="100%" border = 0 id="tbFeeFound" runat="server">
                    <tr>
                        <td width="100%"  valign="top">
                            <asp:DataGrid ID="dgFees" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" OnItemDataBound="dgFees_Databound" >
                                <Columns>
                                   
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Fee Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            Rs. <%# DataBinder.Eval(Container.DataItem, "AnnualFee") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="60%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Posted By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PostedBy")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Posted for the Month
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DatePosted")).ToString("MMM yyyy")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateColumn>	
                                    
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                        </td>
                        
                    </tr>
                </table>
          
                <table id="tbNoFeeFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No Annual Fee Found.</td>
                    </tr>
                </table>		
            </td>
        </tr>
        
        <tr id="loginRow3" runat="server" visible="true">
            <td colspan="2" class="SubHeading" style="height: 23px">Notes</td>
        </tr>
	    <tr id="loginRow4" runat="server" visible="true"> 
            <td class="CellLabel" colspan="2">
                <table width="100%" border = 0 id="tbNotesFound" runat="server">
                    <tr>
                        <td width="100%"  valign="top">
                            <asp:DataGrid ID="dgNotes" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="StanderdGrid" OnItemDataBound="dgNotes_Databound" >
                                <Columns>
                                   
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Note
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Notes") %>
                                        </ItemTemplate>
                                        <ItemStyle Width="60%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Posted By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PostedBy")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateColumn>	
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Posted On
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DatePosted")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateColumn>	
                                    
                                </Columns>
                                <HeaderStyle BackColor="#6699CC" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                        </td>
                        
                    </tr>
                </table>
          
                <table id="tbNoNotesFound" runat="server" width="100%" visible="false">
                    <tr>
                        <td>No Notes Found.</td>
                    </tr>
                </table>		
            </td>
        </tr>
               
    </table>

</asp:Content>