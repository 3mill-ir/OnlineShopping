<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Gallery.ascx.cs" Inherits="Views_Gallery" %>
<% if (false)
   { %>
    <link href="../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
<% } %>
<asp:Panel ID="pnl_Main" runat="server" >
    <div class="GalleryThumbnails">
        <% if (IsInUpdatePanel)
           { %>
            <div style="float : left; margin : 25px 0px 0px 15px;">
                <asp:UpdateProgress ID="progresss" runat="server" >
                    <ProgressTemplate>
                        <div class="Loading_Line"></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        <% } %>
        <asp:DataList ID="DataList_Thumbnails" runat="server" RepeatColumns="10" 
            OnItemCommand="DataList_Thumbnails_ItemCommand">
            <ItemTemplate>
                <asp:Panel ID="pnl_Thumbnail" runat="server" CssClass='<%# ( (bool)Eval("IsSelected") ? "GallerySelectedThumbnail" : "GalleryThumbnailContainter") %>'>
                    <asp:ImageButton ID="btn_Thumbnail" runat="server" Enabled='<%# !(bool)Eval("IsSelected") %>' CssClass='<%# ( (bool)Eval("IsSelected") ? "GallerySelectedThumbniailImage" : "SmallThumbnail") %>' BorderStyle="Solid" BorderWidth="1px" BorderColor="#bcbec0" ImageUrl='<%# Eval("Url") %>' Width='<%# Eval("Width") %>' Height='<%# Eval("Height") %>' CommandName="ShowImage" CommandArgument='<%# Eval("PictureID") + ";" + Eval("PicName") %>'/>
                </asp:Panel>
                <asp:Panel ID="pnl_Arrow" runat="server" CssClass='<%# ( (bool)Eval("IsSelected") ? "GallerySelectedBottom" : "") %>' style="height : 10px;">
                </asp:Panel>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <asp:Panel ID="pnl_LargeImage" runat="server" CssClass="GalleryShowingPicDiv" style="padding : 15px;" Visible="false">
        <table border="0" cellpadding="0" cellspacing="0" class="Centerize">
            <tr>
                <td>
                    <asp:Image ID="img_MaterialPicture" runat="server" CssClass="LargeImage" BorderStyle="Solid"
                        BorderWidth="1px" BorderColor="#bcbec0" BackColor="#bcbec0" Visible="false"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>