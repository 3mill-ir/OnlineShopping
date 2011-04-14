<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCategory.aspx.cs" Inherits="Views_Admin_NewCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ویرایش دسته</title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidden_CategoryID" runat="server" />
    <div style="padding : 10px;">
        <div class="Title_EditCategory"></div>
        <div style="margin : 10px 20px 10px 20px;">
            <asp:UpdatePanel ID="Up_Save" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td align="left">
                                نام دسته :
                            </td>
                            <td>
                                <asp:TextBox ID="tb_Name" runat="server" CssClass="TextBoxes" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="tb_Name" ValidationGroup="Submit" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="padding-right : 20px;">
                                ترتیب :
                            </td>
                            <td>
                                <asp:TextBox ID="tb_Sequence" runat="server" CssClass="TextBoxes" Width="30px"></asp:TextBox>
                            </td>
                            <td style="padding-right : 20px;">
                                <asp:Button ID="btn_Save" runat="server" Text="ذخیـره" Width="90px" CssClass="Buttons" onclick="btn_Save_Click" ValidationGroup="Submit"/>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Up_Save">
                                    <ProgressTemplate>
                                        <div class="LoadingCircle"></div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
