<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetSelector.ascx.cs" Inherits="Views_SetSelector" %>
<% if (false) { %>
    <link href="../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
<% } %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="cmb_Sets" />
    </Triggers>
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="3">
            <tr>
                <td>
                    <asp:Label ID="lbl_Text" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmb_Categories" runat="server" CssClass="ComboBoxes" Width="150px"
                        AutoPostBack="true" OnSelectedIndexChanged="cmb_Categories_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="cmb_Sets" runat="server" CssClass="ComboBoxes" Width="150px" OnSelectedIndexChanged="cmb_Sets_SelectedIndexChanged">
                    </asp:DropDownList>
                    <% if (EnableValidation) { %>
                    <asp:RequiredFieldValidator ID="reg_Sets" ControlToValidate="cmb_Sets" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <% } %>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
