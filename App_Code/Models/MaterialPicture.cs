using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialPicture
/// </summary>
namespace Models
{
    public partial class MaterialPicture
    {
        public string ThumbnailUrl
        {
            get
            {
                return Constants.UrlOfMaterialThumbnails + PicName;
            }
            set { ThumbnailUrl = value; }
        }
        public string Url
        {
            get
            {
                return Constants.UrlOfMaterialImages + PicName;
            }
            set
            {
                Url = value;
            }
        }
        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            if (action == System.Data.Linq.ChangeAction.Delete)
            {
                string image = Constants.UrlOfMaterialImages + PicName;
                string thumbnail = Constants.UrlOfMaterialThumbnails + PicName;
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(image));
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(thumbnail));
            }
        }
        public void SetAsAvatar()
        {
            foreach (MaterialPicture pic in Material.Pictures)
            {
                pic.IsAvatar = false;
            }
            IsAvatar = true;
        }
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
        public Picture PictureObject
        {
            get
            {
                Picture pic = new Picture();
                pic.PictureID = PictureID;
                pic.PicName = PicName;
                pic.Url = Url;
                pic.ThumbnailUrl = ThumbnailUrl;
                return pic;
            }
        }
    }
}

