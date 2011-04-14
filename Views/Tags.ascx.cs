using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_Tags : System.Web.UI.UserControl
{
    public string Tags { get; set; }
    protected string TagsLinks
    {
        get
        {
            string html = "";
            List<string> tagsList = Repository.SplitString(Tags, ";");
            int i = 0;
            foreach (string tag in tagsList)
            {
                html += "<a class=\"Link Bold\">" + tag + "</a>" + (i < tagsList.Count - 1 ? " - " : "");
                i++;
            }
            return html;
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
}
