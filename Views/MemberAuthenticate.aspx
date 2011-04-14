<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="MemberAuthenticate.aspx.cs" Inherits="Views_ShoppingSteps" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%= "تکمیل فرایند خرید" + " | " + Constants.StaticTitle %></title>
    <meta name="Description" content='<%= Constants.StaticDescription %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="ContentMainDiv" style="padding : 15px;">
        <% if (Cart.Current.Items.Count > 0)
           { %>
            <div style="margin-bottom : 10px;"><Myc:PageTitle runat="server" Text="تکمیـل فـرایند خـرید"/></div>
            <% if (User.Identity.IsAuthenticated)
               {%>
                <% if (User.IsInRole(MyRoles.Member)) {
                       MembersRepository mr = new MembersRepository();
                       Models.Member currentMember = mr.GetMember(User.Identity.Name);%>
                    <%= currentMember.Info %>
                    <table border="0" cellpadding="0" cellspacing="0" style="margin-top : 10px;" class="Centerize">
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="NextStep" OnClick="LinkButton1_Clicked"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                <% } %>
            <% }
               else
               { %>
                <asp:UpdatePanel ID="Up_Step1_NotAuthenticated" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="ListTitle" style="padding-bottom : 13px; position : relative;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Up_Step1_NotAuthenticated">
                                <ProgressTemplate>
                                    <div class="Loading_Line" style="position : absolute; left : 15px; top : 15px;"></div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rb_NotRegistered" runat="server" 
                                            GroupName="AreYouRegistered" Checked="true" Text="مشتـری جدید هستـم" AutoPostBack="true"/>
                                    </td>
                                    <td style="padding-right : 25px;">    
                                        <asp:RadioButton ID="rb_Registered" runat="server" GroupName="AreYouRegistered" 
                                            Text="قبـلا از این فـروشگاه خـرید کرده ام" AutoPostBack="true" 
                                            OnCheckedChanged="rb_Registered_CheckedChanged"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="ListContent">
                            <% if (rb_NotRegistered.Checked)
                               { %>
                                <table border="0" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            نـام : 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_Name" MaxLength="50" runat="server" CssClass="TextBoxes" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_Name" ValidationGroup="Step1NotRegistered" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" style="padding-right : 25px;">
                                            نـام خـانوادگی :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_LastName" MaxLength="50" runat="server" CssClass="TextBoxes" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_LastName" ValidationGroup="Step1NotRegistered" 
                                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            ایمیـل : 
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="tb_Email" runat="server" MaxLength="50" CssClass="TextBoxes DirectionLTR" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            موبـایل : 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_Mobile" runat="server" MaxLength="11" CssClass="TextBoxes" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_Mobile" ValidationGroup="Step1NotRegistered" 
                                                ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" style="padding-right : 25px;">
                                            سایر شماره ها جهت تماس :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_Tels" runat="server" CssClass="TextBoxes" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            شهر محل سکونت : 
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="cmb_States" runat="server" CssClass="ComboBoxes" AutoPostBack="true"
                                                Width="130px" onselectedindexchanged="cmb_States_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="cmb_Cities" runat="server" CssClass="ComboBoxes" Width="130px" style="margin-right : 2px;">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="cmb_Cities" ValidationGroup="Step1NotRegistered" 
                                                ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            نـام کاربـری :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="tb_UserName" runat="server" MaxLength="20" CssClass="TextBoxes CenterText" Width="120px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_UserName" ValidationGroup="Step1NotRegistered" 
                                                ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <span class="SmallGray" style="padding-right : 3px;">
                                                با این نام کاربری بعداً میتوانید وارد سایت شده و خرید خود را رهگیری نمایید.
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            رمز عبور :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="tb_Password" runat="server" MaxLength="20" CssClass="TextBoxes CenterText" Width="120px" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_Password" ValidationGroup="Step1NotRegistered" 
                                                ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <span class="SmallGray" style="padding-right : 3px;">
                                                رمز عبور باید حداقل دارای 6 کاراکتر باشد.
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            تکرار رمز عبور :
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="tb_PasswordConfirm" runat="server" MaxLength="20" CssClass="TextBoxes CenterText" Width="120px" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_PasswordConfirm" ValidationGroup="Step1NotRegistered" 
                                                ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="Step1NotRegistered" ErrorMessage="* تکرار رمز صحیح نیست" ControlToValidate="tb_PasswordConfirm" ControlToCompare="tb_Password"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                                <div class="PartSeparator" style="margin-top : 7px; padding-top : 7px;">
                                    <table border="0" cellpadding="3" cellspacing="0" class="Centerize">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btn_SaveAndNext" runat="server" CssClass="NextStep" ValidationGroup="Step1NotRegistered" OnClick="btn_NextStep_Clicked"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            <% }
                               else if (rb_Registered.Checked)
                               { %>
                                <table border="0" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td>
                                            نام کاربری :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_Reg_UserName" runat="server" CssClass="TextBoxes" Width="130px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_Reg_UserName" ValidationGroup="Registered" 
                                                ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="padding-right : 15px;">
                                            رمز عبور :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_Reg_Password" TextMode="Password" runat="server" CssClass="TextBoxes" Width="130px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="tb_Reg_Password" ValidationGroup="Registered" 
                                                ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btn_Check" runat="server" CssClass="Check" OnClick="btn_Check_Clicked" ValidationGroup="Registered"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnl_MemberInfo" runat="server" Visible="false" style="padding-top : 10px;">
                                    <asp:HiddenField ID="hid_UserName" runat="server" Value=""/>
                                    <asp:Label ID="lbl_Info" runat="server" Text="Label"></asp:Label>
                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-top : 10px;" class="Centerize">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btn_Registered_Next" runat="server" CssClass="NextStep" OnClick="btn_Registered_Next_Clicked"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            <% } %>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            <% } %>
        <% }
           else
           { %>
            <div class="EmptyList">
                سبد خـرید شمـا خـالی اسـت.
            </div>
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

