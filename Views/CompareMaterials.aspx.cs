using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Views_CompareMaterials : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Action"] != null && Request.QueryString["MaterialID"] != null && Request.QueryString["Type"] != null)
            {
                CompareCart ccart = CompareCart.Current;
                ccart.AddToCompare(Convert.ToInt32(Request.QueryString["MaterialID"]), Convert.ToInt32(Request.QueryString["Type"]));
                CompareCart.Current = ccart;
            }
        }
    }
}
