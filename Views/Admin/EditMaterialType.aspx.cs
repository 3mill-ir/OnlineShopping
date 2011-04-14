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
            MaterialsRepository mr = new MaterialsRepository();
            int typeId = Convert.ToInt32(Request.QueryString["TypeID"]);
            MaterialType type = mr.GetMaterialType(typeId);
            tb_Name.Text = type.Name;
            hidden_TypeID.Value = type.TypeID.ToString();
        }
    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        MaterialsRepository mr = new MaterialsRepository();
        MaterialType type = mr.GetMaterialType(Convert.ToInt32(hidden_TypeID.Value));
        type.Name = tb_Name.Text;
        //save :
        mr.Save();
        //
        Repository.MessageBoxShow("بـا مـوفقیت ذخیره شد", up_Save, this.GetType());
    }
}
