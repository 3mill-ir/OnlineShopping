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
using Pta;

/// <summary>
/// Summary description for MyDateTime
/// </summary>
public class ShamsiDateTime
{
    public ShamsiDateTime()
	{
        Year = 0;
        Month = 0;
        Day = 0;
	}
    public ShamsiDateTime(int year, int month, int day)
    {
        //
        // TODO: Add constructor logic here
        //
        Year = year;
        Month = month;
        Day = day;
    }
    //*************** Properties :
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public string DayName
    {
        get
        {
            Shamsi shamsi = new Shamsi();
            return GetDayOfWeek(shamsi.GetDayOfWeek(MiladyDate));
        }
    }
    public DateTime MiladyDate
    {
        get
        {
            return Shamsi.ShamsiToMiladi(Year, Month, Day);
        }
    }
    //************** Methods :
    public string ToString()
    {
        return Year.ToString("D4") + "/" + Month.ToString("D2") + "/" + Day.ToString("D2");
    }
    public string ToLongString()
    {
        return DayName + " " + Day.ToString() + " " + MonthName + " " + Year.ToString();
    }
    public string ToLongString(bool ShowTime)
    {
        return DayName + " " + Day.ToString() + " " + MonthName + " " + Year.ToString() + (ShowTime ? "" : "");
    }
    public string ToMediumString()
    {
        return Day.ToString() + " " + MonthName + " " + Year.ToString();
    }
    public string MonthName
    {
        get
        {
            string name = "";
            switch (Month)
            {
                case 1:
                    name = "فروردین";
                    break;
                case 2:
                    name = "اردیبهشت";
                    break;
                case 3:
                    name = "خرداد";
                    break;
                case 4:
                    name = "تیر";
                    break;
                case 5:
                    name = "مرداد";
                    break;
                case 6:
                    name = "شهریور";
                    break;
                case 7:
                    name = "مهر";
                    break;
                case 8:
                    name = "آبان";
                    break;
                case 9:
                    name = "آذر";
                    break;
                case 10:
                    name = "دی";
                    break;
                case 11:
                    name = "بهمن";
                    break;
                case 12:
                    name = "اسفند";
                    break;
            }
            return name;
        }
    }
    public static ShamsiDateTime MiladyToShamsi(DateTime miladyDate)
    {
        string st = Shamsi.MiladiToShamsi(miladyDate);
        System.Text.RegularExpressions.Regex rgxDateSpliter = new System.Text.RegularExpressions.Regex(@"/");
        string[] dateString = rgxDateSpliter.Split(st);
        int year = Convert.ToInt32(dateString[0]);
        int month = Convert.ToInt32(dateString[1]);
        int day = Convert.ToInt32(dateString[2]);
        return new ShamsiDateTime(year, month, day);
    }
    public string GetDayOfWeek(DayOfWeek day)
    {
        string dayName = "";
        if (day == DayOfWeek.Friday)
        {
            dayName = "جمعه";
        }
        else if (day == DayOfWeek.Monday)
        {
            dayName = "دوشنبه";
        }
        else if (day == DayOfWeek.Saturday)
        {
            dayName = "شنبه";
        }
        else if (day == DayOfWeek.Sunday)
        {
            dayName = "یکشنبه";
        }
        else if (day == DayOfWeek.Thursday)
        {
            dayName = "پنجشنبه";
        }
        else if (day == DayOfWeek.Tuesday)
        {
            dayName = "سه شنبه";
        }
        else if (day == DayOfWeek.Wednesday)
        {
            dayName = "چهارشنبه";
        }
        return dayName;
    }
}
