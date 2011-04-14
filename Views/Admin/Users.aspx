<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Views_Admin_Members" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

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
        <asp:HiddenField ID="hidden_Role" runat="server" Value=""/>
        <Myc:PageTitle runat="server" Text="کاربـران"/>
        <div class="ListTitle" style="margin-top : 10px;">
            <asp:DropDownList ID="cmb_Role" runat="server" CssClass="ComboBoxes" AutoPostBack="true"
                Width="150px" OnSelectedIndexChanged="cmb_Role_SelectedIndexChanged">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Text="مشتـریان" Value='<%= MyRoles.Member %>'></asp:ListItem>
                <asp:ListItem Text="فـروشگاه ها" Value='<%= MyRoles.Shop %>'></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="ListContent" style="padding : 15px;">
            <asp:Panel ID="pnl_Users" runat="server" Visible="true">
                <asp:GridView ID="Grid_Users" runat="server" Width="100%" CssClass="Grid2" AutoGenerateColumns="false" 
                    GridLines="None" OnRowCreated="Grid_Orders_RowCreated" AllowPaging="true" 
                    PageSize="15" OnPageIndexChanging="Grid_Users_PageIndexChanging">
                    <HeaderStyle Font-Bold="false" CssClass="GridHeader2"/>
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="فعال">
                            <ItemTemplate>
                                <asp:CheckBox ID="ch_IsUserApproved" runat="server" Checked='<%# Eval("IsApproved") %>' AutoPostBack="true" ValidationGroup='<%# Eval("UserName") %>' OnCheckedChanged="ch_IsUserApproved_ChechChnaged"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="نام کاربری">
                            <ItemTemplate>
                                <%# Eval("UserName_Link") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="تاریخ عضویت">
                            <ItemTemplate>
                                <%# Eval("DateOfJoin") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="تعداد ورودها">
                            <ItemTemplate>
                                <%# Eval("VisitsCount") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="آخرین ورود">
                            <ItemTemplate>
                                <%# Eval("LastLogin") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="">
                            <ItemTemplate>
                                <%# Eval("EditLink") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="pnl_Shops" runat="server" Visible="false">
                <asp:GridView ID="Grid_Shops" runat="server" Width="100%" CssClass="Grid2" 
                    AutoGenerateColumns="false" GridLines="None"
                    OnRowCreated="Grid_Orders_RowCreated" AllowPaging="true" PageSize="15">
                    <HeaderStyle Font-Bold="false" CssClass="GridHeader2"/>
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="فعال" ItemStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="ch_IsShopApproved" runat="server" Checked='<%# Eval("IsApproved") %>' AutoPostBack="true" ValidationGroup='<%# Eval("UserName") %>' Enabled='<%# Eval("HasUserName") %>' OnCheckedChanged="ch_IsShopApproved_CheckChange"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="نام فروشگاه">
                            <ItemTemplate>
                                <%# Eval("Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="اجناس">
                            <ItemTemplate>
                                <%# Eval("MaterialsCount") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="تاریخ عضویت">
                            <ItemTemplate>
                                <%# Eval("DateOfJoin") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="ورودها">
                            <ItemTemplate>
                                <%# Eval("VisitsCount") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="آخرین ورود">
                            <ItemTemplate>
                                <%# Eval("LastLogin") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="GridItem2" HeaderText="">
                            <ItemTemplate>
                                <%#Eval("EditLink") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

