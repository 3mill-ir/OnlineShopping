using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

/// <summary>
/// Summary description for Cart
/// </summary>
[Serializable]
public class Cart
{
    public static Cart Current
    {
        get
        {
            if (HttpContext.Current.Session["ShoppingCart"] != null)
            {
                return (Cart)HttpContext.Current.Session["ShoppingCart"];
            }
            else
            {
                Cart cart = new Cart();
                cart.Items = new List<CartItem>();
                return cart;
            }
        }
        set
        {
            HttpContext.Current.Session["ShoppingCart"] = value;
        }
    }
    public static void Clear()
    {
        Cart cart = new Cart();
        cart.Items = new List<CartItem>();
        Current = cart;
    }
    public List<CartItem> Items { get; private set; }
    public void AddItem(CartItem newItem)
    {
        int index = IndexOfItem(newItem);
        if (index >= 0)
        {
            Items[index].Quantity += newItem.Quantity;
        }
        else
        {
            Items.Add(newItem);
        }
    }
    private int IndexOfItem(CartItem newItem)
    {
        int index = -1;
        int i = 0;
        foreach (CartItem cartItem in Items)
        {
            if (newItem.MaterialID == cartItem.MaterialID)
                return i;
            i++;
        }
        return index;
    }
    public void SetItemQuantity(int materialID, int quantity)
    {
        if (quantity == 0)
        {
        }
        else
        {
        }
    }
    public void RemoveItem(int materialId)
    {
        
    }
    public MaterialPrice TotalPrice_JustAfterInfo { get; private set; }
    public string Info
    {
        get
        {
            MaterialsRepository mr = new MaterialsRepository();
            string html = "";
            html =
                "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"CartInfo\">" +
                    "<tr>" +
                        "<td class=\"CartInfo_Header\">" +
                            "ردیف" +
                        "</td>" +
                        "<td class=\"CartInfo_Header\">" +
                            "نام کالا" +
                        "</td>" +
                        "<td class=\"CartInfo_Header\">" +
                            "قیمت واحد" +
                        "</td>" +
                        "<td class=\"CartInfo_Header\">" +
                            "تعداد" +
                        "</td>" +
                        "<td class=\"CartInfo_Header\" style=\"border-left : none;\">" +
                            "قیمت کل" +
                        "</td>" +
                    "</tr>";
            int i = 1;
            MaterialPrice totalPrice = MaterialPrice.Zero;
            foreach (CartItem item in Items)
            {
                Material material = mr.GetMaterial(item.MaterialID);
                MaterialPrice unitPrice = material.CurrentPrice;
                MaterialPrice multiplicatedPrice = mr.MultiplicationPrice(unitPrice , item.Quantity);
                totalPrice = mr.SumPrice(totalPrice, multiplicatedPrice, MaterialPrice.DefaultUnit);
                html +=
                    "<tr>" +
                        "<td class=\"CartInfo_Item\">" +
                            i.ToString() +
                        "</td>" +
                        "<td class=\"CartInfo_Item\">" +
                            material.NameLink +
                        "</td>" +
                        "<td class=\"CartInfo_Item\">" +
                            unitPrice.ToString() +
                        "</td>" +
                        "<td class=\"CartInfo_Item\">" +
                            item.Quantity.ToString() +
                        "</td>" +
                        "<td class=\"CartInfo_Item\" style=\"border-left : none;\">" +
                           multiplicatedPrice.ToString() +
                        "</td>" +
                    "</tr>";
                i++;
            }
            TotalPrice_JustAfterInfo = totalPrice;
            html +=
                "<tr>" +
                    "<td class=\"CartInfo_Total\" colspan=\"5\">" + 
                        "جمع کل : " +
                        totalPrice.ToString() +
                    "</td>" +
                "</tr>";
            html += "</table>";
            return html;
        }
    }
}
