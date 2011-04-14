<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaterial.aspx.cs" Inherits="Views_Admin_NewMaterial" ValidateRequest="false"%>

<%@ Register Src="~/Views/SetSelector.ascx" TagPrefix="Myc" TagName="SetSelector" %>

<%@ Register Src="~/Views/Keywords.ascx" TagName="Keywords" TagPrefix="Myc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: ویرایش کالا ::</title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript", src="../../Scripts/Windows.js"></script>
    <script type="text/javascript" src="../../Scripts/tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript" src="../../Scripts/tinymce/TinyLoadScripts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidden_MaterialID" runat="server" Value=""/>
    <div style="padding : 10px; display: table; margin-right : auto; margin-left : auto;">
        <div class="PageTitle Title_EditMaterial"></div>
        <table border="0" cellpadding="3" cellspacing="0" style="margin : 10px 20px 5px 20px;">
            <tr>
                <td align="left">
                    کد کالا :
                </td>
                <td>
                    <asp:TextBox ID="tb_Code" runat="server" Width="120px" CssClass="TextBoxes"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Code" ValidationGroup="SubmitForm" ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    عنوان :
                </td>
                <td>
                    <asp:TextBox ID="tb_Name" runat="server" Width="200px" CssClass="TextBoxes"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Name" ValidationGroup="SubmitForm" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td style="padding-right : 25px;">
                    <Myc:SetSelector runat="server" ID="myc_SetSelector" CategoriesComboWidth="130px" SetsComboWidth="130px" Text="دسته :" EnableValidation="true" ValidationGroup="SubmitForm"/>
                </td>
            </tr>
        </table>
        <div style="margin : 0px 20px 0px 20px;">
            <asp:TextBox ID="tb_Description" runat="server" CssClass="TextBoxes" TextMode="MultiLine"></asp:TextBox>
        </div>
        <table border="0" cellpadding="3" cellspacing="0" style="margin : 5px 20px 0px 20px;">
            <tr>
                <td align="left">
                    قیمت خرید
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tb_PurchasePrice" runat="server" CssClass="TextBoxes" Width="50px"></asp:TextBox>
                    <asp:DropDownList ID="cmb_PurchasePriceUnit" runat="server" CssClass="ComboBoxes" Width="80px">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="ریال" Value="1"></asp:ListItem>
                        <asp:ListItem Text="تومان" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="tb_PurchasePrice" ValidationGroup="SubmitForm" ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ControlToValidate="cmb_PurchasePriceUnit" ValidationGroup="SubmitForm" ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="ch_Visible" runat="server" Checked="true" Text="نمـایش یابد"/>
                </td>
            </tr>
            <tr>
                <td align="left">
                    وضعیت :
                </td>
                <td>
                    <asp:DropDownList ID="cmb_Status" runat="server" CssClass="ComboBoxes" 
                        Width="127px" AutoPostBack="true" 
                        onselectedindexchanged="cmb_Status_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>موجود - تعداد نامحدود</asp:ListItem>
                        <asp:ListItem>موجود - تعداد معین</asp:ListItem>
                        <asp:ListItem>موجود نیست</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="cmb_Status" ValidationGroup="SubmitForm" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td align="left" style="padding-right : 30px;">
                    تعداد موجودی :
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmb_Status" EventName="SelectedIndexChanged"/>
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="tb_Counts" runat="server" CssClass="TextBoxes" Width="40px" Enabled="false"></asp:TextBox>
                            <asp:DropDownList ID="cmb_Unit" runat="server" CssClass="ComboBoxes" Width="100px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>عدد</asp:ListItem>
                                <asp:ListItem>بسته</asp:ListItem>
                                <asp:ListItem>کیلوگرم</asp:ListItem>
                                <asp:ListItem>مجموعه</asp:ListItem>
                                <asp:ListItem>متر</asp:ListItem>
                                <asp:ListItem>حلقه</asp:ListItem>
                                <asp:ListItem>دستگاه</asp:ListItem>
                                <asp:ListItem>لیتر</asp:ListItem>
                            </asp:DropDownList>
                            <% if (cmb_Status.SelectedIndex == 2) { %>
                            <asp:RequiredFieldValidator ControlToValidate="tb_Counts" ValidationGroup="SubmitForm" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <% } %>
                            <asp:RequiredFieldValidator ControlToValidate="cmb_Unit" ValidationGroup="SubmitForm" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left">
                    وزن :
                </td>
                <td>
                    <asp:TextBox ID="tb_Weight" runat="server" CssClass="TextBoxes" Width="30px"></asp:TextBox>
                    <asp:DropDownList ID="cmb_WeightUnit" runat="server" CssClass="ComboBoxes" Width="80px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>مثقال</asp:ListItem>
                        <asp:ListItem>گرم</asp:ListItem>
                        <asp:ListItem>کیلوگرم</asp:ListItem>
                        <asp:ListItem>تن</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="cmb_WeightUnit" ValidationGroup="SubmitForm" ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    ابعاد :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="tb_Length" runat="server" CssClass="TextBoxes" Width="30px"></asp:TextBox>
                    *
                    <asp:TextBox ID="tb_Width" runat="server" CssClass="TextBoxes" Width="30px"></asp:TextBox>
                    *
                    <asp:TextBox ID="tb_Height" runat="server" CssClass="TextBoxes" Width="30px"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="طول * عرض * ارتفاع" CssClass="SmallGray"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    نوع کـالا :
                </td>
                <td>
                    <asp:DropDownList ID="cmb_MaterialType" runat="server" CssClass="ComboBoxes" Width="130px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="cmb_MaterialType" ValidationGroup="SubmitForm" ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    کد فروشگاه :
                </td>
                <td colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="tb_ShopID" runat="server" CssClass="TextBoxes" Width="40px"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="tb_ShopID" ValidationGroup="SubmitForm" ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="padding-right : 6px;">
                                <a class="Link" onclick="return OpenCenter('FindShop.aspx', 'FindShop', 500, 300);">یافتن کد فروشگاه</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="margin : 2px 20px 5px 20px;">
            <Myc:Keywords runat="server" ID="myc_Keywords" ItemWidth="150px" RepeatColumns="3" Text="برچسب ها :"/>
        </div>
        <div style="border-top : dashed 1px #75bee9; padding-top : 5px; margin-top : 3px;">
            <table border="0" cellpadding="3" cellspacing="0" class="Centerize">
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_Save" runat="server" Text="ذخیـره" CssClass="Buttons" 
                            Width="80px" onclick="Button1_Click" ValidationGroup="SubmitForm"/>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnl_PicsLink" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <a class="Link" onclick="return OpenCenter('<%="MaterialPictures.aspx?MaterialID=" + Convert.ToInt32(hidden_MaterialID.Value) %>', 'MaterialPictures', 700, 500);">تصاویر محصول</a>
                                    </td>
                                    <td style="padding-right : 10px;">
                                        <a class="Link" onclick="return OpenCenter('<%="MaterialFields.aspx?MaterialID=" + Convert.ToInt32(hidden_MaterialID.Value) %>', 'MaterialFields', 600, 400);">جـزئیات کـالا</a>
                                    </td>
                                    <td style="padding-right : 10px;">
                                        <a class="Link" onclick=" return OpenCenter('<%="SetPrice.aspx?MaterialID=" + Convert.ToInt32(hidden_MaterialID.Value) %>', 'SetPrice', 550, 200);">تنظیم قیمت فروش</a>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
