using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using Models;

public partial class Views_SearchResult : System.Web.UI.Page
{
    //************************
    private const int _CellsCount = 2;
    private const int _RowsCount = 3;
    private const int _PageSize = _CellsCount * _RowsCount;
    //************************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((SearchType)Convert.ToInt32(Request.QueryString["SearchType"]) == SearchType.AllCategories)
            {
                string text = Request.QueryString["Text"].ToString();
                hidden_Text.Value = text;
                MaterialsRepository mr = new MaterialsRepository();
                var materials = mr.Search(text);
                FillMaterialsDatalist(materials);
            }
            else if ((SearchType)Convert.ToInt32(Request.QueryString["SearchType"]) == SearchType.SpecificCategory)
            {
                string text = Request.QueryString["Text"].ToString();
                hidden_Text.Value = text;
                int categoryId = Convert.ToInt32(Request.QueryString["CategoryID"]);
                MaterialsRepository mr = new MaterialsRepository();
                var materials = mr.Search(text, categoryId);
                FillMaterialsDatalist(materials);
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
        lbl_PageCounter.Text = "صفحه " + (pageIndex + 1).ToString() + " از " + pagesCount.ToString();
        lbl_ShowingRecords.Text = "نمایش از " + (pageIndex * _PageSize + 1).ToString() + " تا " + (pageIndex != pagesCount - 1 ? (pageIndex * _PageSize + _PageSize).ToString() : (pageIndex * _PageSize + ((itemsCount % _PageSize > 0 ? itemsCount % _PageSize : _PageSize))).ToString());
        btn_Previous.Enabled = (pageIndex > 0 ? true : false);
        btn_Next.Enabled = (pageIndex < pagesCount - 1 ? true : false);
    }
    protected void btn_Next_Click(object sender, EventArgs e)
    {
        //increase page index :
        int index = Convert.ToInt32(hidden_PageIndex.Value);
        index++;
        hidden_PageIndex.Value = index.ToString();
        //
        string text = hidden_Text.Value;
        MaterialsRepository mr = new MaterialsRepository();
        var materials = mr.Search(text);
        FillMaterialsDatalist(materials);
    }
    protected void btn_Previous_Click(object sender, EventArgs e)
    {
        //decrease page index :
        int index = Convert.ToInt32(hidden_PageIndex.Value);
        index = index - 1;
        hidden_PageIndex.Value = index.ToString();
        //
        string text = hidden_Text.Value;
        MaterialsRepository mr = new MaterialsRepository();
        var materials = mr.Search(text);
        FillMaterialsDatalist(materials);
    }
}
