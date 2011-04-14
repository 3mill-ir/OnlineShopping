using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Models;

public partial class Views_ShoppingSteps : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!User.Identity.IsAuthenticated && rb_NotRegistered.Checked)
            {
                CitiesRepository cr = new CitiesRepository();
                cr.FillStatesList(cmb_States);
            }
        }
    }
    protected void rb_Registered_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void cmb_States_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_States.SelectedIndex > 0)
        {
            CitiesRepository cr = new CitiesRepository();
            int stateId = Convert.ToInt32(cmb_States.SelectedItem.Value);
            cr.FillCitiesList(cmb_Cities, stateId);
        }
        else
        {
            cmb_Cities.Items.Clear();
        }
    }
    protected void btn_NextStep_Clicked(object sender, EventArgs e)
    {
        if (rb_NotRegistered.Checked)
        {
            bool passIsCorrect = (tb_Password.Text.Length >= 6 ? true : false);
            if (passIsCorrect)
            {
                Member member = new Member();
                member.Name = tb_Name.Text;
                member.LastName = tb_LastName.Text;
                member.Email = tb_Email.Text;
                member.Mobile = tb_Mobile.Text;
                member.Tels = tb_Tels.Text;
                member.CityID = Convert.ToInt32(cmb_Cities.SelectedItem.Value);
                member.IP = "196598"; //Repository.ClientIP;
                member.DateOfJoin = Repository.Now;
                /////////////////////// membership :
                MembershipUser currentUser = Membership.GetUser(tb_UserName.Text);
                if (currentUser == null)
                {
                    MembershipUser newUser = Membership.CreateUser(tb_UserName.Text, tb_Password.Text);
                    if (newUser != null)
                    {
                        Roles.AddUserToRole(newUser.UserName, MyRoles.Member);
                        member.UserName = newUser.UserName;
                        MembersRepository mr = new MembersRepository();
                        mr.AddMember(member);
                        mr.Save();
                        //
                        Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                        FormsAuthentication.SetAuthCookie(member.UserName, false);
                        Response.Redirect("~/Views/Members/SaveShoppingCart.aspx");
                    }
                }
                else
                {
                    Repository.MessageBoxShow("این نام کاربری قبلاً استفاده شده است ، نام دیگری را وارد فرمائید", Up_Step1_NotAuthenticated, this.GetType());
                }
            }
            else
            {
                Repository.MessageBoxShow("طول رمز عبور نباید کمتر از 6 کاراکتر باشد", Up_Step1_NotAuthenticated, this.GetType());
            }
        }
        else if (rb_Registered.Checked)
        {
        }
    }
    protected void btn_Check_Clicked(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(tb_Reg_UserName.Text);
        if (user != null && user.GetPassword() == tb_Reg_Password.Text)
        {
            MembersRepository mr = new MembersRepository();
            Member member = mr.GetMember(user.UserName);
            lbl_Info.Text = member.Info;
            hid_UserName.Value = member.UserName;
            //
            pnl_MemberInfo.Visible = true;
        }
        else if (user == null)
        {
            Repository.MessageBoxShow("نام کاربری وارد شده اشتباه است", Up_Step1_NotAuthenticated, this.GetType());
        }
        else if (user != null && user.GetPassword() != tb_Reg_Password.Text)
        {
            Repository.MessageBoxShow("رمز عبور وارد شده اشتباه است", Up_Step1_NotAuthenticated, this.GetType());
        }
    }
    protected void btn_Registered_Next_Clicked(object sender, EventArgs e)
    {
        Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        FormsAuthentication.SetAuthCookie(hid_UserName.Value.ToString(), false);
        Response.Redirect("~/Views/Members/SaveShoppingCart.aspx");
    }
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        Response.Redirect("~/Views/Members/SaveShoppingCart.aspx");
    }
}
