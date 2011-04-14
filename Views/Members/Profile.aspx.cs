using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Models;

public partial class Views_Members_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CitiesRepository cr = new CitiesRepository();
            cr.FillStatesList(cmb_States);
            //************* fill foem :
            MembersRepository mr = new MembersRepository();
            Member member = mr.GetMember(User.Identity.Name);
            tb_Name.Text = member.Name;
            tb_LastName.Text = member.LastName;
            tb_Email.Text = member.Email;
            tb_Mobile.Text = member.Mobile;
            tb_Tels.Text = member.Tels;
            tb_UserName.Text = member.UserName;
            MembershipUser user = Membership.GetUser(member.UserName);
            tb_Password.Attributes["value"] = user.GetPassword();
            tb_PasswordConfirm.Attributes["value"] = user.GetPassword();
            //
            City memberCity = member.City;
            cmb_States.SelectedIndex = cmb_States.Items.IndexOf(new ListItem(memberCity.State.StateName, memberCity.State.StateID.ToString()));
            cr.FillCitiesList(cmb_Cities, memberCity.StateID);
            cmb_Cities.SelectedIndex = cmb_Cities.Items.IndexOf(new ListItem(memberCity.CityName, memberCity.CityID.ToString()));
            //
        }
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
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        bool passIsCorrect = (tb_Password.Text.Length >= 6 ? true : false);
        if (passIsCorrect)
        {
            //Get Member :
            MembersRepository mr = new MembersRepository();
            Member member = mr.GetMember(User.Identity.Name);
            //member :
            member.Name = tb_Name.Text;
            member.LastName = tb_LastName.Text;
            member.Email = tb_Email.Text;
            member.Mobile = tb_Mobile.Text;
            member.Tels = tb_Tels.Text;
            member.CityID = Convert.ToInt32(cmb_Cities.SelectedItem.Value);
            member.IP = Repository.ClientIP;
            //membership :
            MembershipUser user = Membership.GetUser(member.UserName);
            user.ChangePassword(user.GetPassword(), tb_Password.Text);
            //==== SAVE :
            Membership.UpdateUser(user);
            mr.Save();
            //
            Repository.MessageBoxShow("با موفقیت ذخیـره شد", Up_Save, this.GetType());
        }
        else
        {
            Repository.MessageBoxShow("طول رمز عبور نباید کمتر از 6 کاراکتر باشد", Up_Save, this.GetType());
        }
    }
}
