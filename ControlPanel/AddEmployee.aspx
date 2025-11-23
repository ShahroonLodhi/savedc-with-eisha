<%@ Page Language="C#" Theme="Default" MasterPageFile="~/ControlPanel/CPMaster.Master" AutoEventWireup="true"  CodeBehind="AddEmployee.aspx.cs" Inherits="SaveDC.ControlPanel.AddEmployee" Title="Add Employee"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link type="text/css" href="../js/css/ui-lightness/jquery-ui-1.8.18.custom.css" rel="stylesheet" />	    <script type="text/javascript" src="../js/jquery-1.7.1.min.js"> </script>    <script type="text/javascript" src="../js/jquery-ui-1.8.18.custom.min.js"> </script>    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $(".showCalender").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                yearRange: "-100:+0"
            });
            $(".showCalender").datepicker("option", "showAnim", "slideDown");
        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table width="95%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <asp:HiddenField ID="hdnAddEdit" value ="Add" runat="server" />
      
            <td height="20px" class="CellLabel" colspan="2">
        
             
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <%= hdnArea.Value%> :: <%= hdnAddEdit.Value %>  <%= hdnRole.Value %>
                           
                        </td>
                        <td align="right" width="20%">
                            <%--<asp:ImageButton ID="ImageButton1" runat="server" SkinID="sknImgBack" Height="24"
                                             Width="24" OnClientClick="javascript:window.history.back(); return false;" />--%>
                            <asp:ImageButton ID="ImageButton1" runat="server" SkinID="sknImgBack" Height="24"
                                             Width="24" OnClientClick="javascript:window.history.back(); return false;"  />
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
            <asp:HiddenField ID="hdnEditUserId" Value ="0" runat="server" />
            <td class="CellHeading" width="40%"><%= hdnAddEdit.Value %>  <%= hdnRole.Value %></td>
            <td class="ContextLinks" align="right"><%--<a id="context" href= "ListUsers.aspx">[ List Users ]</a> <a id="context" href= "AddUsers.aspx">[ Add Users ]</a>--%>
            </td>
        </tr>

        <asp:HiddenField ID="hdnArea" runat="server" />
        <asp:HiddenField ID="hdnRole" runat="server" />
        <tr runat="server" id="loginRow1" visible="true">
            <td colspan="2" class="SubHeading" style="height: 23px">Employee Information</td>
        </tr>
        <tr runat="server" id="loginRow2" visible="true">
            <td class="CellLabel">First Name : </td>
            <td class="CellData">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                    <asp:TextBox CssClass="Textbox" ID="txtFirstName" runat="server" Columns="50"/>
                    <asp:RequiredFieldValidator ID="RFV_txtFirstName" runat="server" ControlToValidate="txtFirstName"
                                                ErrorMessage="Please enter a valid First Name."></asp:RequiredFieldValidator></asp:PlaceHolder></td>
        </tr> 
        <tr runat="server" id="loginRow3" visible="true"> 
            <td class="CellLabel">Last Name : </td>
            <td class="CellData" >
                <asp:PlaceHolder ID="PlaceHolder2" runat="server">
                    <asp:TextBox CssClass="Textbox" ID="txtLastName" runat="server" Columns="50"/>
                    <asp:RequiredFieldValidator ID="RFV_txtLastName" runat="server" ControlToValidate="txtLastName"
                                                ErrorMessage="Please enter a valid Last Name."></asp:RequiredFieldValidator></asp:PlaceHolder></td>
        </tr>
        <tr> 
            <td class="CellLabel">Date of Birth : </td>
            <td  class="CellData">
                <asp:TextBox CssClass="Textbox showCalender" ID="txtDOB" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDOB"
                                            ErrorMessage="Please enter Date of Birth." Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDOB" 
                    ErrorMessage="Please enter valid Date of Birth. e.g. 15/12/2001" 
                  
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                    Display="Dynamic"></asp:RegularExpressionValidator><br />(valid date format is dd/mm/yyyy. e.g. 15/12/2001)
            </td>
        </tr> 
