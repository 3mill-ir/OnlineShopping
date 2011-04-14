using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_Admin_Materials : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MaterialsRepository mr = new MaterialsRepository();
            var categories = mr.GetAllCategories();
            FillCategoriesCombo(categories);
            FillCategoriesDataList(categories);
            //
            myc_SetSelector.Categories = categories;
        }
    }
    private void FillCategoriesDataList(IEnumerable<Category> categories)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CategoryID", typeof(Int32));
        dt.Columns.Add("Sequence", typeof(Double));
        dt.Columns.Add("Name", typeof(String));
        foreach (Category cat in categories)
        {
            DataRow row = dt.NewRow();
            row["CategoryID"] = cat.CategoryID;
            row["Sequence"] = cat.Sequence;
            row["Name"] = cat.Name;
            dt.Rows.Add(row);
        }
        DataList_Categories.DataSource = dt;
        DataList_Categories.DataBind();
        Material mat = new Material();
    }
    private void FillCategoriesCombo(IEnumerable<Category> categories)
    {
        cmb_Categories.Items.Clear();
        cmb_Categories.Items.Add(new ListItem("", ""));
        foreach (Category cat in categories)
        {
            cmb_Categories.Items.Add(new ListItem(cat.Name, cat.CategoryID.ToString()));
        }
        //
    }
    protected void cmb_Categories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_Categories.SelectedIndex > 0)
        {
            int categoryId = Convert.ToInt32(cmb_Categories.SelectedItem.Value);
            MaterialsRepository mr = new MaterialsRepository();
            var sets = mr.GetCategorySets(categoryId);
            FillSetsDataList(sets);
        }
    }
    private void FillSetsDataList(IEnumerable<Set> Sets)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SetID", typeof(Int32));
        dt.Columns.Add("Sequence", typeof(Double));
        dt.Columns.Add("Name", typeof(String));
        foreach (Set set in Sets)
        {
            DataRow row = dt.NewRow();
            row["SetID"] = set.SetID;
            row["Sequence"] = set.Sequence;
            row["Name"] = set.Name;
            dt.Rows.Add(row);
        }
        DataList_Sets.DataSource = dt;
        DataList_Sets.DataBind();
    }
    protected void SetSelector_SetSelected(object sender, EventArgs e)
    {
        MaterialsRepository mr = new MaterialsRepository();
        var materials = mr.GetSetAllMaterials(myc_SetSelector.SetID);
        FillMaterialsDataList(materials);
    }
    private void FillMaterialsDataList(IEnumerable<Material> materials)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Link", typeof(String));
        dt.Columns.Add("MaterialID", typeof(Int32));
        foreach (Material mat in materials)
        {
            DataRow row = dt.NewRow();
            row["Link"] = mat.Link(MaterialInfoType.Full, MaterialInfoDirection.Left, MaterialInfoShowType.Show, ResizeType.LongerFix, 120, "MaterialInfo", MaterialInfoHeightType.FitPictureHeight, 0);
            row["MaterialID"] = mat.MaterialID;
            dt.Rows.Add(row);
        }
        DataList_Materials.DataSource = dt;
        DataList_Materials.DataBind();
        Image img = new Image();
    }
    protected void DataList_Categories_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteCategory")
        {
            int categoryId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            mr.DeleteCategory(categoryId);
            mr.Save();
            FillCategoriesDataList(mr.GetAllCategories());
        }
    }
    protected void DataList_Sets_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteSet")
        {
            int setId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            Set set = mr.GetSet(setId);
            var sets = set.Category.Sets;
            mr.DeleteSet(set);
            mr.Save();
            //
            FillSetsDataList(sets);
        }
    }
    protected void DataList_Materials_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteMaterial")
        {
            int materialId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            Material material = mr.GetMaterial(materialId);
            Set set = material.Set;
            mr.DeleteMaterial(material);
            mr.Save();
            //
            FillMaterialsDataList(set.Materials);
        }
    }
}
