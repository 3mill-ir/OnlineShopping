using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialTypeField
/// </summary>
namespace Models
{
    public partial class MaterialTypeField
    {
        public MaterialTypeFieldValue ValueOf(int MaterialID)
        {
            var query = from val in Values
                        where val.MaterialID == MaterialID
                        select val;
            try
            {
                return query.Single();
            }
            catch
            {
                return null;
            }
        }
    }
}
