<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/Dummy.Master" AutoEventWireup="true"  CodeBehind="PrintDetailsMonthlyExpense.aspx.cs" Inherits="SaveDC.ControlPanel.PrintDetailsMonthlyExpense" Title="Print Expense Details"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        function roundNumber(num, dec) {
            var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
            return result;
        }

        function CalcSum(stdId) {

            var stdSum = 0;
            var inputElem = document.getElementsByTagName("span");
            if (inputElem) {
                for (var i = 0; i < inputElem.length; i++) {
                    //  alert(inputElem[i].name);
                    if (inputElem[i] && inputElem[i].id.indexOf("_txtAmount") > -1) {
                        if (isNaN(inputElem[i].innerHTML) || parseFloat(inputElem[i].innerHTML) < 0) {
                            //alert("Please enter a valid non-negative value for amount.");
                            return false;
                        }

                        stdSum += parseFloat(inputElem[i].innerHTML);
                    }
                }
            }

            document.getElementById("<%= txtGTotal.ClientID %>").innerHTML = roundNumber(stdSum, 2);
//        var inputTotalElem = document.getElementsByTagName("span");
//        
//         if (inputTotalElem) {
//             for (var i = 0; i < inputTotalElem.length; i++) {
//                 alert(stdId);
//                 if (inputTotalElem[i] && inputTotalElem[i].name.indexOf("txtTotal_" + stdId)) {
//                     inputTotalElem[i].innerHTML = stdSum;
//                     alert("hi");
//                  } 
//             } 
//         }

        }
    </script>

    <table width="75%" border="0" cellpadding="2" cellspacing="2" >
        <tr>
            <td height="20px" id="imageTop" colspan="<%= hdnColSpan.Value %>" class="CellLabel">
                <table border="0" width="100%">
                    <tr>
                        <tr colspan="2">
                            <td height="80" bgcolor="ffffff" align="left">
                                <img width="600" height="63" src="../images/savedc-address.png" />
                            </td>
                        </tr>
                    </tr>
                </table>
            </td>
        </tr>
    
    
    
        <tr>
            <td height="20px">
            </td>
        </tr>
        <tr>
            <td colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
       
        <tr>
            <asp:HiddenField ID="hdnColSpan" runat="server" />
           
            <asp:HiddenField ID="hdnExpenseID" Value ="0" runat="server" />
            <td class="CellHeading" width="25%" colspan="2">Expense Details </td>
            <td class="ContextLinks" align="right"  colspan="<%= hdnColSpan.Value %>"><asp:ImageButton ID="ImageButton2" AlternateText="Print" ToolTip="Click to print and once done; close the form." runat="server"
                                                                                                       SkinID="sknImgPrint" Height="24" Width="24" OnClientClick="javascript:window.print();"/></td>
        </tr>
	
        <tr>
            <td class="CellLabel" colspan="2">
                File #  :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="lblFileNum" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                Voucher # :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="lblVoucherNum" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                For Month :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
            </td>
        </tr>
	
        <tr>
            <td class="CellLabel" colspan="2">
                Expense Category :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="lblExpenseType" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                Posted On :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="lblPostedOn" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <%--<tr>
            <td class="CellLabel" colspan="2">
                Expense Posted By :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="lblExpensePostedBy" runat="server" Text=""></asp:Label>
            </td>
        </tr>--%>
        <asp:Panel ID="PlaceHolderSchool" runat="server">
            <tr>
                <td class="CellLabel" colspan="2">
                    School Name :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <asp:Label ID="lblSchoolName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SubHeading" style="height: 23px; width: 25px">
                    Name
                </td>
                <td class="SubHeading" style="height: 23px; width: 25px">
                    Class
                </td>
                <asp:Repeater ID="Repeater3" runat="server">
                    <ItemTemplate>
                        <td class="SubHeading" style="height: 23px; width: 25px">
                            <%# DataBinder.Eval(Container.DataItem, "ExpenseName") %>
                        </td>
                    </ItemTemplate>
                </asp:Repeater>
                <%-- <td class="SubHeading" style="height: 23px">
           Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           </td> --%>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td class="CellLabel" style="white-space: nowrap">
                            <asp:HiddenField ID="hdnStudentId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "StudentId") %>' />
                            <%# DataBinder.Eval(Container.DataItem, "FirstName") %>
                            <%# DataBinder.Eval(Container.DataItem, "LastName") %>
                        </td>
                        <td class="CellLabel">
                            <%# DataBinder.Eval(Container.DataItem, "ClassName") %>
                        </td>
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <ItemTemplate>
                                <td class="CellData" style="width: 25px">
                                    <asp:HiddenField ID="StudentId1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "StudentId") %>' />
                                    <asp:HiddenField ID="hdnExpenseId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ExpenseId") %>' />
                                    <asp:Label ID='txtAmount' Width="30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%-- <td class="CellLabel">
                           <asp:Label ID="txtTotal"  runat="server" Text="0.00"></asp:Label>
                      </td>--%>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <tr>
                <td class="CellLabel" colspan="2">
                    Grand Total :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <b>
                        <asp:Label ID="txtGTotal" runat="server" Text=""></asp:Label></b>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="PlaceHolderOther" runat="server">
            <tr>
                <td class="CellLabel" colspan="2">
                    Expense Payee :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <asp:Label ID="lblPayee" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="CellLabel" colspan="2">
                    Total Amount :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="CellLabel" colspan="2">
                    Details :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </asp:Panel>
       
        <tr>
            <td class="SubHeading" id="tdpayment" colspan="<%= hdnColSpan.Value %>" style="height: 23px">
                Payment Details
            </td>
        </tr>
        <tr>
            <td class="CellLabel" colspan="2">
                Payment Mode :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="txtPaymentMode" runat="server" Text="" />
            </td>
        </tr>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <tr id="trCheque1">
                <td class="CellLabel" colspan="2">
                    Through Bank :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <asp:Label ID="txtBank" runat="server" Text="" />
                </td>
            </tr>
            <tr id="trCheque2">
                <td class="CellLabel" colspan="2">
                    Beneficiary Name :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <asp:Label ID="txtAccNum" runat="server" Text="" />
                </td>
            </tr>
            <tr id="trCheque3">
                <td class="CellLabel" colspan="2">
                    Cheque # :
                </td>
                <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                    <asp:Label ID="txtChkNum" runat="server" Text="" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="CellLabel" colspan="2">
                Note :
            </td>
            <td class="CellData" colspan="<%= hdnColSpan.Value %>">
                <asp:Label ID="txtNote" runat="server" Text="" />
            </td>
        </tr>

        <script language="javascript" type="text/javascript">
            if ("<%= hdnColSpan.Value %>" != "1") {
                CalcSum(0);
                document.getElementById("tdpayment").colSpan = document.getElementById("tdpayment").colSpan + 1;
                document.getElementById("imageTop").colSpan = document.getElementById("imageTop").colSpan + 1;
            } else {
                document.getElementById("tdpayment").colSpan = document.getElementById("tdpayment").colSpan + 2;
                document.getElementById("imageTop").colSpan = document.getElementById("imageTop").colSpan + 2;

            }

        </script>

        <tr>
            <td colspan="2">
                &nbsp;
            </td>
            <td colspan="<%= hdnColSpan.Value %>" align="right">
                <br /><br />
                <u style="text-decoration: overline;"><font color="gray" size="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;payee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font></u>&nbsp;
                <u style="text-decoration: overline;"><font color="gray" size="1">&nbsp;expense prepared by&nbsp;</font></u>&nbsp;
                <%--<br /><br />--%>
                <u style="text-decoration: overline;"><font color="gray" size="1">&nbsp;expense approved by&nbsp;</font></u>&nbsp;
                <%--<br /><br />--%>              
                <u style="text-decoration: overline;" id="lblPrincipalSign" runat="server"><font color="gray" size="1">&nbsp;principal sign/stamp&nbsp;</font></u>
            </td>
        </tr>

    </table>
</asp:Content>