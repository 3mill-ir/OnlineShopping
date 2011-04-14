<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Keywords.ascx.cs" Inherits="Views_Keywords" %>

<% if (false) { %>
    <link href="../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
<% } %>

<asp:UpdatePanel ID="Up_KeywordsControl" runat="server">
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding : 0px 0px 0px 5px;">
                    <asp:Label ID="lbl_Text" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <asp:TextBox ID="tb_Keyword" runat="server" Width="200px" CssClass="TextBoxes"></asp:TextBox>
                            </td>
                            <td valign="top" style="padding : 5px 0px 0px 0px;">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btn_AddKeyword" runat="server" CssClass="AddButton" Width="25px" Height="25px" OnClick="AddButton_Click"></asp:LinkButton>
                                        </td>
                                        <td style="width : 26px;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Up_KeywordsControl">
                                                <ProgressTemplate>
                                                    <div class="LoadingCircle" style="margin : 0px 5px 0px 5px;"></div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lbl_Statements" runat="server" Text="" CssClass="SmallGray"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="padding-top : 3px;">
                    <asp:DataList ID="list_Keywords" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" OnItemCommand="Datalist_Keywords_ItemCommand">
                        <ItemTemplate>
                            <asp:Panel ID="pnl_Item" runat="server" CssClass="KeywordsItem" Width='<%# Eval("ItemWidth") %>'>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="LabelTest2" Text='<%# Repository.CutString(Eval("Keyword"), (int)Eval("MaxLength")) %>' runat="server" />
                                        </td>
                                        <td valign="top" style="width : 10px;">
                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="ProfileItemDelete" CommandName="DeleteKeyword" CommandArgument='<%# Eval("ID") + ";" + Eval("Keyword") %>'></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
