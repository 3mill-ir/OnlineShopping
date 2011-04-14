<%@ Page Language="C#" MasterPageFile="~/Views/Main.master" EnableViewState="false" AutoEventWireup="true" CodeFile="RetrieveMaterials.aspx.cs" Inherits="Views_RetrieveMaterials" Title="Untitled Page" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%= "کالاهای مشابه" + " | " + Constants.StaticTitle %></title>
    <meta name="Description" content='<%= Constants.StaticDescription %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
    <style type="text/css">
        #MainContent a:hover {text-decoration : underline;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="MainContent" class="ContentMainDiv" style="padding : 15px;">
        <% if (DataList_Materials.Items.Count > 0)
           { %>
             <div style="margin : 5px 5px 10px 5px;">
                <Myc:PageTitle ID="title" runat="server" Text="کـالاهای مشـابه"/>
             </div>
             <div class="Subject" style="margin : 5px 5px 5px 5px;">
                 <asp:Label ID="lbl_Subject" runat="server" Text=""></asp:Label>
             </div>
            <asp:DataList ID="DataList_Materials" runat="server" Width="100%" RepeatColumns="2" RepeatDirection="Horizontal" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Bottom" ItemStyle-Width="50%">
                <ItemTemplate>
                    <div style="margin : 5px;">
                        <%# Eval("Link") %>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <div class="DataListPaging" style="margin : 5px 5px 10px 5px;">
                <div style="position : absolute; right : 10px; top : 8px; color : White;">
                    <asp:Label ID="lbl_PageCounter" runat="server" Text=""></asp:Label>
                </div>
                <asp:HiddenField ID="hidden_PageIndex" runat="server" Value="0" EnableViewState="true"/>
                <asp:HiddenField ID="hid_QueryType" runat="server" Value="" EnableViewState="true"/>
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
        <% }
           else
           { %>
           <div class="EmptyList">
                کالایی جهت نمایش یافت نشد.
           </div>
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

