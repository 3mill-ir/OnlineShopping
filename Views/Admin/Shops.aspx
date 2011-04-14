<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="Shops.aspx.cs" Inherits="Views_Admin_Shops" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../Scripts/Windows.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <div class="Title_Shops"></div>
        <div class="ListTitle" style="margin-top : 10px;">
            <a class="Link" onclick="return OpenCenter('NewShop.aspx', 'NewShop', 550, 650);">تعریف فروشگاه جدید</a>
        </div>
        <div class="ListContent">
            <asp:DataList ID="DataList_Shops" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" ItemStyle-Width="33%" Width="100%" 
                OnItemCommand="DataList_Shops_ItemCommand">
                <ItemTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DataListItem">
                        <tr>
                            <td style="padding-right : 10px;">
                                <%# Eval("Name") %>
                            </td>
                            <td style="width : 20px; padding-left : 10px;">
                                <a class="Edit" onclick="return OpenCenter('<%# "EditShop.aspx?ShopID=" + Eval("ShopID").ToString() %>', 'EditShop',550, 650);"></a>
                            </td>
                            <td style="width : 12px; padding-left : 10px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('تمام اطلاعات مربوط به فروشگاه حذف خواهد شد ، آیا اطمینان دارید؟')" CommandName="DeleteShop" CommandArgument='<%# Eval("ShopID") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

