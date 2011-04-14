using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for FieldsRepository
/// </summary>
public class FieldsRepository
{
    public string ToString(FieldType fieldType)
    {
        string res = "";
        if (fieldType == FieldType.None)
        {
            res = "";
        }
        else if (fieldType == FieldType.YesNo)
        {
            res = "بولی";
        }
        else if (fieldType == FieldType.Text)
        {
            res = "متـنی";
        }
        else if (fieldType == FieldType.Digital)
        {
            res = "عددی";
        }
        return res;
    }
    public List<string> FieldTypes_ListOfStrings()
    {
        //ترتیب باید رعایت بشه :
        return new List<string>(new string[] { "بـولی", "متنی", "عددی"});
    }
    public List<FieldType> FieldTypes_ListOfFieldType()
    {
        //ترتیب باید رعایت بشه :
        return new List<FieldType>(new FieldType[] { FieldType.YesNo, FieldType.Text, FieldType.Digital});
    }
}
public enum FieldType
{
    None = 0,
    YesNo = 1,
    Text = 2,
    Digital = 3
}