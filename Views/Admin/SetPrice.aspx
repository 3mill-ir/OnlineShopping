<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPrice.aspx.cs" Inherits="Views_Admin_SetPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hidden_MaterialID" runat="server" />
        <table border="0" cellpadding="0" cellspacing="0" class="Centerize" style="margin-top : 10px;">
            <tr>
                <td>
                    <asp:TextBox ID="tb_Price" runat="server" CssClass="TextBoxes" Width="120px"></asp:TextBox>
                    <asp:DropDownList ID="cmb_Unit" runat="server" CssClass="ComboBoxes" Width="80px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Text="ریال" Value="1"></asp:ListItem>
                        <asp:ListItem Text="تومان" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Price" ValidationGroup="Submit" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ControlToValidate="cmb_Unit" ValidationGroup="Submit" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td style="padding-right : 5px;">
                    <asp:Button ID="Button1" runat="server" Text="افـزوده شود" CssClass="Buttons" Width="90px" OnClick="btn_Add_Clicked" ValidationGroup="Submit"/>
                </td>
                <td style="padding-right : 5px;">
                    <asp:CheckBox ID="ch_IsCurrent" runat="server" Text="قیمت جـاری" Checked="true"/>
                </td>
            </tr>
        </table>
        <div style="margin : 10px;">
            <asp:DataList ID="DataList_Prices" runat="server" Width="100%" 
                RepeatColumns="3" RepeatDirection="Horizontal" ItemStyle-Width="33%"
                onitemcommand="DataList_Prices_ItemCommand">
                <ItemTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DataListItem">
                        <tr>
                            <td style="padding-right : 10px;">
                                <%# Eval("Price") %>
                            </td>
                            <td style="width : 12px; padding-left : 7px;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LineThroughButton" CommandName="LineThroughPrice" CommandArgument='<%# Eval("PriceID") %>'></asp:LinkButton>
                            </td>
                            <td style="width : 12px; padding-left : 7px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('آیا اطمینان دارید؟')" CommandName="DeletePrice" CommandArgument='<%# Eval("PriceID") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    </form>
</body>
</html>
