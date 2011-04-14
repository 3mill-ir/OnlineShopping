using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using Models;

public partial class Views_Admin_NewMaterial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MaterialsRepository mr = new MaterialsRepository();
            myc_SetSelector.Categories = mr.GetAllCategories();
            IEnumerable<MyUnit> myUnits = MyUnits.ToUnitList;
            var types = mr.GetMaterialTypes();
            FillMaterialTypesCombobax(types);
            //
            int materialID = Convert.ToInt32(Request.QueryString["MaterialID"]);
            Material material = mr.GetMaterial(materialID);
            //
            tb_Code.Text = material.Code;
            tb_Name.Text = material.Name;
            myc_SetSelector.SetID = material.SetID;
            tb_Description.Text = material.Description;
            tb_PurchasePrice.Text = material.PurchasePrice.ToString();
            ch_Visible.Checked = material.Visible;
            cmb_PurchasePriceUnit.SelectedIndex = material.PurchasePriceUnit;
            cmb_Status.SelectedIndex = material.Status;
            if ((MaterialStatus)material.Status == MaterialStatus.Exists_DeterminedCounts) { tb_Counts.Enabled = true; tb_Counts.Text = material.Counts.ToString(); } else { tb_Counts.Text = ""; tb_Counts.Enabled = false; }
            cmb_Unit.SelectedIndex = material.Unit;
            if (material.Weight != null) { tb_Weight.Text = material.Weight.ToString(); }
            cmb_WeightUnit.SelectedIndex = material.WeightUnit;
            if (material.Height != null) { tb_Height.Text = material.Height.ToString(); }
            if (material.Width != null) { tb_Width.Text = material.Width.ToString(); }
            if (material.Length != null) { tb_Length.Text = material.Length.ToString(); }
            cmb_MaterialType.SelectedIndex = IndexOfMaterialType(material.TypeID);
            tb_ShopID.Text = material.ShopID.ToString();
            myc_Keywords.KeywordsString = material.Tags;
            //
            hidden_MaterialID.Value = materialID.ToString();
        }
    }
    private int IndexOfMaterialType(int typeId)
    {
        int index = 0, i = 0;
        foreach (ListItem item in cmb_MaterialType.Items)
        {
            if (item.Value == typeId.ToString())
                index = i;
            i++;
        }
        return index;
    }
    private void FillMaterialTypesCombobax(IEnumerable<MaterialType> types)
    {
        cmb_MaterialType.Items.Clear();
        cmb_MaterialType.Items.Add(new ListItem("", ""));
        foreach (MaterialType type in types)
        {
            cmb_MaterialType.Items.Add(new ListItem(type.Name, type.TypeID.ToString()));
        }
    }
    private void FillUnitsCombo(IEnumerable<MyUnit> m)
    {
        cmb_Unit.Items.Clear();
        cmb_Unit.Items.Add(new ListItem("", ""));
        foreach (MyUnit u in m)
        {
            cmb_Unit.Items.Add(new ListItem(MyUnits.UnitString(u), ((int)u).ToString()));
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MaterialsRepository mr = new MaterialsRepository();
        //check :
        int ShopID = Convert.ToInt32(tb_ShopID.Text);
        ShopsRepository sr = new ShopsRepository();
        Shop shop = sr.GetShop(ShopID);
        if (shop != null)
        {
            int result = 0;
            double resultt = 0;
            //material obj :
            int materialId = Convert.ToInt32(hidden_MaterialID.Value);
            Material material = mr.GetMaterial(materialId);
            material.ShopID = ShopID;
            material.SetID = myc_SetSelector.SetID;
            material.Name = tb_Name.Text;
            material.Code = tb_Code.Text;
            material.Description = tb_Description.Text;
            material.Tags = myc_Keywords.KeywordsString;
            material.Unit = cmb_Unit.SelectedIndex;
            material.Status = cmb_Status.SelectedIndex;
            material.TypeID = Convert.ToInt32(cmb_MaterialType.SelectedItem.Value);
            material.Visible = ch_Visible.Checked;
            material.Keywords = GetKeywords();
            if (Int32.TryParse(tb_PurchasePrice.Text, out result))
                material.PurchasePrice = Int32.Parse(tb_PurchasePrice.Text);
            material.PurchasePriceUnit = Convert.ToInt32(cmb_PurchasePriceUnit.SelectedItem.Value);
            if (Int32.TryParse(tb_Counts.Text, out result))
                material.Counts = Int32.Parse(tb_Counts.Text);
            if (Double.TryParse(tb_Weight.Text, out resultt))
                material.Weight = Double.Parse(tb_Weight.Text);
            material.WeightUnit = cmb_WeightUnit.SelectedIndex;
            if (Int32.TryParse(tb_Width.Text, out result))
                material.Width = Int32.Parse(tb_Width.Text);
            if (Int32.TryParse(tb_Height.Text, out result))
                material.Height = Int32.Parse(tb_Height.Text);
            if (Int32.TryParse(tb_Length.Text, out result))
                material.Length = Int32.Parse(tb_Length.Text);
            //save:
            mr.Save();
            Repository.MessageBoxShow("با موفقیت ذخیره شد.", Page, this.GetType());
        }
        else
        {
            Repository.MessageBoxShow("فروشگاهی با کد وارد شده وجود ندارد", Page, this.GetType());
        }
    }
    private EntitySet<MaterialKeyword> GetKeywords()
    {
        EntitySet<MaterialKeyword> keys = new EntitySet<MaterialKeyword>();
        //CategoryName : 
        MaterialKeyword key_CategoryName = new MaterialKeyword();
        key_CategoryName.Word = myc_SetSelector.CategoryName;
        key_CategoryName.Type = (int)KeywordType.CategoryName;
        keys.Add(key_CategoryName);
        //Set Name :
        MaterialKeyword key_SetName = new MaterialKeyword();
        key_SetName.Word = myc_SetSelector.SetName;
        key_SetName.Type = (int)KeywordType.SetName;
        keys.Add(key_SetName);
        //Material Type :
        MaterialKeyword key_MaterialType = new MaterialKeyword();
        key_MaterialType.Word = cmb_MaterialType.SelectedItem.Text;
        key_MaterialType.Type = (int)KeywordType.MaterialType;
        keys.Add(key_MaterialType);
        //Name :
        MaterialKeyword key_Name = new MaterialKeyword();
        key_Name.Word = tb_Name.Text;
        key_Name.Type = (int)KeywordType.Name;
        keys.Add(key_Name);
        //Tags:
        foreach (string tag in myc_Keywords.Keywords)
        {
            MaterialKeyword key_Tag = new MaterialKeyword();
            key_Tag.Word = tag;
            key_Tag.Type = (int)KeywordType.Tag;
            keys.Add(key_Tag);
        }
        return keys;
        //
    }
    protected void cmb_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        tb_Counts.Enabled = (cmb_Status.SelectedIndex == 2 ? true : false);
    }
}
