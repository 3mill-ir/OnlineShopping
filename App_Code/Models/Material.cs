using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

/// <summary>
/// Summary description for Material
/// </summary>
namespace Models
{
    public partial class Material
    {
        public string Meta_Description
        {
            get
            {
                string meta = Name;
                var keys = from key in Keywords orderby key.Type descending select key.Word;
                foreach (string key in keys)
                {
                    meta += " , " + key;
                }
                return meta;
            }
        }
        public int GetKeywordPoints(string Word)
        {
            int points = (from key in Keywords
                          where key.Word.Contains(Word)
                          select key.Type).Sum();
            return points;
        }
        public int PositiveVotes
        {
            get
            {
                return Votes.Where(v => v.Vote == true).Count();
            }
        }
        public int NegativeVotes
        {
            get
            {
                return Votes.Where(v => v.Vote == false).Count();
            }
        }
        public int VotesAverage
        {
            get
            {
                int count = 0;
                count = (PositiveVotes > NegativeVotes ? PositiveVotes - NegativeVotes : 0);
                return count;
            }
        }
        public int VotesAverage_Real
        {
            get
            {
                int count = 0;
                count = PositiveVotes - NegativeVotes;
                return count;
            }
        }
        public void ClearAvatar()
        {
            foreach (MaterialPicture pic in Pictures)
            {
                pic.IsAvatar = false;
            }
        }
        public MaterialPicture GetPicture(int pictureId)
        {
            try
            {
                return Pictures.Single(pic => pic.PictureID == pictureId);
            }
            catch
            {
                return null;
            }
        }
        public Picture Avatar
        {
            get
            {
                bool hasAvatar = false;
                Picture pic = new Picture();
                foreach (MaterialPicture mPic in Pictures)
                {
                    if (mPic.IsAvatar)
                    {
                        hasAvatar = true;
                        pic = mPic.PictureObject;
                    }
                }
                if (hasAvatar)
                    return pic;
                else
                {
                    Picture picc = new Picture();
                    picc.PicName = "photo_not_available.png";
                    picc.Url = Constants.UrlOfSharedImages + picc.PicName;
                    picc.ThumbnailUrl = Constants.UrlOfSharedImages + picc.PicName;
                    return picc;
                }
            }
        }
        public string NameLink
        {
            get
            {
                string html = "";
                html = "<a class=\"Link Bold\" style=\"line-height : 20px;\" href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" + Name + "</a>";
                return html;
            }
        }
        public string NameLink_Thick
        {
            get
            {
                string html = "";
                html = "<a class=\"Link\" href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" + Name + "</a>";
                return html;
            }
        }
        public string Link(MaterialInfoType type, MaterialInfoDirection dir, MaterialInfoShowType showType, ResizeType ThumbnailResizeType, int MaxLength, string InfoDivID, MaterialInfoHeightType heightType, int height)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, Int32.MaxValue);
            Picture avatar = Avatar;
            MyImage myImage = new MyImage(avatar.Thumbnail_Width, avatar.Thumbnail_Height, ThumbnailResizeType, MaxLength);
            string link = "";
            string id = "btn_" + MaterialID.ToString() + randomNumber.ToString();
            string imgId = "img_" + MaterialID.ToString() + randomNumber.ToString();
            link =
                "<a id=\"" + id + "\" href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" +
                    "<img id=\"" + imgId + "\" border=\"0\" src=\"" + Repository.ToHtmlUrl(avatar.ThumbnailUrl) + "\" class=\"SmallThumbnail\" " + (ThumbnailResizeType != ResizeType.None ? "width=\"" + myImage.Thumb_Width + "px" + "\" height=\"" + myImage.Thumb_Height + "px" + "\"" : "") + "/>" +
                "</a>" +
                "<script type=\"text/javascript\">" +
                    "var padding = 10;" +
                    "var imgPadding = 6;" +
                    "var borderWidth = 2;" +
                    "$('#" + imgId + "').mouseenter(function() {" +
                        "var p = $('#" + imgId + "').position();" +
                        "var dir = " + (dir == MaterialInfoDirection.Left ? "'left'" : (dir == MaterialInfoDirection.Right ? "'right'" : "'auto'")) + ";" +
                        "var x;" +
                        "if (dir == 'left') {" +
                            "x = p.left - $('#" + InfoDivID + "').width() - padding - borderWidth;" +
                        "} else if (dir == 'right') {" +
                            "x = p.left + $('#" + imgId + "').width() + imgPadding + 1;" +
                        "} else if (dir == 'auto') { " +
                            "var screenWidth = screen.width;" +
                            "if (p.left + ($('#" + imgId + "').width() / 2) < (screenWidth / 3)) {" +
                                "x = p.left + $('#" + imgId + "').width() + imgPadding + 1;" +
                            "} else {" +
                                "x = p.left - $('#" + InfoDivID + "').width() - padding - borderWidth;" +
                            "}" +
                        "}" +
                        "var y = p.top;" +
                        "$('#" + InfoDivID + "').css('left', x + 'px').css('top', y + 'px')" + ((heightType != MaterialInfoHeightType.Free) && (heightType != MaterialInfoHeightType.MinHeight) ? ".css('height', " + (heightType == MaterialInfoHeightType.FitPictureHeight ? "$('#" + imgId + "').height() - padding + borderWidth + borderWidth + 'px'" : (heightType == MaterialInfoHeightType.FixSize ? "'" + height.ToString() + "px'" : "")) + ")" : (heightType == MaterialInfoHeightType.MinHeight ? ".css('min-height', '" + height.ToString() + "px')" : "")) + ".html('" + GetMaterialInfo(dir) + "')." + (showType == MaterialInfoShowType.FadeIn ? "fadeIn(250)" : (showType == MaterialInfoShowType.SlideDown ? "slideDown(250)" : "show()")) + ";" +
                    "});" +
                    "$('#" + imgId + "').mouseleave(function(e) {" +
                        "var dir = " + (dir == MaterialInfoDirection.Left ? "'left'" : (dir == MaterialInfoDirection.Right ? "'right'" : "'auto'")) + ";" +
                        "if (dir == 'left') {" + 
                            "var LeaveFromLeft = false;" +
                            "if (e.pageX < $('#" + imgId + "').position().left + 2) {" +
                                "LeaveFromLeft = true;" +
                            "} else {" +
                                "LeaveFromLeft = false;" +
                            "}" +
                            "if (!LeaveFromLeft) { " +
                                "$('#" + InfoDivID + "').hide();" +
                            "}" +
                        "} else if (dir == 'right') {" + 
                            "var LeaveFromRight = false;" +
                            "if (e.pageX > $('#" + imgId + "').position().left + 2 + $('#" + imgId + "').width()) {" +
                                "LeaveFromRight = true;" +
                            "} else {" +
                                "LeaveFromRight = false;" +
                            "}" +
                            "if (!LeaveFromRight) { " +
                                "$('#" + InfoDivID + "').hide();" +
                            "}" +
                        "} else if (dir == 'auto') {" +
                            "var screenWidth = screen.width;" +
                            "var p = $('#" + imgId + "').position();" +
                            "if (p.left + ($('#" + imgId + "').width() / 2) < (screenWidth / 3)) {" +
                                "var LeaveFromRight = false;" +
                                "if (e.pageX > $('#" + imgId + "').position().left + 2 + $('#" + imgId + "').width()) {" +
                                    "LeaveFromRight = true;" +
                                "} else {" +
                                    "LeaveFromRight = false;" +
                                "}" +
                                "if (!LeaveFromRight) { " +
                                    "$('#" + InfoDivID + "').hide();" +
                                "}" +
                            "} else {" +
                                "var LeaveFromLeft = false;" +
                                "if (e.pageX < $('#" + imgId + "').position().left + 2) {" +
                                    "LeaveFromLeft = true;" +
                                "} else {" +
                                    "LeaveFromLeft = false;" +
                                "}" +
                                "if (!LeaveFromLeft) { " +
                                    "$('#" + InfoDivID + "').hide();" +
                                "}" +
                            "}" +
                        "}" +
                    "});" +
                    "$('#" + InfoDivID + "').mouseleave(function(){" + 
                        "$(this).hide();" +
                    "});" +
                "</script>";
            return link;
        }
        public string Info(ResizeType ThumbnailResizeType, int MaxLength)
        {
            VotePercent vPercent = VotesPercent;
            int positivePercent = vPercent.PositivePercent;
            int negativePercent = vPercent.NegativePercent;
            Picture avatar = Avatar;
            MyImage myImage = new MyImage(avatar.Thumbnail_Width, avatar.Thumbnail_Height, ThumbnailResizeType, MaxLength);
            string info = "";
            info =
                "<div class=\"Info\">" + 
                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" + 
                        "<tr>" +
                            "<td valign=\"bottom\" rowspan=\"3\">" +
                                "<a href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" +
                                    "<img border=\"0\" src=\"" + Repository.ToHtmlUrl(avatar.ThumbnailUrl) + "\" class=\"SmallThumbnail\" " + (ThumbnailResizeType != ResizeType.None ? "width=\"" + myImage.Thumb_Width + "px" + "\" height=\"" + myImage.Thumb_Height + "px" + "\"" : "") + "/>" +
                                "</a>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" + 
                            "<td valign=\"top\" style=\"padding : 7px;\">" + 
                                "<div>" + 
                                    NameLink +
                                "</div>" +
                                "<div style=\"padding-top : 10px;\">" +
                                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" + 
                                        "<tr>" +
                                            "<td>" +
                                                "قیمت :" +
                                            "</td>" +    
                                            "<td style=\"padding-right : 4px;\">" +
                                                PriceHtml_Current +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</div>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" + 
                            "<td valign=\"bottom\">" +
                                "<div style=\"padding-right : 5px;\">" +
                                    (new Diagram(DiagramType.Separated, positivePercent, negativePercent, 100, 5 ,1)).ToHtml() +
                                "</div>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                    "<div style=\"position : absolute; left : 10px; bottom : 10px;\">" +
                        "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                            "<tr>" +
                                "<td valign=\"bottom\">" +
                                    "<a class=\"AddToCart\" title=\"افزودن به سبد خرید\" href=\"" + Repository.ToHtmlUrl("~/Views/ShoppingCart.aspx") + "?From=info&MaterialID=" + MaterialID.ToString() + "\"></a>" +
                                "</td>" +
                                "<td valign=\"bottom\" style=\"padding-right : 5px;\">" +
                                    "<a class=\"AddToCompare_Small\" title=\"افزودن به مقایسه\" href=\"" + Repository.ToHtmlUrl("~/Views/CompareMaterials.aspx") + "?Action=Add&MaterialID=" + MaterialID.ToString() + "&Type=" + TypeID.ToString() + "\"></a>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                    "</div>" +
                "</div>";
            return info;
        }
        public string Info_Vertical(ResizeType ThumbnailResizeType, int MaxLength)
        {
            Picture avatar = Avatar;
            MyImage myImage = new MyImage(avatar.Thumbnail_Width, avatar.Thumbnail_Height, ThumbnailResizeType, MaxLength);
            string info = "";
            info =
                "<div class=\"Info\">" +
                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
                        "<tr>" +
                            "<td align=\"center\">" +
                                "<a href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" +
                                    "<img border=\"0\" src=\"" + Repository.ToHtmlUrl(avatar.ThumbnailUrl) + "\" class=\"SmallThumbnail\" " + (ThumbnailResizeType != ResizeType.None ? "width=\"" + myImage.Thumb_Width + "px" + "\" height=\"" + myImage.Thumb_Height + "px" + "\"" : "") + "/>" +
                                "</a>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align=\"center\">" +
                                "<div style=\"padding-top : 10px;\">" +
                                    NameLink +
                                "</div>" +
                                "<div style=\"padding-top : 10px;\">" +
                                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"Centerize\">" +
                                        "<tr>" +
                                            "<td>" +
                                                "قیمت :" +
                                            "</td>" +
                                            "<td style=\"padding-right : 4px;\">" +
                                                PriceHtml_Current +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</div>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                "</div>";
            return info;
        }
        public string TickBoxInfo
        {
            get
            {
                Picture avatar = Avatar;
                MyImage myImage = new MyImage(avatar.Thumbnail_Width, avatar.Thumbnail_Height, ResizeType.HeightFix, 60);
                string info = "";
                info =
                    "<table class=\"TickBoxInfo\" border=\"0\" cellpadding=\"3\" cellspacing=\"0\" style=\"width : 160px;\">" + 
                        "<tr>" +
                            "<td style=\"padding : 10px 10px 0px 10px;\" align=\"center\">" +
                                "<a href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" +
                                    "<img border=\"0\" src=\"" + Repository.ToHtmlUrl(avatar.ThumbnailUrl) + "\" class=\"SmallThumbnail\" width=\"" + myImage.Thumb_Width + "px\" height=\"" + myImage.Thumb_Height + "px\" />" +
                                "</a>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"padding : 5px 10px 0px 10px; direction : rtl; line-height : 22px;\" align=\"center\">" +
                                NameLink_Thick +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style=\"padding : 5px 10px 10px 10px; direction : rtl;\" align=\"center\">" +
                                PriceHtml_Current_Thick +
                            "</td>" +
                        "</tr>" +
                    "</table>";
                    /*
                    "<div class=\"Info\">" +
                        "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
                            "<tr>" +
                                "<td align=\"center\">" +
                                    "<a href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" +
                                        "<img border=\"0\" src=\"" + Repository.ToHtmlUrl(avatar.ThumbnailUrl) + "\" class=\"SmallThumbnail\" width=\"" + myImage.Thumb_Width + "px\" height=\"" + myImage.Thumb_Height + "px\" />" +
                                    "</a>" +
                                "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style=\"direction : rtl;\" align=\"center\">" +
                                    "<div style=\"padding-top : 10px;\">" +
                                        NameLink +
                                    "</div>" +
                                    "<div style=\"padding-top : 10px;\">" +
                                        PriceHtml_Current +
                                    "</div>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                    "</div>";*/
                return info;
            }
        }
        private string GetMaterialInfo(MaterialInfoDirection dir)
        {
            string info = "";
            info =
                "<table border=\"0\" cellpaddin=\"0\" cellspacing=\"0\" width=\"100%\">" +
                    "<tr>" +
                        "<td align=\"right\" style=\" padding-bottom : 3px; border-bottom : solid 1px #bcbec0;\">" +
                            "<a class=\"Link\" href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "\">" + Name + "</a>" +
                        "</td>" +
                    "</tr>" +
                "</table>" +
                "<div style=\"position : absolute; bottom : 5px; " + (dir == MaterialInfoDirection.Left ? "right" : "left") + " : 5px;\">" +
                    GetMaterialSmallPics() +
                "</div>" +
                "<div style=\"position : absolute; bottom : 5px; " + (dir == MaterialInfoDirection.Left ? "left" : "right") + " : 5px;\">" +
                    "<a class=\"AddToCart\" href=\"" + Repository.ToHtmlUrl("~/Views/ShoppingCart.aspx") + "?From=info&MaterialID=" + MaterialID.ToString() + "\"></a>" +
                "</div>";
            return info;
        }
        private string GetMaterialSmallPics()
        {
            string html = "";
            foreach (MaterialPicture pic in Pictures.Take(5))
            {
                html +=
                    "<a href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "&PictureID=" + pic.PictureID.ToString() + "\">" +
                        "<img border=\"0\" src=\"" + Repository.ToHtmlUrl(Constants.UrlOfMaterialThumbnails + pic.PicName) + "\" width=\"20px\" height=\"20px\" class=\"SmallImage\" style=\"margin : 2px 2px 0px 2px;\"/>" +
                    "</a>";
            }
            return html;
        }
        private string GetMaterialSmallPics(int count)
        {
            string html = "";
            foreach (MaterialPicture pic in Pictures.Take(count))
            {
                html +=
                    "<a href=\"" + Repository.ToHtmlUrl("~/Views/MaterialDetails.aspx") + "?MaterialID=" + MaterialID.ToString() + "&PictureID=" + pic.PictureID.ToString() + "\">" +
                        "<img border=\"0\" src=\"" + Repository.ToHtmlUrl(Constants.UrlOfMaterialThumbnails + pic.PicName) + "\" width=\"20px\" height=\"20px\" class=\"SmallImage\" style=\"margin : 2px 2px 0px 2px;\"/>" +
                    "</a>";
            }
            return html;
        }
        public List<string> TagsList
        {
            get
            {
                return Repository.SplitString(Tags, ";");
            }
        }
        public string TagsHtml
        {
            get
            {
                string html = "";
                List<string> tagsList = Repository.SplitString(Tags, ";");
                int i = 0;
                if (tagsList.Count > 0)
                {
                    foreach (string tag in tagsList)
                    {
                        html += "<a class=\"Link Bold\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Tag=" + tag.Replace(" ", "$") + "\">" + tag + "</a>" + (i < tagsList.Count - 1 ? " - " : "");
                        i++;
                    }
                }
                else
                {
                    html = "<span style=\"color : #a7a9ac; font-weight : bold;\">برچسبی موجود نیست</span>";
                }
                return html;
            }
        }
        public string PriceHtml
        {
            get
            {
                string html = "";
                if (Prices.Count > 0)
                {
                    html = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr>";
                    var oldPrices = from p in Prices where !p.IsCurrent orderby p.DateOfAdd descending select p;
                    var newPrices = from p in Prices where p.IsCurrent orderby p.DateOfAdd descending select p;
                    foreach (MaterialPrice price in oldPrices)
                    {
                        if (!price.IsCurrent)
                        {
                            html +=
                                "<td style=\"padding-left : 10px;\">" +
                                    "<span class=\"LineThrough\"><span class=\"OldPrice\">" + price.ToString() + "</span></span>" +
                                "</td>";
                        }
                    }
                    MaterialPrice lastprice = newPrices.Last();
                    html +=
                        "<td style=\"padding-left : 10px;\">" +
                            "<a class=\"Link Bold\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Type=" + TypeID.ToString() + "&Field=price" + "&Value=" + lastprice.Price.ToString() + "&PriceUnit=" + lastprice.Unit.ToString() + "\">" + lastprice.ToString() + "</a>" +
                        "</td>";
                    html += "</tr></table>";
                }
                else
                {
                    html += "<span style=\"color : #a7a9ac; font-weight : bold;\">قیمت ثبت نشده است</span>";
                }
                return html;
            }
        }
        public string PriceHtml_Current
        {
            get
            {
                string html = "";
                if (Prices.Count > 0)
                {
                    var newPrices = from p in Prices where p.IsCurrent orderby p.DateOfAdd descending select p;
                    MaterialPrice lastPrice = newPrices.Last();
                    html = "<a class=\"Link Bold\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Type=" + TypeID.ToString() + "&Field=price" + "&Value=" + lastPrice.Price.ToString() + "&PriceUnit=" + lastPrice.Unit.ToString() + "\">" + lastPrice.ToString() + "</a>";
                }
                else
                {
                    html += "<span style=\"color : #a7a9ac;\">قیمت ثبت نشده است</span>";
                }
                return html;
            }
        }
        public string PriceHtml_Current_Thick
        {
            get
            {
                string html = "";
                if (Prices.Count > 0)
                {
                    var newPrices = from p in Prices where p.IsCurrent orderby p.DateOfAdd descending select p;
                    MaterialPrice lastPrice = newPrices.Last();
                    html = "<a class=\"Link\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Type=" + TypeID.ToString() + "&Field=price" + "&Value=" + lastPrice.Price.ToString() + "&PriceUnit=" + lastPrice.Unit.ToString() + "\">" + lastPrice.ToString() + "</a>";
                }
                else
                {
                    html += "<span style=\"color : #a7a9ac;\">قیمت ثبت نشده است</span>";
                }
                return html;
            }
        }
        public string PriceHtml_Vertical
        {
            get
            {
                string html = "";
                if (Prices.Count > 0)
                {
                    html = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
                    var oldPrices = from p in Prices where !p.IsCurrent orderby p.DateOfAdd descending select p;
                    var newPrices = from p in Prices where p.IsCurrent orderby p.DateOfAdd descending select p;
                    foreach (MaterialPrice price in oldPrices)
                    {
                        if (!price.IsCurrent)
                        {
                            html +=
                                "<tr><td align=\"center\" style=\"padding-top : 5px;\">" +
                                    "<span style=\"text-decoration : line-through; color : #b11116;\"><span style=\"color : #939598; font-weight : normal; font-size : 10px;\">" + price.ToString() + "</span></span>" +
                                "</td></tr>";
                        }
                    }
                    MaterialPrice lastprice = newPrices.Last();
                    html +=
                        "<tr><td align=\"center\" style=\"padding-top : 5px;\">" +
                            "<a class=\"Link Bold\" href=\"" + Repository.ToHtmlUrl("~/Views/RetrieveMaterials.aspx") + "?Type=" + TypeID.ToString() + "&Field=price" + "&Value=" + lastprice.Price.ToString() + "&PriceUnit=" + lastprice.Unit.ToString() + "\">" + lastprice.ToString() + "</a>" +
                        "</td></tr>";
                    html += "</table>";
                }
                else
                {
                    html += "<span style=\"color : #a7a9ac; font-weight : normal;\">قیمت ثبت نشده است</span>";
                }
                return html;
            }
        }
        public MaterialPrice CurrentPrice
        {
            get
            {
                MaterialPrice cPrice = MaterialPrice.Zero;
                if (Prices.Where(p => p.IsCurrent).Count() > 0)
                {
                    cPrice = (from price in Prices
                              where price.IsCurrent
                              select price).Last();
                }
                return cPrice;
            }
        }
        public string VotesHtml
        {
            get
            {
                MaterialsRepository mr = new MaterialsRepository();
                string html = "";
                html = 
                    "<div class=\"Likes_Back\">" +
                        mr.GetVotesAverage(MaterialID).ToString() +
                    "</div>";
                return html;
            }
        }
        public VotePercent VotesPercent
        {
            get
            {
                MaterialsRepository mr = new MaterialsRepository();
                int positiveCount = mr.GetPositiveVotesCount(MaterialID);
                int negativeCount = mr.GetNegativeVotesCount(MaterialID);
                int all = positiveCount + negativeCount;
                int positivePercent = (positiveCount != 0 ? (int)((positiveCount * 100) / all) : 0);
                int negativePercent = (negativeCount != 0 ? 100 - positivePercent : 0);
                VotePercent percent = new VotePercent(positivePercent, negativePercent);
                return percent;
            }
        }
        public string FieldsHtml()
        {
            // field rows :
            string html = "";
            if (FieldValues.Count > 0)
            {
                html = "<table class=\"CompareTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">";
                for (int i = 0; i < FieldValues.Count; i++)
                {
                    MaterialTypeFieldValue fieldValue = FieldValues[i];
                    html +=
                        "<tr><td align=\"left\" class=\"CompareTable_RightHeaderTD\" style=\"width : 140px; " + (i == FieldValues.Count - 1 ? "border-bottom : none;" : "") + "\">" +
                                fieldValue.Field.Name + " :" +
                            "</td>";
                    html +=
                        "<td align=\"right\" class=\"CompareTable_ValueCell\" style=\"border-left : none; " + (i == FieldValues.Count - 1 ? "border-bottom : none;" : "") + "\">";
                    html += fieldValue.GetValueLink(fieldValue.Field.Unit);
                    html += "</td>";
                    html += "</tr>";
                }
                html += "</table>";
            }
            return html;
        }
        public string BuyersHtml
        {
            get
            {
                string html = "";
                MaterialsRepository mr = new MaterialsRepository();
                var buyers = Buyers;
                html =
                    "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                        "<tr>";
                int i = 0;
                foreach (Member member in buyers)
                {
                    html +=
                        (i > 0 ? "<td style=\"padding-left : 7px;\">-</td>" : "") +
                        "<td style=\"padding-left : 7px;\">" +
                            "<a class=\"Link Bold\" onclick=\"return OpenCenter('" + Repository.ToHtmlUrl("~/Views/Members/SendMessage.aspx") + "?MemberID=" + member.MemberID.ToString() + "&Name=" + member.FullName + "', 'SendMessage', 500, 300);\">" + member.FullName + "</a>" +
                        "</td>";
                    i++;
                }
                html += "</tr></table>";
                return html;
            }
        }
        public IEnumerable<Member> Buyers
        {
            get
            {
                int _BuyersCount = 5;
                MaterialsRepository mr = new MaterialsRepository();
                return mr.GetMaterialBuyers(MaterialID, _BuyersCount);
            }
        }
        public MaterialPrice PurchasePrice_Object
        {
            get
            {
                MaterialPrice pp = new MaterialPrice(PurchasePrice, (PriceUnit)PurchasePriceUnit);
                return pp;
            }
        }
    }
}
