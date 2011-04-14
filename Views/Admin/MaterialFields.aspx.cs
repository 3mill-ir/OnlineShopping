using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Web.UI.HtmlControls;

public partial class Views_Admin_MaterialFields : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int materialId = Convert.ToInt32(Request.QueryString["MaterialID"]);
            hidden_MaterialID.Value = materialId.ToString();
        }
    }
    override protected void OnInit(EventArgs e)
    {
        int materialId = Convert.ToInt32(Request.QueryString["MaterialID"]);
        MaterialsRepository mr = new MaterialsRepository();
        Material material = mr.GetMaterial(materialId);
        var fields = material.Type.Fields;
        HtmlTable tbl = new HtmlTable();
        tbl.ID = "MainTable";
        tbl.CellPadding = 3;
        tbl.Border = 0;
        tbl.CellSpacing = 0;
        tbl.Attributes["class"] = "MemberInfo Centerize";
        tbl.Attributes["width"] = "95%";
        tbl.Attributes["style"] = "margin-top : 15px;";
        FieldsRepository fr = new FieldsRepository();
        int i = 0;
        foreach (MaterialTypeField field in fields)
        {
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td_Text = new HtmlTableCell();
            td_Text.Align = "left";
            td_Text.Attributes["style"] = "font-weight : bold;";
            td_Text.Attributes["class"] = "MemberInfo_RightTD";
            HtmlTableCell td_Value = new HtmlTableCell();
            td_Value.Attributes["style"] = "font-weight : normal;";
            td_Value.Attributes["class"] = "MemberInfo_LeftTD";
            //text label :
            Label lbl_Text = new Label();
            lbl_Text.Text = field.Name + " :";
            td_Text.Controls.Add(lbl_Text);
            //id hidden :
            HiddenField hidden_fieldType = new HiddenField();
            hidden_fieldType.ID = "FieldType" + i.ToString();
            hidden_fieldType.Value = field.FieldType.ToString();
            td_Text.Controls.Add(hidden_fieldType);
            HiddenField hidden_fieldId = new HiddenField();
            hidden_fieldId.ID = "FieldID" + i.ToString();
            hidden_fieldId.Value = field.FieldID.ToString();
            td_Text.Controls.Add(hidden_fieldId);
            //input controls :
            if ((FieldType)field.FieldType == FieldType.Text)
            {
                TextBox tb_Value = new TextBox();
                Label lbl_Unit = new Label();
                //att :
                tb_Value.CssClass = "TextBoxes";
                tb_Value.ID = "row_" + i.ToString() + "_value1";
                tb_Value.Width = new Unit("200px");
                lbl_Unit.Text = ( field.Unit != null ? " " + field.Unit.Name : "");
                //set values :
                if (field.ValueOf(materialId) != null)
                {
                    tb_Value.Text = field.ValueOf(materialId).Value;
                }
                //
                td_Value.Controls.Add(tb_Value);
                td_Value.Controls.Add(lbl_Unit);
            }
            else if ((FieldType)field.FieldType == FieldType.YesNo)
            {
                RadioButton ch_yes = new RadioButton();
                RadioButton ch_no = new RadioButton();
                ch_yes.ID = "row_" + i.ToString() + "_value1";
                ch_no.ID = "row_" + i.ToString() + "_value2";
                ch_yes.Text = "بلــه";
                ch_no.Text = "نــه";
                ch_yes.Checked = true;
                ch_no.Attributes["style"] = "padding-right : 15px;";
                ch_yes.GroupName = "YesNoField" + i.ToString();
                ch_no.GroupName = "YesNoField" + i.ToString();
                //set values :
                if (field.ValueOf(materialId) != null)
                {
                    ch_yes.Checked = Convert.ToBoolean(field.ValueOf(materialId).Value);
                    ch_no.Checked = !Convert.ToBoolean(field.ValueOf(materialId).Value);
                }
                //
                td_Value.Controls.Add(ch_yes);
                td_Value.Controls.Add(ch_no);
            }
            else
            {
                TextBox tb_Value = new TextBox();
                Label lbl_Unit = new Label();
                //att :
                tb_Value.CssClass = "TextBoxes";
                tb_Value.ID = "row_" + i.ToString() + "_value1";
                tb_Value.Width = new Unit("40px");
                lbl_Unit.Text = " " + field.Unit.Name;
                //set values :
                if (field.ValueOf(materialId) != null)
                {
                    tb_Value.Text = field.ValueOf(materialId).Value;
                }
                //
                td_Value.Controls.Add(tb_Value);
                td_Value.Controls.Add(lbl_Unit);
            }
            //add :
            tr.Cells.Add(td_Text);
            tr.Cells.Add(td_Value);
            tbl.Rows.Add(tr);
            i++;
        }
        pnl_Controls.Controls.Add(tbl);
        //
        base.OnInit(e);
    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        int materialId = Convert.ToInt32(hidden_MaterialID.Value);
        MaterialsRepository mr = new MaterialsRepository();
        Material material = mr.GetMaterial(materialId);
        mr.DeleteMaterialTypeFieldValues(material.FieldValues);
        HtmlTable tbl = pnl_Controls.FindControl("MainTable") as HtmlTable;
        for (int i = 0; i < tbl.Rows.Count; i++) 
        {
            HtmlTableRow row = tbl.Rows[i];
            HiddenField hidden_type = tbl.Rows[i].Cells[0].FindControl("FieldType" + i.ToString()) as HiddenField;
            HiddenField hidden_id = tbl.Rows[i].Cells[0].FindControl("FieldID" + i.ToString()) as HiddenField;
            int fieldId = Convert.ToInt32(hidden_id.Value);
            FieldType fieldType = (FieldType)Convert.ToInt32(hidden_type.Value);
            //
            MaterialTypeFieldValue fieldValue = new MaterialTypeFieldValue();
            fieldValue.MaterialID = materialId;
            fieldValue.FieldID = fieldId;
            if (fieldType == FieldType.Text)
            {
                TextBox tb_value_1 = row.Cells[1].FindControl("row_" + i.ToString() + "_value1") as TextBox;
                fieldValue.Value = tb_value_1.Text;
            }
            else if (fieldType == FieldType.YesNo)
            {
                RadioButton ch_yes = row.Cells[1].FindControl("row_" + i.ToString() + "_value1") as RadioButton;
                RadioButton ch_no = row.Cells[1].FindControl("row_" + i.ToString() + "_value2") as RadioButton;
                fieldValue.Value = (ch_yes.Checked ? true.ToString() : false.ToString());
            }
            else if (fieldType == FieldType.Digital)
            {
                TextBox tb_value_1 = row.Cells[1].FindControl("row_" + i.ToString() + "_value1") as TextBox;
                fieldValue.Value = tb_value_1.Text;
            }
            else
            {
                TextBox tb_value_1 = row.Cells[1].FindControl("row_" + i.ToString() + "_value1") as TextBox;
                fieldValue.Value = tb_value_1.Text;
            }
            //
            if (fieldValue.Value.Length > 0)
                material.FieldValues.Add(fieldValue);
        }
        mr.Save();
        Repository.MessageBoxShow("با موفقیت ذخیره شد", Page, this.GetType());
    }
}
