<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>:: <%=Constants.StaticTitle %> ::</title>
    <!-- Meta Tags -->
    <meta name="Description" content='<%= Constants.StaticDescription %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
    <meta name="google-site-verification" content="l2Yh8b1Tb6jp7eXspBFAlXS239_NEj1Jmhu_-ED4Bo8" />
    <meta name="y_key" content="88f70d2590cbb091" />
    <!-- Meta Tags -->
    <!-- Tickbox -->
    <link href="Scripts/ThickBox/Assets/css/all.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="Scripts/ThickBox/Assets/js/mootools-core-1.3-full-compat-yc.js"></script>
    <script type="text/javascript" src="Scripts/ThickBox/Source/slideGallery.js"></script>
    <script type="text/javascript" src="Scripts/ThickBox/Assets/js/Settings.js"></script>
    <script type="text/javascript" src="Scripts/ThickBox/Assets/js/SettingsForSecond.js"></script>
    <!-- Tickbox -->
    <!-- Start WOWSlider.com HEAD section -->
	<link rel="stylesheet" type="text/css" href="Scripts/Slider/engine/style.css" media="screen" />
	<style type="text/css">a#vlb{display:none}</style>
	<!-- End WOWSlider.com HEAD section -->
	<style type="text/css">
        #MainContent a:hover {text-decoration : underline;}
        .Bars a:hover {text-decoration : underline; color : #ca6c18;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="HiddenKeywords">
        <h1><%=Constants.StaticDescription + " , " + Constants.StaticKeywords %></h1>
    </div>
    <div id="MainContent" style="margin : 10px 10px 0px 0px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td valign="top" style="padding : 0px 0px 0px 5px;">
                    <div class="Bars">
                        <% if (HasSlidingImages)
                           { %>
                            <!-- Start WOWSlider.com BODY section -->
                            <div id="wowslider-container">
                                <div id="wowslider-images">
                                    <%=SlidingImages_Html %>
                                </div>
                                <div class="ws_bullets">
                                    <%=Bullets_Html %>
                                </div>
                            </div>
                            <script type="text/javascript" src="Scripts/Slider/engine/script.js"></script>
                            <!-- End WOWSlider.com BODY section -->
                        <% } %>
                        <div style="padding : 0px 10px 10px 10px;">
                            <div class="FirstPageTitle_AboutUs"></div>
                            <div class="Statement" style="margin : 5px;">
                                <%= System.IO.File.ReadAllText(Server.MapPath(Constants.TextsRoot + "AboutUs.txt")) %>
                            </div>
                            <div class="FirstPageTitle_HowToBuy" style="margin-top : 10px;"></div>
                            <div class="Statement" style="margin : 5px;">
                                <%= System.IO.File.ReadAllText(Server.MapPath(Constants.TextsRoot + "HowToBuy.txt")) %>
                            </div>
                            <div class="FirstPageTitle_Why" style="margin-top : 10px;"></div>
                            <div class="Statement" style="margin : 5px;">
                                <%= System.IO.File.ReadAllText(Server.MapPath(Constants.TextsRoot + "Why.txt")) %>
                            </div>
                            <div class="FirstPageTitle_InviteShops" style="margin-top : 10px;"></div>
                            <div class="Statement" style="margin : 5px;">
                                <%= System.IO.File.ReadAllText(Server.MapPath(Constants.TextsRoot + "InviteShops.txt")) %>
                            </div>
                        </div>
                    </div>
                </td>
                <td valign="top" style="padding : 0px 5px 0px 0px; width : 200px;">
                    <div class="FirstPageTitle_RandomMaterials" style="margin-bottom : 5px;"></div>
                    <div class="Bars" style="padding : 10px;">
                        <asp:DataList ID="DataList_Random" runat="server" Width="100%">
                            <ItemTemplate>
                                <div style="padding-bottom : 5px;">
                                    <%# Eval("Info") %>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
    <div class="FirstPageTitle_NewMaterials" style="margin-bottom : 5px;"></div>
    <div class="Bars" style="padding : 10px;">
        <asp:DataList ID="DataList_News" runat="server" Width="100%">
            <ItemTemplate>
                <div style="padding-bottom : 5px;">
                    <%# Eval("Info") %>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top : 10px; z-index : 1100;">
        <tr>
            <td>
                <div class="FirstPageTitle_Populars"></div>
            </td>
            <td style="padding-right : 10px;">
                <div class="Bars" style="padding : 10px;">
                    <asp:Label ID="lbl_Populars" runat="server" Text=""></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    <% if (User.IsInRole(MyRoles.Member))
       { %>
    <asp:Panel ID="pnl_Favorites" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top : 10px;">
            <tr>
                <td>
                    <div class="FirstPageTitle_Suggestions"></div>
                </td>
                <td style="padding-right : 10px;">
                    <div class="Bars" style="padding : 10px;">
                        <asp:Label ID="lbl_Suggestions" runat="server" Text=""></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <% } %>
    <div class="Bars" style="margin-top : 10px; padding : 10px 20px 10px 20px; line-height : 25px; text-align : justify;">
        <asp:Label ID="lbl_Tags" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_UnderFooter" Runat="Server">
    <!-- Statistics : حتمـــاً پایین تر از بقیـــه باشــــــد -->
    <div style="position : relative; margin-top : 10px;">
        <script type="text/javascript" language="javascript" src="http://www.webgozar.ir/c.aspx?Code=2103703&amp;t=counter" ></script>
        <noscript><a href="http://www.webgozar.com/counter/stats.aspx?code=2103703" target="_blank">&#1570;&#1605;&#1575;&#1585;</a></noscript>
        <% if(!User.IsInRole(MyRoles.Admin)) { %>
            <div style="background-color : White; width : 100%; height: 120px; position : absolute; top : 0px; left : 0px; z-index : 1000;"></div>
        <% } %>
    </div>
</asp:Content>

