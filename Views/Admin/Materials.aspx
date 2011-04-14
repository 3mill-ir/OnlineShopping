<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="Materials.aspx.cs" Inherits="Views_Admin_Materials" %>

<%@ Register Src="~/Views/SetSelector.ascx" TagPrefix="Myc" TagName="SetSelector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>:: اجنـاس ::</title>
    <script type="text/javascript" src="../../Scripts/Windows.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="padding : 10px 10px 10px 0px;">
        <div class="PageTitle Title_Materials" style="margin-bottom : 15px;"></div>
        <div class="ListTitle">
            <a class="Link" onclick="return OpenCenter('NewCategory.aspx', 'NewSet', 550, 150);">ایجاد دسته بندی جدید</a>
        </div>
        <div class="ListContent">
            <asp:DataList ID="DataList_Categories" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" ItemStyle-Width="33%" Width="100%" 
                OnItemCommand="DataList_Categories_ItemCommand">
                <ItemTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DataListItem">
                        <tr>
                            <td style="width : 40px;">
                                <div class="DataListSubItem">
                                    <%# Eval("Sequence") %>
                                </div>
                            </td>
                            <td style="padding-right : 10px;">
                                <%# Eval("Name") %>
                            </td>
                            <td style="width : 20px; padding-left : 10px;">
                                <a class="Edit" onclick="return OpenCenter('<%# "EditCategory.aspx?CategoryID=" + Eval("CategoryID").ToString() %>', 'EditCategory', 550, 150);"></a>
                            </td>
                            <td style="width : 12px; padding-left : 10px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('تمام اطلاعات مربوط به دسته حذف خواهد شد ، آیا اطمینان دارید؟')" CommandName="DeleteCategory" CommandArgument='<%# Eval("CategoryID") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="ListTitle" style="margin-top : 15px;">
            <table border="0" cellpadding="3" cellspacing="0">
                <tr>
                    <td>
                        <asp:DropDownList ID="cmb_Categories" runat="server" CssClass="ComboBoxes" 
                            Width="200px" AutoPostBack="true" 
                            onselectedindexchanged="cmb_Categories_SelectedIndexChanged">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <a class="Link" style="margin-right : 10px;" onclick="return OpenCenter('NewSet.aspx', 'NewSet', 600, 180);">ایجاد مجموعه جدید</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="ListContent">
            <asp:DataList ID="DataList_Sets" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" ItemStyle-Width="33%" Width="100%" 
                OnItemCommand="DataList_Sets_ItemCommand">
                <ItemTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%" class="DataListItem">
                        <tr>
                            <td style="width : 40px;">
                                <div class="DataListSubItem">
                                    <%# Eval("Sequence") %>
                                </div>
                            </td>
                            <td style="padding-right : 10px;">
                                <%# Eval("Name") %>
                            </td>
                            <td style="width : 20px; padding-left : 10px;">
                                <a class="Edit" onclick="return OpenCenter('<%# "EditSet.aspx?SetID=" + Eval("SetID").ToString() %>', 'EditSet', 600, 180);"></a>
                            </td>
                            <td style="width : 12px; padding-left : 10px;">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('تمام اطلاعات مربوط به دسته حذف خواهد شد ، آیا اطمینان دارید؟')" CommandName="DeleteSet" CommandArgument='<%# Eval("SetID") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="ListTitle" style="margin-top : 15px;">
            <table border="0" cellpadding="3" cellspacing="0">
                <tr>
                    <td>
                        <Myc:SetSelector runat="server" ID="myc_SetSelector" AutoPostBack="true" OnSetSelected="SetSelector_SetSelected"/>
                    </td>
                    <td>
                        <a class="Link" style="margin-right : 10px;" onclick="return OpenCenter('NewMaterial.aspx', 'NewSet', 720, 620);">افزودن کالای جدید</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="ListContent">
            <div id="MaterialInfo" class="MaterialInfoDiv"></div>
            <asp:DataList ID="DataList_Materials" runat="server" RepeatColumns="4" 
                RepeatDirection="Horizontal" CssClass="Centerize" 
                ItemStyle-CssClass="ImagesDataListItem" ItemStyle-HorizontalAlign="Center" 
                ItemStyle-VerticalAlign="Bottom" onitemcommand="DataList_Materials_ItemCommand">
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <%# Eval("Link") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top : 3px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border : solid 1px #bcbec0; background-color : #ededee;">
                                    <tr>
                                        <td align="center" style="padding : 4px 0px 4px 0px; width : 50%;">
                                            <a class="Edit" onclick="return OpenCenter('<%# "EditMaterial.aspx?MaterialID=" + Eval("MaterialID") %>', 'EditMaterial', 720, 620);"></a>
                                        </td>
                                        <td align="center" style="padding : 4px 0px 4px 0px; width : 50%;">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="DeleteSmallButton" OnClientClick="return confirm('آیا اطمینان دارید؟');" CommandName="DeleteMaterial" CommandArgument='<%# Eval("MaterialID") %>'></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>

