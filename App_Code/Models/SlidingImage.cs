using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SlidingImage
/// </summary>
namespace Models
{
    public partial class SlidingImage
    {
        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            if (action == System.Data.Linq.ChangeAction.Delete)
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(Url));
            }
        }
        public string GetHtml(int index)
        {
            string html = "<a " + (AssociatedMaterialID > 0 ? "href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + AssociatedMaterialID.ToString() + "\"" : "") + "><img id=\"wows" + index.ToString() + "\" src=\"" + Repository.ToHtmlUrl(Url) + "\"/></a>";
            return html;
        }
        public static string Bullet { get { return "<a title=\"\" style=\"cursor : pointer;\"></a>"; } }
    }
}
