<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="Views_Admin_OrderDetails" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .ContentMainDiv a:hover { text-decoration : underline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="ContentMainDiv" style="padding : 15px;">
        <asp:HiddenField ID="hidden_OrderID" runat="server" />
        <Myc:PageTitle runat="server" Text="جزئیـات سفـارش"/>
        <% if (Grid_Items.Rows.Count > 0)
           { %>
        <div style="margin-top : 10px;">
            <asp:GridView ID="Grid_Items" runat="server" CssClass="Grid2" Width="100%" 
                AutoGenerateColumns="false">
                <HeaderStyle CssClass="GridHeader2"/>
                <Columns>
                    <asp:TemplateField HeaderText="ردیف" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Index") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="نام کالا" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="قیمت واحد" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("UnitPrice") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تعداد" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Count") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="قیمت کل" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("TotalPrice") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <% }
           else
           { %>
           <div class="EmptyList" style="margin-top : 10px;">
                کالایی جهت نمایش موجود نیست !
           </div>
        <%} %>
        <table border="0" cellpadding="3" cellspacing="0" class="CompareTable" width="100%" style="margin-top : 10px;">
            <tr>
                <td align="left" class="CompareTable_RightHeaderTD">
                    آدرس کامل :
                </td>
                <td class="CompareTable_ValueCell" style="border-left : none;">
                    <asp:Label ID="lbl_Address" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="CompareTable_RightHeaderTD">
                    موبایل :
                </td>
                <td class="CompareTable_ValueCell" style="border-left : none;">
                    <asp:Label ID="lbl_Mobile" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="CompareTable_RightHeaderTD">
                    تلفنهای تماس :
                </td>
                <td class="CompareTable_ValueCell" style="border-left : none;">
                    <asp:Label ID="lbl_Tels" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="CompareTable_RightHeaderTD">
                    کد پستی 10 رقمی :
                </td>
                <td class="CompareTable_ValueCell" style="border-left : none;">
                    <asp:Label ID="lbl_PostalCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="CompareTable_RightHeaderTD" style=" border-bottom : none;">
                    توضیحات :
                </td>
                <td class="CompareTable_ValueCell" style="border-left : none; border-bottom : none;">
                    <asp:Label ID="lbl_Statement" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div class="Cadr" style="margin-top : 10px;">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        وضعیت :
                    </td>
                    <td style="padding-right : 5px;">
                        <asp:DropDownList ID="cmb_Status" runat="server" CssClass="ComboBoxes" Width="150px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Text="در حال بررسی" Value="1"></asp:ListItem>
                            <asp:ListItem Text="عدم تأمین" Value="2"></asp:ListItem>
                            <asp:ListItem Text="تأیید شد" Value="3"></asp:ListItem>
                            <asp:ListItem Text="رد شد" Value="4"></asp:ListItem>
                            <asp:ListItem Text="به پست تحویل شد" Value="5"></asp:ListItem>
                            <asp:ListItem Text="برگشت خورد" Value="6"></asp:ListItem>
                            <asp:ListItem Text="تحویل داده شد" Value="7"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cmb_Status" ValidationGroup="Save" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top : 10px;">
            <asp:UpdatePanel ID="Up_Save" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="Centerize">
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="SaveChanges" OnClick="btn_Save_Clicked" ValidationGroup="Save"></asp:LinkButton>
                            </td>
                            <td style="width : 16px; padding-right : 5px;">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Up_Save">
                                    <ProgressTemplate>
                                        <div class="Loading_Circle_2"></div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

