using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Models;

public partial class Views_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        string userName = Login1.UserName;
        if (Roles.IsUserInRole(userName, MyRoles.Member))
        {
            MembersRepository mr = new MembersRepository();
            int memberId = mr.GetMemberID(userName);
            Models.Login login = new Models.Login();
            login.MemberID = memberId;
            login.DateOfLogin = Repository.Now;
            mr.AddLogin(login);
            mr.Save();
            //
        }
        Response.Redirect("~/Default.aspx");
    }
}
