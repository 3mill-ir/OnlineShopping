using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data.Linq;

/// <summary>
/// Summary description for Repository
/// </summary>
public class Repository
{
    public static DateTime Now { get { return DateTime.Now; } }
    public static string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["ASPNETDBConnectionString"].ConnectionString;
        }
    }
    public static string CutString(object text, int maxLenght)
    {
        string res = "";
        if (text.ToString().Length < maxLenght)
        {
            res = text.ToString();
        }
        else
        {
            res = text.ToString().Substring(0, maxLenght) + " ...";
        }
        return res;
    }
    public static void MessageBoxShow(string MessageText, System.Web.UI.UpdatePanel uPanel, Type typeOfControl)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(uPanel, typeOfControl, "click", @"alert('" + MessageText + "');", true);
    }
    public static void MessageBoxShow(string MessageText, System.Web.UI.Control ctrl, Type typeOfControl)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(ctrl, typeOfControl, "click", @"alert('" + MessageText + "');", true);
    }
    public static string GetRandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }
    public static string GetExtension(string FileName)
    {
        return FileName.Substring(FileName.LastIndexOf('.') + 1);
    }
    public static bool IsSupportedExtension(string Ext)
    {
        if ((Ext.ToLower() == "jpeg") || (Ext.ToLower() == "png") || (Ext.ToLower() == "jpg") || (Ext.ToLower() == "gif"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public System.Drawing.Image GetThumbnailImage(System.Drawing.Image image, int Thumb_Width, int Thumb_Height)
    {
        return image.GetThumbnailImage(Thumb_Width, Thumb_Height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
    }
    public bool ThumbnailCallback()
    {
        return true;
    }
    public static List<string> SplitString(string text, string splitter)
    {
        Regex rgx = new Regex(splitter);
        string[] array = rgx.Split(text);
        List<string> list = new List<string>();
        foreach (string st in array)
        {
            if (st.Trim().Length > 0)
                list.Add(st);
        }
        return list;
    }
    public static string ToHtmlUrl(string AspUrl)
    {
        return VirtualPathUtility.ToAbsolute(AspUrl);
    }
    public static int GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public static string ClientIP
    {
        get
        {
            string clientIPAddress = "";
            string strHostName = System.Net.Dns.GetHostName();
            clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
            return clientIPAddress;
        }
    }
    public static EntitySet<T> ToEntitySet<T>(IEnumerable<T> source) where T : class
    {
        var es = new EntitySet<T>();
        es.AddRange(source);
        return es;
    }
    public bool IsEmailAddress(string emailAddress)
    {
        string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";
        Regex rgx = new Regex(pattern);
        bool isMatch = rgx.IsMatch(emailAddress);
        return isMatch;
    }
    public string GetUserLocation()
    {
        string loc = "";
        
        return loc;
    }

}