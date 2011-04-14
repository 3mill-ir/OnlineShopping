using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using Models;

/// <summary>
/// Summary description for Set
/// </summary>
namespace Models
{
    public partial class Set
    {
        public string MenuItem
        {
            get
            {
                string itemId = "item_" + SetID.ToString();
                string menuItem = "";
                menuItem =
                    "<a id=\"" + itemId + "\" class=\"SubMenuItem\" href=\"" + Repository.ToHtmlUrl("~/Views/SetDetails.aspx") + "?SetID=" + SetID.ToString() + "\">" +
                        Name + " ( " + Materials.Count.ToString() + " )" +
                    "</a>";
                return menuItem;
            }
        }
        public IEnumerable<Material> GetPopularMaterials(int Count)
        {
            var populars = from material in Materials
                           where material.Visible
                           orderby material.VotesAverage_Real descending
                           select material;
            return populars.Take(Count);
        }
        public IEnumerable<Material> LastAddedMaterials
        {
            get
            {
                var query = from material in Materials
                            where material.Visible
                            orderby material.DateOfAdd descending
                            select material;
                return query;
            }
        }
        public IEnumerable<Material> VisibleMaterials
        {
            get
            {
                var query = from material in Materials
                            where material.Visible
                            orderby material.DateOfAdd descending
                            select material;
                return query;
            }
        }
        public IEnumerable<Material> GetSortedMaterials(SortField sortField, SortType sortType)
        {
            MaterialsRepository mr = new MaterialsRepository();
            IEnumerable<Material> materials = mr.Sort(Materials, sortField, sortType);
            return materials;
        }
        public string Meta_Description
        {
            get
            {
                string meta = Category.Name + " , " + Name;
                var names = (from material in Materials select material.Name).Distinct();
                foreach (string name in names)
                {
                    meta += " , " + name;
                }
                return meta;
            }
        }
        public string AllMaterialsLinks
        {
            get
            {
                string links = "";
                int i = 0;
                foreach (Material material in Materials)
                {
                    links += material.NameLink_Thick + ( i < Materials.Count() - 1 ? " - " : "" );
                    i++;
                }
                return links;
            }
        }
    }
}
