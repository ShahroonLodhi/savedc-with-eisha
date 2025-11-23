<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" CodeBehind="DetailsStudent.aspx.cs"
         Inherits="SaveDC.ControlPanel.DetailsStudent" Title="Student Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        function ShowHide(preFix) {

            var rows = document.getElementsByTagName("tr");
            for (var i = 1; i < rows.length; i++) {

                if (rows[i].id.indexOf(preFix + '_') > -1) {
                    if (rows[i].style.display == "none")
                        rows[i].style.display = "";
                    else
                        rows[i].style.display = "none";
                }
            }

        }

        function PrintForm() {
            var studentId = "<%= hdnEditStudentId.Value %>";
            //winname = window.open('PrintDetailsStudent.aspx?StudentId=' + studentId, 'StudentDetails', 'left=100,top=30,screenX=100,screenY=30, height=550,width=900,toolbar=no,directories=no,status=no,menubar=yes,modal=no,scrollbars=yes');

            winname = window.open('PrintDetailsStudent.aspx?StudentId=' + studentId, "1328942502c");

            winname.print();


        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" colspan="3" class="CellLabel">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Student Manager :: Student Details
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
            <td colspan="3">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <asp:PlaceHolder ID="panPrintable" runat="server">
            <tr>
                <asp:HiddenField ID="hdnEditStudentId" Value="0" runat="server" />
                <td class="CellHeading" width="40%">
                    Student Details
                </td>
                <td class="ContextLinks" colspan="3" align="right">
                    <asp:ImageButton ID="ImageButton2" AlternateText="Print" ToolTip="Print" runat="server"
                                     SkinID="sknImgPrint" Height="24" Width="24" OnClientClick="PrintForm();" />
                </td>
            </tr>
            <tr>
                <td colspan="3" class="SubHeading" style="height: 23px">
                    Student Profile
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    First Name :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtStudentFName" runat="server" />
                </td>
                <td rowspan="5" width="190px">
                    <table cellpadding='0' cellspacing='0' border="0" width='100%' style='margin-bottom: 10px;'>
                        <tr>
                            <td class='profile_photo' width='162'>
                                <img class='photo' alt="" height="160" runat="server" width="150" id="imgUpload"
                                     src='../images/nophoto.gif' border='0' />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Last Name :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtStudentLName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Date of Birth :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtDOB" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Educational Level :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtEduLevel" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Note :
                </td>
                <td class="CellData">
                    <asp:Label ID="txtNote" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" class="SubHeading" style="height: 23px">
                    Student History
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Is Studying Currently? :
                </td>
                <td class="CellData" colspan="2">
                    <asp:Label ID="txtIsDoingStudy" runat="server" />
                </td>
            </tr>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <tr id="classIn" runat="server">
                    <td class="CellLabel">
                        Class Doing Study In :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtClassDoingStudyIn" runat="server" />
                    </td>
                </tr>
                <tr id="classLeft1" runat="server">
                    <td class="CellLabel">
                        Class Left In :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtClassLeftIn" runat="server" />
                    </td>
                </tr>
                <tr id="classLeft2" runat="server">
                    <td class="CellLabel">
                        Period Left Since (<i>months</i>):
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtPeriodLeftSince" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="CellLabel">
                    Last School Attended :
                </td>
                <td class="CellData" colspan="2">
                    <asp:Label ID="txtLastSchoolAttended" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="CellLabel">
                    Note :
                </td>
                <td class="CellData" colspan="2">
                    <asp:Label ID="txtHNote" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="CellHeading" width="40%">
                    Family Details
                </td>
                <td class="ContextLinks" colspan="2" align="right">
                    [ <a id="context" href="javascript:return false;" onclick=" ShowHide('family'); ">Show/Hide</a>
                    ]
                </td>
            </tr>
            <asp:PlaceHolder ID="PlaceHolderFamilyNorec" runat="server">
                <tr id="family_11211" style="display: none">
                    <td class="CellLabel" colspan="3">
                        No family detail found.
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PlaceHolderFamily" runat="server">
                <tr id="family_111" style="display: none">
                    <td colspan="3" class="SubHeading" style="height: 23px">
                        Family Information
                    </td>
                </tr>
                <tr id="family_122" style="display: none">
                    <td class="CellLabel">
                        Family Name :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtDisplayName" runat="server" />
                    </td>
                </tr>
                <tr id="family_133" style="display: none">
                    <td colspan="3" class="SubHeading" style="height: 23px">
                        Parents Information
                    </td>
                </tr>
                <tr id="family_1" style="display: none">
                    <td class="CellLabel">
                        Father Name :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtFatherName" runat="server" />
                    </td>
                </tr>
                <tr id="family_2222" style="display: none">
                    <td class="CellLabel">
                        Mother Name :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtMotherName" runat="server" />
                    </td>
                </tr>
                <tr id="family_2" style="display: none">
                    <td class="CellLabel">
                        Is Father Alive? :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtIsFatherAlive" runat="server" />
                    </td>
                </tr>
                <tr id="family_4" runat="server" style="display: none">
                    <td class="CellLabel">
                        Father's Age :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtFatherAge" runat="server" />
                    </td>
                </tr>
                <tr id="family_3" style="display: none">
                    <td class="CellLabel">
                        Is Mother Alive? :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtIsMotherAlive" runat="server" />
                    </td>
                </tr>
                <tr id="family_5" runat="server" style="display: none">
                    <td class="CellLabel">
                        Mother's Age :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtMotherAge" runat="server" />
                    </td>
                </tr>
                <tr id="family_6" style="display: none">
                    <td class="CellLabel">
                        Father's Occupation :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtFatherOccupation" runat="server" />
                    </td>
                </tr>
                <tr id="family_7" style="display: none">
                    <td class="CellLabel">
                        Mother's Occupation :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtMotherOccupation" runat="server" />
                    </td>
                </tr>
                <tr id="family_8" style="display: none">
                    <td class="CellLabel">
                        Father's CNIC :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtFatherCNIC" runat="server" />
                    </td>
                </tr>
                <tr id="family_9" style="display: none">
                    <td class="CellLabel">
                        Mother's CNIC :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtMotherCNIC" runat="server" />
                    </td>
                </tr>
                <tr id="family_109" style="display: none">
                    <td colspan="3" class="SubHeading" style="height: 23px">
                        Parents Relation
                        <%--<span style="padding-left: 3%;">[ <a href="javascript:return false;"  onclick="ShowHide('relation');">
                    Show/Hide</a> ]</span>--%>
                    </td>
                </tr>
                <tr id="family_10" style="display: none">
                    <td class="CellLabel">
                        Is Parents Divorced/Separated? :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtIsParentsDivorced" runat="server" />
                    </td>
                </tr>
                <tr id="family_11" runat="server" style="display: none">
                    <td class="CellLabel">
                        Divorced Period :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtDivorcedPeriod" runat="server" />
                    </td>
                </tr>
                <tr id="family_12" runat="server" style="display: none">
                    <td class="CellLabel">
                        Who is Guardian now? :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtQardian" runat="server" />
                    </td>
                </tr>
                <tr id="family_13" runat="server" style="display: none">
                    <td class="CellLabel">
                        Address of Father/Mother not leaving with :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtDAddress" runat="server" />
                    </td>
                </tr>
                <tr id="family_123" style="display: none">
                    <td colspan="3" class="SubHeading" style="height: 23px">
                        Family Members Details
                        <%--<span style="padding-left: 3%;">[ <a href="javascript:return false;"  onclick="ShowHide('fmember');">
                    Show/Hide</a> ]</span>--%>
                    </td>
                </tr>
                <tr id="family_14" style="display: none">
                    <td class="CellLabel">
                        Male Members :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtMaleMembers" runat="server" />
                    </td>
                </tr>
                <tr id="family_15" style="display: none">
                    <td class="CellLabel">
                        Female Members :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtFemleMembers" runat="server" />
                    </td>
                </tr>
                <tr id="family_16" style="display: none">
                    <td class="CellLabel">
                        Other Details :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtMemberDetails" runat="server" />
                    </td>
                </tr>
                <tr id="family_173" style="display: none">
                    <td colspan="3" class="SubHeading" style="height: 23px">
                        Property/Financial Details
                        <%--<span style="padding-left: 3%;">[ <a href="javascript:return false;"  onclick="ShowHide('financial');">
                    Show/Hide</a> ]</span>--%>
                    </td>
                </tr>
                <tr id="family_17" style="display: none">
                    <td class="CellLabel">
                        Is Living in Own House? :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtIsOwnHouse" runat="server" />
                    </td>
                </tr>
                <tr id="family_18" style="display: none">
                    <td class="CellLabel">
                        House Area (<i>marla</i>) :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtHouseArea" runat="server" />
                    </td>
                </tr>
                <tr id="family_19" style="display: none">
                    <td class="CellLabel">
                        Rooms in House :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtRooms" runat="server" />
                    </td>
                </tr>
                <tr id="family_20" style="display: none">
                    <td class="CellLabel">
                        Living Period :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtLivingPeriod" runat="server" />
                    </td>
                </tr>
                <tr id="family_21" style="display: none">
                    <td class="CellLabel">
                        Monthly Income (<i>PKR</i>) :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtIncome" runat="server" />
                    </td>
                </tr>
                <tr id="family_1123" style="display: none">
                    <td colspan="3" class="SubHeading" style="height: 23px">
                        Family Contact Details
                        <%--<span style="padding-left: 3%;">[ <a href="javascript:return false;"  onclick="ShowHide('contact');">
                    Show/Hide</a> ]</span>--%>
                    </td>
                </tr>
                <tr id="family_22" style="display: none">
                    <td class="CellLabel">
                        Permanent Address :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtPermanentAddress" runat="server" />
                    </td>
                </tr>
                <tr id="family_23" style="display: none">
                    <td class="CellLabel">
                        Current Address :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtCurrentAddress" runat="server" />
                    </td>
                </tr>
                <tr id="family_24" style="display: none">
                    <td class="CellLabel">
                        Landline Number :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtLandline" runat="server" />
                    </td>
                </tr>
                <tr id="family_25" style="display: none">
                    <td class="CellLabel">
                        Cell Number :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtCell" runat="server" />
                    </td>
                </tr>
                <tr id="family_11213" style="display: none">
                    <td colspan="3" class="SubHeading" style="height: 23px">
                        Other Family Details
                        <%--<span style="padding-left: 3%;">[ <a href="javascript:return false;" onclick="ShowHide('family_');">
                    Show/Hide</a> ]</span>--%>
                    </td>
                </tr>
                <tr id="family__27" style="display: none">
                    <td class="CellLabel">
                        Note :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtFamilyNote" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="CellHeading" width="40%">
                    Verification Details
                </td>
                <td class="ContextLinks" colspan="2" align="right">
                    [ <a id="context" href="javascript:return false;" onclick=" ShowHide('varification'); ">
                          Show/Hide</a> ]
                </td>
            </tr>
            <asp:PlaceHolder ID="PlaceHolderVarificationNorec" runat="server">
                <tr id="varification_32" style="display: none">
                    <td class="CellLabel" colspan="3">
                        No verification detail found.
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PlaceHolderVarification" runat="server">
                <tr id="varification_29" style="display: none">
                    <td class="CellLabel">
                        Verified By :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtVarifiedBy" runat="server" />
                    </td>
                </tr>
                <tr id="varification_28" style="display: none">
                    <td class="CellLabel">
                        Is Information Authentic? :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtIsVarified" runat="server" />
                    </td>
                </tr>
                <tr id="varification_1" style="display: none">
                    <td class="CellLabel">
                        Verification Date :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtVDate" runat="server" />
                    </td>
                </tr>
                <tr id="varification_2" style="display: none">
                    <td class="CellLabel">
                        Remarks :
                    </td>
                    <td class="CellData" colspan="2">
                        <asp:Label ID="txtVRemarks" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="CellHeading" width="40%">
                    Approval Details
                </td>
                <td class="ContextLinks" colspan="2" align="right">
                    <%-- [ <a id="context" href="javascript:return false;" onclick="ShowHide('approv');">Show/Hide</a>
                ]--%>
                </td>
            </tr>
            <%--<asp:PlaceHolder ID="PlaceHolderApprovNorec" runat="server">
            <tr id="Tr1" style="display: none">
                <td class="CellLabel" colspan="2">
                    No approval detail found.
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PlaceHolderApprov" runat="server">--%>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <tr>
                        <td class="SubHeading" style="height: 20px">
                            Admin Name
                        </td>
                        <td class="SubHeading" colspan="2" style="height: 20px">
                            Remarks
                        </td>
                        <%--<td class="SubHeading" style="height: 20px">
                            Signatures
                        </td>--%>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="CellLabel">
                            <%# DataBinder.Eval(Container.DataItem, "ApproverName") %>
                            (<i><%# DataBinder.Eval(Container.DataItem, "Status") %>
                                 on
                                 <%# DataBinder.Eval(Container.DataItem, "ApprovedDateShortString") %></i>)
                        </td>
                        <td class="CellData" colspan="2">
                            <%# DataBinder.Eval(Container.DataItem, "Remarks") %>
                        </td>
                        <%-- <td class="CellData">
                            __________________________
                        </td>--%>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </asp:PlaceHolder>
        <tr id="trApprovalRemarks" runat="server">
            <td class="CellLabel">
                Your Approval Remarks:
            </td>
            <td class="CellData" colspan="2">
                <asp:TextBox CssClass="Textbox" ID="txtApprovRemarks" runat="server" Columns="50"
                             MaxLength="1024" Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        <%--</asp:PlaceHolder>--%>
        <tr id="trBtns" runat="server">
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="ButtonSpacer">
                <asp:ImageButton runat="server" ID="btnApprove" SkinID="sknImgBtnApprove" OnClick="btnApprove_Click" />
                <asp:ImageButton runat="server" ID="btnReject" SkinID="sknImgBtnReject" OnClick="btnReject_Click" />
            </td>
        </tr>
    </table>
</asp:Content>