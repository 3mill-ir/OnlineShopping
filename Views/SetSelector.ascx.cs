using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_SetSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public IEnumerable<Category> Categories
    {
        set
        {
            cmb_Categories.Items.Clear();
            cmb_Categories.Items.Add(new ListItem("", ""));
            foreach(Category cat in value)
            {
                cmb_Categories.Items.Add(new ListItem(cat.Name, cat.CategoryID.ToString()));
            }
        }
    }
    public bool AutoPostBack
    {
        get
        {
            return cmb_Sets.AutoPostBack;
        }
        set
        {
            cmb_Sets.AutoPostBack = value;
        }
    }
    public event EventHandler SetSelected;
    protected void cmb_Sets_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_Sets.SelectedIndex > 0 && SetSelected != null)
            SetSelected(this, EventArgs.Empty);
    }
    protected void cmb_Categories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_Categories.SelectedIndex > 0)
        {
            int categoryId = Convert.ToInt32(cmb_Categories.SelectedItem.Value);
            MaterialsRepository mr = new MaterialsRepository();
            var sets = mr.GetCategorySets(categoryId);
            //
            cmb_Sets.Items.Clear();
            cmb_Sets.Items.Add(new ListItem("", ""));
            foreach (Set set in sets)
            {
                cmb_Sets.Items.Add(new ListItem(set.Name, set.SetID.ToString()));
            }
        }
        else
        {
            cmb_Sets.Items.Clear();
            cmb_Sets.Items.Add(new ListItem("", ""));
        }
    }
    public Unit CategoriesComboWidth
    {
        get
        {
            return cmb_Categories.Width;
        }
        set
        {
            cmb_Categories.Width = value;
        }
    }
    public Unit SetsComboWidth
    {
        get
        {
            return cmb_Sets.Width;
        }
        set
        {
            cmb_Sets.Width = value;
        }
    }
    public string Text
    {
        get
        {
            return lbl_Text.Text;
        }
        set
        {
            lbl_Text.Text = value;
        }
    }
    public int SetID
    {
        get
        {
            return (cmb_Sets.SelectedIndex > 0 ? Convert.ToInt32(cmb_Sets.SelectedValue) : -1);
        }
        set
        {
            int setId = value;
            MaterialsRepository mr = new MaterialsRepository();
            Set set = mr.GetSet(setId);
            Category = set.Category;
            cmb_Sets.SelectedIndex = cmb_Sets.Items.IndexOf(new ListItem(set.Name, set.SetID.ToString()));
        }
    }
    public string SetName
    {
        get
        {
            return (cmb_Sets.SelectedIndex > 0 ? cmb_Sets.SelectedItem.Text : "");
        }
    }
    public string CategoryName
    {
        get
        {
            return (cmb_Categories.SelectedIndex > 0 ? cmb_Categories.SelectedItem.Text : "");
        }
    }
    public bool EnableValidation { get; set; }
    public string ValidationGroup
    {
        get
        {
            return reg_Sets.ValidationGroup;
        }
        set
        {
            reg_Sets.ValidationGroup = value;
        }
    }
    public Category Category
    {
        get
        {
            if (cmb_Categories.SelectedIndex > 0)
            {
                int categoryId = Convert.ToInt32(cmb_Categories.SelectedItem.Value);
                MaterialsRepository mr = new MaterialsRepository();
                return mr.GetCategory(categoryId);
            }
            else return null;
        }
        set
        {
            cmb_Categories.SelectedIndex = cmb_Categories.Items.IndexOf(new ListItem(value.Name, value.CategoryID.ToString()));
            var sets = value.Sets;
            cmb_Sets.Items.Clear();
            cmb_Sets.Items.Add(new ListItem("", ""));
            foreach (Set set in sets)
            {
                cmb_Sets.Items.Add(new ListItem(set.Name, set.SetID.ToString()));
            }
        }
    }
}
