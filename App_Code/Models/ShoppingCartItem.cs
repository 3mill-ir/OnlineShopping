using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ShoppingCartItem
/// </summary>
namespace Models
{
    public partial class ShoppingCartItem
    {
        public MaterialPrice FinalPrice_Object
        {
            get
            {
                MaterialPrice mp = new MaterialPrice();
                mp.Price = FinalPrice;
                mp.Unit = FinalPriceUnit;
                return mp;
            }
        }
    }
}
