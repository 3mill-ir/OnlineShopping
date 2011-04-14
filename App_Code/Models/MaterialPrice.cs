using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialPrice
/// </summary>
namespace Models
{
    public partial class MaterialPrice
    {
        public MaterialPrice(int price, PriceUnit unit)
        {
            Price = price;
            Unit = (int)unit;
        }
        public static MaterialPrice Zero
        {
            get
            {
                MaterialPrice zero = new MaterialPrice();
                zero.Price = 0;
                zero.Unit = (int)DefaultUnit;
                zero.IsCurrent = false;
                return zero;
            }
        }
        public string ToString()
        {
            string st = "";
            st = Price.ToString("N0") + " " + UnitString((PriceUnit)Unit);
            return st;
        }
        private string UnitString(PriceUnit unit)
        {
            string st = "";
            if (unit == PriceUnit.Rial)
                st = "ریـال";
            else if (unit == PriceUnit.Tuman)
                st = "تومـان";
            return st;
        }
        public MaterialPrice ChangeUnit(PriceUnit unit)
        {
            if ((int)unit == Unit)
            {
                return this;
            }
            else if ((PriceUnit)Unit == PriceUnit.Tuman && unit == PriceUnit.Rial)
            {
                MaterialPrice newPrice = this;
                newPrice.Price = newPrice.Price * 10;
                newPrice.Unit = (int)PriceUnit.Rial;
                return newPrice;
            }
            else if ((PriceUnit)Unit == PriceUnit.Rial && unit == PriceUnit.Tuman)
            {
                MaterialPrice newPrice = this;
                newPrice.Price = (int)(newPrice.Price / 10);
                newPrice.Unit = (int)PriceUnit.Tuman;
                return newPrice;
            }
            else
            {
                return null;
            }
                
        }
        public static PriceUnit DefaultUnit
        {
            get { return PriceUnit.Tuman; }
        }
        public int Tumans
        {
            get
            {
                int price = 0;
                if ((PriceUnit)this.Unit == PriceUnit.Tuman)
                {
                    price = this.Price;
                }
                else if ((PriceUnit)this.Unit == PriceUnit.Rial)
                {
                    price = (int)(this.Price / 10);
                }
                return price;
            }
        }
        public int Rials
        {
            get
            {
                int price = 0;
                if ((PriceUnit)this.Unit == PriceUnit.Rial)
                {
                    price = this.Price;
                }
                else if ((PriceUnit)this.Unit == PriceUnit.Tuman)
                {
                    price = this.Price * 10;
                }
                return price;
            }
        }
    }
}
