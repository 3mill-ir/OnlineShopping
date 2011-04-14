<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" EnableViewState="false" CodeFile="Statistics.aspx.cs" Inherits="Views_Members_Statistics" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .ContentMainDiv a:hover { text-decoration : underline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px; min-height : 300px; position : relative;">    
        <Myc:PageTitle ID="PageTitle1" runat="server" Text="آمـار و ارقـام"/>
        <div style="margin-top : 10px;">
            <asp:Label ID="lbl_Statistics" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

