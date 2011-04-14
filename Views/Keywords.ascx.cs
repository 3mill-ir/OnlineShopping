using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Views_Keywords : System.Web.UI.UserControl
{
    //***************
    const string _Splitter = ";";
    Unit defaultItemWidth = new Unit("200px");
    int maxLength = 25;
    //***************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
    protected void AddButton_Click(object sender, EventArgs e)
    {
        if (tb_Keyword.Text.Replace(_Splitter, "").Trim().Length > 0)
        {
            //retrieve list :
            List<Hashtable> keywords;
            if (ViewState[ID] != null)
            {
                keywords = (List<Hashtable>)ViewState[ID];
            }
            else
            {
                keywords = new List<Hashtable>();
            }
            Hashtable keyword = new Hashtable();
            keyword["ID"] = Repository.GetRandomString(5, true);
            keyword["Keyword"] = tb_Keyword.Text.Replace(_Splitter, " ");
            keywords.Add(keyword);
            ViewState[ID] = keywords;
            //make data table:
            FillKeywordsList(keywords);
            /////////
            tb_Keyword.Text = "";
        }
    }
    private void FillKeywordsList(List<Hashtable> keywords)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(String));
        dt.Columns.Add("Keyword", typeof(String));
        dt.Columns.Add("ItemWidth", typeof(Unit));
        dt.Columns.Add("MaxLength", typeof(Int32));
        foreach (Hashtable word in keywords)
        {
            DataRow row = dt.NewRow();
            row["ID"] = word["ID"];
            row["Keyword"] = word["Keyword"].ToString();
            row["ItemWidth"] = ItemWidth;
            row["MaxLength"] = MaxLength;
            dt.Rows.Add(row);
        }
        //
        list_Keywords.DataSource = dt;
        list_Keywords.DataBind();
    }
    public List<string> Keywords
    {
        get
        {
            List<Hashtable> keywords;
            if (ViewState[ID] != null)
            {
                keywords = (List<Hashtable>)ViewState[ID];
            }
            else
            {
                keywords = new List<Hashtable>();
            }
            //
            List<string> list = new List<string>();
            foreach (Hashtable hash in keywords)
            {
                list.Add(hash["Keyword"].ToString());
            }
            return list;
        }
        set
        {
            List<string> words = value;
            List<Hashtable> keywords = new List<Hashtable>();
            foreach (string word in words)
            {
                Hashtable hash = new Hashtable();
                hash["ID"] = Repository.GetRandomString(5, true);
                hash["Keyword"] = word;
                keywords.Add(hash);
            }
            //////
            ViewState[ID] = keywords;
            FillKeywordsList(keywords);
        }
    }
    public int RepeatColumns
    {
        get
        {
            return list_Keywords.RepeatColumns;
        }
        set
        {
            list_Keywords.RepeatColumns = value;
        }
    }
    protected void Datalist_Keywords_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteKeyword")
        {
            try
            {
                List<string> args = Repository.SplitString(e.CommandArgument.ToString(), ";");
                string id = args[0];
                string keyword = args[1];
                //
                List<Hashtable> keywords;
                if (ViewState[ID] != null)
                {
                    keywords = (List<Hashtable>)ViewState[ID];
                }
                else
                {
                    keywords = new List<Hashtable>();
                }
                //remove item from list :
                int index = -1;
                for(int i = 0; i < keywords.Count; i++)
                {
                    Hashtable word = keywords[i];
                    if ((word["ID"].ToString() == id) && (word["Keyword"].ToString() == keyword))
                        index = i;
                }
                keywords.RemoveAt(index);
                //
                ViewState[ID] = keywords;
                FillKeywordsList(keywords);
            }
            catch(Exception ex)
            {
                Repository.MessageBoxShow(ex.Message, Up_KeywordsControl, this.GetType());
            }
        }
    }
    public string KeywordsString
    {
        get
        {
            string keys = "";
            for (int i = 0; i < Keywords.Count; i++) 
            {
                keys += Keywords[i] + (i < Keywords.Count - 1 ? ";" : "");
            }
            return keys;
        }
        set
        {
            List<string> keys = Repository.SplitString(value, _Splitter);
            Keywords = keys;
        }
    }
    public Unit TextBoxWidth
    {
        get
        {
            return tb_Keyword.Width;
        }
        set
        {
            tb_Keyword.Width = value;
        }
    }
    public string Statements
    {
        get
        {
            return lbl_Statements.Text;
        }
        set
        {
            lbl_Statements.Text = value;
        }
    }
    public Unit ItemWidth
    {
        get
        {
            return defaultItemWidth;
        }
        set
        {
            defaultItemWidth = value;
        }
    }
    public int MaxLength
    {
        get
        {
            return maxLength;
        }
        set
        {
            maxLength = value;
        }
    }
    public string ID { get; set; }
    public string Text
    {
        get
        {
            return lbl_Text.Text;
        }
        set
        {
            lbl_Text.Text = value;
        }
    }
}
