<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" EnableViewState="false" CodeFile="CategoryDetails.aspx.cs" Inherits="Views_CategoryDetails" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%= Category.Name + " | " + Constants.StaticTitle %></title>
    <meta name="Description" content='<%= Constants.StaticDescription %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords + " , " + Category.Meta_Description %>'/>
    <style type="text/css">
        #SetLinks a:hover { background-color : #d8d9da; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <Myc:PageTitle runat="server" ID="title"/>
        <div id="SetLinks" style="margin-top : 10px;">
            <asp:DataList ID="DataList_Sets" runat="server" ItemStyle-Width="25%" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                <ItemTemplate>
                    <a class="ButtonLink" href='<%# Repository.ToHtmlUrl("~/Views/SetDetails.aspx") + "?SetID=" + Eval("SetID") %>'><%# Eval("Name") %></a>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>

