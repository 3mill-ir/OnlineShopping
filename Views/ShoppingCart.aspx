<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="Views_ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%="سبد خرید" + " | " + Constants.StaticTitle%></title>
    <meta name="Description" content='<%= Constants.StaticDescription %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
    <style type="text/css"> 
        #MainContent a:hover { text-decoration : underline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="MainContent" class="ContentMainDiv" style="padding : 15px;">
        <asp:Panel ID="pnl_Cart" runat="server" Visible="true">
            <div class="Title_ShoppingCart" style="margin-bottom : 5px;"></div>
            <asp:HiddenField ID="hid_Total" runat="server" Value=""/>
            <asp:GridView ID="Grid_Cart" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" CssClass="Grid">
                <HeaderStyle CssClass="GridHeader"/>
                <Columns>
                    <asp:BoundField DataField="Index" HeaderText="ردیف" ItemStyle-CssClass="GridItem"/>
                    <asp:TemplateField HeaderText="نام کالا" ItemStyle-CssClass="GridItem">
                        <ItemTemplate>
                            <span><%# Eval("MaterialName")%></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="قیمت واحد" ItemStyle-CssClass="GridItem">
                        <ItemTemplate>
                            <span><%# Eval("Price") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تعداد" ItemStyle-CssClass="GridItem">
                        <ItemTemplate>
                            <asp:HiddenField ID="hidden_MaterialID" runat="server" Value='<%# Eval("MaterialID") %>' />
                            <asp:TextBox ID="tb_Quantity" runat="server" CssClass="TextBoxes CenterText" Width="30px" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="GridItem">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_Delete" runat="server" CssClass="DeleteSmallButton"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="CartGridFoot">
                <table border="0" cellpadding="0" cellspacing="0" style="float : left; margin : 5px 0px 0px 20px;">
                    <tr>
                        <td>
                            جمع کل :
                        </td>
                        <td style="padding-right : 5px;">
                            <asp:Label ID="lbl_TotalPrice" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="padding-right : 5px;">
                            <asp:LinkButton ID="btn_RefreshCart" runat="server" CssClass="Refresh" ToolTip="بروزرسانی" OnClick="btn_Refresh_Clicked"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <table border="0" cellpadding="0" cellspacing="0" class="Centerize" style="margin-top : 7px;">
                <tr>
                    <td>
                        <asp:LinkButton ID="btn_SaveCart" runat="server" CssClass="OkeyCart" OnClick="btn_SaveCart_Clicked"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_Empty" runat="server">
           <div class="EmptyList">
                سبد خـرید شمـا خـالی اسـت.
           </div>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

