using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for City
/// </summary>
namespace Models
{
    public partial class City
    {
        public string FullName
        {
            get
            {
                return State.StateName + " - " + CityName;
            }
        }
    }
}
