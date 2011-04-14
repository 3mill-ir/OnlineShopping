using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data;

public partial class Views_Admin_MaterialTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MaterialsRepository mr = new MaterialsRepository();
            var types = mr.GetMaterialTypes();
            FillTypesDataList(types);
            FillTypesComboBox(types);
            var fieldUnits = mr.GetFieldUnits();
            FillFieldUnitsDatalist(fieldUnits);
        }
    }
    private void FillFieldUnitsDatalist(IEnumerable<FieldUnit> units)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(Int32));
        dt.Columns.Add("Name", typeof(String));
        foreach (FieldUnit unit in units)
        {
            DataRow row = dt.NewRow();
            row["ID"] = unit.ID;
            row["Name"] = unit.Name;
            dt.Rows.Add(row);
        }
        DataList_units.DataSource = dt;
        DataList_units.DataBind();
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
    private void FillTypesDataList(IEnumerable<MaterialType> types)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("TypeID", typeof(Int32));
        dt.Columns.Add("Name", typeof(String));
        foreach (MaterialType type in types)
        {
            DataRow row = dt.NewRow();
            row["TypeID"] = type.TypeID;
            row["Name"] = type.Name;
            dt.Rows.Add(row);
        }
        DataList_Types.DataSource = dt;
        DataList_Types.DataBind();
    }
    protected void cmb_Types_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_Types.SelectedIndex > 0)
        {
            int typeId = Convert.ToInt32(cmb_Types.SelectedItem.Value);
            MaterialsRepository mr = new MaterialsRepository();
            var fields = mr.GetMaterialTypeFields(typeId);
            FillFieldsDataList(fields);
        }
    }
    private void FillFieldsDataList(IEnumerable<MaterialTypeField> Fields)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FieldID", typeof(Int32));
        dt.Columns.Add("Name", typeof(String));
        foreach (MaterialTypeField field in Fields)
        {
            DataRow row = dt.NewRow();
            row["FieldID"] = field.FieldID;
            row["Name"] = field.Name;
            dt.Rows.Add(row);
        }
        DataList_Fields.DataSource = dt;
        DataList_Fields.DataBind();
    }
    protected void DataList_Types_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteType")
        {
            int typeId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            mr.DeleteMaterialType(typeId);
            mr.Save();
            //
            var types = mr.GetMaterialTypes();
            FillTypesDataList(types);
        }
    }
    protected void DataList_Fields_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteField")
        {
            int fieldId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            MaterialTypeField field = mr.GetMaterialTypeField(fieldId);
            mr.DeleteMaterialTypeField(field);
            mr.Save();
            //
            var fields = mr.GetMaterialTypeFields(field.TypeID);
            FillFieldsDataList(fields);
        }
    }
}