<%--        <tr runat="server" id="loginRow4" visible="true"> 
            <td class="CellLabel">Confirm Password : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtConfirmpassword" runat="server" TextMode="Password"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtConfirmpassword"
                                            ErrorMessage="Please re-enter the Password."></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtUserpassword"
                                      ControlToValidate="txtConfirmpassword" ErrorMessage="Confirm Password must match." ValueToCompare=""></asp:CompareValidator></td>
        </tr> --%>
        <%--<tr>
            <td colspan="2" class="SubHeading" style="height: 23px">Profile Information</td>
        </tr>--%>
	
        <%--<tr id="trRoles" runat="server"> 
            <td class="CellLabel">User Role : </td>
            <td class="CellData">
                <asp:DropDownList ID="comboUserRoles" runat="server">
                </asp:DropDownList></td>
        </tr> --%>
        <%--<tr> 
            <td class="CellLabel">First Name : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtFName" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFName"
                                            ErrorMessage="Please enter a First Name."></asp:RequiredFieldValidator></td>
        </tr> 
        <tr> 
            <td class="CellLabel">Last Name : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtLName" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLName"
                                            ErrorMessage="Please enter a Last Name."></asp:RequiredFieldValidator></td>
        </tr>--%> 
        <tr> 
            <td class="CellLabel">Email Adress : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtEmail" runat="server" Columns="50"/></td>
        </tr>
        <tr> 
            <td class="CellLabel">Cell # : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtPhone" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone"
                                            ErrorMessage="Please enter a Phone Number."></asp:RequiredFieldValidator></td>
        </tr> 
        <%--<tr id="trReceivingDate" runat="server" visible="false"> 
            <td class="CellLabel">Receiving Date : </td>
            <td  class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtRD" runat="server" Columns="50"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRD"
                                            ErrorMessage="Please enter Receiving Date." Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRD" 
                    ErrorMessage="Please enter valid Receiving Date. e.g. 15/12/2001" 
                  
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" 
                    Display="Dynamic"></asp:RegularExpressionValidator><br />(valid date format is dd/mm/yyyy. e.g. 15/12/2001)
            </td>
        </tr>--%>
        <tr id="trGender" runat="server" visible="true"> 
            <td class="CellLabel">Gender : </td>
            <td class="CellData"><asp:RadioButton Checked="true" ID="txtGenderM" GroupName="Gender" runat="server" Text="Male"/>
            <asp:RadioButton ID="txtGenderF" GroupName="Gender" runat="server" Text="Female"/></td>
        </tr>
        <tr id="trCNIC" runat="server" visible="true"> 
            <td class="CellLabel">CNIC # : </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtCNIC" runat="server" Columns="50"/>
                <cc2:MaskedEditExtender
            ID="MaskedEditExtender2"
            runat="server"
            TargetControlID="txtCNIC"
            Mask="99999-9999999-9"
            MessageValidatorTip="true"
            OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError"
            MaskType="None"
            ClearMaskOnLostFocus="false"
            ErrorTooltipEnabled="True" />
            </td>
        </tr>
        <tr id="trOccupation" runat="server" visible="true"> 
            <td class="CellLabel">Designation : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtDesignation" runat="server" Columns="50"/></td>
        </tr>
        <tr id="trQualification" runat="server" visible="true"> 
            <td class="CellLabel">Department : </td>
            <td class="CellData"><asp:TextBox CssClass="Textbox" ID="txtDepartment" runat="server" Columns="50"/></td>
        </tr>
        <tr> 
            <td class="CellLabel">Address : </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtAddress" runat="server"  Columns="50" MaxLength="1024" Rows="4" TextMode="MultiLine"/>
            </td>
        </tr> 
        <%--<tr>
            <td class="CellLabel">
                Country :
            </td>
            <td class="CellData">
                <select runat="server" id="cbCountry" name="cbCountry" size="1">
                    <option value="Afghanistan">Afghanistan</option>
                    <option value="Albania">Albania</option>
                    <option value="Algeria">Algeria</option>
                    <option value="American Samoa">American Samoa</option>
                    <option value="Andorra">Andorra</option>
                    <option value="Angola">Angola</option>
                    <option value="Anguilla">Anguilla</option>
                    <option value="Antarctica">Antarctica</option>
                    <option value="Antigua and Barbuda">Antigua and Barbuda</option>
                    <option value="Argentina">Argentina</option>
                    <option value="Armenia">Armenia</option>
                    <option value="Aruba">Aruba</option>
                    <option value="Australia">Australia</option>
                    <option value="Austria">Austria</option>
                    <option value="Azerbaijan">Azerbaijan</option>
                    <option value="Bahamas">Bahamas</option>
                    <option value="Bahrain">Bahrain</option>
                    <option value="Bangladesh">Bangladesh</option>
                    <option value="Barbados">Barbados</option>
                    <option value="Belarus">Belarus</option>
                    <option value="Belgium">Belgium</option>
                    <option value="Belize">Belize</option>
                    <option value="Benin">Benin</option>
                    <option value="Bermuda">Bermuda</option>
                    <option value="Bhutan">Bhutan</option>
                    <option value="Bolivia">Bolivia</option>
                    <option value="Bosnia and Herzegovina">Bosnia and Herzegovina</option>
                    <option value="Botswana">Botswana</option>
                    <option value="Bouvet Island">Bouvet Island</option>
                    <option value="Brazil">Brazil</option>
                    <option value="British Indian Ocean Territory">British Indian Ocean Territory</option>
                    <option value="Brunei Darussalam">Brunei Darussalam</option>
                    <option value="Bulgaria">Bulgaria</option>
                    <option value="Burkina Faso">Burkina Faso</option>
                    <option value="Burundi">Burundi</option>
                    <option value="Cambodia">Cambodia</option>
                    <option value="Cameroon">Cameroon</option>
                    <option value="Canada">Canada</option>
                    <option value="Cape Verde">Cape Verde</option>
                    <option value="Cayman Islands">Cayman Islands</option>
                    <option value="Central African Republic">Central African Republic</option>
                    <option value="Chad">Chad</option>
                    <option value="Chile">Chile</option>
                    <option value="China">China</option>
                    <option value="Christmas Island">Christmas Island</option>
                    <option value="Cocos (Keeling) Islands">Cocos (Keeling) Islands</option>
                    <option value="Colombia">Colombia</option>
                    <option value="Comoros">Comoros</option>
                    <option value="Congo">Congo</option>
                    <option value="Congo, the Democratic Republic of the">Congo, the Democratic Republic
                        of the</option>
                    <option value="Cook Islands">Cook Islands</option>
                    <option value="Costa Rica">Costa Rica</option>
                    <option value="Croatia">Croatia</option>
                    <option value="Cuba">Cuba</option>
                    <option value="Cyprus">Cyprus</option>
                    <option value="Czech Republic">Czech Republic</option>
                    <option value="Denmark">Denmark</option>
                    <option value="Djibouti">Djibouti</option>
                    <option value="Dominica">Dominica</option>
                    <option value="Dominican Republic">Dominican Republic</option>
                    <option value="Ecuador">Ecuador</option>
                    <option value="Egypt">Egypt</option>
                    <option value="El Salvador">El Salvador</option>
                    <option value="Equatorial Guinea">Equatorial Guinea</option>
                    <option value="Eritrea">Eritrea</option>
                    <option value="Estonia">Estonia</option>
                    <option value="Ethiopia">Ethiopia</option>
                    <option value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</option>
                    <option value="Faroe Islands">Faroe Islands</option>
                    <option value="Fiji">Fiji</option>
                    <option value="Finland">Finland</option>
                    <option value="France">France</option>
                    <option value="French Guiana">French Guiana</option>
                    <option value="French Polynesia">French Polynesia</option>
                    <option value="French Southern Territories">French Southern Territories</option>
                    <option value="Gabon">Gabon</option>
                    <option value="Gambia">Gambia</option>
                    <option value="Georgia">Georgia</option>
                    <option value="Germany">Germany</option>
                    <option value="Ghana">Ghana</option>
                    <option value="Gibraltar">Gibraltar</option>
                    <option value="Greece">Greece</option>
                    <option value="Greenland">Greenland</option>
                    <option value="Grenada">Grenada</option>
                    <option value="Guadeloupe">Guadeloupe</option>
                    <option value="Guam">Guam</option>
                    <option value="Guatemala">Guatemala</option>
                    <option value="Guernsey">Guernsey</option>
                    <option value="Guinea">Guinea</option>
                    <option value="Guinea-Bissau">Guinea-Bissau</option>
                    <option value="Guyana">Guyana</option>
                    <option value="Haiti">Haiti</option>
                    <option value="Heard Island and Mcdonald Islands">Heard Island and Mcdonald Islands</option>
                    <option value="Holy See (Vatican City State)">Holy See (Vatican City State)</option>
                    <option value="Honduras">Honduras</option>
                    <option value="Hong Kong">Hong Kong</option>
                    <option value="Hungary">Hungary</option>
                    <option value="Iceland">Iceland</option>
                    <option value="India">India</option>
                    <option value="Indonesia">Indonesia</option>
                    <option value="Iran, Islamic Republic of">Iran, Islamic Republic of</option>
                    <option value="Iraq">Iraq</option>
                    <option value="Ireland">Ireland</option>
                    <option value="Isle of Man">Isle of Man</option>
                    <option value="Israel">Israel</option>
                    <option value="Italy">Italy</option>
                    <option value="Jamaica">Jamaica</option>
                    <option value="Japan">Japan</option>
                    <option value="Jersey">Jersey</option>
                    <option value="Jordan">Jordan</option>
                    <option value="Kazakhstan">Kazakhstan</option>
                    <option value="Kenya">Kenya</option>
                    <option value="Kiribati">Kiribati</option>
                    <option value="Korea, Democratic People's Republic of">Korea, Democratic People's Republic
                        of</option>
                    <option value="Korea, Republic of">Korea, Republic of</option>
                    <option value="Kuwait">Kuwait</option>
                    <option value="Kyrgyzstan">Kyrgyzstan</option>
                    <option value="Lao People's Democratic Republic">Lao People's Democratic Republic</option>
                    <option value="Latvia">Latvia</option>
                    <option value="Lebanon">Lebanon</option>
                    <option value="Lesotho">Lesotho</option>
                    <option value="Liberia">Liberia</option>
                    <option value="Libyan Arab Jamahiriya">Libyan Arab Jamahiriya</option>
                    <option value="Liechtenstein">Liechtenstein</option>
                    <option value="Lithuania">Lithuania</option>
                    <option value="Luxembourg">Luxembourg</option>
                    <option value="Macao">Macao</option>
                    <option value="Macedonia, the Former Yugoslav Republic of">Macedonia, the Former Yugoslav
                        Republic of</option>
                    <option value="Madagascar">Madagascar</option>
                    <option value="Malawi">Malawi</option>
                    <option value="Malaysia">Malaysia</option>
                    <option value="Maldives">Maldives</option>
                    <option value="Mali">Mali</option>
                    <option value="Malta">Malta</option>
                    <option value="Marshall Islands">Marshall Islands</option>
                    <option value="Martinique">Martinique</option>
                    <option value="Mauritania">Mauritania</option>
                    <option value="Mauritius">Mauritius</option>
                    <option value="Mayotte">Mayotte</option>
                    <option value="Mexico">Mexico</option>
                    <option value="Micronesia, Federated States of">Micronesia, Federated States of</option>
                    <option value="Moldova, Republic of">Moldova, Republic of</option>
                    <option value="Monaco">Monaco</option>
                    <option value="Mongolia">Mongolia</option>
                    <option value="Montenegro">Montenegro</option>
                    <option value="Montserrat">Montserrat</option>
                    <option value="Morocco">Morocco</option>
                    <option value="Mozambique">Mozambique</option>
                    <option value="Myanmar">Myanmar</option>
                    <option value="Namibia">Namibia</option>
                    <option value="Nauru">Nauru</option>
                    <option value="Nepal">Nepal</option>
                    <option value="Netherlands">Netherlands</option>
                    <option value="Netherlands Antilles">Netherlands Antilles</option>
                    <option value="New Caledonia">New Caledonia</option>
                    <option value="New Zealand">New Zealand</option>
                    <option value="Nicaragua">Nicaragua</option>
                    <option value="Niger">Niger</option>
                    <option value="Nigeria">Nigeria</option>
                    <option value="Niue">Niue</option>
                    <option value="Norfolk Island">Norfolk Island</option>
                    <option value="Northern Mariana Islands">Northern Mariana Islands</option>
                    <option value="Norway">Norway</option>
                    <option value="Oman">Oman</option>
                    <option value="Pakistan">Pakistan</option>
                    <option value="Palau">Palau</option>
                    <option value="Palestinian Territory, Occupied">Palestinian Territory, Occupied</option>
                    <option value="Panama">Panama</option>
                    <option value="Papua New Guinea">Papua New Guinea</option>
                    <option value="Paraguay">Paraguay</option>
                    <option value="Peru">Peru</option>
                    <option value="Philippines">Philippines</option>
                    <option value="Pitcairn">Pitcairn</option>
                    <option value="Poland">Poland</option>
                    <option value="Portugal">Portugal</option>
                    <option value="Puerto Rico">Puerto Rico</option>
                    <option value="Qatar">Qatar</option>
                    <option value="Reunion">Reunion</option>
                    <option value="Romania">Romania</option>
                    <option value="Russian Federation">Russian Federation</option>
                    <option value="Rwanda">Rwanda</option>
                    <option value="Saint Helena">Saint Helena</option>
                    <option value="Saint Kitts and Nevis">Saint Kitts and Nevis</option>
                    <option value="Saint Lucia">Saint Lucia</option>
                    <option value="Saint Pierre and Miquelon">Saint Pierre and Miquelon</option>
                    <option value="Saint Vincent and the Grenadines">Saint Vincent and the Grenadines</option>
                    <option value="Samoa">Samoa</option>
                    <option value="San Marino">San Marino</option>
                    <option value="Sao Tome and Principe">Sao Tome and Principe</option>
                    <option value="Saudi Arabia">Saudi Arabia</option>
                    <option value="Senegal">Senegal</option>
                    <option value="Serbia">Serbia</option>
                    <option value="Seychelles">Seychelles</option>
                    <option value="Sierra Leone">Sierra Leone</option>
                    <option value="Singapore">Singapore</option>
                    <option value="Slovakia">Slovakia</option>
                    <option value="Slovenia">Slovenia</option>
                    <option value="Solomon Islands">Solomon Islands</option>
                    <option value="Somalia">Somalia</option>
                    <option value="South Africa">South Africa</option>
                    <option value="South Georgia and the South Sandwich Islands">South Georgia and the South
                        Sandwich Islands</option>
                    <option value="Spain">Spain</option>
                    <option value="Sri Lanka">Sri Lanka</option>
                    <option value="Sudan">Sudan</option>
                    <option value="Suriname">Suriname</option>
                    <option value="Svalbard and Jan Mayen">Svalbard and Jan Mayen</option>
                    <option value="Swaziland">Swaziland</option>
                    <option value="Sweden">Sweden</option>
                    <option value="Switzerland">Switzerland</option>
                    <option value="Syrian Arab Republic">Syrian Arab Republic</option>
                    <option value="Taiwan">Taiwan</option>
                    <option value="Tajikistan">Tajikistan</option>
                    <option value="Tanzania, United Republic of">Tanzania, United Republic of</option>
                    <option value="Thailand">Thailand</option>
                    <option value="Timor-Leste">Timor-Leste</option>
                    <option value="Togo">Togo</option>
                    <option value="Tokelau">Tokelau</option>
                    <option value="Tonga">Tonga</option>
                    <option value="Trinidad and Tobago">Trinidad and Tobago</option>
                    <option value="Tunisia">Tunisia</option>
                    <option value="Turkey">Turkey</option>
                    <option value="Turkmenistan">Turkmenistan</option>
                    <option value="Turks and Caicos Islands">Turks and Caicos Islands</option>
                    <option value="Tuvalu">Tuvalu</option>
                    <option value="Uganda">Uganda</option>
                    <option value="Ukraine">Ukraine</option>
                    <option value="United Arab Emirates">United Arab Emirates</option>
                    <option value="United Kingdom">United Kingdom</option>
                    <option value="United States">United States</option>
                    <option value="United States Minor Outlying Islands">United States Minor Outlying Islands</option>
                    <option value="Uruguay">Uruguay</option>
                    <option value="Uzbekistan">Uzbekistan</option>
                    <option value="Vanuatu">Vanuatu</option>
                    <option value="Venezuela">Venezuela</option>
                    <option value="Viet Nam">Viet Nam</option>
                    <option value="Virgin Islands, British">Virgin Islands, British</option>
                    <option value="Virgin Islands, U.s.">Virgin Islands, U.s.</option>
                    <option value="Wallis and Futuna">Wallis and Futuna</option>
                    <option value="Western Sahara">Western Sahara</option>
                    <option value="Yemen">Yemen</option>
                    <option value="Zambia">Zambia</option>
                    <option value="Zimbabwe">Zimbabwe</option>
                </select>
            </td>
        </tr> --%>
        <tr> 
            <td class="CellLabel">Note : </td>
            <td class="CellData">
                <asp:TextBox CssClass="Textbox" ID="txtNote" runat="server"  Columns="50" MaxLength="1024" Rows="4" TextMode="MultiLine"/></td>
        </tr>  
        <tr>
            <td>&nbsp;</td>
            <td class="ButtonSpacer"><asp:ImageButton runat="server" ID="btnUpdate" SkinID="sknImgBtnAddUser"  OnClick="btnUpdate_Click"/></td>
        </tr>
               
    </table>

</asp:Content>