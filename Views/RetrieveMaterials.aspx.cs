using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Models;
using System.Collections.Generic;

public partial class Views_RetrieveMaterials : System.Web.UI.Page
{
    //*********************
    private const int _CellsCount = 2;
    private const int _RowsCount = 3;
    private const int _PageSize = _CellsCount * _RowsCount;
    //*********************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Type"] == null && Request.QueryString["Field"] != null && Request.QueryString["Value"] != null)
            {
                hid_QueryType.Value = "Field";
                int fieldId = Convert.ToInt32(Request.QueryString["Field"]);
                string fieldValue = Request.QueryString["Value"].ToString().Replace('$', ' ');
                MaterialsRepository mr = new MaterialsRepository();
                var materials = mr.GetMaterials(fieldId, fieldValue);
                FillMaterialsDatalist(mr.Sort(materials, SortField.DateOfAdd, SortType.Descending));
                lbl_Subject.Text = GetSubject(fieldId, fieldValue);
            }
            else if (Request.QueryString["PriceUnit"] != null)
            {
                hid_QueryType.Value = "Price";
                int typeId = Convert.ToInt32(Request.QueryString["Type"]);
                string field = Request.QueryString["Field"].ToString();
                int price = Convert.ToInt32(Request.QueryString["Value"]);
                int priceUnit = Convert.ToInt32(Request.QueryString["PriceUnit"]);
                MaterialsRepository mr = new MaterialsRepository();
                var materials = mr.GetMaterials(typeId, price, priceUnit);
                if (materials != null)
                    FillMaterialsDatalist(mr.Sort(materials, SortField.DateOfAdd, SortType.Descending));
                lbl_Subject.Text = GetSubject(price, priceUnit);
            }
            else if (Request.QueryString["Tag"] != null)
            {
                hid_QueryType.Value = "Tag";
                string tag = Request.QueryString["Tag"].ToString().Replace("$", " ");
                MaterialsRepository mr = new MaterialsRepository();
                var materials = mr.GetMaterials(tag);
                FillMaterialsDatalist(mr.Sort(materials, SortField.DateOfAdd, SortType.Descending));
                lbl_Subject.Text = GetSubject(tag);
            }
        }
    }
    protected string GetSubject(int price, int priceUnit)
    {
        string res = "";
        res = "لیست کـالاهـای مشـابه دارای وجـه اشتـراک در ویـژگی : " + "<b>قیمت</b>" + " با مقدار : " + "<b>" + price.ToString() + ( (PriceUnit)priceUnit == PriceUnit.Rial ? " ریـال" : ( (PriceUnit)priceUnit == PriceUnit.Tuman ? "تومـان" : "")) + " +- 10%" + "</b>";
        return res;
    }
    protected string GetSubject(int fieldId, string fieldValue)
    {
        string res = "";
        MaterialsRepository mr = new MaterialsRepository();
        MaterialTypeField field = mr.GetMaterialTypeField(fieldId);
        res = "لیست کـالاهـای مشـابه دارای وجـه اشتـراک در ویـژگی : " + "<b>" + field.Name + "</b>" + " با مقدار : " + "<b>" + fieldValue + (field.Unit != null ? " " + field.Unit.Name : "") + "</b>";
        return res;
    }
    protected string GetSubject(string Tag)
    {
        string res = "";
        res = "لیست کـالاهـای دارای برچسب : " + "<b>" + Tag + "</b>";
        return res;
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
    protected void btn_Previous_Click(object sender, EventArgs e)
    {
        if (hid_QueryType.Value == "Field")
        {
            int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
            hidden_PageIndex.Value = ( pageIndex - 1 >= 0 ? (pageIndex - 1).ToString() : "0");
            //
            int fieldId = Convert.ToInt32(Request.QueryString["Field"]);
            string fieldValue = Request.QueryString["Value"].ToString().Replace('$', ' ');
            MaterialsRepository mr = new MaterialsRepository();
            var materials = mr.GetMaterials(fieldId, fieldValue);
            FillMaterialsDatalist(materials);
            lbl_Subject.Text = GetSubject(fieldId, fieldValue);
        }
        else if (hid_QueryType.Value == "Price")
        {
            int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
            hidden_PageIndex.Value = (pageIndex - 1 >= 0 ? (pageIndex - 1).ToString() : "0");
            //
            int typeId = Convert.ToInt32(Request.QueryString["Type"]);
            string field = Request.QueryString["Field"].ToString();
            int price = Convert.ToInt32(Request.QueryString["Value"]);
            int priceUnit = Convert.ToInt32(Request.QueryString["PriceUnit"]);
            MaterialsRepository mr = new MaterialsRepository();
            var materials = mr.GetMaterials(typeId, price, priceUnit);
            if (materials != null)
                FillMaterialsDatalist(materials);
            lbl_Subject.Text = GetSubject(price, priceUnit);
        }
        else if (hid_QueryType.Value == "Tag")
        {
            int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
            hidden_PageIndex.Value = (pageIndex - 1 >= 0 ? (pageIndex - 1).ToString() : "0");
            //
            string tag = Request.QueryString["Tag"].ToString().Replace("$", " ");
            MaterialsRepository mr = new MaterialsRepository();
            var materials = mr.GetMaterials(tag);
            FillMaterialsDatalist(materials);
            lbl_Subject.Text = GetSubject(tag);
        }
    }
    protected void btn_Next_Click(object sender, EventArgs e)
    {
        if (hid_QueryType.Value == "Field")
        {
            int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
            hidden_PageIndex.Value = (pageIndex + 1).ToString();
            //
            int fieldId = Convert.ToInt32(Request.QueryString["Field"]);
            string fieldValue = Request.QueryString["Value"].ToString().Replace('$', ' ');
            MaterialsRepository mr = new MaterialsRepository();
            var materials = mr.GetMaterials(fieldId, fieldValue);
            FillMaterialsDatalist(mr.Sort(materials, SortField.DateOfAdd, SortType.Descending));
            lbl_Subject.Text = GetSubject(fieldId, fieldValue);
        }
        else if (hid_QueryType.Value == "Price")
        {
            int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
            hidden_PageIndex.Value = (pageIndex + 1).ToString();
            //
            int typeId = Convert.ToInt32(Request.QueryString["Type"]);
            string field = Request.QueryString["Field"].ToString();
            int price = Convert.ToInt32(Request.QueryString["Value"]);
            int priceUnit = Convert.ToInt32(Request.QueryString["PriceUnit"]);
            MaterialsRepository mr = new MaterialsRepository();
            var materials = mr.GetMaterials(typeId, price, priceUnit);
            if (materials != null)
                FillMaterialsDatalist(mr.Sort(materials, SortField.DateOfAdd, SortType.Descending));
            lbl_Subject.Text = GetSubject(price, priceUnit);
        }
        else if (hid_QueryType.Value == "Tag")
        {
            int pageIndex = Convert.ToInt32(hidden_PageIndex.Value);
            hidden_PageIndex.Value = (pageIndex + 1).ToString();
            //
            string tag = Request.QueryString["Tag"].ToString().Replace("$", " ");
            MaterialsRepository mr = new MaterialsRepository();
            var materials = mr.GetMaterials(tag);
            FillMaterialsDatalist(mr.Sort(materials, SortField.DateOfAdd, SortType.Descending));
            lbl_Subject.Text = GetSubject(tag);
        }
    }
}
