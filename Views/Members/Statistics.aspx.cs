using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Members_Statistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MembersRepository mr = new MembersRepository();
            Member member = mr.GetMember(User.Identity.Name);
            //
            Statistics st = new Statistics(member, HtmlType.ForMember);
            lbl_Statistics.Text = st.ToHtml();
        }
    }
}
