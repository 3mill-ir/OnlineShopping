using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_SaveShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CitiesRepository cr = new CitiesRepository();
            cr.FillStatesList(cmb_State);
        }
    }
    protected void cmb_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_State.SelectedIndex > 0)
        {
            CitiesRepository cr = new CitiesRepository();
            cr.FillCitiesList(cmb_City, Convert.ToInt32(cmb_State.SelectedItem.Value));
        }
        else
        {
            cmb_City.Items.Clear();
        }
    }
    protected void btn_SaveCart_Click(object sender, EventArgs e)
    {
        MaterialsRepository matRep = new MaterialsRepository();
        MembersRepository mr = new MembersRepository();
        Member member = mr.GetMember(User.Identity.Name);
        //
        ShoppingCart cart = new ShoppingCart();
        cart.DateOfCreate = Repository.Now;
        cart.CityID = Convert.ToInt32(cmb_City.SelectedItem.Value);
        cart.Address = tb_Address.Text;
        cart.Mobile = tb_Mobile.Text;
        cart.Tels = tb_Tels.Text;
        cart.PostalCode = tb_PostalCode.Text;
        cart.Statement = tb_Statement.Text;
        Cart currentCart = Cart.Current;
        cart.Cost = currentCart.TotalPrice_JustAfterInfo.Price;
        cart.CostUnit = currentCart.TotalPrice_JustAfterInfo.Unit;
        cart.Status = (int)CartStatus.Checking;
        foreach (CartItem item in currentCart.Items)
        {
            MaterialPrice price = matRep.GetMaterialCurrentPrice(item.MaterialID);
            ShoppingCartItem newItem = new ShoppingCartItem();
            newItem.MaterialID = item.MaterialID;
            newItem.Quantity = item.Quantity;
            newItem.FinalPrice = price.Price;
            newItem.FinalPriceUnit = price.Unit;
            cart.Items.Add(newItem);
        }
        //Save :
        member.ShoppingCarts.Add(cart);
        mr.Save();
        //empty shopping cart :
        Cart.Clear();
        Response.Redirect("~/Views/Members/Orders.aspx?From=ShoppingCart");
    }
}
