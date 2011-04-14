<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="Views_Members_SendMessage" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:HiddenField ID="hidden_RecieverID" runat="server" />
        <div style="margin : 10px;">
            <Myc:PageTitle ID="PageTitle1" runat="server" Text="ارسـال پیـام خصوصی"/>
        </div>
        <table border="0" cellpadding="3" cellspacing="0"  style="margin : 10px 20px 0px 20px;">
            <tr>
                <td align="left">
                    گیرنده پیام :
                </td>
                <td>
                    <div class="HighLight"><%=Request.QueryString["Name"].ToString() %></div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    عنـوان :
                </td>
                <td>
                    <asp:TextBox ID="tb_Subject" runat="server" CssClass="TextBoxes" Width="200px" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Subject" ValidationGroup="Submit" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" style="padding-top : 6px;">
                    متن پیام :
                </td>
                <td>
                    <asp:TextBox ID="tb_Text" runat="server" CssClass="TextBoxes TextArea" Width="350px" TextMode="MultiLine" Height="60px"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tb_Text" ValidationGroup="Submit" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div style="margin-top : 10px;">
            <asp:UpdatePanel ID="Up_Send" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" cellpadding="3" cellspacing="0" class="Centerize">
                        <tr>
                            <td>
                                <asp:LinkButton ID="btn_Send" runat="server" CssClass="SendMessage" ValidationGroup="Submit" OnClick="btn_Send_Clicked"></asp:LinkButton>
                            </td>
                            <td style="width : 16px;" align="right">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Up_Send">
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
    </div>
    </form>
</body>
</html>
