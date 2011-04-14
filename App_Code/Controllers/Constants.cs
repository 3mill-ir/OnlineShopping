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

/// <summary>
/// Summary description for Constants
/// </summary>

public class Constants
{
    public static int MenuItemWidth { get { return 220; } }
    public static string UrlOfMaterialImages { get { return "~/Content/Images/Materials/"; } }
    public static string UrlOfMaterialThumbnails { get { return "~/Content/Images/Materials/Thumbnails/"; } }
    public static string UrlOfSharedImages { get { return "~/Content/Images/Shared/"; } }
    public static string UrlOfSlidingImages { get { return "~/Scripts/Slider/data/images/"; } }
    public static int MaxPhotoBytes { get { return 1024 * 256; } }
    public static int ThumbnailFitSize { get { return 120; } }
    public static Unit SlidingImageWidth { get { return new Unit("463px"); } }
    public static Unit SlidingImageHeight { get { return new Unit("110px"); } }
    public static int CompareCapacity { get { return 5; } }
    public static int MinimumBenefitTumans { get { return 1000000; } }
    public static int Prize { get { return MinimumBenefitTumans / 10; } }
    public static string TextsRoot { get { return "~/Content/Text/"; } }
    private static string Words { get { return "بازار مرزی ایرانیان , فروشگاه , فروشگاه اینترنتی , فروشگاه آنلاین , خرید , خرید اینترنتی , خرید پستی , فروش , بازارچه مرزی , بازار مرزی , فروشگاه مرزی ,خرید ارزان , خرید آنلاین , اينترنتي , بانه , پیرانشهر , ارومیه"; } }
    public static string StaticKeywords 
    {
        get
        {
            return Words;
        }
    }
    public static string StaticDescription 
    { 
        get 
        {
            MaterialsRepository mr = new MaterialsRepository();
            return Words + " , " + mr.GetAllCategories_MetaDescription();
        } 
    }
    public static string StaticTitle { get { return "بازار مرزی ایرانیان | مجموعه آنلاین فروشگاه های مرز غربی ایران"; } }
}
public class MyRoles
{
    public static string Admin { get { return "Admin"; } }
    public static string Member { get { return "Member"; } }
    public static string Shop { get { return "Shop"; } }
}
public class MyUnits
{
    public static string UnitString(MyUnit units)
    {
        if (units == MyUnit.Number)
            return "عدد";
        else if (units == MyUnit.Box)
            return "بسته";
        else if (units == MyUnit.Kilogeram)
            return "کیلوگرم";
        else if (units == MyUnit.Set)
            return "مجموعه";
        else if (units == MyUnit.Meter)
            return "متر";
        else if (units == MyUnit.Halghe)
            return "حلقه";
        else if (units == MyUnit.System)
            return "دستگاه";
        else if (units == MyUnit.Liter)
            return "لیتر";
        else return "";
    }
    public static List<string> ToStringList
    {
        get
        {
            List<string> list = new List<string>(new string[] {"عدد", "بسته", "کیلوگرم", "مجموعه", "متر", "حلقه", "دستگاه", "لیتر" });
            return list;
        }
    }
    public static List<MyUnit> ToUnitList
    {
        get
        {
            List<MyUnit> list = new List<MyUnit>(new MyUnit[] { (MyUnit)1, (MyUnit)2, (MyUnit)3, (MyUnit)4, (MyUnit)5, (MyUnit)6, (MyUnit)7, (MyUnit)8, });
            return list;
        }
    }
}

