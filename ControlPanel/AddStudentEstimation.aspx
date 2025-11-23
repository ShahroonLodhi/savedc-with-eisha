<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"   EnableEventValidation="False" CodeBehind="AddStudentEstimation.aspx.cs" Inherits="SaveDC.ControlPanel.AddStudentEstimation" Title="Add Student Estimation"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        function ShowModel() {

            window.open('AddSchool.aspx?modalwin=1', 'AddSchool', 'left=100,top=30,screenX=100,screenY=30, height=550,width=840,toolbar=no,directories=no,status=no,menubar=no,modal=yes,scrollbars=no');
        }

        function AddListItem(text, val) {
            var familyCombo = document.getElementById('<%= ddSchools.ClientID %>');
            // remove the No family item if exists.
            var selectedItem = familyCombo.options[familyCombo.selectedIndex];


            if (selectedItem.value == "0") {
                alert(selectedItem.value);
                familyCombo.remove(0);
            }
            // add new item.
            var option = document.createElement("option");
            option.text = text;
            option.value = val;
            try {
                familyCombo.add(option, null); //Standard
            } catch(error) {
                familyCombo.add(option); // IE only
            }

            // reset the childs detail to 0.

            familyCombo.selectedIndex = familyCombo.options.length - 1;

        }

    </script>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td height="20px" class="CellLabel"  colspan="3">
                <asp:HiddenField ID="hdnAddEdit" value ="Add" runat="server" />
	
	
                <table width="100%">
                    <tr>
                        <td width="80%">
                            Student Manager :: List Students :: Student Estimations :: <%= hdnAddEdit.Value %> Estimation
                           
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
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
       
        <tr>
            <asp:HiddenField ID="hdnEditExtimationId" Value ="0" runat="server" />
            <td class="CellHeading" width="40%"><%= hdnAddEdit.Value %> School Estimation for <asp:Label ID="lblStdName" style="font-style: italic"  runat="server" Text=""></asp:Label></td>
            <td class="ContextLinks" colspan="2" align="right"></td>
        </tr>
	
        <%-- <tr>
           <td class="CellLabel">
               Student Name :
           </td>
           <td class="CellData" colspan="2">
               <asp:Label ID="lblStdName" runat="server" Text=""></asp:Label>
           </td>
       </tr>--%>
        <tr>
            <td class="CellLabel">
                School Name :
            </td>
            <td class="CellData" colspan="2">
                <asp:DropDownList ID="ddSchools" runat="server"/>&nbsp;&nbsp;<a href="###" onclick=" ShowModel() ">Add New?</a>
            </td>
        </tr>
    
	
	
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <tr>
                    <td class="SubHeading" style="height: 23px">
                        Estimation Category
                    </td>
                    <td class="SubHeading" style="height: 23px">
                        Estimated Amount
                    </td>
                    <td class="SubHeading" style="height: 23px">
                        Actual Amount
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="CellLabel">
                        <asp:HiddenField ID="hdnEstimationCateID" runat="server" Value = '<%# DataBinder.Eval(Container.DataItem, "ExpenseId") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "ExpenseName") %>  :
                    </td>
                    <td class="CellData">
                        <asp:TextBox CssClass="Textbox" ID='txtEstimation' runat="server" Columns="50" Text ='<%# DataBinder.Eval(Container.DataItem, "EstimatedAmount") %>' />
                    </td>
                    <td class="CellData">
                        <asp:TextBox CssClass="Textbox" ID='txtActual' runat="server" Columns="50" Text ='<%# DataBinder.Eval(Container.DataItem, "ActualAmount") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
	      
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="ButtonSpacer">
                <asp:ImageButton runat="server" ID="btnUpdate" SkinID="sknImgBtnAddUser" OnClick="btnUpdate_Click" />
            </td>
        </tr>
               
    </table>

</asp:Content>