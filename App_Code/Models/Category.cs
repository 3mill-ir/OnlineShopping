using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// Summary description for Category
/// </summary>
namespace Models
{
    public partial class Category
    {
        public IEnumerable<Set> SortedSets
        {
            get
            {
                var query = from set in Sets
                            orderby set.Sequence
                            select set;
                return query;
            }
        }
        public int MaterialsCount
        {
            get
            {
                int count = 0;
                foreach (Set set in Sets)
                {
                    count += set.VisibleMaterials.Count();
                }
                return count;
            }
        }
        public string MenuItem
        {
            get
            {
                int setsCount = SortedSets.Count();
                string item = "";
                string itemId = "btn_" + CategoryID.ToString();
                item =
                    "<a id=\"" + itemId + "\" class=\"MenuItem" + (setsCount > 0 ? " MenuItemArrow" : "") + "\" href=\"" + Repository.ToHtmlUrl("~/Views/CategoryDetails.aspx") + "?CategoryID=" + CategoryID.ToString() + "\">" +
                        Name + " ( " + MaterialsCount.ToString() + " )" +
                    "</a>" +
                    "<script type=\"text/javascript\">" +
                        "var padding = 20;" +
                        "$('#" + itemId + "').mouseenter(function(){" +
                            "$('#PopupMenu').html('" + GetSetsHtml(SortedSets) + "');" +
                            "var w = $('#PopupMenu').width();" +
                            "$(this).addClass('MenuDiv_Hover" + (setsCount > 0 ? " MenuItemArrow_Hover" : "") + "');" +
                            "var p = $('#" + itemId + "').position();" +
                            "$('#PopupMenu').hide();" +
                            (setsCount > 0 ? "$('#PopupMenu').css('top', p.top + 'px').css('left', p.left - w + 'px').show();" : "") +
                        "});" +
                        "$('#" + itemId + "').mouseleave(function(e){" +
                            "var leaveFromLeft = false;" +
                            "var setsCount = " + setsCount.ToString() + ";" +
                            "if (e.pageX < $(this).position().left) { " +
                            "leaveFromLeft = true; } else { " +
                            "leaveFromLeft = false;}" +
                            "if (!leaveFromLeft || (leaveFromLeft && setsCount == 0)) { " +
                                "$(this).removeClass('MenuDiv_Hover').removeClass('MenuItemArrow_Hover');" +
                                "$('#PopupMenu').hide();" +
                            "}" +
                        "});" +
                        "$('#PopupMenu').mouseleave(function(){" +
                            "$(this).hide();" +
                            "$('#" + itemId + "').removeClass('MenuDiv_Hover').removeClass('MenuItemArrow_Hover');" +
                        "});" +
                    "</script>";
                return item;
            }
        }
        private string GetSetsHtml(IEnumerable<Set> Sets)
        {
            string html = "";
            int columnsCount = GetColumnsCount(Sets.Count());
            int tdWidthPercent = (int)(100 / columnsCount);
            html +=
                    "<table border=\"0\" cellpadding=\"2\" cellspacing=\"0\" width=\"" + columnsCount * Constants.MenuItemWidth + "\" style=\"margin : 10px 0px 10px 0px;\"><tr>";
            int i = 0;
            foreach(Set set in Sets)
            {
                if (i % columnsCount != 0)
                {
                    html +=
                        "<td style=\"width : " + tdWidthPercent.ToString() + "%;\">" +
                            set.MenuItem +
                        "</td>";
                }
                else
                {
                    html +=
                        "</tr><tr>" +
                        "<td style=\"width : " + tdWidthPercent.ToString() + "%;\">" +
                            set.MenuItem +
                        "</td>";
                }
                i++;
            }
            html += "</tr></table>";
            return html;
        }
        private int GetColumnsCount(int setsCount)
        {
            if (setsCount <= 10)
            {
                return 1;
            }
            else if (setsCount > 10 && setsCount <= 20)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        public string Meta_Description
        {
            get
            {
                string meta = Name;
                var keys = from set in Sets select set.Name;
                foreach (string key in keys)
                {
                    meta += " , " + key;
                }
                return meta;
            }
        }
    }
}
