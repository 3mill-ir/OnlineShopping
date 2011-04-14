using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;
using System.Web.Security;

public partial class Views_Admin_Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cmb_Role.SelectedIndex = 1;
            hidden_Role.Value = MyRoles.Member;
            MembersRepository mr = new MembersRepository();
            var members = mr.GetAllMembers();
            FillUsersGrid(members);
        }
    }
    protected void cmb_Role_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_Role.SelectedIndex > 0)
        {
            if (cmb_Role.SelectedIndex == 1)
            {
                MembersRepository mr = new MembersRepository();
                var members = mr.GetAllMembers();
                FillUsersGrid(members);
                //
                hidden_Role.Value = MyRoles.Member;
                pnl_Users.Visible = true;
                pnl_Shops.Visible = false;
            }
            else if (cmb_Role.SelectedIndex == 2)
            {
                ShopsRepository shr = new ShopsRepository();
                var shops = shr.GetShops();
                FillShopsGrid(shops);
                //
                hidden_Role.Value = MyRoles.Shop;
                pnl_Users.Visible = false;
                pnl_Shops.Visible = true;
            }
        }
        else
        {
            pnl_Users.Visible = false;
            pnl_Shops.Visible = false;
        }
    }
    private void FillUsersGrid(IEnumerable<Member> users)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(Int32));
        dt.Columns.Add("UserName", typeof(String));
        dt.Columns.Add("UserName_Link", typeof(String));
        dt.Columns.Add("DateOfJoin", typeof(String));
        dt.Columns.Add("VisitsCount", typeof(Int32));
        dt.Columns.Add("LastLogin", typeof(String));
        dt.Columns.Add("EditLink", typeof(String));
        dt.Columns.Add("IsApproved", typeof(Boolean));
        foreach (Member user in users)
        {
            DataRow row = dt.NewRow();
            row["ID"] = user.MemberID;
            row["UserName"] = user.UserName;
            row["UserName_Link"] = "<a class=\"Link\" onclick=\"return OpenCenter('" + Repository.ToHtmlUrl("~/Views/Admin/MemberStatistics.aspx") + "?MemberID=" + user.MemberID.ToString() + "', 'MemberStatistics', 700, 500);\">" + user.UserName + "</a>";
            row["DateOfJoin"] = ShamsiDateTime.MiladyToShamsi(user.DateOfJoin).ToMediumString();
            row["VisitsCount"] = user.Logins.Count();
            row["LastLogin"] = user.LastLogin != null ? ShamsiDateTime.MiladyToShamsi(user.LastLogin.DateOfLogin).ToMediumString() : "-----";
            row["EditLink"] = "<a class=\"Link\" onclick=\"return OpenCenter('" + Repository.ToHtmlUrl("~/Views/Admin/EditMemberProfile.aspx") + "?MemberID=" + user.MemberID.ToString() + "', 'EditMemberProfile', 720, 400);\">ویـرایش</a>";
            row["IsApproved"] = Membership.GetUser(user.UserName).IsApproved;
            dt.Rows.Add(row);
        }
        Grid_Users.DataSource = dt;
        Grid_Users.DataBind();
    }
    private void FillShopsGrid(IEnumerable<Shop> shops)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(Int32));
        dt.Columns.Add("Name", typeof(String));
        dt.Columns.Add("UserName", typeof(String));
        dt.Columns.Add("MaterialsCount", typeof(Int32));
        dt.Columns.Add("DateOfJoin", typeof(String));
        dt.Columns.Add("VisitsCount", typeof(Int32));
        dt.Columns.Add("LastLogin", typeof(String));
        dt.Columns.Add("EditLink", typeof(String));
        dt.Columns.Add("HasUserName", typeof(Boolean));
        dt.Columns.Add("IsApproved", typeof(Boolean));
        foreach (Shop shop in shops)
        {
            DataRow row = dt.NewRow();
            row["ID"] = shop.ShopID;
            row["Name"] = shop.Name;
            row["UserName"] = shop.UserName;
            row["MaterialsCount"] = shop.Materials.Count();
            row["DateOfJoin"] = ShamsiDateTime.MiladyToShamsi(shop.DateOfJoin).ToMediumString();
            row["VisitsCount"] = shop.Logins.Count();
            row["LastLogin"] = shop.LastLogin != null ? ShamsiDateTime.MiladyToShamsi(shop.LastLogin.DateOfLogin).ToMediumString() : "-----";
            row["EditLink"] = "<a class=\"Link\" onclick=\"return OpenCenter('" + Repository.ToHtmlUrl("~/Views/Admin/EditShop.aspx") + "?ShopID=" + shop.ShopID.ToString() + "', 'EditShop', '550', '550');\">ویـرایش</a>";
            row["HasUserName"] = shop.UserName.Trim().Length > 0 ? true : false;
            row["IsApproved"] = shop.UserName.Trim().Length > 0 ? Membership.GetUser(shop.UserName).IsApproved : false;
            dt.Rows.Add(row);
        }
        Grid_Shops.DataSource = dt;
        Grid_Shops.DataBind();
    }
    protected void Grid_Orders_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            TableCell td1 = (TableCell)e.Row.Controls[0];
            td1.Attributes["class"] = "GridPagerBack";
            ///////
            Table table1 = (Table)td1.Controls[0];
            table1.Attributes["class"] = "GridPager_LinksContainerTable";
            table1.Attributes["cellspacing"] = "3";
            ///////
            TableRow tr_LinkCellsContainer = (TableRow)table1.Rows[0];
            /////// ge link td :
            for (int i = 0; i < tr_LinkCellsContainer.Controls.Count; i++)
            {
                TableCell td = (TableCell)tr_LinkCellsContainer.Controls[i];
                try
                {
                    Label span = (Label)td.Controls[0];
                    span.CssClass = "GridPager_SelectedLink";
                }
                catch
                {
                    LinkButton link = (LinkButton)td.Controls[0];
                    link.CssClass = "GridPager_Links";
                }
            }
        }
    }
    protected void Grid_Users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Users.PageIndex = e.NewPageIndex;
        //
        MembersRepository mr = new MembersRepository();
        var members = mr.GetAllMembers();
        FillUsersGrid(members);
    }
    protected void ch_IsUserApproved_ChechChnaged(object sender, EventArgs e)
    {
        CheckBox ch_IsApproved = sender as CheckBox;
        string userName = ch_IsApproved.ValidationGroup;
        MembershipUser user = Membership.GetUser(userName);
        user.IsApproved = !user.IsApproved;
        Membership.UpdateUser(user);
        //
        ShopsRepository shr = new ShopsRepository();
        var shops = shr.GetShops();
        FillShopsGrid(shops);
    }
    protected void ch_IsShopApproved_CheckChange(object sender, EventArgs e)
    {
        CheckBox ch_IsApproved = sender as CheckBox;
        string userName = ch_IsApproved.ValidationGroup;
        MembershipUser user = Membership.GetUser(userName);
        user.IsApproved = !user.IsApproved;
        Membership.UpdateUser(user);
        //
        ShopsRepository shr = new ShopsRepository();
        var shops = shr.GetShops();
        FillShopsGrid(shops);
    }
}
