using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Members_SendMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hidden_RecieverID.Value = Request.QueryString["MemberID"].ToString();
        }
    }
    protected void btn_Send_Clicked(object sender, EventArgs e)
    {
        MembersRepository mr = new MembersRepository();
        int memberId = mr.GetMemberID(User.Identity.Name);
        int recieverId = Convert.ToInt32(hidden_RecieverID.Value);
        if (memberId != recieverId)
        {
            //Save to SentBox :
            SentBoxMessage sentMessage = new SentBoxMessage();
            sentMessage.MemberID = memberId;
            sentMessage.RecieverID = recieverId;
            sentMessage.Subject = tb_Subject.Text;
            sentMessage.Text = tb_Text.Text;
            sentMessage.DateOfSend = Repository.Now;
            //Save to InboxMessage
            InboxMessage inboxMessage = new InboxMessage();
            inboxMessage.MemberID = Convert.ToInt32(hidden_RecieverID.Value);
            inboxMessage.SenderID = memberId;
            inboxMessage.Subject = tb_Subject.Text;
            inboxMessage.Text = tb_Text.Text;
            inboxMessage.DateOfRecieve = Repository.Now;
            inboxMessage.IsRead = false;
            //
            MessagesRepository mesRep = new MessagesRepository();
            mesRep.AddToInbox(inboxMessage);
            mesRep.AddToSentBox(sentMessage);
            //Save :
            mesRep.Save();
            Repository.MessageBoxShow("پیام شما با موفقیت ارسال شد", Up_Send, this.GetType());
        }
        else
        {
            Repository.MessageBoxShow("نمی توانید برای خودتان پیام ارسال کنید", Up_Send, this.GetType());
        }
    }
}
