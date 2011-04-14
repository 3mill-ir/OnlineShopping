using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Collection : System.Web.UI.UserControl
{
    //****************
    int maxPictures = 50, maxWidth = 950, picsHeight = 80;
    MaterialInfoShowType showType = MaterialInfoShowType.Show;
    CollectionDirection direction = CollectionDirection.Horizontal;
    bool visible = true;
    IEnumerable<Material> materials = null;
    //****************
    //************ Properties :
    public CollectionDirection Direction 
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }
    public List<CollectionMaterial> CollectionMaterials 
    {
        get
        {
            return (List<CollectionMaterial>)ViewState["CollectionMaterials"];
        }
        set
        {
            ViewState["CollectionMaterials"] = value;
        }
    }
    public IEnumerable<Material> Materials
    {
        get
        {
            return materials;
        }
        set
        {
            //List<CollectionMaterial> colMats = GetMaterials(value).Take(MaxPictures).ToList<CollectionMaterial>();
            //CollectionMaterials = colMats;
            materials = value;
            Visible = (value.Count() > 0 ? true : false);
            lbl_Pics.Text = HorizantalHtml;
        }
    }
    public string PopupInfoID { get; set; }
    protected string HorizantalHtml
    {
        get
        {
            int _MaxHeight = 50, tableWidth = 0, imgPadding = 8, tdPadding = 4, totalPadding = 0;
            totalPadding = imgPadding + tdPadding;
            string html = "<table border=\"0\" cellpadding=\"2\" cellspacing=\"0\"><tr>";
            foreach (Material cMaterial in Materials)
            {
                Picture avatar = cMaterial.Avatar;
                MyImage myImage = new MyImage(avatar.Thumbnail_Width, avatar.Thumbnail_Height, ResizeType.HeightFix, _MaxHeight);
                if (tableWidth + myImage.Thumb_Width < MaxWidth)
                {
                    MaterialInfoDirection direction = (tableWidth + myImage.Thumb_Width < (MaxWidth / 2) ? MaterialInfoDirection.Left : MaterialInfoDirection.Right);
                    html +=
                        "<td>" +
                            cMaterial.Link(MaterialInfoType.NameAndInfo, MaterialInfoDirection.Automatic, MaterialInfoShowType.Show, ResizeType.HeightFix, _MaxHeight, PopupInfoID, MaterialInfoHeightType.MinHeight, 100) +
                        "</td>";
                    tableWidth += myImage.Thumb_Width + totalPadding;
                }
            }
            html += "</tr></table>";
            return html;
        }
    }
    protected string VerticalHtml
    {
        get
        {
            string html = "<table border=\"0\" cellpadding=\"2\" cellspacing=\"0\" style=\"background-color : #f1f2f2; border : solid 1px #d1d3d4;\">";
            int i = 0;
            foreach (CollectionMaterial cMaterial in CollectionMaterials)
            {
                string topPadding = (i == 0 ? "10px" : "5px");
                string bottomPadding = (i == CollectionMaterials.Count() - 1 ? "10px" : "5px");
                html +=
                    "<tr>" + 
                        "<td style=\"padding : " + topPadding + " 10px " + bottomPadding + " 10px;\">" +
                            cMaterial.Link +
                        "</td>" + 
                    "</tr>";
                i++;
            }
            html += "</table>";
            return html;
        }
    }
    public string ID { get; set; }
    public string Title { get; set; }
    public bool Visible
    {
        get
        {
            return visible;
        }
        set
        {
            visible = value;
        }
    }
    public int MaxWidth { get { return maxWidth; } set { maxWidth = value; } }
    public int MaxPictures
    {
        get { return maxPictures; }
        set { maxPictures = value; }
    }
    public MaterialInfoShowType InfoShowType
    {
        get
        {
            return showType;
        }
        set
        {
            showType = value;
        }
    }
    private List<CollectionMaterial> GetMaterials(IEnumerable<Material> materials)
    {
        List<CollectionMaterial> list = new List<CollectionMaterial>();
        int _MaxHeight = 50;
        foreach (Material mat in materials)
        {
            CollectionMaterial cMat = new CollectionMaterial();
            cMat.Link = mat.Link(MaterialInfoType.NameAndInfo, MaterialInfoDirection.Automatic, MaterialInfoShowType.Show, ResizeType.HeightFix, _MaxHeight, PopupInfoID, MaterialInfoHeightType.MinHeight, 80);
            cMat.Avatar = mat.Avatar;
            list.Add(cMat);
        }
        return list;
    }
}
