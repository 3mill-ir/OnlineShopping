using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Admin_NewMaterialType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int unitId = Convert.ToInt32(Request.QueryString["UnitID"]);
            MaterialsRepository mr = new MaterialsRepository();
            FieldUnit unit = mr.GetFieldUnit(unitId);
            tb_Name.Text = unit.Name;
            hidden_UnitID.Value = unitId.ToString();
        }
    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        MaterialsRepository mr = new MaterialsRepository();
        int unitId = Convert.ToInt32(hidden_UnitID.Value);
        FieldUnit unit = mr.GetFieldUnit(unitId);
        unit.Name = tb_Name.Text;
        //save :
        mr.Save();
        //
        Repository.MessageBoxShow("بـا مـوفقیت ذخیـره شد", up_Save, this.GetType());
    }
}
