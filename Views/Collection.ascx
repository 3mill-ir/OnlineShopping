<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Collection.ascx.cs" Inherits="Views_Collection" %>
<% if (Visible && Direction == CollectionDirection.Horizontal)
   { %>
    <table border="0" cellpadding="0" cellspacing="0" style="height : 23px; background-color : #808285;">
        <tr>
            <td>
                <img src="../Content/Themes/Default/CollectionTitle_Right.png"/>
            </td>
            <td style="background-color : #808285; padding : 0px 5px 0px 5px; color : White;">
                <%= Title %>
            </td>
            <td>
                <img src="../Content/Themes/Default/CollectionTitle_Left.png"/>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Pics" runat="server" BackColor="#f1f2f2" BorderStyle="Solid" BorderWidth="1px" BorderColor="#808285" style="padding : 10px; overflow : hidden;">
        <asp:Label ID="lbl_Pics" runat="server" Text="Label"></asp:Label>
    </asp:Panel>
<% }
   else if (Visible && Direction == CollectionDirection.Vertical)
   { %>
    <%= VerticalHtml %>
<% } %>