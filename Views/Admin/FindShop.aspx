<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindShop.aspx.cs" Inherits="Views_Admin_FindShop" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <Myc:PageTitle runat="server" Text="یافتـن فروشگـاه"/>
        <table border="0" cellpadding="3" cellspacing="0" class="Centerize" style="margin-top : 10px;">
            <tr>
                <td>
                    <asp:TextBox ID="tb_ShopName" runat="server" CssClass="TextBoxes" Width="150px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Clicked" Text="جستجـو" CssClass="Buttons" Width="80px"/>
                </td>
            </tr>
        </table>
        <div style="margin : 10px;">
            <asp:DataList ID="DataList_Shops" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" ItemStyle-Width="33%" Width="100%">
                <ItemTemplate>
                    <%# Eval("ShopLink") %>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    </form>
</body>
</html>
