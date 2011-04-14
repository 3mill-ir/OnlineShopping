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
using System.Collections.Generic;
using Models;

/// <summary>
/// Summary description for CompareCart
/// </summary>
public class CompareCart
{
    public List<int> Materials { get; private set; }
    public int CompareType { get; set; }
    public static CompareCart Current
    {
        get
        {
            if (HttpContext.Current.Session["CompareCart"] != null)
            {
                return (CompareCart)HttpContext.Current.Session["CompareCart"];
            }
            else
            {
                CompareCart ccart = new CompareCart();
                ccart.Materials = new List<int>();
                return ccart;
            }
        }
        set
        {
            HttpContext.Current.Session["CompareCart"] = value;
        }
    }
    public static void Clear()
    {
        CompareCart ccart = new CompareCart();
        ccart.Materials = new List<int>();
        Current = ccart;
    }
    public void AddToCompare(int materialId, int typeId)
    {
        if (CompareType == typeId)
        {
            this.Materials.Add(materialId);
        }
        else
        {
            this.Materials = new List<int>();
            this.Materials.Add(materialId);
            this.CompareType = typeId;
        }
    }
    public string ToHtml()
    {
        MaterialsRepository mr = new MaterialsRepository();
        var materials = mr.GetMaterials(Materials);
        string html = "";
        if (materials.Count() > 0)
        {
            int _TotalWidth = 560;
            var fields = materials.First().Type.Fields;
            html =
                "<table id=\"CompareTable\" class=\"CompareTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
                    "<tr><td class=\"CompareTable_TopRightHeaderTD\"></td>";
            //////// image head : 
            var compareMaterials = (materials.Count() > Constants.CompareCapacity ? materials.Skip(materials.Count() - Constants.CompareCapacity).Take(Constants.CompareCapacity) : materials);
            foreach (Material material in compareMaterials)
            {
                Picture avatar = material.Avatar;
                MyImage myImage = new MyImage(avatar.Thumbnail_Width, avatar.Thumbnail_Height, ResizeType.WidthFix, GetThumbnailWidth(_TotalWidth, compareMaterials.Count()));
                html +=
                    "<td align=\"center\" class=\"CompareTable_TopHeaderTD\">" +
                        "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                            "<tr>" +
                                "<td align=\"center\">" +
                                    "<a href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + material.MaterialID.ToString() + "\">" +
                                        "<img border=\"0\" src=\"" + Repository.ToHtmlUrl(avatar.ThumbnailUrl) + "\" width=\"" + myImage.Thumb_Width.ToString() + "px\" height=\"" + myImage.Thumb_Height.ToString() + "px\" class=\"SmallThumbnail\"/>" +
                                    "</a>" +
                                "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td align=\"center\" style=\"padding-top : 7px;\">" +
                                    material.NameLink_Thick +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                    "</td>";
            }
            html +=
                "</tr>";
            // field rows :
            foreach (MaterialTypeField field in fields)
            {
                html +=
                    "<tr><td align=\"left\" class=\"CompareTable_RightHeaderTD\">" +
                        field.Name +
                    "</td>";
                foreach (Material material in compareMaterials)
                {
                    html +=
                        "<td style=\"width : " + ((int)(_TotalWidth / compareMaterials.Count())).ToString() + "px;\" align=\"center\" class=\"CompareTable_ValueCell\">";
                    MaterialTypeFieldValue fieldValue = field.ValueOf(material.MaterialID);
                    if (fieldValue != null)
                    {
                        html += fieldValue.GetValueLink(field.Unit);
                    }
                    else
                    {
                        html += "-----";
                    }
                    html += "</td>";
                }
                html += "</tr>";
            }
            //price row :
            html +=
                "<tr><td align=\"left\" class=\"CompareTable_RightHeaderTD\">" +
                    "قیمت" +
                "</td>";
            foreach (Material material in compareMaterials)
            {
                html +=
                    "<td style=\"width : " + ((int)(_TotalWidth / compareMaterials.Count())).ToString() + "px; padding : 10px 0px 10px 0px;\" align=\"center\" class=\"CompareTable_ValueCell\">" +
                        material.PriceHtml_Vertical +
                    "</td>";
            }
            html += "</tr>";
            // add to cart row :
            html +=
                    "<tr><td class=\"CompareTable_TopRightHeaderTD\"></td>";
            foreach (Material material in compareMaterials)
            {
                html +=
                    "<td style=\"width : " + ((int)(_TotalWidth / compareMaterials.Count())).ToString() + "px; padding : 10px 0px 10px 0px;\" align=\"center\" class=\"CompareTable_ValueCell\">" +
                        "<a class=\"CompareTable_AddToCart\" href=\"" + Repository.ToHtmlUrl("~/Views/ShoppingCart.aspx") + "?From=info&MaterialID=" + material.MaterialID.ToString() + "\"></a>" +
                    "</td>";
            }
            html += "</tr>";
            //close tag :
            html += "</table>";
        }
        else
        {
            html =
                "<div class=\"EmptyList\">" + 
                    "کالایی برای مقایسه انتخاب نشده است." +
                "</div>";
        }
        return html;
    }
    private int GetThumbnailWidth(int totalWidth, int materialsCount)
    {
        int w = 100;
        int pad = 25;
        if (totalWidth / materialsCount > w + pad)
        {
            return w;
        }
        else
        {
            return (int)(totalWidth / materialsCount) - pad;
        }
    }
}
