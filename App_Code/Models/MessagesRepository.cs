using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
/// <summary>
/// Summary description for MessagesRepository
/// </summary>
public class MessagesRepository
{
    //**************************
    MyDatabaseDataContext db = new MyDatabaseDataContext(Repository.ConnectionString);
    //**************************
    //******************************************** ADD Methods :
    public void AddToInbox(InboxMessage message)
    {
        db.InboxMessages.InsertOnSubmit(message);
    }
    public void AddToSentBox(SentBoxMessage message)
    {
        db.SentBoxMessages.InsertOnSubmit(message);
    }
    //******************************************** GET Methods :
    public IEnumerable<InboxMessage> GetInboxMessages(int memberId)
    {
        return db.InboxMessages.Where(mes => mes.MemberID == memberId).OrderByDescending(mes => mes.DateOfRecieve);
    }
    public IEnumerable<SentBoxMessage> GetSentBoxMessages(int memberId)
    {
        return db.SentBoxMessages.Where(mes => mes.MemberID == memberId).OrderByDescending(mes => mes.DateOfSend);
    }
    public InboxMessage GetInboxMessage(int inboxId)
    {
        try
        {
            return db.InboxMessages.Single(mes => mes.InboxID == inboxId);
        }
        catch
        {
            return null;
        }
    }
    public SentBoxMessage GetSentBoxMessage(int sentboxId)
    {
        try
        {
            return db.SentBoxMessages.Single(mes => mes.SentBoxID == sentboxId);
        }
        catch
        {
            return null;
        }
    }
    //******************************************** Delete Methods :
    public void DeleteInboxMessage(int InboxID)
    {
        db.InboxMessages.DeleteAllOnSubmit(db.InboxMessages.Where(mes => mes.InboxID == InboxID));
    }
    public void DeleteSentBoxMessage(int SentBoxID)
    {
        db.SentBoxMessages.DeleteAllOnSubmit(db.SentBoxMessages.Where(mes => mes.SentBoxID == SentBoxID));
    }
    //******************************************** Submit
    public void Save()
    {
        db.SubmitChanges();
    }
}
