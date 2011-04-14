<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberStatistics.aspx.cs" Inherits="Views_Admin_MemberStatistics" %>
<%@ Register Src="~/Views/PageTitle.ascx" TagPrefix="Myc" TagName="PageTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #form1 a:hover { text-decoration : underline; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding : 15px;">
        <div style="margin-bottom : 10px;"><Myc:PageTitle runat="server" Text="آمـار و اطلاعـات کاربـر"/></div>
        <% MembersRepository mr = new MembersRepository(); %>
        <% Models.Member member = mr.GetMember(Convert.ToInt32(Request.QueryString["MemberID"])); %>
        <%= member.GetStatistics(HtmlType.ForAdmin).ToHtml() %>
    </div>
    </form>
</body>
</html>
