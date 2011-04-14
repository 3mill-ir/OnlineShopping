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
/// Summary description for Tag
/// </summary>
public class Tag
{
    public Tag(string text, int repetition)
    {
        Text = text;
        Repetition = repetition;
    }
    private int _MinFontSize = 10;
    private int _MaxFontSize = 30;
    public string Text { get; set; }
    public int Repetition { get; set; }
    public string GetLink(int minRepeats, int maxRepeats)
    {
        double fontSize = GetFontSize(minRepeats, maxRepeats);
        string link = "<a class=\"Link\" style=\"font-size : " + fontSize.ToString() + "px\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Tag=" + Text.Replace(" ", "$") + "\">" + Text + "</a>";
        return link;
    }
    private double GetFontSize(int minRepeats, int maxRepeats)
    {
        double size = _MinFontSize;
        //
        double percent = (100 * (Repetition - minRepeats)) / (maxRepeats - minRepeats);
        size = _MinFontSize + (((_MaxFontSize - _MinFontSize) * percent) / 100);
        //
        return size;
    }
}
