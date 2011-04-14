using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Picture
/// </summary>
[Serializable]
public class Picture
{
    public int PictureID { get; set; }
    public string PicName { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Url { get; set; }
    public int Width
    {
        get
        {
            System.Drawing.Image pic = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Url));
            return pic.Width;
        }
    }
    public int Height
    {
        get
        {
            System.Drawing.Image pic = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Url));
            return pic.Height;
        }
    }
    public int Thumbnail_Width
    {
        get
        {
            System.Drawing.Image thumbnail = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(ThumbnailUrl));
            return thumbnail.Width;
        }
    }
    public int Thumbnail_Height
    {
        get
        {
            System.Drawing.Image thumbnail = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(ThumbnailUrl));
            return thumbnail.Height;
        }
    }
    public static Picture FromFile(string Url)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Url));
        Picture pic = new Picture();
        pic.Url = Url;
        return pic;
    }
}
