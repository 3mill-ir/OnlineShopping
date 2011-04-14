using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_Admin_FindMaterial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hid_ClientId.Value = Request.QueryString["ClientID"].ToString();
            MaterialsRepository mr = new MaterialsRepository();
            setSelector.Categories = mr.GetAllCategories();
        }
    }
    protected void btn_Search_Clicked(object sender, EventArgs e)
    {
        MaterialsRepository mr = new MaterialsRepository();
        var materials = mr.FindMaterial(tb_MaterialName.Text, setSelector.SetID);
        FillMaterialsDataList(materials);
    }
    private void FillMaterialsDataList(IEnumerable<Material> materials)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Link", typeof(String));
        foreach (Material mat in materials)
        {
            DataRow row = dt.NewRow();
            row["Link"] = GetSelectLink(mat);
            dt.Rows.Add(row);
        }
        DataList_Shops.DataSource = dt;
        DataList_Shops.DataBind();
    }
    private string GetSelectLink(Material material)
    {
        string html = "";
        html = "<a id=\"" + material.MaterialID.ToString() + "\" class=\"ButtonLink\" style=\"font-weight : normal;\">" + material.Name + "</a>";
        html +=
            "<script type=\"text/javascript\">" +
                "$(function() {" +
                    "$('#" + material.MaterialID.ToString() + "').click(function() {" +
                        "var myForm = opener.document.forms[0];" +
                         "myForm[\"" + hid_ClientId.Value + "\"].value = " + material.MaterialID.ToString() + ";" +
                         "window.close();" +
                    "});" +
                "});" +
            "</script>";
        return html;
    }

}
