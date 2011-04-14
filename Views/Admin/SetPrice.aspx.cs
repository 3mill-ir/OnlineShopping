using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data;

public partial class Views_Admin_SetPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int materialId = Convert.ToInt32(Request.QueryString["MaterialID"]);
            MaterialsRepository mr = new MaterialsRepository();
            var Prices = mr.GetMaterialPrices(materialId);
            FillPricesDataList(Prices);
            //
            hidden_MaterialID.Value = materialId.ToString();
        }
    }
    private void FillPricesDataList(IEnumerable<MaterialPrice> prices)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PriceID", typeof(Int32));
        dt.Columns.Add("Price", typeof(String));
        foreach (MaterialPrice price in prices)
        {
            DataRow row = dt.NewRow();
            row["PriceID"] = price.PriceID;
            row["Price"] = (price.IsCurrent ? "<span class=\"CurrentPrice\">" + price.ToString() + "</span>" : "<span class=\"LineThrough\"><span class=\"OldPrice\">" + price.ToString() + "</span></span>");
            dt.Rows.Add(row);
        }
        DataList_Prices.DataSource = dt;
        DataList_Prices.DataBind();
    }
    protected void btn_Add_Clicked(object sender, EventArgs e)
    {
        int tester = 0;
        int materialId = Convert.ToInt32(hidden_MaterialID.Value);
        MaterialPrice price = new MaterialPrice();
        price.MaterialID = materialId;
        price.Price = (Int32.TryParse(tb_Price.Text, out tester) ? Int32.Parse(tb_Price.Text) : 0);
        price.Unit = Convert.ToInt32(cmb_Unit.SelectedItem.Value);
        price.IsCurrent = ch_IsCurrent.Checked;
        price.DateOfAdd = Repository.Now;
        MaterialsRepository mr = new MaterialsRepository();
        mr.AddMaterialPrice(price);
        mr.Save();
        //**************
        var Prices = mr.GetMaterialPrices(materialId);
        FillPricesDataList(Prices);
    }
    protected void DataList_Prices_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeletePrice")
        {
            int priceId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            mr.DeletePrice(priceId);
            mr.Save();
            //**************
            int materialId = Convert.ToInt32(hidden_MaterialID.Value);
            var Prices = mr.GetMaterialPrices(materialId);
            FillPricesDataList(Prices);
        }
        else if (e.CommandName == "LineThroughPrice")
        {
            int priceId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            mr.ChangePriceStatus(priceId);
            mr.Save();
            //**************
            int materialId = Convert.ToInt32(hidden_MaterialID.Value);
            var Prices = mr.GetMaterialPrices(materialId);
            FillPricesDataList(Prices);
        }
    }
}
