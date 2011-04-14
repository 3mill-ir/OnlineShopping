<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vote.ascx.cs" Inherits="Views_Vote" %>
<% if (false)
   { %>
    <link href="../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
<% } %>

<asp:HiddenField ID="hidden_MemberID" runat="server" />
<table border="0" cellpadding="0" cellspacing="0" class="Vote">
    <tr>
        <td align="center" style="padding : 9px 5px 3px 5px;">
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Yes" OnClick="Btn_Yes_Clicked"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="center" class="VoteCounts">
            <div class="VotesCountDiv">
                <asp:Label ID="lbl_Counter" runat="server" Text="" CssClass="VotesCountLabel"></asp:Label>
            </div>
        </td>
    </tr>
    <tr>
        <td align="center" style="padding : 3px 5px 3px 5px;">
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="No" OnClick="Btn_No_Clicked"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td style="padding : 3px 0px 3px 0px;">
            <% if (IsInUpdatePanel)
               { %>
            <asp:UpdateProgress ID="progress" runat="server">
                <ProgressTemplate>
                    <div class="Loading_Circle_2 Centerize"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <% } %>
        </td>
    </tr>
</table>
