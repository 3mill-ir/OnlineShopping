<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" EnableViewState="true" CodeFile="Messages.aspx.cs" Inherits="Views_Members_Messages" %>
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
        <Myc:PageTitle ID="pageTitle" runat="server"/>
        <div style="margin-top : 10px;">
            <asp:HiddenField ID="hidden_ShowType" runat="server" Value=""/>
            <asp:HiddenField ID="hidden_MemberID" runat="server" Value=""/>
            <asp:GridView ID="GridMessages" runat="server" AutoGenerateColumns="false" 
                GridLines="None" Width="100%" CssClass="Grid2" ShowHeader="false" 
                onrowcommand="GridMessages_RowCommand">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="GridItem2" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <%# Eval("Subject") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="GridItem2" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <%# Eval("ToFrom") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="GridItem2" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("DateOfX") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="GridItem2" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="DeleteSmallButton" CommandName="DeleteMessage" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('آیـا اطمینـان دارید؟');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Panel ID="pnl_Details" runat="server" CssClass="Cadr" style="padding : 15px; margin-top : 10px;" Visible="false">
            <asp:Label ID="lbl_Details" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

