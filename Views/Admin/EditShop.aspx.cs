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
        if (!Page.IsPostBack)
        {
            int shopId = Convert.ToInt32(Request.QueryString["ShopID"]);
            hidden_ShopID.Value = shopId.ToString();
            ShopsRepository shr = new ShopsRepository();
            Shop shop = shr.GetShop(shopId);
            //set :
            rb_OnlineShop.Checked = (shop.Type == (int)ShopType.Online ? true : false);
            rb_RealShop.Checked = (shop.Type == (int)ShopType.Real ? true : false);
            tb_Name.Text = shop.Name;
            tb_Owner.Text = shop.Owner;
            if (shop.Type == (int)ShopType.Real) { tb_Address.Text = shop.Address; }
            if (shop.Type == (int)ShopType.Online) { tb_WebSite.Text = shop.Address; }
            tb_Mobile.Text = shop.Mobile;
            tb_Tels.Text = shop.Tels;
            if (shop.UserName.Trim().Length > 0)
            {
                MembershipUser user = Membership.GetUser(shop.UserName);
                if (user != null)
                {
                    hidden_HasAcount.Value = "True";
                    ch_CreateUser.Checked = true;
                    tb_Password.Enabled = true;
                    tb_Confirm.Enabled = true;
                    ch_IsApproved.Enabled = true;
                    tbl_CreateUser.Attributes["style"] = "color : Black;";
                    //
                    string pass = user.GetPassword();
                    tb_UserName.Text = user.UserName;
                    tb_Password.Attributes["value"] = pass;
                    tb_Confirm.Attributes["value"] = pass;
                    ch_IsApproved.Checked = user.IsApproved;
                }
            }
        }
    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        int shopId = Convert.ToInt32(hidden_ShopID.Value);
        ShopsRepository sr = new ShopsRepository();
        Shop shop = sr.GetShop(shopId);
        shop.Type = (rb_RealShop.Checked ? (int)ShopType.Real : (int)ShopType.Online);
        shop.Name = tb_Name.Text;
        shop.Owner = tb_Owner.Text;
        shop.Address = (shop.Type == (int)ShopType.Real ? tb_Address.Text : tb_WebSite.Text);
        shop.Mobile = tb_Mobile.Text;
        shop.Tels = tb_Tels.Text;
        bool hadAcount = Convert.ToBoolean(hidden_HasAcount.Value);
        bool hasAcount = ch_CreateUser.Checked;
        if (!hadAcount && hasAcount)
        {
            int _PasswordMinLength = 6;
            bool passLengthIsEnough = (tb_Password.Text.Trim().Length >= _PasswordMinLength ? true : false);
            MembershipUser existingUser = Membership.GetUser(tb_UserName.Text);
            if (existingUser == null && passLengthIsEnough)
            {
                MembershipUser newUser = Membership.CreateUser(tb_UserName.Text, tb_Password.Text);
                newUser.IsApproved = ch_IsApproved.Checked;
                Membership.UpdateUser(newUser);
                shop.UserName = newUser.UserName;
                //save :
                sr.Save();
                Repository.MessageBoxShow("با موفقیت ذخیـره شد", up_Save, this.GetType());
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
        else if (hadAcount && !hasAcount)
        {
            Membership.DeleteUser(shop.UserName);
            Repository.MessageBoxShow("با موفقیت ذخیـره شد", up_Save, this.GetType());
        }
        else if (hadAcount && hasAcount)
        {
            int _PasswordMinLength = 6;
            bool passLengthIsEnough = (tb_Password.Text.Trim().Length >= _PasswordMinLength ? true : false);
            MembershipUser existingUser = Membership.GetUser(shop.UserName);
            if (existingUser != null && passLengthIsEnough)
            {
                existingUser.ChangePassword(existingUser.GetPassword(), tb_Password.Text);
                existingUser.IsApproved = ch_IsApproved.Checked;
                Membership.UpdateUser(existingUser);
                shop.UserName = existingUser.UserName;
                //save :
                sr.Save();
                Repository.MessageBoxShow("با موفقیت ذخیـره شد", up_Save, this.GetType());
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
            sr.Save();
            Repository.MessageBoxShow("با موفقیت ایجاد شد", up_Save, this.GetType());
        }
    }
    protected void ch_CreateUser_CheckedChanged(object sender, EventArgs e)
    {
        if (ch_CreateUser.Checked)
        {
            bool hadAcount = Convert.ToBoolean(hidden_HasAcount.Value);
            tb_UserName.Enabled = (!hadAcount ? true : false);
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
