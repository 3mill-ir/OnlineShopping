using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using Models;
using MyExtensionMethods;

/// <summary>
/// Summary description for MembersRepository
/// </summary>
public class MembersRepository
{
    //*******************************
    MyDatabaseDataContext db = new MyDatabaseDataContext();
    //*******************************
    //************* GET METHODS :
    public IEnumerable<Member> GetAllMembers()
    {
        return db.Members.OrderBy(member => member.UserName);
    }
    public Member GetMember(string UserName)
    {
        try
        {
            return db.Members.Single(member => member.UserName == UserName);
        }
        catch
        {
            return null;
        }
    }
    public Member GetMember(int MemberID)
    {
        try
        {
            return db.Members.Single(member => member.MemberID == MemberID);
        }
        catch
        {
            return null;
        }
    }
    public int GetMemberID(string UserName)
    {
        try
        {
            var query = from member in db.Members
                        where member.UserName == UserName
                        select member.MemberID;
            return query.Single();
        }
        catch
        {
            return -1;
        }
    }
    public IEnumerable<ShoppingCart> GetOrders(int memberId)
    {
        var query = from order in db.ShoppingCarts
                    where order.MemberID == memberId
                    orderby order.DateOfCreate descending
                    select order;
        return query;
    }
    public int GetOrdersCount(int MemberId)
    {
        return db.ShoppingCarts.Where(cart => cart.MemberID == MemberId).Count();
    }
    public IEnumerable<MemberInterest> GetMemberInterests(int memberId)
    {
        IEnumerable<MemberInterest> interests = from mi in db.MemberInterests
                                                where mi.MemberID == memberId
                                                select mi;
        return interests;
    }
    public DateTime GetDateOfJoin(int memberId)
    {
        return db.Members.Single(mem => mem.MemberID == memberId).DateOfJoin;
    }
    public int GetVisitsCount(int memberId)
    {
        return db.Logins.Where(log => log.MemberID == memberId).Count();
    }
    public int GetPurchasedMaterialsCount(int MemberId)
    {
        return db.ShoppingCartItems.Where(item => item.ShoppingCart.MemberID == MemberId).Select(item => item.Quantity).Sum();
    }
    public MaterialPrice GetTotalPurchaseAmount(int memberId)
    {
        MaterialsRepository mr = new MaterialsRepository();
        int total = (from order in db.ShoppingCarts
                     where order.MemberID == memberId
                     select ((PriceUnit)order.CostUnit == PriceUnit.Rial ? order.Cost : order.Cost * 10)).Sum();
        return new MaterialPrice(total, PriceUnit.Rial);
    }
    public IEnumerable<MemberInterest> GetTopInterests(int MemberId, int Count)
    {
        return db.MemberInterests.Where(mi => mi.MemberID == MemberId).OrderByDescending(interest => interest.TotalPoints).Take(Count);
    }
    public int GetPositiveVotesCount(string UserName)
    {
        return db.MaterialVotes.Where(vote => vote.Voter == UserName && vote.Vote).Count();
    }
    public int GetNegativeVotesCount(string UserName)
    {
        return db.MaterialVotes.Where(vote => vote.Voter == UserName && !vote.Vote).Count();
    }
    public IEnumerable<MaterialVote> GetNegativeVotes(string UserName)
    {
        return db.MaterialVotes.Where(vote => vote.Voter == UserName && !vote.Vote);
    }
    public IEnumerable<MaterialVote> GetPositiveVotes(string UserName)
    {
        return db.MaterialVotes.Where(vote => vote.Voter == UserName && vote.Vote);
    }
    public double GetSuccessfullPurchasesPercent(int memberId)
    {
        int successCount = db.ShoppingCarts.Where(cart => cart.MemberID == memberId && cart.Status == (int)CartStatus.Delivered).Count();
        int allCount = db.ShoppingCarts.Where(cart => cart.MemberID == memberId).Count();
        return (successCount * 100) / allCount;
    }
    public string GetMemberIP(int memberId)
    {
        try
        {
            return db.Members.Single(member => member.MemberID == memberId).IP;
        }
        catch
        {
            return "";
        }
    }
    //************* ADD METHODS :
    public void AddMember(Member member)
    {
        db.Members.InsertOnSubmit(member);
    }
    public void AddKeywordsToInterests(IEnumerable<MaterialKeyword> keywords, InterestType interestType)
    {
        Member member = GetMember(HttpContext.Current.User.Identity.Name);
        EntitySet<MemberInterest> memberInterests = member.Interests;
        foreach (MaterialKeyword key in keywords)
        {
            MemberInterest mi = new MemberInterest();
            mi.MemberID = member.MemberID;
            mi.Name = key.Word;
            mi.TotalPoints = (int)interestType * key.Type;
            //
            IEnumerable<MemberInterest> query = from i in memberInterests where i.Name == mi.Name.Replace("ـ", "") select i;
            if (query.Count() > 0)
            {
                MemberInterest last = query.Last();
                last.TotalPoints += mi.TotalPoints;
            }
            else
            {
                mi.Name = mi.Name.Replace("ـ", "");
                memberInterests.Add(mi);
            }
        }
    }
    public void AddInterest(MemberInterest interest)
    {
        IEnumerable<MemberInterest> query = from i in db.MemberInterests where i.Name == interest.Name.Replace("ـ", "") select i;
        if (query.Count() > 0)
        {
            MemberInterest last = query.Last();
            last.TotalPoints += interest.TotalPoints;
        }
        else
        {
            interest.Name = interest.Name.Replace("ـ", "");
            db.MemberInterests.InsertOnSubmit(interest);
        }
    }
    public void AddLogin(Login login)
    {
        db.Logins.InsertOnSubmit(login);
    }
    //************* SUBMIT :
    public void Save()
    {
        db.SubmitChanges();
    }
}
