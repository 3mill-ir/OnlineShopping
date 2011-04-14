using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Enums
/// </summary>
public enum MaterialStatus
{
    None = 0,
    Exists_Unlimit = 1,
    Exists_DeterminedCounts = 2,
    NotExists = 3
}
public enum MyUnit
{
    Number = 1,
    Box = 2,
    Kilogeram = 3,
    Set = 4,
    Meter = 5,
    Halghe = 6,
    System = 7,
    Liter = 8
}
public enum WeightUnit
{
    Mesghal = 1,
    Geram = 2,
    Kilogeram = 3,
    Ton = 4
}
public enum MaterialInfoDirection
{ 
    Right = 1,
    Left = 2,
    Automatic = 3
}
public enum MaterialInfoShowType
{
    Show = 1,
    FadeIn = 2,
    SlideDown = 3
}
public enum MaterialInfoType
{
    None = 0,
    Full = 1,
    JustName = 2,
    NameAndInfo = 3,
    NameAndPhotos = 4
}
public enum CollectionDirection
{
    Horizontal = 1,
    Vertical = 2
}
public enum MaterialInfoHeightType
{
    FitPictureHeight = 1,
    FixSize = 2,
    Free = 3,
    MinHeight = 4
}
public enum VoterType
{
    Anonymous = 1,
    Member = 2
}
public enum PriceUnit
{
    Rial = 1,
    Tuman = 2,
    Dollar = 3,
    Euro = 4
}
public enum ShopType
{
    Real = 1,
    Online = 2
}
public enum CartStatus
{
    Checking = 1,
    NotExists = 2,
    Confirmed = 3,
    NotConfirmed = 4,
    DeliveredToPost = 5,
    Returned = 6,
    Delivered = 7
}
public enum InterestType
{
    View = 1,
    Search = 2,
    Like = 3,
    Buy = 4
}
public enum KeywordType
{
    CategoryName = 1,
    SetName = 2,
    MaterialType = 4,
    Name = 3,
    Tag = 5
}
public enum SortField
{
    None = 0,
    DateOfAdd = 1,
    Popularity = 2,
    Reviews = 3,
    Price = 4
}
public enum SortType
{
    Ascending = 1,
    Descending = 2
}
public enum SearchType
{
    AllCategories = 1,
    SpecificCategory = 2
}
public enum DiagramType
{
    Combined = 1,
    Separated = 2,
    JustGreen = 3,
    JustRed = 4
}
public enum Markets
{
    Urmia = 1,
    Baneh = 2,
    Piranshahr = 3
}
