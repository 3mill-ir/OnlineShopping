using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_Gallery : System.Web.UI.UserControl
{
    //*****************
    IEnumerable<MaterialPicture> pictures;
    int _ImageWidthFix = 630;
    Picture defaultPic = new Picture();
    bool isSelected = false;
    //*****************
    public bool IsVisible
    {
        set
        {
            pnl_Main.Visible = value;
        }
    }
    public string AssociatedUpdatePanelID
    {
        set
        {
            progresss.AssociatedUpdatePanelID = value;
        }
    }
    public bool IsInUpdatePanel { get; set; }
    public Picture Default
    {
        get
        {
            return defaultPic;
        }
        set
        {
            defaultPic = value;
            IsSelected = true;
            MyImage myImage = new MyImage(value.Width, value.Height, ResizeType.WidthFix, _ImageWidthFix);
            //
            img_MaterialPicture.ImageUrl = value.Url;
            img_MaterialPicture.Width = new Unit(myImage.Thumb_Width);
            img_MaterialPicture.Height = new Unit(myImage.Thumb_Height);
            img_MaterialPicture.Visible = true;
            pnl_LargeImage.Visible = true;
            //
        }
    }
    public IEnumerable<MaterialPicture> Pictures
    {
        get
        {
            return pictures;
        }
        set
        {
            pictures = value;
            PicturesCount = value.Count();
            FillThumbnailsDataList(value);
        }
    }
    protected int PicturesCount { get; set; }
    protected bool IsSelected { get { return isSelected; } set { isSelected = value; } }
    private void FillThumbnailsDataList(IEnumerable<MaterialPicture> pics)
    {
        int _MaxHeight = 36;
        DataTable dt = new DataTable();
        dt.Columns.Add("PictureID", typeof(Int32));
        dt.Columns.Add("PicName", typeof(String));
        dt.Columns.Add("Url", typeof(String));
        dt.Columns.Add("Width", typeof(Unit));
        dt.Columns.Add("Height", typeof(Unit));
        dt.Columns.Add("IsSelected", typeof(Boolean));
        foreach (MaterialPicture pic in pics)
        {
            MyImage myImage = new MyImage(pic.Thumbnail_Width, pic.Thumbnail_Height, ResizeType.HeightFix, _MaxHeight);
            DataRow row = dt.NewRow();
            row["PictureID"] = pic.PictureID;
            row["PicName"] = pic.PicName;
            row["Url"] = pic.ThumbnailUrl;
            row["Width"] = new Unit(myImage.Thumb_Width);
            row["Height"] = new Unit(myImage.Thumb_Height);
            row["IsSelected"] = (pic.PictureID == Default.PictureID ? true : false);
            dt.Rows.Add(row);
        }
        DataList_Thumbnails.DataSource = dt;
        DataList_Thumbnails.DataBind();
    }
    private void RefreshDatalist()
    {
        foreach (DataListItem item in DataList_Thumbnails.Items)
        {
            Panel pnl_Thumbnail = item.FindControl("pnl_Thumbnail") as Panel;
            Panel pnl_Arrow = item.FindControl("pnl_Arrow") as Panel;
            ImageButton btn_Thumbnail = pnl_Thumbnail.FindControl("btn_Thumbnail") as ImageButton;
            pnl_Thumbnail.CssClass = "GalleryThumbnailContainter";
            pnl_Arrow.CssClass = "";
            btn_Thumbnail.Enabled = true;
            btn_Thumbnail.CssClass = "SmallThumbnail";
        }
    }
    protected void DataList_Thumbnails_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ShowImage")
        {
            IsSelected = true;
            List<string> args = Repository.SplitString(e.CommandArgument.ToString(), ";");
            int pictureId = Convert.ToInt32(args[0]);
            string picName = args[1];
            string url = Constants.UrlOfMaterialImages + picName;
            Picture pic = Picture.FromFile(url);
            MyImage myImage = new MyImage(pic.Width, pic.Height, ResizeType.WidthFix, _ImageWidthFix);
            //
            img_MaterialPicture.ImageUrl = url;
            img_MaterialPicture.Width = new Unit(myImage.Thumb_Width);
            img_MaterialPicture.Height = new Unit(myImage.Thumb_Height);
            img_MaterialPicture.Visible = true;
            pnl_LargeImage.Visible = true;
            //select thumbnail :
            RefreshDatalist();
            Panel pnl_Thumbnail = e.Item.FindControl("pnl_Thumbnail") as Panel;
            Panel pnl_Arrow = e.Item.FindControl("pnl_Arrow") as Panel;
            ImageButton btn_Thumbnail = pnl_Thumbnail.FindControl("btn_Thumbnail") as ImageButton;
            pnl_Thumbnail.CssClass = "GallerySelectedThumbnail";
            pnl_Arrow.CssClass = "GallerySelectedBottom";
            btn_Thumbnail.Enabled = false;
            btn_Thumbnail.CssClass = "GallerySelectedThumbniailImage";
        }
    }
}
