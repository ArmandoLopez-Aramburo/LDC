using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public string VillageName = "";
    public int Population;
    public float[] Location = new float[3];

    public List<string> TownProfessions = new List<string>();

    public int Day;
    public int Month;
    public int Year;
    public int gp;

    // Function called to update the village info variables
    public void VillageInfo(VillageBuilder village)
    {
        VillageName = village.Name;
        Population = village.Population;
        Debug.Log("VILLAGEDATA POPULATION: " + Population);
        Location = new float[3];
        Location[0] = village.location.x;
        Location[1] = village.location.y;
        Location[2] = village.location.z;
        TownProfessions = village.TownProfessions;
    }

    // Function called to update the detail info variables
    public void DetailData(Pastime detail)
    {
        Day = detail.Day;
        Month = detail.Month;
        Year = detail.Year;
        gp = detail.gp;
    }
}