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

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Double result = 0;
        Category category = new Category();
        category.Name = tb_Name.Text;
        category.Sequence = ( Double.TryParse(tb_Sequence.Text, out result) ? Double.Parse(tb_Sequence.Text) : GetLastSequence() + 1);
        MaterialsRepository mr = new MaterialsRepository();
        mr.AddCategory(category);
        mr.Save();
        Repository.MessageBoxShow("با موفقیت ایجاد شد", Up_Save, this.GetType());
    }
    private double GetLastSequence()
    {
        double seq = 0.0;
        MaterialsRepository mr = new MaterialsRepository();
        seq = mr.GetCategoriesLastSquence();
        return seq;
    }
}
