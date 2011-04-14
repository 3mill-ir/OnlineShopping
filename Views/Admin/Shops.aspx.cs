using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_Admin_Shops : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillShopsDatalist();
        }
    }
    private void FillShopsDatalist()
    {
        ShopsRepository sr = new ShopsRepository();
        var shops = sr.GetShops();
        DataTable dt = new DataTable();
        dt.Columns.Add("ShopID", typeof(Int32));
        dt.Columns.Add("Name", typeof(String));
        foreach (Shop shop in shops)
        {
            DataRow row = dt.NewRow();
            row["ShopID"] = shop.ShopID;
            row["Name"] = shop.Name;
            dt.Rows.Add(row);
        }
        DataList_Shops.DataSource = dt;
        DataList_Shops.DataBind();
    }
    protected void DataList_Shops_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteShop")
        {
            int shopId = Convert.ToInt32(e.CommandArgument);
            ShopsRepository shr = new ShopsRepository();
            shr.DeleteShop(shopId);
            shr.Save();
            FillShopsDatalist();
        }
    }
}
