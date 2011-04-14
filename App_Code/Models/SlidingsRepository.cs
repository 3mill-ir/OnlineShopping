using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

/// <summary>
/// Summary description for SlidingsRepository
/// </summary>
public class SlidingsRepository
{
    //************************************
    MyDatabaseDataContext db = new MyDatabaseDataContext(Repository.ConnectionString);
    //************************************
    public void AddSlidingImage(SlidingImage image)
    {
        db.SlidingImages.InsertOnSubmit(image);
    }
    public double GetLastSequence()
    {
        double last = 0.0;
        var query = from img in db.SlidingImages
                    select img.Sequence;
        return (query.Count() > 0 ? query.Max() : -1);
    }
    public IEnumerable<SlidingImage> GetSlidingImages()
    {
        var images = from img in db.SlidingImages
                     orderby img.Sequence
                     select img;
        return images;
    }
    public string GetSlidingImagesHtml()
    {
        string html = "";
        int i = 0;
        foreach (SlidingImage img in GetSlidingImages())
        {
            html += img.GetHtml(i);
            i++;
        }
        return html;
    }
    public void DeleteSlidingImage(int id)
    {
        db.SlidingImages.DeleteAllOnSubmit(db.SlidingImages.Where(img => img.ID == id));
    }
    public void Save()
    {
        db.SubmitChanges();
    }
}
