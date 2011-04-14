<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFieldUnit.aspx.cs" Inherits="Views_Admin_NewMaterialType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidden_UnitID" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="up_Save" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table border="0" cellpadding="2" cellspacing="0" class="Centerize" style="margin-top : 25px;">
                    <tr>
                        <td>
                            عنوان واحد :
                        </td>
                        <td>
                            <asp:TextBox ID="tb_Name" runat="server" CssClass="TextBoxes"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="tb_Name" ValidationGroup="Submit" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button ID="btn_Save" runat="server" ValidationGroup="Submit" Text="ذخیـره شـود" CssClass="Buttons" Width="90px" OnClick="btn_Save_Clicked"/>
                        </td>
                        <td style="width : 16px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up_Save">
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
    </form>
</body>
</html>
