<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMemberProfile.aspx.cs" Inherits="Views_Admin_EditMemberProfile2" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="ContentMainDiv" style="padding : 15px;">
            <asp:HiddenField ID="hid_MemberId" runat="server" />
            <Myc:PageTitle runat="server" Text="اطلاعـات کاربـری"/>
            <div style="margin-top : 10px;">
                <table border="0" cellpadding="2" cellspacing="0" style="margin : 0px 20px 10px 20px;">
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmb_States" runat="server" CssClass="ComboBoxes" AutoPostBack="true"
                                        Width="130px" onselectedindexchanged="cmb_States_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="cmb_Cities" runat="server" CssClass="ComboBoxes" Width="130px" style="margin-right : 2px;">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="cmb_Cities" ValidationGroup="Step1NotRegistered" 
                                        ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            نـام کاربـری :
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tb_UserName" runat="server" MaxLength="20" CssClass="TextBoxes CenterText" Width="120px" Enabled="false"></asp:TextBox>
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
                <div class="TopDashed" style="padding-top : 7px;">
                    <asp:UpdatePanel ID="Up_Save" runat="server" UpdateMode="Conditional">  
                        <ContentTemplate>
                            <table border="0" cellpadding="3" cellspacing="0" class="Centerize">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btn_Save" runat="server" CssClass="SaveChanges"  ValidationGroup="Step1NotRegistered" OnClick="btn_Save_Clicked"></asp:LinkButton>
                                    </td>
                                    <td style="width : 16px;">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Up_Save">
                                            <ProgressTemplate>
                                                <div class="Loading_Circle_2">
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
