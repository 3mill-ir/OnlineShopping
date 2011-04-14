<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="SaveShoppingCart.aspx.cs" Inherits="Views_SaveShoppingCart" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    <style type="text/css">
        #MainContent a:hover {text-decoration : underline;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="MainContent" class="ContentMainDiv" style="padding : 15px;">
        <Myc:PageTitle ID="PageTitle2" runat="server" Text="تکمیـل فـرایند خـرید" EnableViewState="false"/>
        <div class="Title_ShoppingCartFinal" style="margin-top : 10px;" enableviewstate="false"></div>
        <div style="margin-top : 5px;" enableviewstate="false"><%= Cart.Current.Info %></div>
        <div style="margin : 10px 0px 10px 0px;" enableviewstate="false">
            <Myc:PageTitle ID="PageTitle1" runat="server" Text="مشخصـات کـامل جهت دریـافت سفـارش"/>
        </div>
        <table border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td align="left">
                    شهر مقصد :
                </td>
                <td>
                    <asp:UpdatePanel ID="Up_Cities" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmb_State" runat="server" CssClass="ComboBoxes" 
                                            Width="140px" AutoPostBack="true"
                                            OnSelectedIndexChanged="cmb_State_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding-right : 5px;">
                                        <asp:DropDownList ID="cmb_City" runat="server" CssClass="ComboBoxes" Width="130px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="cmb_City" ValidationGroup="SubmitCart" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="padding-right : 2px;">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Up_Cities">
                                            <ProgressTemplate>
                                                <div class="Loading_Circle_2"></div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" style="padding-top : 10px;">
                    آدرس دقیق پستی :
                </td>
                <td>
                    <asp:TextBox ID="tb_Address" EnableViewState="false" runat="server" CssClass="TextBoxes TextArea" TextMode="MultiLine" Width="350px" Height="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Address" ValidationGroup="SubmitCart" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    کد پستی 10 رقمی :
                </td>
                <td>
                    <asp:TextBox ID="tb_PostalCode" EnableViewState="false" runat="server" CssClass="TextBoxes" Width="100px" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_PostalCode" ValidationGroup="SubmitCart" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <span class="SmallGray">بدون خط تیره</span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    شماره موبایل :
                </td>
                <td>
                    <asp:TextBox ID="tb_Mobile" EnableViewState="false" runat="server" CssClass="TextBoxes" Width="100px" MaxLength="11"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Mobile" ValidationGroup="SubmitCart" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <span class="SmallGray">مثال : 09123456789</span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    سایر شماره تلفن ها :
                </td>
                <td>
                    <asp:TextBox ID="tb_Tels" EnableViewState="false" runat="server" CssClass="TextBoxes" Width="250px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" style="padding-top : 10px;">
                    توضیحات :
                </td>
                <td>
                    <asp:TextBox ID="tb_Statement" EnableViewState="false" runat="server" CssClass="TextBoxes TextArea" TextMode="MultiLine" Width="350px" Height="50px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" class="Centerize" style="margin-top : 5px;">
            <tr>
                <td>
                    <asp:LinkButton ID="btn_SaveCart" runat="server" CssClass="SaveShoppingCart" ValidationGroup="SubmitCart" OnClick="btn_SaveCart_Click"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

