using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Member
/// </summary>
namespace Models
{
    public partial class Member
    {
        //const int _Prize = 100000;
        public IEnumerable<ShoppingCart> SortedShoppingCarts
        {
            get
            {
                var carts = from cart in ShoppingCarts
                            orderby cart.DateOfCreate descending
                            select cart;
                return carts;
            }
        }
        public string FullName { get { return Name + " " + LastName; } }
        public string Info
        {
            get
            {
                string html = "";
                html =
                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"MemberInfo\">" + 
                        "<tr>" +
                            "<td align=\"left\" class=\"MemberInfo_RightTD\">" +
                                "نام و نام خانوادگی :" +
                            "</td>" +
                            "<td align=\"right\" class=\"MemberInfo_LeftTD\">" +
                                FullName +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align=\"left\" class=\"MemberInfo_RightTD\">" +
                                "ایمیـل :" +
                            "</td>" +
                            "<td align=\"right\" class=\"MemberInfo_LeftTD\">" +
                                Email +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align=\"left\" class=\"MemberInfo_RightTD\">" +
                                "موبـایل :" +
                            "</td>" +
                            "<td align=\"right\" class=\"MemberInfo_LeftTD\">" +
                                Mobile +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align=\"left\" class=\"MemberInfo_RightTD\">" +
                                "سایر شماره های تماس :" +
                            "</td>" +
                            "<td align=\"right\" class=\"MemberInfo_LeftTD\">" +
                                Tels +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align=\"left\" class=\"MemberInfo_RightTD\">" +
                                "شهـر محـل سکـونت :" +
                            "</td>" +
                            "<td align=\"right\" class=\"MemberInfo_LeftTD\">" +
                                City.FullName +
                            "</td>" +
                        "</tr>" +
                    "</table>";
                return html;
            }
        }
        public IEnumerable<ShoppingCart> Orders
        {
            get
            {
                var orders = from order in ShoppingCarts
                             orderby order.DateOfCreate descending
                             select order;
                return orders;
            }
        }
        public MaterialPrice TotalPurchaseAmount
        {
            get
            {
                MembersRepository mr = new MembersRepository();
                return mr.GetTotalPurchaseAmount(MemberID);
            }
        }
        public IEnumerable<MemberInterest> GetTopInterests(int Count)
        {
            return Interests.OrderByDescending(interest => interest.TotalPoints).Take(Count);
        }
        public int PositiveVotesCount
        {
            get
            {
                MembersRepository mr = new MembersRepository();
                return mr.GetPositiveVotesCount(UserName);
            }
        }
        public int NegativeVotesCount
        {
            get
            {
                MembersRepository mr = new MembersRepository();
                return mr.GetNegativeVotesCount(UserName);
            }
        }
        public IEnumerable<MaterialVote> PositiveVotes
        {
            get
            {
                MembersRepository mr = new MembersRepository();
                return mr.GetPositiveVotes(UserName);
            }
        }
        public IEnumerable<MaterialVote> NegativeVotes
        {
            get
            {
                MembersRepository mr = new MembersRepository();
                return mr.GetNegativeVotes(UserName);
            }
        }
        public Statistics GetStatistics(HtmlType HType)
        {
            return new Statistics(this, HType);
        }
        public MaterialPrice TotalBenefit
        {
            get
            {
                MaterialPrice mp = new MaterialPrice(0, MaterialPrice.DefaultUnit);
                MaterialsRepository mr = new MaterialsRepository();
                foreach (ShoppingCart cart in ShoppingCarts)
                {
                    if (cart.Status == (int)CartStatus.Delivered)
                    {
                        mp = mr.SumPrice(mp, cart.Benefit, MaterialPrice.DefaultUnit);
                    }
                }
                return mp;
            }
        }
        public Login LastLogin
        {
            get
            {
                var query = from login in Logins
                            orderby login.DateOfLogin
                            select login;
                return (query.Count() > 0 ? query.Last() : null);
            }
        }
        public IEnumerable<Material> GetFavoriteMaterials(int Count)
        {
            MaterialsRepository mr = new MaterialsRepository();
            IEnumerable<Material> res = Enumerable.Empty<Material>();
            //
            int topInterestsCount = 5;
            List<MemberInterest> interests = (from interest in Interests orderby interest.TotalPoints descending select interest).Distinct().Take(topInterestsCount).ToList<MemberInterest>();
            return mr.GetMaterials(interests).Take(Count);
        }
    }
}