using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Models;

/// <summary>
/// Summary description for CitiesRepository
/// </summary>
public class CitiesRepository
{
    //*********************************
    MyDatabaseDataContext db = new MyDatabaseDataContext(Repository.ConnectionString);
    //*********************************
    //***************** GET Methods :
    public IEnumerable<State> GetStates()
    {
        //var states = from state in db.States
        //             orderby state.StateName
        //             select state;
        return db.States;
    }
    public State GetState(int stateId)
    {
        try
        {
            return db.States.Single(state => state.StateID == stateId);
        }
        catch
        {
            return null;
        }
    }
    public IEnumerable<City> GetStateCities(int stateId)
    {
        var stateCities = from city in db.Cities
                          where city.StateID == stateId
                          orderby city.CityName
                          select city;
        return stateCities;
    }
    //***************** ACTION Methods :
    public void FillStatesList(DropDownList list)
    {
        list.Items.Clear();
        list.Items.Add(new ListItem("", ""));
        foreach (State state in GetStates())
        {
            list.Items.Add(new ListItem(state.StateName, state.StateID.ToString()));
        }
    }
    public void FillCitiesList(DropDownList list, int stateId)
    {
        list.Items.Clear();
        list.Items.Add(new ListItem("", ""));
        foreach (City city in GetStateCities(stateId))
        {
            list.Items.Add(new ListItem(city.CityName, city.CityID.ToString()));
        }
    }
    //**************** Submit :
    public void Save()
    {
        db.SubmitChanges();
    }
}
