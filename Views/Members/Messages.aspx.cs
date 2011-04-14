using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;

public partial class Views_Members_Messages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MembersRepository memRep = new MembersRepository();
            int memberId = memRep.GetMemberID(User.Identity.Name);
            hidden_MemberID.Value = memberId.ToString();
            if (Request.QueryString["show"].ToString() == "inbox")
            {
                hidden_ShowType.Value = "inbox";
                pageTitle.Text = "صنـدوق ورودی";
                MessagesRepository mr = new MessagesRepository();
                IEnumerable<InboxMessage> inboxMessages = mr.GetInboxMessages(memberId);
                FillGridMessages(inboxMessages);
                //
                if (Request.QueryString["id"] != null)
                {
                    int inboxId = Convert.ToInt32(Request.QueryString["id"]);
                    InboxMessage message = mr.GetInboxMessage(inboxId);
                    message.IsRead = true;
                    mr.Save();
                    lbl_Details.Text = message.DetailsHtml;
                    pnl_Details.Visible = true;
                }
            }
            else
            {
                hidden_ShowType.Value = "sentbox";
                pageTitle.Text = "موارد ارسـال شده";
                MessagesRepository mr = new MessagesRepository();
                IEnumerable<SentBoxMessage> sentboxMessages = mr.GetSentBoxMessages(memberId);
                FillGridMessages(sentboxMessages);
                //
                if (Request.QueryString["id"] != null)
                {
                    int sentboxId = Convert.ToInt32(Request.QueryString["id"]);
                    SentBoxMessage message = mr.GetSentBoxMessage(sentboxId);
                    lbl_Details.Text = message.DetailsHtml;
                    pnl_Details.Visible = true;
                }
            }
        }
    }
    private void FillGridMessages(IEnumerable<InboxMessage> messages)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Subject", typeof(String));
        dt.Columns.Add("ToFrom", typeof(String));
        dt.Columns.Add("DateOfX", typeof(String));
        dt.Columns.Add("ID", typeof(Int32));
        foreach (InboxMessage mes in messages)
        {
            DataRow row = dt.NewRow();
            row["ID"] = mes.InboxID;
            row["Subject"] = mes.Subject_Link;
            row["ToFrom"] = mes.Sender_Link;
            row["DateOfX"] = ShamsiDateTime.MiladyToShamsi(mes.DateOfRecieve).ToLongString();
            dt.Rows.Add(row);
        }
        GridMessages.DataSource = dt;
        GridMessages.DataBind();
    }
    private void FillGridMessages(IEnumerable<SentBoxMessage> messages)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Subject", typeof(String));
        dt.Columns.Add("ToFrom", typeof(String));
        dt.Columns.Add("DateOfX", typeof(String));
        dt.Columns.Add("ID", typeof(Int32));
        foreach (SentBoxMessage mes in messages)
        {
            DataRow row = dt.NewRow();
            row["ID"] = mes.SentBoxID;
            row["Subject"] = mes.Subject_Link;
            row["ToFrom"] = mes.Reciever_Link;
            row["DateOfX"] = ShamsiDateTime.MiladyToShamsi(mes.DateOfSend).ToLongString();
            dt.Rows.Add(row);
        }
        GridMessages.DataSource = dt;
        GridMessages.DataBind();
    }
    protected void GridMessages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteMessage")
        {
            if (hidden_ShowType.Value == "inbox")
            {
                int InboxID = Convert.ToInt32(e.CommandArgument);
                MessagesRepository mr = new MessagesRepository();
                mr.DeleteInboxMessage(InboxID);
                mr.Save();
                //
                Response.Redirect("~/Views/Members/Messages.aspx?show=inbox");
            }
            else if (hidden_ShowType.Value == "sentbox")
            {
                int sentBoxID = Convert.ToInt32(e.CommandArgument);
                MessagesRepository mr = new MessagesRepository();
                mr.DeleteSentBoxMessage(sentBoxID);
                mr.Save();
                //
                Response.Redirect("~/Views/Members/Messages.aspx?show=sentbox");
            }
        }
    }
}
