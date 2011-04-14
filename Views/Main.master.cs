using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Main : System.Web.UI.MasterPage

















{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCategoriesCombo();
            ShopsRepository shr = new ShopsRepository();
          //  Hyper_Shops.Text = "فروشگاه ها" + " ( " + shr.ShopsCount.ToString() + " ) ";
        }
    }
    private void FillCategoriesCombo()
    {
        cmb_Categoriesssss.Items.Clear();
        cmb_Categoriesssss.Items.Add(new ListItem("همه دسته ها", "0"));
        MaterialsRepository mr = new MaterialsRepository();
        var cats = mr.GetAllCategories();
        foreach (Category cat in cats)
        {
            cmb_Categoriesssss.Items.Add(new ListItem(cat.Name, cat.CategoryID.ToString()));
        }
    }
    protected string GetMenu()
    {
        string menu = "";
        MaterialsRepository or = new MaterialsRepository();
        menu = or.GetAllCategoriesMenu();
        return menu;
    }
    protected void btn_Search_Clicked(object sender, EventArgs e)
    {
        int categoryId = Convert.ToInt32(hidden_CategoryID.Value);
        string text = tb_Search.Text;
        string searchType = (cmb_Categoriesssss.SelectedIndex > 0 ? "2" : "1");
        if (categoryId == 0)
        {
            string url = "~/Views/SearchResult.aspx?Text=" + HttpUtility.UrlEncode(text) + "&SearchType=" + searchType;
            Response.Redirect(url);
        }
        else if (categoryId > 0)
        {

            string url = "~/Views/SearchResult.aspx?Text=" + HttpUtility.UrlEncode(text) + "&SearchType=" + searchType + "&CategoryID=" + categoryId.ToString();
            Response.Redirect(url);
        }
    }
}
