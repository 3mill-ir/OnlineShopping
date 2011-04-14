using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Admin_NewCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int categoryId = Convert.ToInt32(Request.QueryString["CategoryID"]);
            MaterialsRepository mr = new MaterialsRepository();
            Category category = mr.GetCategory(categoryId);
            tb_Name.Text = category.Name;
            tb_Sequence.Text = category.Sequence.ToString();
            hidden_CategoryID.Value = categoryId.ToString();
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Double result = 0;
        MaterialsRepository mr = new MaterialsRepository();
        Category category = mr.GetCategory(Convert.ToInt32(hidden_CategoryID.Value));
        category.Name = tb_Name.Text;
        category.Sequence = ( Double.TryParse(tb_Sequence.Text, out result) ? Double.Parse(tb_Sequence.Text) : GetLastSequence() + 1);
        mr.Save();
        Repository.MessageBoxShow("با موفقیت ذخیره شد", Up_Save, this.GetType());
    }
    private double GetLastSequence()
    {
        double seq = 0.0;
        MaterialsRepository mr = new MaterialsRepository();
        seq = mr.GetCategoriesLastSquence();
        return seq;
    }
}
