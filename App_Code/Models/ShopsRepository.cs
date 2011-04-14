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
using Models;

/// <summary>
/// Summary description for ShopsRepository
/// </summary>
public class ShopsRepository
{
    //**********************************
    MyDatabaseDataContext db = new MyDatabaseDataContext(Repository.ConnectionString);
    //********************************** GET METHODS :
    public int ShopsCount
    {
        get
        {
            return db.Shops.Count();
        }
    }
    public Shop GetShop(int shopId)
    {
        try
        {
            return db.Shops.Single(sh => sh.ShopID == shopId);
        }
        catch
        {
            return null;
        }
    }
    public IEnumerable<Shop> GetShops()
    {
        return db.Shops.OrderBy(shop => shop.Name);
    }
    public IEnumerable<Shop> FindShop(string ShopName)
    {
        IEnumerable<Shop> query = from shop in db.Shops
                                  where shop.Name.Contains(ShopName)
                                  select shop;
        return query;
    }
    //********************************** Add Methods :
    public void AddShop(Shop shop)
    {
        db.Shops.InsertOnSubmit(shop);
    }
    //********************************** Delete Methods :
    public void DeleteShop(int shopId)
    {
        db.Shops.DeleteAllOnSubmit(db.Shops.Where(shop => shop.ShopID == shopId));
    }
    //***** Save ;
    public void Save()
    {
        db.SubmitChanges();
    }
}
