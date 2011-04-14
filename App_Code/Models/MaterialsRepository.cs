using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Web.UI.WebControls;
using System.Data.Linq;
using MyExtensionMethods;
using System.Data.Linq.SqlClient;

/// <summary>
/// Summary description for ObjectsRepository
/// </summary>
public class MaterialsRepository
{
    //********************************
    MyDatabaseDataContext db = new MyDatabaseDataContext(Repository.ConnectionString);
    //********************************
    //********************************************* Add Methods :
    public void AddCategory(Category category)
    {
        db.Categories.InsertOnSubmit(category);
    }
    public void AddSet(Set set)
    {
        db.Sets.InsertOnSubmit(set);
    }
    public void AddVote(MaterialVote vote)
    {
        db.MaterialVotes.InsertOnSubmit(vote);
    }
    public void AddMaterialType(MaterialType type)
    {
        db.MaterialTypes.InsertOnSubmit(type);
    }
    public void AddMaterialTypeField(MaterialTypeField field)
    {
        db.MaterialTypeFields.InsertOnSubmit(field);
    }
    public void AddFieldUnit(FieldUnit unit)
    {
        db.FieldUnits.InsertOnSubmit(unit);
    }
    public void AddMaterialPrice(MaterialPrice price)
    {
        if (price.IsCurrent)
        {
            var otherPrices = GetMaterialPrices(price.MaterialID);
            foreach (MaterialPrice otherprice in otherPrices)
                otherprice.IsCurrent = false;
        }
        db.MaterialPrices.InsertOnSubmit(price);
    }
    //********************************************* GET METHODS :
    public IEnumerable<Tag> GetTags(int Count)
    {
        var query = from keyword in db.MaterialKeywords
                    where (KeywordType)keyword.Type == KeywordType.Tag
                    orderby keyword.Material.DateOfAdd descending
                    group keyword by keyword.Word into kg
                    select new Tag(kg.Key, kg.Count());
        return query.Take(Count);
    }
    public string GetAllCategories_MetaDescription()
    {
        string meta = "";
        var cats = GetAllCategories();
        int i = 0;
        foreach (Category cat in cats)
        {
            meta += cat.Meta_Description + (i < cats.Count() - 1 ? " , " : "");
            i++;
        }
        return meta;
    }
    public IEnumerable<Category> GetAllCategories()
    {
        var cats = from cat in db.Categories
                   orderby cat.Sequence
                   select cat;
        return cats;

    }
    public IEnumerable<Material> GetMaterials(int Count)
    {
        var mats = from mat in db.Materials
                   where mat.Visible
                   orderby mat.DateOfAdd descending
                   select mat;
        return mats.Take(Count);
    }
    public IEnumerable<Material> GetRandomMaterials(int Count)
    {
        IEnumerable<Material> Remaining = GetMaterials(200);
        if (Remaining.Count() > Count * 2)
        {
            Random gen = new Random((int)DateTime.Now.Ticks);
            List<int> generated = new List<int>();
            while (Count >= 1)
            {
                int randomNumber = gen.Next(1, Remaining.Count());
                if (!generated.Contains(randomNumber))
                {
                    generated.Add(randomNumber);
                    Material temp = Remaining.Skip(randomNumber - 1).Take(1).Single();
                    Count--;
                    yield return temp;
                }
            }
        }
        else
        {
            foreach(Material material in Remaining.Take(Count))
            {
                yield return material;
            }
        }
    }
    public string GetAllCategoriesMenu()
    {
        string menu = "";
        var categories = GetAllCategories();
        foreach(Category cat in categories)
        {
            menu += cat.MenuItem;
        }
        return menu;
    }
    public IEnumerable<Set> GetCategorySets(int CategoryID)
    {
        var sets = from set in db.Sets
                   where set.CategoryID == CategoryID
                   orderby set.Sequence
                   select set;
        return sets;
    }
    public Material GetMaterial(int materialId)
    {
        try
        {
            return db.Materials.Single(mat => mat.MaterialID == materialId);
        }
        catch
        {
            return null;
        }
    }
    public IEnumerable<Material> GetSetMaterials(int setId)
    {
        IEnumerable<Material> materials = from mat in db.Materials 
                                          where mat.SetID == setId && mat.Visible
                                          orderby mat.DateOfAdd descending 
                                          select mat;
        return materials;
    }
    public IEnumerable<Material> GetSetAllMaterials(int setId)
    {
        IEnumerable<Material> materials = from mat in db.Materials
                                          where mat.SetID == setId
                                          orderby mat.DateOfAdd descending
                                          select mat;
        return materials;
    }
    public Set GetSet(int setId)
    {
        try
        {
            return db.Sets.Single(set => set.SetID == setId);
        }
        catch
        {
            return null;
        }
    }
    public Category GetCategory(int CategoryId)
    {
        try
        {
            return db.Categories.Single(cat => cat.CategoryID == CategoryId);
        }
        catch
        {
            return null;
        }
    }
    public MaterialPicture GetPicture(int picId)
    {
        try
        {
            return db.MaterialPictures.Single(p => p.PictureID == picId);
        }
        catch
        {
            return null;
        }
    }
    public double GetCategoriesLastSquence()
    {
        var query = from cat in db.Categories
                    select cat.Sequence;
        return (query.Count() > 0 ? query.Max() : 0);
    }
    public double GetSetsLastSquence()
    {
        var query = from set in db.Sets
                    select set.Sequence;
        return (query.Count() > 0 ? query.Max() : 0);
    }
    public int GetPositiveVotesCount(int materialId)
    {
        var query = from vote in db.MaterialVotes
                    where ((vote.MaterialID == materialId) && (vote.Vote == true))
                    select vote.VoteID;
        return query.Count();
    }
    public int GetNegativeVotesCount(int materialId)
    {
        var query = from vote in db.MaterialVotes
                    where ((vote.MaterialID == materialId) && (vote.Vote == false))
                    select vote.VoteID;
        return query.Count();
    }
    public int GetVotesAverage(int materialId)
    {
        int pVotes = GetPositiveVotesCount(materialId);
        int nVotes = GetNegativeVotesCount(materialId);
        return (pVotes > nVotes ? pVotes - nVotes : 0);
    }
    public IEnumerable<MaterialPicture> GetMaterialPictures(int materialId)
    {
        return db.MaterialPictures.Where(mp => mp.MaterialID == materialId);
    }
    public bool HasVoted(string UserName, int materialId)
    {
        return (db.MaterialVotes.Where(mv => mv.MaterialID == materialId && mv.Voter == UserName).Count() > 0 ? true : false);
    }
    public IEnumerable<MaterialType> GetMaterialTypes()
    {
        var types = from type in db.MaterialTypes
                    orderby type.Name
                    select type;
        return types;
    }
    public IEnumerable<MaterialTypeField> GetMaterialTypeFields(int TypeID)
    {
        var fields = from field in db.MaterialTypeFields
                    where field.TypeID == TypeID
                    orderby field.Name
                    select field;
        return fields;
    }
    public MaterialType GetMaterialType(int TypeId)
    {
        try
        {
            return db.MaterialTypes.Single(type => type.TypeID == TypeId);
        }
        catch
        {
            return null;
        }
    }
    public MaterialTypeField GetMaterialTypeField(int FieldId)
    {
        try
        {
            return db.MaterialTypeFields.Single(field => field.FieldID == FieldId);
        }
        catch
        {
            return null;
        }
    }
    public IEnumerable<FieldUnit> GetFieldUnits()
    {
        var units = from unit in db.FieldUnits
                    orderby unit.Name
                    select unit;
        return units;
    }
    public FieldUnit GetFieldUnit(int UnitId)
    {
        try
        {
            return db.FieldUnits.Single(unit => unit.ID == UnitId);
        }
        catch
        {
            return null;
        }
    }
    public IEnumerable<Material> GetMaterials(int FieldID, string Value)
    {
        var query = from fv in db.MaterialTypeFieldValues
                    where fv.Material.Visible && fv.FieldID == FieldID && fv.Value.Contains(Value)
                    orderby fv.Material.DateOfAdd descending
                    select fv.Material;
        return query;
    }
    public IEnumerable<Material> GetMaterials(List<int> materialIds)
    {
        IEnumerable<Material> materials = from material in db.Materials
                                          where material.Visible && materialIds.Contains(material.MaterialID)
                                          orderby material.DateOfAdd descending
                                          select material;
        return materials;
    }
    public IEnumerable<Material> GetMaterials(string Tag)
    {
        IEnumerable<Material> materials = from material in db.Materials 
                                          where material.Visible && material.Tags.Contains(Tag) 
                                          orderby material.DateOfAdd descending
                                          select material;
        return materials;
    }
    public IEnumerable<Material> GetMaterials(int typeId, int price, int priceUnit)
    {
        int _Percent = 10;
        int priceRials = ( (PriceUnit)priceUnit == PriceUnit.Rial ? price : price * 10 );
        int _PricePercent = (int)((priceRials * _Percent) / 100);
        var materials = from mp in db.MaterialPrices
                        where mp.IsCurrent && mp.Material.TypeID == typeId && mp.Material.Visible && 
                            ((PriceUnit)mp.Unit == PriceUnit.Rial ?
                            (mp.Price <= priceRials + _PricePercent && mp.Price >= priceRials - _PricePercent) :
                            (mp.Price * 10 <= priceRials + _PricePercent && mp.Price * 10 >= priceRials - _PricePercent) )
                        select mp.Material;
        return materials;
    }
    public IEnumerable<Material> FindMaterial(string name)
    {
        var materials = from material in db.Materials
                        where SqlMethods.Like(material.Name, "%" + name + "%")
                        select material;
        return materials;
    }
    public IEnumerable<Material> FindMaterial(string name, int SetId)
    {
        var materials = from material in db.Materials
                        where material.SetID == SetId && SqlMethods.Like(material.Name, "%" + name + "%")
                        select material;
        return materials;
    }
    public IEnumerable<Material> GetMaterials(List<MemberInterest> Interests)
    {
        IEnumerable<object[]> all = Enumerable.Empty<object[]>();
        List<object[]> all_List = new List<object[]>();
        for (int i = 0; i < Interests.Count(); i++)
        {
            IEnumerable<object[]> materials_points = GetMaterials(Interests[i]);
            AddToList(all_List, materials_points);
        }
        var res = from mp in all_List 
                  orderby (int)mp[1] descending 
                  select mp[0] as Material;
        return res;
    }
    private void AddToList(List<object[]> list, IEnumerable<object[]> material_points)
    {
        foreach (object[] mp in material_points)
        {
            bool isInList = false;
            foreach (object[] item in list)
            {
                if (((Material)item[0]).MaterialID == ((Material)mp[0]).MaterialID)
                {
                    item[1] = (int)item[1] + (int)mp[1];
                    isInList = true;
                }
            }
            if (!isInList)
                list.Add(mp);
        }
    }
    public IEnumerable<object[]> GetMaterials(MemberInterest Interest)
    {
        IEnumerable<object[]> materials = from keyword in db.MaterialKeywords
                                          where SqlMethods.Like(keyword.Word, "%" + Interest.Name + "%") || SqlMethods.Like(Interest.Name, "%" + keyword.Word + "%")
                                          group keyword by keyword.Material into kg
                                          orderby (from key in kg select key.Type).Sum() * Interest.TotalPoints descending
                                          select new object[] { kg.Key, (from key in kg select key.Type).Sum() * Interest.TotalPoints };
        return materials;
    }
    public IEnumerable<Material> GetLastMaterials(int Count)
    {
        return db.Materials.Where(mat => mat.Visible).OrderByDescending(mat => mat.DateOfAdd).Take(Count);
    }
    public IEnumerable<Material> GetPopularMaterials(int Count)
    {
        var query = db.Materials.Where(m => m.Visible).OrderByDescending(m => m.Votes.Where(v => v.Vote).Count() - m.Votes.Where(v => !v.Vote).Count()).ThenByDescending(m => m.Votes.Where(v => v.Vote).Count()).ThenByDescending(m => m.DateOfAdd);
        return query.Take(Count);
    }
    public IEnumerable<MaterialKeyword> GetMaterialKeywords(int MaterialId)
    {
        var query = from key in db.MaterialKeywords where key.MaterialID == MaterialId select key;
        return query;
    }
    public IEnumerable<MaterialPrice> GetMaterialPrices(int materialId)
    {
        var query = from price in db.MaterialPrices
                    where price.MaterialID == materialId
                    orderby price.DateOfAdd
                    select price;
        return query;
    }
    public MaterialPrice GetMaterialPrice(int PriceID)
    {
        try
        {
            return db.MaterialPrices.Single(p => p.PriceID == PriceID);
        }
        catch
        {
            return null;
        }
    }
    public MaterialPrice GetMaterialCurrentPrice(int MaterialId)
    {
        IEnumerable<MaterialPrice> query = from price in db.MaterialPrices
                                           where price.MaterialID == MaterialId && price.IsCurrent
                                           orderby price.DateOfAdd
                                           select price;
        return ( query.Count() > 0 ? query.Last() : MaterialPrice.Zero);
    }
    public IEnumerable<Member> GetMaterialBuyers(int MaterialId, int Count)
    {
        IEnumerable<Member> buyers = (from item in db.ShoppingCartItems
                                      where item.MaterialID == MaterialId
                                      orderby item.ShoppingCart.DateOfCreate descending
                                      select item.ShoppingCart.Member).Distinct().Take(Count);
        return buyers;
    }
    //********************************************* DELETE METHODS :
    public void DeletePicture(int picId)
    {
        db.MaterialPictures.DeleteAllOnSubmit(db.MaterialPictures.Where(p => p.PictureID == picId));
    }
    public void DeletePicture(MaterialPicture pic)
    {
        db.MaterialPictures.DeleteOnSubmit(pic);
    }
    public void DeleteCategory(int CategoryID)
    {
        db.Categories.DeleteAllOnSubmit(db.Categories.Where(cat => cat.CategoryID == CategoryID));
    }
    public void DeleteSet(int setId)
    {
        db.Sets.DeleteAllOnSubmit(db.Sets.Where(set => set.SetID == setId));
    }
    public void DeleteSet(Set set)
    {
        db.Sets.DeleteOnSubmit(set);
    }
    public void DeleteMaterialType(int typeId)
    {
        db.MaterialTypes.DeleteAllOnSubmit(db.MaterialTypes.Where(type => type.TypeID == typeId));
    }
    public void DeleteMaterialTypeField(int FieldId)
    {
        db.MaterialTypeFields.DeleteAllOnSubmit(db.MaterialTypeFields.Where(field => field.FieldID == FieldId));
    }
    public void DeleteMaterialTypeField(MaterialTypeField field)
    {
        db.MaterialTypeFields.DeleteOnSubmit(field);
    }
    public void DeleteMaterialTypeFieldValues(EntitySet<MaterialTypeFieldValue> values)
    {
        db.MaterialTypeFieldValues.DeleteAllOnSubmit(values);
    }
    public void DeletePrice(int PriceID)
    {
        db.MaterialPrices.DeleteAllOnSubmit(db.MaterialPrices.Where(mp => mp.PriceID == PriceID));
    }
    public void DeleteMaterial(Material material)
    {
        db.Materials.DeleteOnSubmit(material);
    }
    //******************************************** UPDATE Methods :
    public void ChangePriceStatus(int PriceId)
    {
        MaterialPrice price = GetMaterialPrice(PriceId);
        if (!price.IsCurrent)
        {
            var allPrices = GetMaterialPrices(price.MaterialID);
            foreach (MaterialPrice otherprice in allPrices)
                otherprice.IsCurrent = false;
        }
        price.IsCurrent = !price.IsCurrent;
    }
    //********************************************* Action Methods :
    public bool CodeExists(string Code)
    {
        return db.Materials.Where(material => material.Code == Code).Count() > 0 ? true : false;
    }
    public IEnumerable<Material> Search(string Text)
    {
        IEnumerable<Material> materials = from key in db.MaterialKeywords
                                          where (SqlMethods.Like(key.Word, "%" + Text + "%") || SqlMethods.Like(Text, "%" + key.Word + "%")) && key.Material.Visible
                                          group key.Type by key.Material into kg
                                          orderby kg.Sum() descending
                                          select kg.Key;
        return materials;
    }
    public IEnumerable<Material> Search(string Text, int CategoryId)
    {
        IEnumerable<Material> materials = from key in db.MaterialKeywords
                                          where key.Material.Set.Category.CategoryID == CategoryId && (SqlMethods.Like(key.Word, "%" + Text + "%") || SqlMethods.Like(Text, "%" + key.Word + "%")) && key.Material.Visible
                                          group key.Type by key.Material into kg
                                          orderby kg.Sum() descending
                                          select kg.Key;
        return materials;
    }
    public IEnumerable<Material> Sort(IEnumerable<Material> materials, SortField sortField, SortType sortType)
    {
        if (sortField == SortField.DateOfAdd)
        {
            if (sortType == SortType.Ascending)
            {
                materials = materials.OrderBy(mat => mat.DateOfAdd);
            }
            else if (sortType == SortType.Descending)
            {
                materials = materials.OrderByDescending(mat => mat.DateOfAdd);
            }
        }
        else if (sortField == SortField.Popularity)
        {
            if (sortType == SortType.Ascending)
            {
                materials = materials.OrderBy(m => m.VotesAverage_Real).ThenBy(m => m.PositiveVotes);
            }
            else if (sortType == SortType.Descending)
            {
                materials = materials.OrderByDescending(m => m.VotesAverage_Real).ThenByDescending(m => m.PositiveVotes);
            }
        }
        else if (sortField == SortField.Price)
        {
            if (sortType == SortType.Ascending)
            {
                materials = from mat in materials
                            where mat.CurrentPrice.Price > 0
                            orderby mat.CurrentPrice.Rials ascending
                            select mat;
            }
            else if (sortType == SortType.Descending)
            {
                materials = from mat in materials
                            where mat.CurrentPrice.Price > 0
                            orderby mat.CurrentPrice.Rials descending
                            select mat;
            }
        }
        else if (sortField == SortField.Reviews)
        {
            if (sortType == SortType.Ascending)
            {
                materials = materials.OrderBy(mat => mat.Reviews);
            }
            else if (sortType == SortType.Descending)
            {
                materials = materials.OrderByDescending(mat => mat.Reviews);
            }
        }
        return materials;
    }
    public MaterialPrice SumPrice(MaterialPrice price1, MaterialPrice price2, PriceUnit unit)
    {
        MaterialPrice p1 = (price1 != null ? price1.ChangeUnit(unit) : MaterialPrice.Zero);
        MaterialPrice p2 = (price2 != null ? price2.ChangeUnit(unit) : MaterialPrice.Zero);
        p1.Price += p2.Price;
        return p1;
    }
    public MaterialPrice MultiplicationPrice(MaterialPrice price, int Quantity)
    {
        MaterialPrice p = new MaterialPrice();
        p.Unit = price.Unit;
        p.Price = (price != null ? price.Price * Quantity : 0);
        return p;
    }
    public void FillCategoriesCombobox(DropDownList combo)
    {
        var cats = GetAllCategories();
        combo.Items.Clear();
        combo.Items.Add(new ListItem("", ""));
        foreach(Category cat in cats)
        {
            combo.Items.Add(new ListItem(cat.Name, cat.CategoryID.ToString()));
        }
    }
    //submit :
    public void Save()
    {
        db.SubmitChanges();
    }
}
