using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_SetDetails : System.Web.UI.Page
{
    //*********************
    private const int _CellsCount = 2;
    private const int _RowsCount = 3;
    private const int _PageSize = _CellsCount * _RowsCount;
    private const int _PopularsCount = 10;
    private const int _TickBoxWidth = 680;
    //*********************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int setId = Convert.ToInt32(Request.QueryString["SetID"]);
            MaterialsRepository mr = new MaterialsRepository();
            Set set = mr.GetSet(setId);
            Set = set;
            IEnumerable<Material> setMaterials = mr.Sort(set.Materials, SortField.DateOfAdd, SortType.Descending);
            title.Text = set.Name;
            Page.Title = Constants.StaticTitle + " | " + set.Name;
            hid_SetID.Value = setId.ToString();
            //***** All Links :
            lbl_Links.Text = set.AllMaterialsLinks;
            //***** Populars :
            TickBox tickbox = new TickBox(set.GetPopularMaterials(_PopularsCount), _TickBoxWidth);
            lbl_Populars.Text = tickbox.Html;
            //
            hidden_PageIndex.Value = "0";
            FillMaterialsDatalist(setMaterials);
            //Add to Interests : 
            if (User.Identity.IsAuthenticated && User.IsInRole(MyRoles.Member))
            {
                MembersRepository mRep = new MembersRepository();
                int memberId = mRep.GetMemberID(User.Identity.Name);
                MemberInterest mi = new MemberInterest();
                mi.MemberID = memberId;
                mi.Name = set.Name;
                mi.TotalPoints = (int)KeywordType.SetName * (int)InterestType.View;
                mRep.AddInterest(mi);
                mRep.Save();
            }
        }
    }
    private void FillMaterialsDatalist(IEnumerable<Material> materials)
    {
        int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
        int itemsCount = materials.Count();
        int pagesCount = (int)(itemsCount / _PageSize + (itemsCount % _PageSize == 0 ? 0 : 1));
        DataTable dt = new DataTable();
        dt.Columns.Add("Link", typeof(String));
        foreach (Material material in materials.Skip(pageIndex * _PageSize).Take(_PageSize))
        {
            DataRow row = dt.NewRow();
            row["Link"] = material.Info(ResizeType.WidthFix, 110);//material.Link(MaterialInfoType.NameAndInfo, MaterialInfoDirection.Automatic, MaterialInfoShowType.Show, ResizeType.WidthFix, 110, "MaterialMainInfo", MaterialInfoHeightType.FitPictureHeight, 80);
            dt.Rows.Add(row);
        }
        DataList_Materials.DataSource = dt;
        DataList_Materials.DataBind();
        //
        lbl_PageCounter.Text = "صفحه " + (pageIndex + 1).ToString() + " از " + pagesCount.ToString() ;
        lbl_ShowingRecords.Text = "نمایش از " + (pageIndex * _PageSize + 1).ToString() + " تا " + (pageIndex != pagesCount - 1 ? (pageIndex * _PageSize + _PageSize).ToString() : (pageIndex * _PageSize + ((itemsCount % _PageSize > 0 ? itemsCount % _PageSize : _PageSize))).ToString());
        btn_Previous.Enabled = (pageIndex > 0 ? true : false);
        btn_Next.Enabled = (pageIndex < pagesCount - 1 ? true : false);
    }
    protected void btn_Previous_Click(object sender, EventArgs e)
    {
        int setId = Convert.ToInt32(hid_SetID.Value);
        SortField sortField = (SortField)Convert.ToInt32(hidden_SortField.Value);
        SortType sortType = (SortType)Convert.ToInt32(hidden_SortType.Value);
        MaterialsRepository mr = new MaterialsRepository();
        Set set = mr.GetSet(setId);
        IEnumerable<Material> materials = mr.Sort(set.Materials, sortField, sortType);
        int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
        hidden_PageIndex.Value = (pageIndex - 1).ToString();
        //
        Page.Title = Constants.StaticTitle + " | " + set.Name;
        title.Text = set.Name;
        Set = set;
        //***** Populars :
        TickBox tickbox = new TickBox(set.GetPopularMaterials(_PopularsCount), _TickBoxWidth);
        lbl_Populars.Text = tickbox.Html;
        //
        FillMaterialsDatalist(materials);
    }
    protected void btn_Next_Click(object sender, EventArgs e)
    {
        int setId = Convert.ToInt32(hid_SetID.Value);
        SortField sortField = (SortField)Convert.ToInt32(hidden_SortField.Value);
        SortType sortType = (SortType)Convert.ToInt32(hidden_SortType.Value);
        MaterialsRepository mr = new MaterialsRepository();
        Set set = mr.GetSet(setId);
        IEnumerable<Material> materials = mr.Sort(set.Materials, sortField, sortType);
        int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
        hidden_PageIndex.Value = (pageIndex + 1).ToString();
        //
        Page.Title = Constants.StaticTitle + " | " + set.Name;
        title.Text = set.Name;
        Set = set;
        //***** Populars :
        TickBox tickbox = new TickBox(set.GetPopularMaterials(_PopularsCount), _TickBoxWidth);
        lbl_Populars.Text = tickbox.Html;
        //
        FillMaterialsDatalist(materials);
    }
    protected void btn_Go_Clicked(object sender, EventArgs e)
    {
        int setId = Convert.ToInt32(hid_SetID.Value);
        SortField sortField = (SortField)Convert.ToInt32(cmb_SortField.SelectedItem.Value);
        SortType sortType = (SortType)Convert.ToInt32(cmb_SortType.SelectedItem.Value);
        hidden_SortField.Value = ((int)sortField).ToString();
        hidden_SortType.Value = ((int)sortType).ToString();
        //
        MaterialsRepository mr = new MaterialsRepository();
        Set set = mr.GetSet(setId);
        IEnumerable<Material> setMaterials = set.Materials;
        setMaterials = mr.Sort(setMaterials, sortField, sortType);
        //
        hidden_PageIndex.Value = "0";
        FillMaterialsDatalist(setMaterials);
        //
        Set = set;
        Page.Title = Constants.StaticTitle + " | " + set.Name;
        title.Text = set.Name;
        //***** Populars :
        TickBox tickbox = new TickBox(set.GetPopularMaterials(_PopularsCount), _TickBoxWidth);
        lbl_Populars.Text = tickbox.Html;
        //
    }
    protected Set Set { get; set; }
}

