using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for ShoppingCart
/// </summary>
namespace Models
{
    public partial class ShoppingCart
    {
        public MaterialPrice CostPrice
        {
            get
            {
                MaterialPrice mp = MaterialPrice.Zero;
                mp.Price = Cost;
                mp.Unit = CostUnit;
                return mp;
            }
        }
        public string StatusText
        {
            get
            {
                string text = "";
                if ((CartStatus)Status == CartStatus.Checking)
                {
                    text = "در حال بررسی";
                }
                else if ((CartStatus)Status == CartStatus.Confirmed)
                {
                    text = "تأیید شد";
                }
                else if ((CartStatus)Status == CartStatus.NotConfirmed)
                {
                    text = "رد شد";
                }
                else if ((CartStatus)Status == CartStatus.DeliveredToPost)
                {
                    text = "به پست تحویل شد";
                }
                else if ((CartStatus)Status == CartStatus.Delivered)
                {
                    text = "تحویل داده شد";
                }
                else if ((CartStatus)Status == CartStatus.Returned)
                {
                    text = "برگشت خورد";
                }
                return text;
            }
        }
        public string StatusHtml
        {
            get
            {
                string html = "";
                html = 
                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"Centerize\">" + 
                        "<tr>" + 
                            "<td>" +
                                "<div class=\"" + ((CartStatus)Status == CartStatus.Confirmed ? "Tick_Orange" : ((CartStatus)Status == CartStatus.NotConfirmed ? "Zarbdar" : ((CartStatus)Status == CartStatus.DeliveredToPost ? "Posted" : ((CartStatus)Status == CartStatus.Delivered ? "Tick" : ((CartStatus)Status == CartStatus.Returned ? "Back" : ""))))) + "\"></div>" +
                            "</td>" +
                            "<td style=\"padding-right : 5px; " + ((CartStatus)Status == CartStatus.Confirmed ? "color : #ca6c18;" : ((CartStatus)Status == CartStatus.NotConfirmed ? "color : #c31111;" : ((CartStatus)Status == CartStatus.DeliveredToPost ? "color : #146eb4;" : ((CartStatus)Status == CartStatus.Delivered ? "color : #21b113;" : ((CartStatus)Status == CartStatus.Returned ? "color : #dd4257;" : ""))))) + "\">" +
                                StatusText +
                            "</td>" +
                        "</tr>" +
                    "</table>";
                return html;
            }
        }
        public int MaterialsCount
        {
            get
            {
                int count = 0;
                var query = from item in Items
                            select item.Quantity;
                foreach (int quantity in query)
                {
                    count += quantity;
                }
                return count;
            }
        }
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
                foreach (ShoppingCartItem item in Items)
                {
                    Material material = mr.GetMaterial(item.MaterialID);
                    MaterialPrice unitPrice = item.FinalPrice_Object;
                    MaterialPrice multiplicatedPrice = mr.MultiplicationPrice(unitPrice, item.Quantity);
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
                html +=
                    "<tr>" +
                        "<td class=\"CartInfo_Total\" colspan=\"5\">" +
                            "جمع کل : " +
                            CostPrice.ToString() +
                        "</td>" +
                    "</tr>";
                html += "</table>";
                return html;
            }
        }
        public MaterialPrice Benefit
        {
            get
            {
                MaterialsRepository mr= new MaterialsRepository();
                int kharaj = Kharaj.Price;
                int total = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    int purchasePrice = item.Material.PurchasePrice_Object.ChangeUnit(MaterialPrice.DefaultUnit).Price;
                    int sellPrice = item.FinalPrice_Object.ChangeUnit(MaterialPrice.DefaultUnit).Price;
                    int benefit = ((sellPrice - purchasePrice) * item.Quantity);
                    total += benefit;
                }
                total -= kharaj;
                return new MaterialPrice(total, MaterialPrice.DefaultUnit);
            }
        }
        public MaterialPrice Kharaj
        {
            get
            {
                MaterialPrice kh = new MaterialPrice((int)((CostPrice.ChangeUnit(MaterialPrice.DefaultUnit).Price * KharajPercent) / 100), MaterialPrice.DefaultUnit);
                return kh;
            }
        }
        public double KharajPercent
        {
            get
            {
                return 5.0;
            }
        }
    }
}
