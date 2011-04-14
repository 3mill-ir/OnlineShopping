using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data;

public partial class Views_Admin_OrderDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["OrderID"] != null)
            {
                int orderId = Convert.ToInt32(Request.QueryString["OrderID"]);
                hidden_OrderID.Value = orderId.ToString();
                OrdersRepository or = new OrdersRepository();
                ShoppingCart order = or.GetOrder(orderId);
                FillGrid_Items(order.Items);
                //fill info :
                lbl_Address.Text = order.City.State.StateName + " - " + order.City.CityName + " - " + order.Address;
                lbl_Mobile.Text = order.Mobile;
                lbl_Tels.Text = order.Tels;
                lbl_PostalCode.Text = order.PostalCode;
                lbl_Statement.Text = order.Statement;
                cmb_Status.SelectedIndex = GetIndex(order.Status);
            }
        }
    }
    private int GetIndex(int status)
    {
        int index = 0;
        int i = 0;
        foreach (ListItem item in cmb_Status.Items)
        {
            if (item.Value == status.ToString())
            {
                index = i;
            }
            i++;
        }
        return index;
    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        OrdersRepository or = new OrdersRepository();
        int orderId = Convert.ToInt32(hidden_OrderID.Value);
        ShoppingCart order = or.GetOrder(orderId);
        order.Status = Convert.ToInt32(cmb_Status.SelectedItem.Value);
        or.Save();
        //
        Repository.MessageBoxShow("با موفقیت ذخیره شد", Up_Save, this.GetType());
    }
    private void FillGrid_Items(IEnumerable<ShoppingCartItem> items)
    {
        MaterialsRepository mr = new MaterialsRepository();
        DataTable dt = new DataTable();
        dt.Columns.Add("Index", typeof(Int32));
        dt.Columns.Add("ItemID", typeof(Int32));
        dt.Columns.Add("Name", typeof(String));
        dt.Columns.Add("UnitPrice", typeof(String));
        dt.Columns.Add("Count", typeof(String));
        dt.Columns.Add("TotalPrice", typeof(String));
        int i= 1;
        foreach (ShoppingCartItem item in items)
        {
            DataRow row = dt.NewRow();
            row["Index"] = i;
            row["ItemID"] = item.CartItemID;
            row["Name"] = item.Material.NameLink_Thick;
            row["UnitPrice"] = item.FinalPrice_Object.ChangeUnit(MaterialPrice.DefaultUnit).ToString();
            row["Count"] = item.Quantity;
            row["TotalPrice"] = mr.MultiplicationPrice(item.FinalPrice_Object, item.Quantity).ChangeUnit(MaterialPrice.DefaultUnit).ToString();
            dt.Rows.Add(row);
            i++;
        }
        Grid_Items.DataSource = dt;
        Grid_Items.DataBind();
    }
}
