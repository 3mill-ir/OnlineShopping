using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_DateSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public ShamsiDateTime SelectedDate_Shamsi
    {
        get
        {
            int day = (cmb_Day.SelectedIndex > 0 ? Convert.ToInt32(cmb_Day.SelectedItem.Value) : 1);
            int month = (cmb_Month.SelectedIndex > 0 ? Convert.ToInt32(cmb_Month.SelectedItem.Value) : 1);
            int year = (cmb_Year.SelectedIndex > 0 ? Convert.ToInt32(cmb_Year.SelectedItem.Value) : 1300);
            return new ShamsiDateTime(year, month, day);
        }
    }
    public DateTime SelectedDate_Miladi
    {
        get
        {
            int day = (cmb_Day.SelectedIndex > 0 ? Convert.ToInt32(cmb_Day.SelectedItem.Value) : 1);
            int month = (cmb_Month.SelectedIndex > 0 ? Convert.ToInt32(cmb_Month.SelectedItem.Value) : 1);
            int year = (cmb_Year.SelectedIndex > 0 ? Convert.ToInt32(cmb_Year.SelectedItem.Value) : 1300);
            return new ShamsiDateTime(year, month, day).MiladyDate;
        }
        set
        {
            ShamsiDateTime shamsi = ShamsiDateTime.MiladyToShamsi(value);
            cmb_Day.SelectedIndex = cmb_Day.Items.IndexOf(new ListItem(shamsi.Day.ToString(), shamsi.Day.ToString()));
            cmb_Month.SelectedIndex = cmb_Month.Items.IndexOf(new ListItem(shamsi.MonthName, shamsi.Month.ToString()));
            cmb_Year.SelectedIndex = cmb_Year.Items.IndexOf(new ListItem(shamsi.Year.ToString(), shamsi.Year.ToString()));
        }
    }
    public bool EnableValidation { get; set; }
}
