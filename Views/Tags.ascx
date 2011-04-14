<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tags.ascx.cs" Inherits="Views_Tags" %>
<% if (false)
   { %>
    <link href="../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
<% } %>
<div class="Cadr">
    <table border="0" cellpadding="3" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lbl_Text" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <%= TagsLinks %>
            </td>
        </tr>
    </table>
</div>