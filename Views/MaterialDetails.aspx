<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="MaterialDetails.aspx.cs" Inherits="Views_MaterialDetails" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>
<%@ Register Src="~/Views/Gallery.ascx" TagPrefix="Myc" TagName="Gallery" %>
<%@ Register Src="~/Views/Vote.ascx" TagPrefix="Myc" TagName="Vote" %>
<%@ Register Src="~/Views/Tags.ascx" TagPrefix="Myc" TagName="Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%= Material.Name + " | " + Constants.StaticTitle%></title>
    <meta name="Description" content='<%= Constants.StaticDescription + " , " + Material.Meta_Description %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
    <script type="text/javascript" src="../Scripts/Windows.js"></script>
    <style type="text/css">
        #MainContent a:hover {text-decoration : underline;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hid_MaterialID" runat="server" Value=""/>
    <asp:HiddenField ID="hid_MaterialType" runat="server" Value=""/>
    <asp:HiddenField ID="hid_MemberID" runat="server" Value=""/>
    <div class="HiddenKeywords">
        <% if (!Page.IsPostBack)
           { %>
        <h1><%=Material.Meta_Description %></h1>
        <% } %>
    </div>
    <div id="MainContent" class="ContentMainDiv">
        <div style="margin : 15px;">
            <table border="0" cellpadding="0" cellspacing="0" style="float : left;">
                <tr>
                    <td>
                        <asp:LinkButton ID="btn_AddToCart" runat="server" CssClass="AddToCartBig" OnClick="btn_AddToCart_Clicked" EnableViewState="false"></asp:LinkButton>
                    </td>
                    <td style="padding-right : 7px;">
                        <asp:LinkButton ID="btn_AddToCompare" runat="server" CssClass="AddToCompare" EnableViewState="false" OnClick="btn_AddToCompare_Clicked"></asp:LinkButton>
                    </td>
                </tr>
            </table>
             <div style="margin : 5px 0px 20px 0px;">
                <Myc:PageTitle ID="title" runat="server" Text="" EnableViewState="false"/>
             </div>
             <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top" style="padding-left : 10px;">
                        <div>
                            <asp:UpdatePanel ID="Up_Vote" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <Myc:Vote runat="server" ID="vote" IsInUpdatePanel="true" AssociatedUpdatePanelID="Up_Vote" OnYesClicked="vote_YesClicked"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                         </div>
                    </td>
                    <td valign="top" style="width : 100%;">
                        <div style="min-height : 100px; line-height : 25px; text-align : justify;" class="Cadr">
                             <asp:Label ID="lbl_Description" runat="server" Text="" EnableViewState="false"></asp:Label>
                        </div>
                    </td>
                </tr>
             </table>
             <div style="margin-top : 10px;">
                 <asp:UpdatePanel ID="Up_Gallery" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <Myc:Gallery runat="server" ID="gallery" IsInUpdatePanel="true" AssociatedUpdatePanelID="Up_Gallery"/>
                    </ContentTemplate>
                 </asp:UpdatePanel>
             </div>
             <div style="margin-top : 10px;">
                 <asp:Label ID="lbl_Fields" runat="server" Text=""></asp:Label>
             </div>
             <div class="Cadr" style="margin-top : 10px;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="قیمت :" EnableViewState="false"></asp:Label>
                        </td>
                        <td style="padding-right : 10px;">
                            <asp:Label ID="lbl_Price" runat="server" Text="" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                </table>
             </div>
             <% if (User.IsInRole(MyRoles.Member))
                { %>
                <asp:Panel ID="pnl_Buyers" CssClass="Cadr" runat="server" style="margin-top : 10px;" Visible="false">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="تعدادی از خـریداران :" EnableViewState="false"></asp:Label>
                            </td>
                            <td style="padding-right : 5px;">
                                <asp:Label ID="lbl_Buyers" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            <% } %>
            <div class="Cadr" style="margin :10px 0px 10px 0px;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="برچسب ها :" EnableViewState="false"></asp:Label>
                        </td>
                        <td style="padding-right : 5px;">
                            <asp:Label ID="lbl_Tags" runat="server" Text="" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                </table>
             </div>
        </div>
    </div>
</asp:Content>

