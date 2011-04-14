using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using Models;

public partial class Views_MaterialDetails : System.Web.UI.Page
{
    //************************
    const int _BuyersCount = 5;
    //************************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int materialId = Convert.ToInt32(Request.QueryString["MaterialID"]);
            hid_MaterialID.Value = materialId.ToString();
            MaterialsRepository mr = new MaterialsRepository();
            Material material = mr.GetMaterial(materialId);
            hid_MaterialType.Value = material.TypeID.ToString();
            Material = material;
            // fill form :
            title.Text = material.Name;
            lbl_Description.Text = material.Description;
            lbl_Fields.Text = material.FieldsHtml();
            vote.AssociatedMaterialID = material.MaterialID;
            lbl_Price.Text = material.PriceHtml;
            lbl_Tags.Text = material.TagsHtml;
            if (Request.QueryString["PictureID"] != null)
            {
                Picture defaultPic = material.GetPicture(Convert.ToInt32(Request.QueryString["PictureID"])).PictureObject;
                gallery.Default = defaultPic;
            }
            gallery.Pictures = material.Pictures;
            gallery.IsVisible = (gallery.Pictures.Count() > 0 ? true : false);
            btn_AddToCart.Visible = material.Visible;
            btn_AddToCompare.Visible = material.Visible;
            //increase reviews :
            material.Reviews++;
            mr.Save();
            //Authenticated users :
            if (User.Identity.IsAuthenticated && User.IsInRole(MyRoles.Member))
            {
                //add interests :
                MembersRepository mRep = new MembersRepository();
                mRep.AddKeywordsToInterests(material.Keywords, InterestType.View);
                mRep.Save();
                //Buyers :
                if (material.Buyers.Count() > 0)
                {
                    pnl_Buyers.Visible = true;
                    lbl_Buyers.Text = material.BuyersHtml;
                }
            }
        }
    }
    protected void vote_YesClicked(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole(MyRoles.Member) && !vote.HasVoted)
        {
            MaterialsRepository mr = new MaterialsRepository();
            int materialId = Convert.ToInt32(hid_MaterialID.Value);
            Material material = mr.GetMaterial(materialId);
            MembersRepository mRep = new MembersRepository();
            mRep.AddKeywordsToInterests(material.Keywords, InterestType.Like);
            mRep.Save();
        }
    }
    protected Material Material { get; set; }
    protected void btn_AddToCart_Clicked(object sender, EventArgs e)
    {
        Cart cart = Cart.Current;
        CartItem item = new CartItem(Convert.ToInt32(hid_MaterialID.Value), 1);
        cart.AddItem(item);
        Cart.Current = cart;
        //add interests :
        if (User.Identity.IsAuthenticated && User.IsInRole(MyRoles.Member) && !vote.HasVoted)
        {
            MaterialsRepository mr = new MaterialsRepository();
            int materialId = Convert.ToInt32(hid_MaterialID.Value);
            Material material = mr.GetMaterial(materialId);
            MembersRepository mRep = new MembersRepository();
            mRep.AddKeywordsToInterests(material.Keywords, InterestType.Buy);
            mRep.Save();
        }
        //
        Response.Redirect("~/Views/ShoppingCart.aspx");
    }
    protected void btn_AddToCompare_Clicked(object sender, EventArgs e)
    {
        CompareCart ccart = CompareCart.Current;
        ccart.AddToCompare(Convert.ToInt32(hid_MaterialID.Value), Convert.ToInt32(hid_MaterialType.Value));
        CompareCart.Current = ccart;
        Response.Redirect("~/Views/CompareMaterials.aspx");
    }
}
