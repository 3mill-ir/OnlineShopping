using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MemberInterest
/// </summary>
namespace Models
{
    public partial class MemberInterest
    {
        public string Name_Link
        {
            get
            {
                string html = "";
                html = "<a class=\"Link Bold\" href=\"" + Repository.ToHtmlUrl("~/Views/SearchResult.aspx") + "?Text=" + Name + "&SearchType=1" + "\">" + Name + "</a>";
                return html;
            }
        }
    }
}
