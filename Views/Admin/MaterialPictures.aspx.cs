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
using System.Collections.Generic;
using Models;

public partial class Views_Admin_MaterialPictures : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int materialId = Convert.ToInt32(Request.QueryString["MaterialID"]);
            Hidden_MaterialID.Value = materialId.ToString();
            MaterialsRepository mr = new MaterialsRepository();
            Material material = mr.GetMaterial(materialId);
            if (material != null)
                lbl_Title.Text = "<b>" + material.Set.Category.Name + "</b>" + " --> " + "<b>" +material.Set.Name + "</b>" + " --> " + "<b>" + material.Name + "</b>";
            //fill 
            FillPicturesDatalist(material.Pictures);
        }
    }
    private void FillPicturesDatalist(IEnumerable<MaterialPicture> pics)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ThumbnailUrl", typeof(String));
        dt.Columns.Add("PictureID", typeof(Int32));
        dt.Columns.Add("IsAvatar", typeof(Boolean));
        foreach (MaterialPicture pic in pics)
        {
            DataRow row = dt.NewRow();
            row["ThumbnailUrl"] = pic.ThumbnailUrl;
            row["PictureID"] = pic.PictureID;
            row["IsAvatar"] = pic.IsAvatar;
            dt.Rows.Add(row);
        }
        DataList_Pics.DataSource = dt;
        DataList_Pics.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (uploader.HasFile && Repository.IsSupportedExtension(Repository.GetExtension(uploader.FileName)) && (uploader.FileContent.Length < Constants.MaxPhotoBytes))
        {
            string extension = ".jpeg";
            string randomName = Repository.GetRandomString(10, true);
            string picName = randomName + extension;
            string urlOfImage = Constants.UrlOfMaterialImages + picName;
            string urlOfThumbnail = Constants.UrlOfMaterialThumbnails + picName;
            //
            System.Drawing.Image image = System.Drawing.Image.FromStream(uploader.FileContent);
            MyImage myImage = new MyImage(image.Width, image.Height, ResizeType.LongerFix, Constants.ThumbnailFitSize);
            Repository rep = new Repository();
            System.Drawing.Image thumbnail = rep.GetThumbnailImage(image, myImage.Thumb_Width, myImage.Thumb_Height);
            //data :
            MaterialsRepository mr = new MaterialsRepository();
            Material material = mr.GetMaterial(Convert.ToInt32(Hidden_MaterialID.Value));
            if (ch_Avatar.Checked)
                material.ClearAvatar();
            MaterialPicture pic = new MaterialPicture();
            pic.PicName = picName;
            pic.IsAvatar = (ch_Avatar.Checked ? true : false);
            material.Pictures.Add(pic);
            //Save :
            mr.Save();
            thumbnail.Save(Server.MapPath(urlOfThumbnail), System.Drawing.Imaging.ImageFormat.Jpeg);
            image.Save(Server.MapPath(urlOfImage), System.Drawing.Imaging.ImageFormat.Jpeg);
            //
            FillPicturesDatalist(material.Pictures);
        }
        else if (!uploader.HasFile)
        {
            Repository.MessageBoxShow("فایل تصویر را انتخاب نمایید", Page, this.GetType());
        }
        else if (!Repository.IsSupportedExtension(Repository.GetExtension(uploader.FileName)))
        {
            Repository.MessageBoxShow("فرمت فایل انتخاب شده مجاز نمی باشد", Page, this.GetType());
        }
        else if (!(uploader.FileContent.Length < Constants.MaxPhotoBytes))
        {
            Repository.MessageBoxShow("حجم فایل انتخاب شده بیش از مقدار مجاز است", Page, this.GetType());
        }
    }
    protected void DataList_Pics_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "SetAsAvatar")
        {
            int picId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            MaterialPicture pic = mr.GetPicture(picId);
            pic.SetAsAvatar();
            mr.Save();
            Repository.MessageBoxShow("با موفقیت انجام شد", Page, this.GetType());
            FillPicturesDatalist(pic.Material.Pictures);
        }
        else if (e.CommandName == "DeletePicture")
        {
            int picId = Convert.ToInt32(e.CommandArgument);
            MaterialsRepository mr = new MaterialsRepository();
            MaterialPicture pic = mr.GetPicture(picId);
            Material material = pic.Material;
            mr.DeletePicture(pic);
            mr.Save();
            //
            FillPicturesDatalist(material.Pictures);
        }
    }
}
