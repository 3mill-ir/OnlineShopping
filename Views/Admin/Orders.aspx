<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Views_Admin_Orders" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle"%>
<%@ Register Src="~/Views/DateSelector.ascx" TagPrefix="Myc" TagName="DateSelector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../Scripts/Windows.js"></script>
    <style type="text/css">
        .ContentMainDiv a:hover { text-decoration : underline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <Myc:PageTitle runat="server" Text="سفـارشـات"/>
        <div class="ListTitle" style="margin-top : 10px;">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <Myc:DateSelector runat="server" ID="dateSelector"/>
                    </td>
                    <td style="padding-right : 5px;">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Go" 
                            onclick="LinkButton1_Click"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="ListContent" style="padding : 15px;">
            <% if (Grid_Orders.Rows.Count > 0)
               { %>
            <asp:GridView ID="Grid_Orders" runat="server" CssClass="Grid2" Width="100%" 
                AllowPaging="true" PageSize="15" AutoGenerateColumns="false" 
                OnPageIndexChanging="Grid_Orders_PageIndexChanging" 
                OnRowCreated="Grid_Orders_RowCreated">
                <HeaderStyle CssClass="GridHeader2"/>
                <Columns>
                    <asp:TemplateField HeaderText="سفارش دهنده" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Member") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تعداد اقلام" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Count") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="مبلغ کل" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Total") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="وضعیت" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <%# Eval("Status") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="GridItem2">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="Link" NavigateUrl='<%# "~/Views/Admin/OrderDetails.aspx?OrderID=" + Eval("OrderID") %>'>جزئیـات سفارش</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <% }
               else
               { %>
               <div class="EmptyList">
                موردی جهت نمایش یافت نشد!
               </div>
            <% } %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

