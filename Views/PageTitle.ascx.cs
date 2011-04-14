using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_PageTitle : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string Text
    {
        get
        {
            return lbl_PageTitle.Text;
        }
        set
        {
            lbl_PageTitle.Text = value;
        }
    }
}
