<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="CartDetails.aspx.cs" Inherits="Views_Members_CartDetails" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #MainContent a:hover { text-decoration : underline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="MainContent" class="ContentMainDiv" style="padding : 15px;">
        <div style="float : left; font-weight : bold; border : solid 1px #c7c8ca; background-color : #f6f6f6; padding : 3px 7px 3px 7px;">
            <table border="0" cellpadding="3" cellspacing="0">
                <tr>
                    <td>
                        وضعیت :
                    </td>
                    <td>
                        <asp:Label ID="lbl_Status" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <Myc:PageTitle runat="server" Text="جـزئیات سفـارش"/>
        <div style="margin-top : 10px;">
            <asp:Label ID="lbl_Info" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

