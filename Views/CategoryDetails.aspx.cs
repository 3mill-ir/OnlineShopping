using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_CategoryDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MaterialsRepository mr = new MaterialsRepository();
            int categoryId = Convert.ToInt32(Request.QueryString["CategoryID"]);
            Category category = mr.GetCategory(categoryId);
            Category = category;
            title.Text = category.Name;
            //Fill Sets Datalist :
            var sets = mr.GetCategorySets(categoryId);
            FillSetsDataList(sets);
            //Add to Interests : 
            if (User.Identity.IsAuthenticated && User.IsInRole(MyRoles.Member))
            {
                MembersRepository mRep = new MembersRepository();
                int memberId = mRep.GetMemberID(User.Identity.Name);
                MemberInterest mi = new MemberInterest();
                mi.MemberID = memberId;
                mi.Name = category.Name;
                mi.TotalPoints = (int)KeywordType.CategoryName * (int)InterestType.View;
                mRep.AddInterest(mi);
                mRep.Save();
            }
        }
    }
    private void FillSetsDataList(IEnumerable<Set> Sets)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SetID", typeof(Int32));
        dt.Columns.Add("Name", typeof(String));
        foreach (Set set in Sets)
        {
            DataRow row = dt.NewRow();
            row["SetID"] = set.SetID;
            row["Name"] = set.Name;
            dt.Rows.Add(row);
        }
        DataList_Sets.DataSource = dt;
        DataList_Sets.DataBind();
    }
    protected Category Category { get; set; }
}
