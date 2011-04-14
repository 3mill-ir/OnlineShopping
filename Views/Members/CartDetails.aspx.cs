using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Members_CartDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int cartId = Convert.ToInt32(Request.QueryString["CartID"]);
            OrdersRepository or = new OrdersRepository();
            ShoppingCart order = or.GetOrder(cartId);
            lbl_Info.Text = order.Info;
            lbl_Status.Text = order.StatusHtml;
        }
    }
}
