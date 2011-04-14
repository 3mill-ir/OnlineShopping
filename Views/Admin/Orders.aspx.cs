using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_Admin_Orders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dateSelector.SelectedDate_Miladi = Repository.Now;
            //fill grid :
            OrdersRepository or = new OrdersRepository();
            var orders = or.GetOrders(dateSelector.SelectedDate_Miladi);
            FillGridOrders(orders);
        }
    }
    protected void Grid_Orders_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Orders.PageIndex = e.NewPageIndex;
        //
        OrdersRepository or = new OrdersRepository();
        var orders = or.GetOrders(dateSelector.SelectedDate_Miladi);
        FillGridOrders(orders);
    }
    private void FillGridOrders(IEnumerable<ShoppingCart> Orders)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("OrderID", typeof(Int32));
        dt.Columns.Add("Member", typeof(String));
        dt.Columns.Add("Count", typeof(Int32));
        dt.Columns.Add("Total", typeof(String));
        dt.Columns.Add("Status", typeof(String));
        foreach (ShoppingCart order in Orders)
        {
            DataRow row = dt.NewRow();
            row["OrderID"] = order.CartID;
            row["Member"] = "<a class=\"Link\" onclick=\"return OpenCenter('" + Repository.ToHtmlUrl("~/Views/Admin/MemberStatistics.aspx") + "?MemberID=" + order.MemberID.ToString() + "', 'MemberStatistics', 700, 500);\">" + order.Member.FullName + "</a>";
            row["Count"] = order.MaterialsCount;
            row["Total"] = order.CostPrice.ToString();
            row["Status"] = order.StatusHtml;
            dt.Rows.Add(row);
        }
        Grid_Orders.DataSource = dt;
        Grid_Orders.DataBind();
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        OrdersRepository or = new OrdersRepository();
        var orders = or.GetOrders(dateSelector.SelectedDate_Miladi);
        FillGridOrders(orders);
    }
}
