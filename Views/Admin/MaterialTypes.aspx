<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="MaterialTypes.aspx.cs" Inherits="Views_Admin_MaterialTypes" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../Scripts/Windows.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_UnderMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <Myc:PageTitle runat="server" Text="مـدیریت انـواع کـالاهـا"/>
        <div class="ListTitle" style="margin-top : 10px;">
            <a class="Link" onclick="return OpenCenter('NewMaterialType.aspx', 'NewMaterialType', 500, 100);">افـزودن نوع کـالای جدید</a>
        </div>
        <div class="ListContent">
            <asp:DataList ID="DataList_Types" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" ItemStyle-Width="33%" Width="100%" 
                onitemcommand="DataList_Types_ItemCommand">
                <ItemTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DataListItem">
                        <tr>
                            <td style="padding-right : 10px;">
                                <%# Eval("Name") %>
                            </td>
                            <td style="width : 20px; padding-left : 10px;">
                                <a class="Edit" onclick="return OpenCenter('<%# "EditMaterialType.aspx?TypeID=" + Eval("TypeID").ToString() %>', 'EditType', 500, 100);"></a>
                            </td>
                            <td style="width : 12px; padding-left : 10px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('آیا اطمینان دارید؟')" CommandName="DeleteType" CommandArgument='<%# Eval("TypeID") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="ListTitle" style="margin-top : 15px;">
            <asp:DropDownList ID="cmb_Types" runat="server" CssClass="ComboBoxes" 
                Width="170px" AutoPostBack="true" 
                onselectedindexchanged="cmb_Types_SelectedIndexChanged">
            </asp:DropDownList>
            <a class="Link" style="margin-right : 10px;" onclick="return OpenCenter('NewField.aspx', 'NewField', 500, 160);">افـزودن فیلد جدید</a>
        </div>
        <div class="ListContent">
            <asp:DataList ID="DataList_Fields" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" ItemStyle-Width="33%" Width="100%" 
                onitemcommand="DataList_Fields_ItemCommand">
                <ItemTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DataListItem">
                        <tr>
                            <td style="padding-right : 10px;">
                                <%# Eval("Name") %>
                            </td>
                            <td style="width : 20px; padding-left : 10px;">
                                <a class="Edit" onclick="return OpenCenter('<%# "EditField.aspx?FieldID=" + Eval("FieldID").ToString() %>', 'EditField', 500, 160);"></a>
                            </td>
                            <td style="width : 12px; padding-left : 10px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('آیا اطمینان دارید؟')" CommandName="DeleteField" CommandArgument='<%# Eval("FieldID") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="ListTitle" style="margin-top : 15px;">
            <a class="Link" onclick="return OpenCenter('NewFieldUnit.aspx', 'NewFieldUnit', 500, 100);">افـزودن واحـد جدید</a>
        </div>
        <div class="ListContent">
            <asp:DataList ID="DataList_units" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" ItemStyle-Width="33%" Width="100%" >
                <ItemTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DataListItem">
                        <tr>
                            <td style="padding-right : 10px;">
                                <%# Eval("Name") %>
                            </td>
                            <td style="width : 20px; padding-left : 10px;">
                                <a class="Edit" onclick="return OpenCenter('<%# "EditFieldUnit.aspx?UnitID=" + Eval("ID").ToString() %>', 'EditFieldUnit', 500, 100);"></a>
                            </td>
                            <td style="width : 12px; padding-left : 10px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('آیا اطمینان دارید؟')" CommandName="DeleteFieldUnit" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Foot" Runat="Server">
</asp:Content>

