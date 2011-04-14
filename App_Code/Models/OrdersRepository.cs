using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

/// <summary>
/// Summary description for OrdersRepository
/// </summary>
public class OrdersRepository
{
    //***************************
    MyDatabaseDataContext db = new MyDatabaseDataContext(Repository.ConnectionString);
    //*************************** GET METHOD :
    public ShoppingCart GetOrder(int CartID)
    {
        try
        {
            return db.ShoppingCarts.Single(cart => cart.CartID == CartID);
        }
        catch
        {
            return null;
        }
    }
    public IEnumerable<ShoppingCart> GetOrders(DateTime date)
    {
        return db.ShoppingCarts.Where(cart => cart.DateOfCreate.Date == date.Date).OrderByDescending(cart => cart.DateOfCreate);
    }
    public ShoppingCartItem GetOrderItem(int ItemId)
    {
        try
        {
            return db.ShoppingCartItems.Single(item => item.CartItemID == ItemId);
        }
        catch
        {
            return null;
        }
    }
    //*************************** SAVE :
    public void Save()
    {
        db.SubmitChanges();
    }
}
