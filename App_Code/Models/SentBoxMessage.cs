using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SentBoxMessage
/// </summary>
namespace Models
{
    public partial class SentBoxMessage
    {
        public string Subject_Link
        {
            get
            {
                return "<a href=\"" + Repository.ToHtmlUrl("~/Views/Members/Messages.aspx") + "?show=sentbox&id=" + SentBoxID.ToString() + "\" class=\"Link\">" + Subject + "</a>";
            }
        }
        public string Reciever_Link
        {
            get
            {
                return "گیرنده : " + "<a class=\"Link\" onclick=\"return OpenCenter('" + Repository.ToHtmlUrl("~/Views/Members/SendMessage.aspx") + "?MemberID=" + Reciever.MemberID.ToString() + "&Name=" + Reciever.FullName + "', 'SendMessage', 500, 300);\">" + Reciever.FullName + "</a>";
            }
        }
        public string DetailsHtml
        {
            get
            {
                string html = "";
                html =
                    "<div style=\"padding-right : 5px;color : #146eb4; font-weight : bold; font-size : 9pt;\">" +
                        Subject +
                    "</div>" +
                    "<div style=\"line-height : 25px; text-decoration : justify; background-color : #f1f2f2; margin : 10px; padding : 5px 15px 5px 15px; border : solid 1px #dcddde;\">" +
                        Text +
                    "</div>" +
                    "<div style=\"padding : 0px 0px 10px 10px;\">" +
                        "<a class=\"Link\" style=\"float : left;\" onclick=\"return OpenCenter('" + Repository.ToHtmlUrl("~/Views/Members/SendMessage.aspx") + "?MemberID=" + Reciever.MemberID.ToString() + "&Name=" + Reciever.FullName + "', 'SendMessage', 500, 300);\">" + "پـاسخ به پیـام " + Reciever.FullName + "</a>" +
                    "</div>";
                return html;
            }
        }
    }
}
