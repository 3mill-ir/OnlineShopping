<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="SlidingImages.aspx.cs" Inherits="Views_Admin_SlidingImages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../Scripts/Windows.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Myc:PageTitle runat="server" Text="مدیـریت تصـاویر متحـرک صفحـه اصلی سـایت"/>
        <table border="0" cellpadding="3" cellspacing="0" class="Centerize" style="margin-top : 10px;">
            <tr>
                <td colspan="4" align="center">
                    <asp:FileUpload ID="uploader" runat="server" CssClass="Uploader" Width="300px"/>
                </td>
            </tr>
            <tr>
                <td align="left">
                    ترتیب نمایش :
                </td>
                <td>
                    <asp:TextBox ID="tb_Sequence" runat="server" CssClass="TextBoxes" Width="30px"></asp:TextBox>
                </td>
                <td align="left" style="padding-right : 20px;">
                    کالای مربوطه :
                </td>
                <td>
                    <asp:TextBox ID="tb_MaterialID" runat="server" CssClass="TextBoxes" Width="30px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tb_MaterialID" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    <a class="Link" onclick="OpenCenter('<%="FindMaterial.aspx?ClientID=" + tb_MaterialID.ClientID %>', 'FindMaterial', 500, 400)">انتخاب کالا</a>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="Button1" runat="server" Text="ارسـال" CssClass="Buttons" Width="70px" OnClick="btn_Send_Clicked" ValidationGroup="Submit"/>
                </td>
            </tr>
        </table>
        <div style="margin-top : 10px;">
            <asp:GridView ID="Grid_Images" runat="server" Width="100%" ShowHeader="false" 
                AutoGenerateColumns="false" GridLines="None" AllowPaging="true" PageSize="7" 
                OnRowCommand="Grid_Images_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Panel ID="Panel1" runat="server" Width='<%# Eval("Width") %>' style="position : relative; padding-bottom : 5px;" CssClass="Centerize">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="DeleteImage" CssClass="DeleteSmallButton" style="position : absolute; left : 10px; top : 10px;"></asp:LinkButton>
                                <asp:Image ID="Image1" runat="server" Width='<%# Eval("Width") %>' Height='<%# Eval("Height") %>' ImageUrl='<%# Eval("Url") %>' AlternateText='<%# Eval("Sequence") %>'/>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

