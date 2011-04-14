<%@ Page Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" EnableViewState="false" CodeFile="CompareMaterials.aspx.cs" Inherits="Views_CompareMaterials" Title="Untitled Page" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%="مقایسه | " + Constants.StaticTitle %></title>
    <meta name="Description" content='<%= Constants.StaticDescription %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <Myc:PageTitle runat="server" Text="مقـایسـه کـالاهـا"/>
        <div style="margin-top : 10px;">
            <%= CompareCart.Current.ToHtml() %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

