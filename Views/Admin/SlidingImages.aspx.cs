using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_Admin_SlidingImages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SlidingsRepository sr = new SlidingsRepository();
            var images = sr.GetSlidingImages();
            FillImagesDataList(images);
        }
    }
    protected void btn_Send_Clicked(object sender, EventArgs e)
    {
        if (uploader.HasFile)
        {
            string ext = Repository.GetExtension(uploader.FileName);
            if (Repository.IsSupportedExtension(ext))
            {
                string randomName = Repository.GetRandomString(10, true);
                string imgName = randomName + "." + ext;
                string url = Constants.UrlOfSlidingImages + imgName;
                //
                SlidingsRepository sr = new SlidingsRepository();
                SlidingImage img = new SlidingImage();
                double test = 0.0;
                int test2 = 0;
                img.Url = Constants.UrlOfSlidingImages + imgName;
                img.Sequence = (Double.TryParse(tb_Sequence.Text, out test) ? Double.Parse(tb_Sequence.Text) : sr.GetLastSequence() + 1);
                img.AssociatedMaterialID = (Int32.TryParse(tb_MaterialID.Text,out test2) ? Int32.Parse(tb_MaterialID.Text) : 0);
                sr.AddSlidingImage(img);
                //Save :
                uploader.SaveAs(Server.MapPath(url));
                sr.Save();
                //
                var images = sr.GetSlidingImages();
                FillImagesDataList(images);
            }
            else
            {
                Repository.MessageBoxShow("تصویری انتخاب شده دارای فرمت مناسب نیست", Page, this.GetType());
            }
        }
        else
        {
            Repository.MessageBoxShow("تصویری انتخاب نشده است", Page, this.GetType());
        }
    }
    private void FillImagesDataList(IEnumerable<SlidingImage> images)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(Int32));
        dt.Columns.Add("Url", typeof(String));
        dt.Columns.Add("Width", typeof(Unit));
        dt.Columns.Add("Height", typeof(Unit));
        dt.Columns.Add("Sequence", typeof(String));
        foreach(SlidingImage img in images)
        {
            DataRow row = dt.NewRow();
            row["ID"] = img.ID;
            row["Url"] = img.Url;
            row["Width"] = Constants.SlidingImageWidth;
            row["Height"] = Constants.SlidingImageHeight;
            row["Sequence"] = img.Sequence.ToString();
            dt.Rows.Add(row);
        }
        Grid_Images.DataSource = dt;
        Grid_Images.DataBind();
    }
    protected void Grid_Images_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteImage")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            SlidingsRepository sr = new SlidingsRepository();
            sr.DeleteSlidingImage(id);
            sr.Save();
            //
            var images = sr.GetSlidingImages();
            FillImagesDataList(images);
        }
    }
}
