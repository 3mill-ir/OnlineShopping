<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Views_Members_Orders" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #MainContent a:hover { text-decoration : underline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hidden_MemberID" runat="server" />
    <div id="MainContent" class="ContentMainDiv" style="padding : 15px;">
        <% if (Request.QueryString["From"] != null && Request.QueryString["From"].ToString() == "ShoppingCart")
           { %>
            <div class="CartSuccessfullySaved" style="margin-bottom : 10px;"></div>
        <% } %>
        <Myc:PageTitle ID="PageTitle1" runat="server" Text="لیست سفـارشـات شمـا"/>
        <div style="margin-top : 10px;">
            <asp:GridView ID="Grid_Orders" runat="server" CssClass="Grid2" Width="100%" 
                AllowPaging="true" PageSize="10" AutoGenerateColumns="false" 
                onpageindexchanging="Grid_Orders_PageIndexChanging" 
                onrowcreated="Grid_Orders_RowCreated">
                <HeaderStyle CssClass="GridHeader2"/>
                <Columns>
                    <asp:TemplateField HeaderText="تاریخ سفـارش" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("DateOfOrder") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تعداد اقلام" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Count") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="مبلغ کل" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Cost") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="وضعیت" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Status") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

