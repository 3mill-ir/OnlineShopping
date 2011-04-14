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
/// Summary description for State
/// </summary>
namespace Models
{
    public partial class State
    {
        public IEnumerable<City> OrderedCities
        {
            get
            {
                var cities = from city in Cities
                             orderby city.CityName
                             select city;
                return cities;
            }
        }
    }
}