<%@ Page Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="MarketSelection.aspx.cs" Inherits="Views_MarketSelection" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>انتخاب بازارچه مرزی | <%=Constants.StaticTitle %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <div class="MarketsBG">
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="Market_Urmia" NavigateUrl="~/Views/MarketShops.aspx?Market=Urmia" ToolTip="بازار ارومیه"></asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="Market_Piranshahr" NavigateUrl="~/Views/MarketShops.aspx?Market=Piranshahr" ToolTip="بازار پیرانشهر"></asp:HyperLink>
            <asp:HyperLink ID="HyperLink3" runat="server" CssClass="Market_Baneh" NavigateUrl="~/Views/MarketShops.aspx?Market=Baneh" ToolTip="بازار بانه"></asp:HyperLink>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_UnderFooter" Runat="Server">
</asp:Content>

