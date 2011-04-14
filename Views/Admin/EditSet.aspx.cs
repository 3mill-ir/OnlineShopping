using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Admin_NewSet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCategoriesCombo();
            int setId = Convert.ToInt32(Request.QueryString["SetID"]);
            hid_SetID.Value = setId.ToString();
            //
            MaterialsRepository mr = new MaterialsRepository();
            Set set = mr.GetSet(setId);
            //
            cmb_Categories.SelectedIndex = cmb_Categories.Items.IndexOf(new ListItem(set.Category.Name, set.CategoryID.ToString()));
            tb_Name.Text = set.Name;
            tb_Sequence.Text = set.Sequence.ToString();
        }
    }
    private void FillCategoriesCombo()
    {
        MaterialsRepository mr = new MaterialsRepository();
        mr.FillCategoriesCombobox(cmb_Categories);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MaterialsRepository mr = new MaterialsRepository();
        double result = 0.0;
        Set set = mr.GetSet(Convert.ToInt32(hid_SetID.Value));
        set.CategoryID = Convert.ToInt32(cmb_Categories.SelectedItem.Value);
        set.Name = tb_Name.Text;
        set.Sequence = (Double.TryParse(tb_Sequence.Text, out result) ? Double.Parse(tb_Sequence.Text) : mr.GetSetsLastSquence() + 1);
        //
        mr.Save();
        Repository.MessageBoxShow("با موفقیت ذخیره شد", Up_Save, this.GetType());
    }
}
