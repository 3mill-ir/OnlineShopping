using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data;

public partial class Views_ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["From"] != null)
            {
                string from = Request.QueryString["From"].ToString();
                if (from == "info")
                {
                    int materialId = Convert.ToInt32(Request.QueryString["MaterialID"]);
                    Cart cart = Cart.Current;
                    cart.AddItem(new CartItem(materialId, 1));
                    Cart.Current = cart;
                    //
                    FillCartItemsList(cart);
                    pnl_Cart.Visible = true;
                    pnl_Empty.Visible = false;
                    //Add To Interests :
                    if (User.Identity.IsAuthenticated && User.IsInRole(MyRoles.Member))
                    {
                        MaterialsRepository mr = new MaterialsRepository();
                        MembersRepository mRep = new MembersRepository();
                        mRep.AddKeywordsToInterests(mr.GetMaterialKeywords(materialId), InterestType.Buy);
                        mRep.Save();
                    }
                }
            }
            else
            {
                Cart cart = Cart.Current;
                if (cart.Items.Count > 0)
                {
                    FillCartItemsList(cart);
                    pnl_Cart.Visible = true;
                    pnl_Empty.Visible = false;
                }
                else
                {
                    pnl_Empty.Visible = true;
                    pnl_Cart.Visible = false;
                }
            }
        }
    }
    private void FillCartItemsList(Cart cart)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Index", typeof(Int32));
        dt.Columns.Add("MaterialID", typeof(Int32));
        dt.Columns.Add("Quantity", typeof(Int32));
        dt.Columns.Add("MaterialName", typeof(String));
        dt.Columns.Add("Price", typeof(String));
        MaterialsRepository mr = new MaterialsRepository();
        int i = 1;
        MaterialPrice totalPrice = MaterialPrice.Zero;
        foreach(CartItem item in cart.Items)
        {
            Material material = mr.GetMaterial(item.MaterialID);
            MaterialPrice currentPrice = material.CurrentPrice;
            DataRow row = dt.NewRow();
            row["Index"] = i;
            row["MaterialID"] = item.MaterialID;
            row["Quantity"] = item.Quantity;
            row["MaterialName"] = material.NameLink;
            row["Price"] = (currentPrice != null ? currentPrice.ToString() : "0");
            totalPrice = mr.SumPrice(totalPrice, mr.MultiplicationPrice(currentPrice, item.Quantity), MaterialPrice.DefaultUnit);
            dt.Rows.Add(row);
            i++;
        }
        Grid_Cart.DataSource = dt; 
        Grid_Cart.DataBind();
        lbl_TotalPrice.Text = totalPrice.ToString();
        hid_Total.Value = totalPrice.Price.ToString();
    }
    protected void btn_SaveCart_Clicked(object sender, EventArgs e)
    {
        Cart.Clear();
        int tester = 0;
        foreach (GridViewRow row in Grid_Cart.Rows)
        {
            if (row.RowType != DataControlRowType.Header)
            {
                HiddenField hidden = row.Cells[3].FindControl("hidden_MaterialID") as HiddenField;
                TextBox tb_Quantity = row.Cells[3].FindControl("tb_Quantity") as TextBox;
                int materialId = Convert.ToInt32(hidden.Value);
                int quantity = (Int32.TryParse(tb_Quantity.Text, out tester) ? Int32.Parse(tb_Quantity.Text) : 0);
                if (quantity > 0)
                {
                    Cart newCart = Cart.Current;
                    CartItem item = new CartItem(materialId, quantity);
                    newCart.AddItem(item);
                    Cart.Current = newCart;
                }
            }
        }
        //
        Response.Redirect("~/Views/MemberAuthenticate.aspx");
    }
    protected void btn_Refresh_Clicked(object sender, EventArgs e)
    {
        Cart.Clear();
        int tester = 0;
        foreach (GridViewRow row in Grid_Cart.Rows)
        {
            if (row.RowType != DataControlRowType.Header)
            {
                HiddenField hidden = row.Cells[3].FindControl("hidden_MaterialID") as HiddenField;
                TextBox tb_Quantity = row.Cells[3].FindControl("tb_Quantity") as TextBox;
                int materialId = Convert.ToInt32(hidden.Value);
                int quantity = (Int32.TryParse(tb_Quantity.Text, out tester) ? Int32.Parse(tb_Quantity.Text) : 0);
                if (quantity > 0)
                {
                    Cart newCart = Cart.Current;
                    CartItem item = new CartItem(materialId, quantity);
                    newCart.AddItem(item);
                    Cart.Current = newCart;
                }
            }
        }
        Cart cart = Cart.Current;
        if (cart.Items.Count > 0)
        {
            FillCartItemsList(cart);
            pnl_Cart.Visible = true;
            pnl_Empty.Visible = false;
        }
        else
        {
            pnl_Empty.Visible = true;
            pnl_Cart.Visible = false;
        }
    }
}
