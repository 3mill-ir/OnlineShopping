<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditShop.aspx.cs" Inherits="Views_Admin_NewShop" %>
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
    <div style="padding : 10px; margin-right : auto; margin-left : auto;">
        <asp:HiddenField ID="hidden_ShopID" runat="server"/>
        <asp:HiddenField ID="hidden_HasAcount" runat="server" Value="False"/>
        <Myc:PageTitle runat="server" Text="ویـرایش فـروشگـاه"/>
        <table border="0" cellpadding="3" cellspacing="0" style="margin : 10px 20px 0px 0px;">
            <tr>
                <td>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_RealShop" runat="server" Text="حقیقـی" GroupName="ShopType" Checked="true" AutoPostBack="true"/>
                            </td>
                            <td style="padding-right : 20px;">
                                <asp:RadioButton ID="rb_OnlineShop" runat="server" Text="اینتـرنتی" GroupName="ShopType" AutoPostBack="true"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    نام فروشگاه :
                </td>
                <td>
                    <asp:TextBox ID="tb_Name" runat="server" CssClass="TextBoxes" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Name" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="padding-top : 5px;">
                    توصیف :
                </td>
                <td>
                    <asp:TextBox ID="tb_Description" runat="server" CssClass="TextBoxes TextArea" Width="180px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Description" ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    نام مالک :
                </td>
                <td>
                    <asp:TextBox ID="tb_Owner" runat="server" CssClass="TextBoxes" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Owner" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <% if (rb_RealShop.Checked)
               { %>
               <tr>
                    <td align="left">
                        بازارچه :
                    </td>
                    <td>
                        <asp:DropDownList ID="cmb_Market" runat="server" CssClass="ComboBoxes" Width="150px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Text="ارومیه" Value="1"></asp:ListItem>
                            <asp:ListItem Text="بانه" Value="2"></asp:ListItem>
                            <asp:ListItem Text="پیرانشهر" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="cmb_Market" ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        آدرس :
                    </td>
                    <td>
                        <asp:TextBox ID="tb_Address" runat="server" CssClass="TextBoxes" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="tb_Address" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            <% } else if (rb_OnlineShop.Checked) { %>
                <tr>
                    <td align="left">
                        وبسـایت :
                    </td>
                    <td>
                        <asp:TextBox ID="tb_WebSite" runat="server" CssClass="TextBoxes" Width="120px"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="tb_WebSite" ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            <% } %>
            <tr>
                <td align="left">
                    موبایل مالک :
                </td>
                <td>
                    <asp:TextBox ID="tb_Mobile" runat="server" CssClass="TextBoxes" Width="180px" MaxLength="11"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Mobile" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    تلفنهای تماس :
                </td>
                <td>
                    <asp:TextBox ID="tb_Tels" runat="server" CssClass="TextBoxes" Width="250px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="Up_Membership" runat="server" UpdateMode="Conditional"> 
            <ContentTemplate>
                <div class="ListTitle" style="margin-top : 10px;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="ch_CreateUser" runat="server" AutoPostBack="true" Text="حسـاب کاربری اختصـاص داده شود" oncheckedchanged="ch_CreateUser_CheckedChanged"/>
                            </td>
                            <td align="left">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Up_Membership">
                                    <ProgressTemplate>
                                        <div class="Loading_Line"></div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="ListContent">
                    <table runat="server" id="tbl_CreateUser" border="0" cellpadding="2" cellspacing="0" style="color : Gray;">
                        <tr>
                            <td align="left">
                                نـام کاربـری :
                            </td>
                            <td>
                                <asp:TextBox ID="tb_UserName" runat="server" CssClass="TextBoxes" Width="120px" Enabled="false"></asp:TextBox>
                                <% if (ch_CreateUser.Checked)
                                   { %>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="SubmitForm" ControlToValidate="tb_UserName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <% } %>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                رمـز عبـور :
                            </td>
                            <td>
                                <asp:TextBox ID="tb_Password" runat="server" CssClass="TextBoxes" Width="120px" Enabled="false" TextMode="Password"></asp:TextBox>
                                <% if (ch_CreateUser.Checked)
                                   { %>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="SubmitForm" ControlToValidate="tb_Password" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <% } %>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                تکـرار رمـز عبـور :
                            </td>
                            <td>
                                <asp:TextBox ID="tb_Confirm" runat="server" CssClass="TextBoxes" Width="120px" Enabled="false" TextMode="Password"></asp:TextBox>
                                <% if (ch_CreateUser.Checked)
                                   { %>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="SubmitForm" ControlToValidate="tb_Confirm" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ValidationGroup="SubmitForm" ControlToValidate="tb_Confirm" ControlToCompare="tb_Password"></asp:CompareValidator>
                                <% } %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="ch_IsApproved" runat="server" Text="حساب کاربری فعال باشد" Checked="true" Enabled="false"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="TopDashed" style="padding-top : 10px; margin-top : 10px;">
            <table border="0" cellpadding="0" cellspacing="0" class="Centerize">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="up_Save" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="ذخیـره شـود" CssClass="Buttons" Width="90px" OnClick="btn_Save_Clicked" ValidationGroup="SubmitForm"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="padding-right : 6px; width : 16px;">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up_Save">
                            <ProgressTemplate>
                                <div class="LoadingCircle"></div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
