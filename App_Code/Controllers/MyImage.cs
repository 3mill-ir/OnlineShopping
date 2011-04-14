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
/// Summary description for DocumentImage
/// </summary>
public class MyImage
{
    private ResizeType type = ResizeType.LongerFix;
	private int maxLength = 100;
    public MyImage(int width, int height)
	{
        Width = width;
        Height = height;
	}
    public MyImage(int width, int height, ResizeType resizeType, int maximumLength)
    {
        Width = width;
        Height = height;
        Type = resizeType;
        MaxLength = maximumLength;
    }
    public int MaxLength 
    { 
        get 
        { 
            return maxLength; 
        }
        set
        {
            maxLength = value;
        }
    }
    public int Height { get; set; }
    public int Width { get; set; }
    public ResizeType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    public int Thumb_Height 
    {
        get
        {
            if (Type == ResizeType.LongerFix)
            {
                if (Width > Height)
                {
                    return (int)((MaxLength * Height) / Width);
                }
                else if (Width < Height)
                {
                    return MaxLength;
                }
                else
                {
                    return MaxLength;
                }
            }
            else if (Type == ResizeType.WidthFix)
            {
                return (int)((MaxLength * Height) / Width);
            }
            else
            {
                return MaxLength;
            }
        }
    }
    public int Thumb_Width 
    {
        get
        {
            if (Type == ResizeType.LongerFix)
            {
                if (Width > Height)
                {
                    return MaxLength;
                }
                else if (Width < Height)
                {
                    return (int)((MaxLength * Width) / Height);
                }
                else
                {
                    return MaxLength;
                }
            }
            else if (Type == ResizeType.WidthFix)
            {
                return MaxLength;
            }
            else
            {
                return (int)((MaxLength * Width) / Height);
            }
        }
    }
}
public enum ResizeType
{
    None = 0,
    WidthFix = 1,
    HeightFix = 2,
    LongerFix = 3
}
