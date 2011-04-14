using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Diagram
/// </summary>
public class Diagram
{
    //******************** variables :
    int width = 150;//px
    int height = 8;//px
    int padding = 1;//px
    string greenColor = "#00d71b";
    string redColor = "#ff0000";
    string borderColor = "#bcbec0";
    string font_weight = "normal";
    string font_size = "7pt";
    //******************** Constructors :
    public Diagram(DiagramType type, double greenValue, double redValue)
    {
        DType = type;
        GreenValue = greenValue;
        RedValue = redValue;
    }
    public Diagram(DiagramType type, double greenValue, double redValue, int diagramWidth, int diagramHeight, int diagramPadding)
    {
        DType = type;
        GreenValue = greenValue;
        RedValue = redValue;
        Width = diagramWidth;
        Height = diagramHeight;
        Padding = diagramPadding;
    }
    public Diagram(DiagramType type, double greenValue, double redValue, int diagramWidth, int diagramHeight, int diagramPadding, string diagramFontSize, string diagramFontWeight)
    {
        DType = type;
        GreenValue = greenValue;
        RedValue = redValue;
        Width = diagramWidth;
        Height = diagramHeight;
        Padding = diagramPadding;
        Font_Weight = diagramFontWeight;
        Font_Size = diagramFontSize;
    }
    //******************** Properties :
    private DiagramType DType { get; set; }
    private double GreenValue { get; set; }
    private double RedValue { get; set; }
    public string GreenColor { get { return greenColor; } set { greenColor = value; } }
    public string RedColor { get { return redColor; } set { redColor = value; } }
    public string BorderColor { get { return borderColor; } set { borderColor = value; } }
    public string Font_Weight { get { return font_weight; } set { font_weight = value; } }
    public string Font_Size { get { return font_size; } set { font_size = value; } }
    public int Width { get { return width; } set { width = value; } }
    public int Height { get { return height; } set { height = value; } }
    public int Padding { get { return padding; } set { padding = value; } }
    //******************** Methods :
    public string ToHtml()
    {
        string html = "";
        if (DType == DiagramType.Combined)
        {
            html =
                "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"" +
                    "<tr>" +
                        "<td style=\"padding-left : 3px; font-weight : " + Font_Weight + "; color : #939598; font-size : " + Font_Size + ";\">" +
                            GreenValue.ToString() + "%" +
                        "</td>" +
                        "<td>" +
                            "<div style=\"height : " + Height.ToString() + "px; width: " + Width.ToString() + "px; padding : " + Padding.ToString() + "px; border : solid 1px " + BorderColor + "; background-color : White;\">" +
                            "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"" + (GreenValue + RedValue > 0 ? "100%" : "0px") + "\">" +
                                    "<tr>" +
                                        "<td style=\"height : " + Height.ToString() + "px; border-left : solid 1px White; background-color : " + GreenColor + "; width : " + GreenValue.ToString() + "%;\">" +
                                        "</td>" +
                                        "<td style=\"height : " + Height.ToString() + "px; background-color : " + RedColor + "; width : " + RedValue.ToString() + "%;\">" +
                                        "</td>" +
                                    "</tr>" +
                                "</table>" +
                            "</div>" +
                        "</td>" +
                        "<td style=\"padding-right : 3px; font-weight : " + Font_Weight + "; color : #939598; font-size : " + Font_Size + ";\">" +
                            RedValue.ToString() + "%" +
                        "</td>" +
                    "</tr>" +
                "</table>";
        }
        else if (DType == DiagramType.Separated)
        {
            html =
                "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"" + Width.ToString() + "px;\" style=\"height : 26px;\">" +
                    "<tr>" +
                        "<td style=\"background-color : #bcbec0; width : 13px;\" rowspan=\"3\">" +
                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" +
                            "<div style=\"background-color : White; border-top : solid 1px " + BorderColor + "; border-bottom : solid 1px " + BorderColor + "; border-left : solid 1px " + BorderColor + "; width : 100%; padding : " + Padding.ToString() + "px 0px " + Padding.ToString() + "px " + Padding.ToString() + "px; height : " + Height.ToString() + "px;\">" +
                                "<div style=\"background-color : " + GreenColor + "; width : " + GreenValue.ToString() + "%; height : " + Height.ToString() + "px;\"></div>" +
                            "</div>" +
                        "</td>" +
                        "<td style=\"width : 20px; padding-right : 3px;\">" +
                            "<span style=\"font-weight : " + Font_Weight + "; font-size : " + Font_Size + "; color : Gray;\">" + GreenValue.ToString() + "%</span>" +
                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" +
                            "<div style=\"background-color : White; border-top : solid 1px " + BorderColor + "; border-bottom : solid 1px " + BorderColor + "; border-left : solid 1px " + BorderColor + "; width : 100%; padding : " + Padding.ToString() + "px 0px " + Padding.ToString() + "px " + Padding.ToString() + "px; height : " + Height.ToString() + "px;\">" +
                                "<div style=\"background-color : " + RedColor + "; width : " + RedValue.ToString() + "%; height : " + Height.ToString() + "px;\"></div>" +
                            "</div>" +
                        "</td>" +
                        "<td style=\"width : 20px; padding-right : 3px;\">" +
                            "<span style=\"font-weight : " + Font_Weight + "; font-size : " + Font_Size + "; color : Gray;\">" + RedValue.ToString() + "%</span>" +
                        "</td>" +
                    "</tr>" +
                "</table>";
        }
        else if (DType == DiagramType.JustGreen)
        {
            html =
                "<table>" + 
                    "<tr>" + 
                        "<td>" +
                            "<div style=\"background-color : White; border : solid 1px " + BorderColor + "; width : " + Width.ToString() + "px; padding : " + Padding.ToString() + "px; height : " + Height.ToString() + "px;\">" +
                                "<div style=\"background-color : " + GreenColor + "; width : " + GreenValue.ToString() + "%; height : " + Height.ToString() + "px;\"></div>" +
                            "</div>" +
                        "</td>" +
                        "<td style=\"font-weight : " + Font_Weight + "; font-size : " + Font_Size + "; padding-right : 4px; color : Gray;\">" + 
                            GreenValue.ToString() + "%" +
                        "</td>" +
                    "</tr>" +
                "</table>";
        }
        else if (DType == DiagramType.JustRed)
        {
            html =
                "<table>" +
                    "<tr>" +
                        "<td>" +
                            "<div style=\"background-color : White; border : solid 1px " + BorderColor + "; width : " + Width.ToString() + "px; padding : " + Padding.ToString() + "px; height : " + Height.ToString() + "px;\">" +
                                "<div style=\"background-color : " + RedColor + "; width : " + RedValue.ToString() + "%; height : " + Height.ToString() + "px;\"></div>" +
                            "</div>" +
                        "</td>" +
                        "<td style=\"font-weight : " + Font_Weight + "; font-size : " + Font_Size + "; padding-right : 4px; color : Gray;\">" +
                            RedValue.ToString() + "%" +
                        "</td>" +
                    "</tr>" +
                "</table>";
        }
        return html;
    }
}
