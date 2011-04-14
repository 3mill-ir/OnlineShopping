<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditField.aspx.cs" Inherits="Views_Admin_NewField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_FieldID" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="padding-top : 15px;">
        <table border="0" cellpadding="2" cellspacing="0" class="Centerize">
            <tr>
                <td align="left">
                    نـوع کـالا :
                </td>
                <td>
                    <asp:DropDownList ID="cmb_Types" runat="server" CssClass="ComboBoxes" Width="130px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="cmb_Types" ValidationGroup="Submit" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td align="left" style="padding-right : 20px;">
                    عنـوان فیلد :
                </td>
                <td>
                    <asp:TextBox ID="tb_Name" runat="server" CssClass="TextBoxes" Width="130px"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Name" ValidationGroup="Submit" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    نـوع فیلـد :
                </td>
                <td>
                    <asp:DropDownList ID="cmb_FieldType" runat="server" CssClass="ComboBoxes" Width="130px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="cmb_FieldType" ValidationGroup="Submit" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    
                </td>
                <td align="left" style="padding-right : 20px;">
                    واحـد :
                </td>
                <td>
                    <asp:DropDownList ID="cmb_Unit" runat="server" CssClass="ComboBoxes" Width="130px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="up_Save" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="2" cellspacing="0" class="Centerize">
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_Save" ValidationGroup="Submit" runat="server" Text="افـزوده شـود" CssClass="Buttons" Width="90px" OnClick="btn_Save_Clicked"/>
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
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
