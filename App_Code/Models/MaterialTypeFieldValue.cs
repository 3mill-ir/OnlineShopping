using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for MaterialTypeFieldValue
/// </summary>
namespace Models
{
    public partial class MaterialTypeFieldValue
    {
        public string ValueLink
        {
            get
            {
                string html = "";
                FieldType fieldType = (FieldType)Field.FieldType;
                html = "<a class=\"" + (fieldType == FieldType.YesNo ? (Convert.ToBoolean(Value) ? "True" : "False") : "Link") + "\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Field=" + FieldID.ToString() + "&Value=" + Value.Replace(' ', '$') + "\">" + (fieldType == FieldType.YesNo ? "" : Value) + "</a>";
                return html;
            }
        }
        public string GetValueLink(FieldUnit unit)
        {
            string html = "";
            FieldType fieldType = (FieldType)Field.FieldType;
            html = "<a class=\"" + (fieldType == FieldType.YesNo ? (Convert.ToBoolean(Value) ? "True" : "False") : "Link") + "\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Field=" + FieldID.ToString() + "&Value=" + Value.Replace(' ', '$') + "\">" + (fieldType == FieldType.YesNo ? "" : Value + (unit != null ? " " + unit.Name : "")) + "</a>";
            return html;
        }
    }
}
