<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialFields.aspx.cs" Inherits="Views_Admin_MaterialFields" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidden_MaterialID" runat="server" />
        <div>
            <div style="padding : 10px 10px 0px 0px;">
                <Myc:PageTitle ID="PageTitle1" runat="server" Text="جـزئیات کـالا"/>
            </div>
            <asp:Panel ID="pnl_Controls" runat="server">
            </asp:Panel>
            <div style="padding-top : 7px;">
                <table border="0" cellpadding="0" cellspacing="0" class="Centerize">
                    <tr>
                        <td>
                            <asp:Button ID="btn_Save" runat="server" Text="ذخیـره اطلاعات" CssClass="Buttons" Width="100px" OnClick="btn_Save_Clicked"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
