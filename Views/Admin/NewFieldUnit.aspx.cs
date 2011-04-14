﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Views_Admin_NewMaterialType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Save_Clicked(object sender, EventArgs e)
    {
        FieldUnit unit = new FieldUnit();
        unit.Name = tb_Name.Text;
        //save :
        MaterialsRepository mr = new MaterialsRepository();
        mr.AddFieldUnit(unit);
        mr.Save();
        //
        Repository.MessageBoxShow("بـا مـوفقیت ایجـاد شد", up_Save, this.GetType());
    }
}
