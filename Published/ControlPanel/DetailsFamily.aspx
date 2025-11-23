<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="DetailsFamily.aspx.cs" Inherits="SaveDC.ControlPanel.DetailsFamily" Title="Family Details"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr id="trBredCrum" runat="server">
            <td height="20px" colspan="2" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Family Manager :: Family Details
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
            <asp:HiddenField ID="hdnEditFamilyId" Value="0" runat="server" />
            <td class="CellHeading" width="40%">
                Family Details
            </td>
            <td class="ContextLinks" align="right">
                <%--<a id="context" href="#">[ Student Profile ]</a> <a id="context" href="#">[ Student Family ]</a>--%>
            </td>
        </tr>
        
        <tr>
            <td colspan="2" class="SubHeading" style="height: 23px">
                Family Information
            </td>
        </tr>
        <tr>
            <td class="CellLabel">
                Family Name :
            </td>
            <td class="CellData">
                <asp:Label ID="txtDisplayName" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="SubHeading" style="height: 23px">
                Parents Information
            </td>
        </tr>
        <tr id="family_1"  >
            <td class="CellLabel">
                Father Name :
            </td>
            <td class="CellData">
                <asp:Label ID="txtFatherName" runat="server" />
            </td>
        </tr>
        <tr id="family_2222"  >
            <td class="CellLabel">
                Mother Name :
            </td>
            <td class="CellData">
                <asp:Label ID="txtMotherName" runat="server" />
            </td>
        </tr>
        <tr id="family_2"  >
            <td class="CellLabel">
                Is Father Alive? :
            </td>
            <td class="CellData">
                <asp:Label ID="txtIsFatherAlive" runat="server" />
            </td>
        </tr>
        <tr id="family_4" runat="server" >
            <td class="CellLabel">
                Father's Age :
            </td>
            <td class="CellData">
                <asp:Label ID="txtFatherAge" runat="server" />
            </td>
        </tr>
        <tr id="family_3"  >
            <td class="CellLabel">
                Is Mother Alive? :
            </td>
            <td class="CellData">
                <asp:Label ID="txtIsMotherAlive" runat="server" />
            </td>
        </tr>
    
        <tr id="family_5"  runat="server" >
            <td class="CellLabel">
                Mother's Age :
            </td>
            <td class="CellData">
                <asp:Label ID="txtMotherAge" runat="server" />
            </td>
        </tr>
        <tr id="family_6"  >
            <td class="CellLabel">
                Father's Occupation :
            </td>
            <td class="CellData">
                <asp:Label ID="txtFatherOccupation" runat="server" />
            </td>
        </tr>
        <tr id="family_7"  >
            <td class="CellLabel">
                Mother's Occupation :
            </td>
            <td class="CellData">
                <asp:Label ID="txtMotherOccupation" runat="server" />
            </td>
        </tr>
        <tr id="family_8"  >
            <td class="CellLabel">
                Father's CNIC :
            </td>
            <td class="CellData">
                <asp:Label ID="txtFatherCNIC" runat="server" />
            </td>
        </tr>
        <tr id="family_9"  >
            <td class="CellLabel">
                Mother's CNIC :
            </td>
            <td class="CellData">
                <asp:Label ID="txtMotherCNIC" runat="server" />
            </td>
        </tr>
        <tr id="family_109"  >
            <td colspan="2" class="SubHeading" style="height: 23px">
                Parents Relation
            </td>
        </tr>
        <tr id="family_10"  >
            <td class="CellLabel">
                Is Parents Divorced/Separated? :
            </td>
            <td class="CellData">
                <asp:Label ID="txtIsParentsDivorced" runat="server" />
            </td>
        </tr>
        <tr id="family_11"  runat="server" >
            <td class="CellLabel">
                Divorced Period :
            </td>
            <td class="CellData">
                <asp:Label ID="txtDivorcedPeriod" runat="server" />
            </td>
        </tr>
        <tr id="family_12"  runat="server" >
            <td class="CellLabel">
                Who is Guardian now? :
            </td>
            <td class="CellData">
                <asp:Label ID="txtQardian" runat="server" />
            </td>
        </tr>
        <tr id="family_13"  runat="server" >
            <td class="CellLabel">
                Address of Father/Mother not leaving with :
            </td>
            <td class="CellData">
                <asp:Label ID="txtDAddress" runat="server" />
            </td>
        </tr>
        <tr id="family_123"  >
            <td colspan="2" class="SubHeading" style="height: 23px">
                Family Members Details
            </td>
        </tr>
        <tr id="family_14"  >
            <td class="CellLabel">
                Male Members :
            </td>
            <td class="CellData">
                <asp:Label ID="txtMaleMembers" runat="server" />
            </td>
        </tr>
        <tr id="family_15"  >
            <td class="CellLabel">
                Female Members :
            </td>
            <td class="CellData">
                <asp:Label ID="txtFemleMembers" runat="server" />
            </td>
        </tr>
        <tr id="family_16"  >
            <td class="CellLabel">
                Other Details :
            </td>
            <td class="CellData">
                <asp:Label ID="txtMemberDetails" runat="server" />
            </td>
        </tr>
        <tr id="family_173"  >
            <td colspan="2" class="SubHeading" style="height: 23px">
                Property/Financial Details
            </td>
        </tr>
        <tr id="family_17"  >
            <td class="CellLabel">
                Is Living in Own House? :
            </td>
            <td class="CellData">
                <asp:Label ID="txtIsOwnHouse" runat="server" />
            </td>
        </tr>
        <tr id="family_18"  >
            <td class="CellLabel">
                House Area (<i>square feet</i>) :
            </td>
            <td class="CellData">
                <asp:Label ID="txtHouseArea" runat="server" />
            </td>
        </tr>
        <tr id="family_19"  >
            <td class="CellLabel">
                Rooms in House :
            </td>
            <td class="CellData">
                <asp:Label ID="txtRooms" runat="server" />
            </td>
        </tr>
        <tr id="family_20"  >
            <td class="CellLabel">
                Living Period :
            </td>
            <td class="CellData">
                <asp:Label ID="txtLivingPeriod" runat="server" />
            </td>
        </tr>
        <tr id="family_21"  >
            <td class="CellLabel">
                Monthly Income (<i>PKR</i>) :
            </td>
            <td class="CellData">
                <asp:Label ID="txtIncome" runat="server" />
            </td>
        </tr>
        
        <tr>
            <td colspan="2" class="SubHeading" style="height: 23px">
                Utility Bills Details
            </td>
        </tr>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <tr>
                <td class="CellLabel">
                    Bill I :
                </td>
                <td class="CellData">
                    <a href="javascript:void(0)" runat ="server" id="Viewbill1">View Bill</a>
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Bill II :
                </td>
                <td class="CellData">
                    <a href="javascript:void(0)"  runat ="server" id="Viewbill2">View Bill</a>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="SubHeading" style="height: 23px">
                Family Certificates
            </td>
        </tr>
        <asp:PlaceHolder ID="PlaceHolder2" runat="server">
            <tr>
                <td class="CellLabel">
                    Death Certificate :
                </td>
                <td class="CellData">
                    <a href="javascript:void(0)" runat ="server"  id="viewCert1">View Certificate</a>
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Divorce Certificate :
                </td>
                <td class="CellData">
                    <a href="javascript:void(0)"  runat ="server"  id="viewCert2">View Certificate</a>
                </td>
            </tr>
        </asp:PlaceHolder>
        
        
        <tr id="family_1123"  >
            <td colspan="2" class="SubHeading" style="height: 23px">
                Contact Details
                <%--<span style="padding-left: 3%;">[ <a href="javascript:return false;"  onclick="ShowHide('contact');">
                    Show/Hide</a> ]</span>--%>
            </td>
        </tr>
        <tr id="family_22"  >
            <td class="CellLabel">
                Permanent Address :
            </td>
            <td class="CellData">
                <asp:Label ID="txtPermanentAddress" runat="server" />
            </td>
        </tr>
        <tr id="family_23"  >
            <td class="CellLabel">
                Current Address :
            </td>
            <td class="CellData">
                <asp:Label ID="txtCurrentAddress" runat="server" />
            </td>
        </tr>
        <tr id="family_24"  >
            <td class="CellLabel">
                Landline Number :
            </td>
            <td class="CellData">
                <asp:Label ID="txtLandline" runat="server" />
            </td>
        </tr>
        <tr id="family_25"  >
            <td class="CellLabel">
                Cell Number :
            </td>
            <td class="CellData">
                <asp:Label ID="txtCell" runat="server" />
            </td>
        </tr>
        <tr id="family_11213"  >
            <td colspan="2" class="SubHeading" style="height: 23px">
                Other Details
            </td>
        </tr>
        <tr id="family__27"  >
            <td class="CellLabel">
                Note :
            </td>
            <td class="CellData">
                <asp:Label ID="txtFamilyNote" runat="server" />
            </td>
        </tr>
    </table>

</asp:Content>