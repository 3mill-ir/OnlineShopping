using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Models;
using System.Collections.Generic;

public partial class Views_Admin_FindShop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Search_Clicked(object sender, EventArgs e)
    {
        string shopName = tb_ShopName.Text;
        ShopsRepository shr = new ShopsRepository();
        IEnumerable<Shop> shops = shr.FindShop(shopName);
        FillShopsDataList(shops);
    }
    private void FillShopsDataList(IEnumerable<Shop> Shops)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ShopLink", typeof(String));
        foreach (Shop shop in Shops)
        {
            DataRow row = dt.NewRow();
            row["ShopLink"] = shop.SelectLink;
            dt.Rows.Add(row);
        }
        DataList_Shops.DataSource = dt;
        DataList_Shops.DataBind();
    }
}
