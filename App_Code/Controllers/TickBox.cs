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
using Models;
using System.Collections.Generic;

/// <summary>
/// Summary description for ThickBox
/// </summary>
public class TickBox
{
    //***************************
    private int width = 500;
    private int holderwidth = 88;
    private int endEmptyPlace = 200;
    //***************************Constructors :
    public TickBox(IEnumerable<Material> materials, int WidthOfThickBox)
    {
        Materials = materials;
        Width = WidthOfThickBox;
    }
    public TickBox(IEnumerable<Material> materials, int WidthOfThickBox, int WidthOfHolder_InPercent)
    {
        Materials = materials;
        Width = WidthOfThickBox;
        HolderWidthPercentage = WidthOfHolder_InPercent;
    }
    //***************************
    public IEnumerable<Material> Materials { get; set; }
    public int Width { get { return width; } set { width = value; } }
    public int HolderWidthPercentage { get { return holderwidth; } set { holderwidth = value; } }
    public string Html
    {
        get
        {
            string html = "";
            html =
                "<div class=\"ThickBoxContainer\">" +
                    "<div class=\"gallery\" style=\"margin-right : auto; margin-left : auto; width : " + Width.ToString() + "px;\">" +
                        "<div class=\"holder\" style=\"width: " + HolderWidthPercentage.ToString() + "%; border : solid 1px #c7c8ca; background-color : #f1f2f2; padding : 10px;\">" +
                            "<ul>";
            foreach (Material material in Materials)
            {
                html +=
                    "<li>" +
                        material.TickBoxInfo + //material.Link(MaterialInfoType.Full, MaterialInfoDirection.Automatic, MaterialInfoShowType.Show, ResizeType.HeightFix, 110, "", MaterialInfoHeightType.MinHeight, 80) +
                    "</li>";
            }
            for (int i = 0; i < (int)(Materials.Count() / 3); i++)
            {
                html += "<li></li>";
            }
            html +=
                "</ul>" +
                        "</div>" +
                        "<a href=\"#\" class=\"prev\"></a>" +
                        "<a href=\"#\" class=\"next\"></a>" +
                    "</div>" +
                "</div>";
            return html;
        }
    }
}
