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
            var units = mr.GetFieldUnits();
            //
            FieldsRepository fr = new FieldsRepository();
            FillFieldTypesComboBox(fr.FieldTypes_ListOfFieldType());
            FillFieldUnitsCombobox(units);
        }
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
        //System.Threading.Thread.Sleep(3000);
        MaterialTypeField field = new MaterialTypeField();
        field.TypeID = Convert.ToInt32(cmb_Types.SelectedItem.Value);
        field.Name = tb_Name.Text;
        field.FieldType = cmb_FieldType.SelectedIndex;
        if (cmb_Unit.SelectedIndex > 0)
            field.UnitID = Int32.Parse(cmb_Unit.SelectedItem.Value);
        //
        MaterialsRepository mr = new MaterialsRepository();
        mr.AddMaterialTypeField(field);
        mr.Save();
        //
        Repository.MessageBoxShow("بـا مـوفقیت افـزوده شد", up_Save, this.GetType());
    }
}
