using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Web.Security;

/// <summary>
/// Summary description for Statistics
/// </summary>
public class Statistics
{
    //*****************Contructors :
    public Statistics(Member member, HtmlType htmlType)
    {
        Member = member;
        HType = htmlType;
    }
    //***************** Properties :
    private Member Member { get; set; }
    private HtmlType HType { get; set; }
    public double PrizePercent
    {
        get
        {
            double percent = 0.0;
            return percent;
        }
    }
    public DateTime DateOfJoin
    {
        get
        {
            DateTime dateOfJoin = Repository.Now;
            dateOfJoin = Member.DateOfJoin;
            return dateOfJoin;
        }
        set
        {
            this.DateOfJoin = value;
        }
    }
    public int Visits
    {
        get
        {
            int count = 0;
            count = Member.Logins.Count;
            return count;
        }
        set
        {
            this.Visits = value;
        }
    }
    public int ShoppingCarts
    {
        get
        {
            int count = 0;
            count = Member.ShoppingCarts.Count();
            return count;
        }
        set
        {
            this.ShoppingCarts = value;
        }
    }
    public int PurchasedMaterials
    {
        get
        {
            int count = 0;
            MembersRepository mr = new MembersRepository();
            count = mr.GetPurchasedMaterialsCount(Member.MemberID);
            return count;
        }
        set
        {
            this.PurchasedMaterials = value;
        }
    }
    public MaterialPrice TotalPurchaseAmount
    {
        get
        {
            MaterialPrice totalPrice = new MaterialPrice();
            MembersRepository mr = new MembersRepository();
            totalPrice = Member.TotalPurchaseAmount;
            return totalPrice;
        }
    }
    public double SuccessfullPurchases
    {
        get
        {
            double count = 0;
            MembersRepository mr = new MembersRepository();
            count = mr.GetSuccessfullPurchasesPercent(Member.MemberID);
            return count;
        }
        set
        {
            this.SuccessfullPurchases = value;
        }
    }
    public List<string> Interests
    {
        get
        {
            List<string> interests = new List<string>();
            var q = Member.GetTopInterests(5);
            interests = (from interest in q select interest.Name_Link).ToList<string>();
            return interests;
        }
    }
    public int PositiveVotes
    {
        get
        {
            int count = 0;
            count = Member.PositiveVotesCount;
            return count;
        }
        set
        {
            this.PositiveVotes = value;
        }
    }
    public int NegativeVotes
    {
        get
        {
            int count = 0;
            count = Member.NegativeVotesCount;
            return count;
        }
        set
        {
            this.NegativeVotes = value;
        }
    }
    //********** Titles :
    private string DateOfJoin_Title { get { return "تـاریخ عضویت"; } }
    private string Visits_Title { get { return "تعداد کل بازدیدها از سایت"; } }
    private string PurchasedMaterials_Title { get { return "تعداد کل کـالاهای خریداری شده"; } }
    private string ShoppingCarts_Title { get { return "تعداد کل سفارشـات"; } }
    private string TotalPurchaseAmount_Title { get { return "مبلغ کل سفـارشات"; } }
    private string SuccessfullPurchases_Title { get { return "درصد سفـارشات تحویل داده شده"; } }
    private string Interests_Title { get { return "برخی علاقه مندی هـا"; } }
    private string Votes_Title { get { return "آراء صـادر شده"; } }
    //******************* Methods :
    public string ToHtml()
    {
        string html = "<table class=\"CompareTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">";
        html +=
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                    "نـام" + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    Member.FullName +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                    "هدایای دریافتی" + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +

                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                    "هدیه یک میلیون ریالی" + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    (new Diagram(DiagramType.JustGreen, 64.5, 0, 180, 9, 1)).ToHtml() +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                    DateOfJoin_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    ShamsiDateTime.MiladyToShamsi(DateOfJoin).ToLongString() +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px; \">" +
                    Visits_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    Visits.ToString() +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; \">" +
                    ShoppingCarts_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    ShoppingCarts.ToString() +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; \">" +
                    PurchasedMaterials_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    PurchasedMaterials.ToString() +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; \">" +
                    TotalPurchaseAmount_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    TotalPurchaseAmount.ChangeUnit(MaterialPrice.DefaultUnit).ToString() +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; \">" +
                    SuccessfullPurchases_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    (new Diagram(DiagramType.JustGreen, SuccessfullPurchases, 0, 180, 9, 1)).ToHtml() +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; \">" +
                    Interests_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    GetInterestsHtml(Interests) +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; \">" +
                    Votes_Title + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                        "<tr>" +
                            "<td>" +
                                "مثبت : " + PositiveVotes.ToString() +
                            "</td>" +
                            "<td style=\"padding : 0px 15px 0px 15px;\">" +
                            (new Diagram(DiagramType.Combined, (PositiveVotes + NegativeVotes > 0 ? (PositiveVotes * 100) / (PositiveVotes + NegativeVotes) : 0), (PositiveVotes + NegativeVotes > 0 ? (100 - (PositiveVotes * 100) / (PositiveVotes + NegativeVotes)) : 0), 180, 9, 1)).ToHtml() +
                            "</td>" +
                            "<td>" +
                                "منفی : " + NegativeVotes.ToString() +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                "</td>" +
            "</tr>";
        if (HType == HtmlType.ForAdmin)
        {
            html +=
                "<tr>" +
                    "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                        "نام کاربری" + " :" +
                    "</td>" +
                    "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                        Member.UserName +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                        "شهر محل سکونت" + " :" +
                    "</td>" +
                    "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                        Member.City.State.StateName + " - " + Member.City.CityName +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                        "موبایل" + " :" +
                    "</td>" +
                    "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                        Member.Mobile +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                        "سایر شماره ها" + " :" +
                    "</td>" +
                    "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                        Member.Tels +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 180px;\">" +
                        "ایمیل" + " :" +
                    "</td>" +
                    "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; \">" +
                        Member.Email +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; border-bottom : none;\">" +
                    "آی پی هنگام ثبت نام" + " :" +
                "</td>" +
                "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; font-weight : bold; border-bottom : none;\">" +
                    Member.IP +
                "</td>" +
            "</tr>";
        }
        html += "</table>";
        return html;
    }
    private string GetInterestsHtml(List<string> interests)
    {
        string html =
            "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr>";
        int i = 0;
        foreach (string link in interests)
        {
            html +=
                "<td style=\"padding-left : 5px;\">" + 
                    link +
                "</td>" +
                (i < interests.Count - 1 ? "<td style=\"padding-left : 5px;\">-</td>" : "");
            i++;
        }
        html += "</tr></table>";
        return html;
    }
}
public enum HtmlType
{
    ForMember = 1,
    ForAdmin = 2
}
