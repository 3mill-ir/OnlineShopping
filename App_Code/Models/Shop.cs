using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Shop
/// </summary>
namespace Models
{
    public partial class Shop
    {
        public string SelectLink
        {
            get
            {
                string html = "";
                html = "<a id=\"" + ShopID.ToString() + "\" class=\"ButtonLink\" style=\"font-weight : normal;\">" + Name + "</a>";
                html +=
                    "<script type=\"text/javascript\">" +
                        "$(function() {" +
                            "$('#" + ShopID.ToString() + "').click(function() {" +
                                "var myForm = opener.document.forms[0];" +
                                 "myForm[\"tb_ShopID\"].value = " + ShopID + ";" +
                                 "window.close();" +
                            "});" +
                        "});" +
                    "</script>";
                return html;
            }
        }
        public ShopLogin LastLogin
        {
            get
            {
                var query = from login in Logins
                            orderby login.DateOfLogin
                            select login;
                return (query.Count() > 0 ? query.Last() : null);
            }
        }
    }
}