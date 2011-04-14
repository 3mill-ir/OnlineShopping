<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialPictures.aspx.cs" Inherits="Views_Admin_MaterialPictures" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: تصاویر کالا ::</title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="Hidden_MaterialID" runat="server" Value=""/>
    <div class="ListTitle" style="text-align : center; margin : 10px;">
        <asp:Label ID="lbl_Title" runat="server" Text=""></asp:Label>
    </div>
    <div style="margin : 10px;">
        <table border="0" cellpadding="3" cellspacing="0" class="Centerize">
            <tr>
                <td>
                    <asp:FileUpload ID="uploader" runat="server" CssClass="Uploader" Width="250px"/>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="ارسـال" CssClass="Buttons" 
                        Width="70px" onclick="Button1_Click"/>
                </td>
                <td>
                    <asp:CheckBox ID="ch_Avatar" runat="server" Text="تصویر اصلی شود"/>
                </td>
            </tr>
        </table>
    </div>
    <div style="border : solid 1px #939598; padding : 15px; margin : 10px;">
        <asp:DataList ID="DataList_Pics" runat="server" RepeatColumns="4" 
            RepeatDirection="Horizontal" CssClass="Centerize" 
            ItemStyle-CssClass="ImagesDataListItem" 
            OnItemCommand="DataList_Pics_ItemCommand" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ThumbnailUrl") %>' CssClass='<%# ((bool)Eval("IsAvatar") ? "AvatarImage" : "Image") %>' BorderStyle="Solid" BorderColor="#bcbec0" BorderWidth="1px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border : solid 1px #bcbec0; background-color : #ededee;">
                                <tr>
                                    <td align="center" style="padding : 4px 0px 4px 0px; width : 50%;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="Avatar" CommandName="SetAsAvatar" CommandArgument='<%# Eval("PictureID") %>'></asp:LinkButton>
                                    </td>
                                    <td align="center" style="padding : 4px 0px 4px 0px; width : 50%;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('آیا اطمینان دارید؟');" CommandName="DeletePicture" CommandArgument='<%# Eval("PictureID") %>'></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
