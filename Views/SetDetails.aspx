<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" EnableViewState="false" CodeFile="SetDetails.aspx.cs" Inherits="Views_SetDetails" %>
<%@ Register Src="~/Views/Collection.ascx" TagName="Collection" TagPrefix="myc" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%=Set.Name + " | " + Constants.StaticTitle%></title>
    <meta name="Description" content='<%= Constants.StaticDescription + " , " + Set.Meta_Description %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
    <link href="../Scripts/ThickBox/Assets/css/all.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/ThickBox/Assets/js/mootools-core-1.3-full-compat-yc.js"></script>
    <script type="text/javascript" src="../Scripts/ThickBox/Source/slideGallery.js"></script>
    <script type="text/javascript" src="../Scripts/ThickBox/Assets/js/Settings.js"></script>
    <style type="text/css">
        #MainContent a:hover {text-decoration : underline;}
        .Bars a:hover {text-decoration : underline; color : #ca6c18;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hid_SetID" runat="server" Value="" EnableViewState="true"/>
    <asp:HiddenField ID="hidden_SortField" runat="server" Value="1" EnableViewState="true"/>
    <asp:HiddenField ID="hidden_SortType" runat="server" Value="2" EnableViewState="true"/>
    <div id="MainContent" class="ContentMainDiv">
        <div style="margin : 15px;">
            <% if (DataList_Materials.Items.Count > 0)
               { %>
                 <div style="margin : 5px 5px 10px 5px;">
                    <Myc:PageTitle ID="title" runat="server" Text=""/>
                 </div>
                <div class="ListTitle" style="margin : 0px 5px 5px 5px;">
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                مرتب سازی بر اساس :
                            </td>
                            <td>
                                <asp:DropDownList ID="cmb_SortField" runat="server" CssClass="ComboBoxes" Width="150px">
                                    <asp:ListItem Text="زمـان افزودن کـالا" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="محبوبیت کـالا" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="تعداد یـازدیدها" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="قیمـت کـالا" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding-right : 25px;">
                                نمایش بصورت :
                            </td>
                            <td>
                                <asp:DropDownList ID="cmb_SortType" runat="server" CssClass="ComboBoxes" Width="80px">
                                    <asp:ListItem Text="نـزولی" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="صعـودی" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding-right : 10px;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Go" OnClick="btn_Go_Clicked"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:DataList ID="DataList_Materials" runat="server" Width="100%" RepeatColumns="2" RepeatDirection="Horizontal" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" ItemStyle-Width="50%">
                    <ItemTemplate>
                        <div style="margin : 5px;">
                            <%# Eval("Link") %>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="DataListPaging" style="margin-top : 10px;">
                    <div style="position : absolute; right : 10px; top : 8px; color : White;">
                        <asp:Label ID="lbl_PageCounter" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:HiddenField ID="hidden_PageIndex" runat="server" Value="0" EnableViewState="true"/>
                    <table border="0" cellpadding="0" cellspacing="0" class="Centerize">
                        <tr>
                            <td>
                                <asp:LinkButton ID="btn_Previous" runat="server" CssClass="Paging_Previous" OnClick="btn_Previous_Click" ></asp:LinkButton>
                            </td>
                            <td style="padding : 0px 10px 0px 10px; color : White;">
                                <asp:Label ID="lbl_ShowingRecords" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton ID="btn_Next" runat="server" CssClass="Paging_Next" OnClick="btn_Next_Click" ></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top : 10px;">
                    <Myc:PageTitle ID="title_Populars" runat="server" Text="محبوبتـرین کـالاهـای این دستـه :"/>
                </div>
                <div style="margin-top : 10px;">
                    <asp:Label ID="lbl_Populars" runat="server" Text="Label"></asp:Label>
                </div>
            <% }
               else
               { %>
               <div class="EmptyList">
                    کالایی در این دسته موجود نمی باشد.
               </div>
            <% } %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
    <div class="InnerTitle_Links">
    </div>
    <div class="Bars" style="padding : 10px 20px 10px 20px; margin-top : 5px; line-height : 25px; text-align : justify;">
        <asp:Label ID="lbl_Links" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>