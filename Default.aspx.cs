using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    //**************************
    private const int _PopularsCount = 15;
    private const int _TickBoxWidth = 895;
    private const int _NewMaterialsCount = 5; 
    private const int _RandomMaterialsCount = 6;
    private const int _TagsMaxCount = 600;
    //**************************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //slider :
            SlidingsRepository sr = new SlidingsRepository();
            var slidings = sr.GetSlidingImages();
            HasSlidingImages = (slidings.Count() >= 0 ? true : false);
            int i = 0;
            foreach (SlidingImage img in slidings)
            {
                SlidingImages_Html += img.GetHtml(i);
                Bullets_Html += SlidingImage.Bullet;
                i++;
            }
            //random materials :
            MaterialsRepository mr = new MaterialsRepository();
            var randomMaterials = mr.GetRandomMaterials(_RandomMaterialsCount);
            FillRandomDataList(randomMaterials);
            //new materials :
            FillNewsDataList(mr.GetLastMaterials(_NewMaterialsCount));
            //popular materials :
            TickBox tickbox = new TickBox(mr.GetPopularMaterials(_PopularsCount), _TickBoxWidth, 91);
            lbl_Populars.Text = tickbox.Html;
            //Tags
            var tags = mr.GetTags(_TagsMaxCount);
            int tagCounter = 0;
            foreach (Tag tag in tags)
            {
                var sortedTags = tags.OrderBy(t => t.Repetition);
                lbl_Tags.Text += tag.GetLink(sortedTags.First().Repetition, sortedTags.Last().Repetition) + (tagCounter < tags.Count() - 1 ? " - " : "");
                tagCounter++;
            }
            //suggestion :
            if (User.IsInRole(MyRoles.Member))
            {
                MembersRepository memRep = new MembersRepository();
                Member member = memRep.GetMember(User.Identity.Name);
                var fm = member.GetFavoriteMaterials(10);
                TickBox tickbox_Suggestion = new TickBox(fm, _TickBoxWidth, 91);
                lbl_Suggestions.Text = tickbox_Suggestion.Html;
                pnl_Favorites.Visible = fm.Count() > 0 ? true : false;
            }
        }
    }
    protected string SlidingImages_Html { get; set; }
    protected string Bullets_Html { get; set; }
    private void FillRandomDataList(IEnumerable<Material> materials)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Info", typeof(String));
        foreach (Material m in materials)
        {
            DataRow row = dt.NewRow();
            row["Info"] = m.Info_Vertical(ResizeType.WidthFix, 100);
            dt.Rows.Add(row);
        }
        DataList_Random.DataSource = dt;
        DataList_Random.DataBind();
    }
    private void FillNewsDataList(IEnumerable<Material> materials)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Info", typeof(String));
        foreach (Material m in materials)
        {
            DataRow row = dt.NewRow();
            row["Info"] = m.Info_Vertical(ResizeType.WidthFix, 110);
            dt.Rows.Add(row);
        }
        DataList_News.DataSource = dt;
        DataList_News.DataBind();
    }
    protected bool HasSlidingImages { get; set; }
    protected string GetMenu()
    {
        string menu = "";
        MaterialsRepository or = new MaterialsRepository();
        menu = or.GetAllCategoriesMenu();
        return menu;
    }
    protected string GetBullets()
    {
        string bullets = "";
        return bullets;
    }
}
