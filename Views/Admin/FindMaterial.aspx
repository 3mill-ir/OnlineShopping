<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindMaterial.aspx.cs" Inherits="Views_Admin_FindMaterial" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagName="PageTitle" TagPrefix="Myc" %>
<%@ Register Src="~/Views/SetSelector.ascx" TagPrefix="Myc" TagName="SetSelector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin : 10px;">
        <asp:HiddenField ID="hid_ClientId" runat="server" />
        <Myc:PageTitle runat="server" Text="یـافتن کـالا"/>
        <table border="0" cellpadding="3" cellspacing="0" class="Centerize" style="margin-top : 10px;">
            <tr>
                <td colspan="2">
                    <Myc:SetSelector runat="server" ID="setSelector" EnableValidation="true" ValidationGroup="Search"/>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:TextBox ID="tb_MaterialName" runat="server" CssClass="TextBoxes" Width="150px"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="btn_Search" runat="server" Text="جستجـو" CssClass="Buttons" Width="80px" OnClick="btn_Search_Clicked" ValidationGroup="Search"/>
                </td>
            </tr>
        </table>
        <div style="margin : 10px;">
            <asp:DataList ID="DataList_Shops" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" ItemStyle-Width="50%" Width="100%">
                <ItemTemplate>
                    <%# Eval("Link") %>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    </form>
</body>
</html>
