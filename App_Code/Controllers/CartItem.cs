using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartItem
/// </summary>
public class CartItem
{
    public CartItem(int materialId, int quantity)
    {
        MaterialID = materialId;
        Quantity = quantity;
    }
    public int MaterialID { get; set; }
    public int Quantity { get; set; }
}
