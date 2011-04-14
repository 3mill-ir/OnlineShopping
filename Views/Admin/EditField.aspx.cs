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
using System.Collections.Generic;
using Models;

public partial class Views_Admin_NewField : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MaterialsRepository mr = new MaterialsRepository();
            var types = mr.GetMaterialTypes();
            FillTypesComboBox(types);
            //
            FieldsRepository fr = new FieldsRepository();
            FillFieldTypesComboBox(fr.FieldTypes_ListOfFieldType());
            var units = mr.GetFieldUnits();
            FillFieldUnitsCombobox(units);
            //
            int fieldId = Convert.ToInt32(Request.QueryString["FieldID"]);
            hid_FieldID.Value = fieldId.ToString();
            MaterialTypeField field = mr.GetMaterialTypeField(fieldId);
            cmb_Types.SelectedIndex = GetMaterialTypeIndex(field.TypeID);
            tb_Name.Text = field.Name;
            cmb_Unit.SelectedIndex = IndexOfUnitID((field.UnitID != null ? field.UnitID.Value : 0));
            cmb_FieldType.SelectedIndex = field.FieldType;
        }
    }
    private int IndexOfUnitID(int unitId)
    {
        int index = 0, i = 0;
        foreach (ListItem item in cmb_Unit.Items)
        {
            if (item.Value == unitId.ToString())
                index = i;
            i++;
        }
        return index;
    }
    private int GetMaterialTypeIndex(int typeId)
    {
        int index = 0;
        int i = 0;
        foreach (ListItem item in cmb_Types.Items)
        {
            if (item.Value == typeId.ToString())
                index = i;
            i++;
        }
        return index;
    }
    private void FillFieldUnitsCombobox(IEnumerable<FieldUnit> fieldUnits)
    {
        FieldsRepository fr = new FieldsRepository();
        cmb_Unit.Items.Clear();
        cmb_Unit.Items.Add(new ListItem("", ""));
        foreach (FieldUnit unit in fieldUnits)
        {
            cmb_Unit.Items.Add(new ListItem(unit.Name, unit.ID.ToString()));
        }
    }
    private void FillFieldTypesComboBox(List<FieldType> fieldTypes)
    {
        FieldsRepository fr = new FieldsRepository();
        cmb_FieldType.Items.Clear();
        cmb_FieldType.Items.Add(new ListItem("", ""));
        foreach (FieldType fieldType in fieldTypes)
        {
            cmb_FieldType.Items.Add(new ListItem(fr.ToString(fieldType), ((int)fieldType).ToString()));
        }
    }
    private void FillTypesComboBox(IEnumerable<MaterialType> types)
    {
        cmb_Types.Items.Clear();
        cmb_Types.Items.Add(new ListItem("", ""));
        foreach (MaterialType type in types)
        {
            cmb_Types.Items.Add(new ListItem(type.Name, type.TypeID.ToString()));
        }
    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        MaterialsRepository mr = new MaterialsRepository();
        MaterialTypeField field = mr.GetMaterialTypeField(Convert.ToInt32(hid_FieldID.Value));
        field.TypeID = Convert.ToInt32(cmb_Types.SelectedItem.Value);
        field.Name = tb_Name.Text;
        field.FieldType = cmb_FieldType.SelectedIndex;
        if (cmb_Unit.SelectedIndex > 0)
            field.UnitID = Int32.Parse(cmb_Unit.SelectedItem.Value);
        //
        mr.Save();
        //
        Repository.MessageBoxShow("بـا مـوفقیت ذخیـره شد", up_Save, this.GetType());
    }
}
