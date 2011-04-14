using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Web.Security;

public partial class Views_Admin_NewShop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        Shop shop = new Shop();
        shop.Type = (rb_RealShop.Checked ? (int)ShopType.Real : (int)ShopType.Online);
        shop.Name = tb_Name.Text;
        shop.Owner = tb_Owner.Text;
        shop.Address = (shop.Type == (int)ShopType.Real ? tb_Address.Text : tb_WebSite.Text);
        shop.Mobile = tb_Mobile.Text;
        shop.Tels = tb_Tels.Text;
        shop.DateOfJoin = Repository.Now;
        if (ch_CreateUser.Checked)
        {
            int _PasswordMinLength = 6;
            bool passLengthIsEnough = (tb_Password.Text.Trim().Length >= _PasswordMinLength ? true : false);
            MembershipUser existingUser = Membership.GetUser(tb_UserName.Text);
            if (existingUser == null && passLengthIsEnough)
            {
                MembershipUser newUser = Membership.CreateUser(tb_UserName.Text, tb_Password.Text);
                newUser.IsApproved = ch_IsApproved.Checked;
                Roles.AddUserToRole(newUser.UserName, MyRoles.Shop);
                Membership.UpdateUser(newUser);
                shop.UserName = newUser.UserName;
                //save :
                ShopsRepository sr = new ShopsRepository();
                sr.AddShop(shop);
                sr.Save();
                Repository.MessageBoxShow("با موفقیت ایجاد شد", up_Save, this.GetType());
            }
            else if (existingUser != null)
            {
                Repository.MessageBoxShow("این نام کاربری قبلاً ثبت شده است", up_Save, this.GetType());
            }
            else if (!passLengthIsEnough)
            {
                Repository.MessageBoxShow("رمـز عبـور نباید کمتـر از 6 کاراکتـر داشتـه باشـد", up_Save, this.GetType());
            }
        }
        else
        {
            shop.UserName = "";
            //save :
            ShopsRepository sr = new ShopsRepository();
            sr.AddShop(shop);
            sr.Save();
            Repository.MessageBoxShow("با موفقیت ایجاد شد", up_Save, this.GetType());
        }
    }
    protected void ch_CreateUser_CheckedChanged(object sender, EventArgs e)
    {
        if (ch_CreateUser.Checked)
        {
            tb_UserName.Enabled = true;
            tb_Password.Enabled = true;
            tb_Confirm.Enabled = true;
            ch_IsApproved.Enabled = true;
            tbl_CreateUser.Attributes["style"] = "color : Black;";
        }
        else
        {
            tb_UserName.Enabled = false;
            tb_Password.Enabled = false;
            tb_Confirm.Enabled = false;
            ch_IsApproved.Enabled = false;
            tbl_CreateUser.Attributes["style"] = "color : Gray;";
        }
    }
}
