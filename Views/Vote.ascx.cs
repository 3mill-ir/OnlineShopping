using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Vote : System.Web.UI.UserControl
{
    //**************************
    public event EventHandler YesClicked;
    public event EventHandler NoClicked;
    protected void Btn_Yes_Clicked(object sender, EventArgs e)
    {
        //
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            MaterialsRepository mr = new MaterialsRepository();
            int materialId = AssociatedMaterialID;
            bool hasVoted = mr.HasVoted(HttpContext.Current.User.Identity.Name, materialId);
            HasVoted = hasVoted;
            if (!hasVoted || HttpContext.Current.User.IsInRole(MyRoles.Admin))
            {
                MaterialVote vote = new MaterialVote();
                vote.MaterialID = materialId;
                vote.Vote = true;
                vote.VoterType = (int)VoterType.Member;
                vote.Voter = HttpContext.Current.User.Identity.Name;
                vote.DateOfVote = Repository.Now;
                mr.AddVote(vote);
                mr.Save();
                //
                lbl_Counter.Text = mr.GetVotesAverage(materialId).ToString();
            }
            else
            {
                Repository.MessageBoxShow("رأی شما برای این کالا قبلاً ثبت شده است", Page, this.GetType());
            }
        }
        else
        {
            Repository.MessageBoxShow("تنها اعضای سایت قادر به رأی دادن به کالا میباشند", Page, this.GetType());
        }
        //fire event :
        if (YesClicked != null)
            YesClicked(this, EventArgs.Empty);
    }
    public bool HasVoted { get; private set; }
    protected void Btn_No_Clicked(object sender, EventArgs e)
    {
        //fire event :
        if (NoClicked != null)
            NoClicked(this, EventArgs.Empty);
        //
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            MaterialsRepository mr = new MaterialsRepository();
            int materialId = AssociatedMaterialID;
            bool hasVoted = mr.HasVoted(HttpContext.Current.User.Identity.Name, materialId);
            if (!hasVoted || HttpContext.Current.User.IsInRole(MyRoles.Admin))
            {
                MaterialVote vote = new MaterialVote();
                vote.MaterialID = materialId;
                vote.Vote = false;
                vote.VoterType = (int)VoterType.Member;
                vote.Voter = HttpContext.Current.User.Identity.Name;
                vote.DateOfVote = Repository.Now;
                mr.AddVote(vote);
                mr.Save();
                //
                lbl_Counter.Text = mr.GetVotesAverage(materialId).ToString();
            }
            else
            {
                Repository.MessageBoxShow("رأی شما برای این کالا قبلاً ثبت شده است", Page, this.GetType());
            }
        }
        else
        {
            Repository.MessageBoxShow("تنها اعضای سایت قادر به رأی دادن به کالا میباشند", Page, this.GetType());
        }
    }
    public int AssociatedMaterialID
    {
        get
        {
            return Convert.ToInt32(ViewState["Vote_MaterialID"]);
        }
        set
        {
            ViewState["Vote_MaterialID"] = value;
            MaterialsRepository mr = new MaterialsRepository();
            lbl_Counter.Text = mr.GetVotesAverage(value).ToString();
        }
    }
    public string AssociatedUpdatePanelID
    {
        set
        {
            progress.AssociatedUpdatePanelID = value;
        }
    }
    public bool IsInUpdatePanel { get; set; }
}
