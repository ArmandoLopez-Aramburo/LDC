using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// Current Serialized class that is modified, saved, and stored
public class VillageData
{
    // NEED TO CHECK IF VILLAGE IS ALREADY THERE IN THE FILE
    public string VillageName = "";
    public int Population = 0;
    public float[] Location = new float[3];

    public List<string> TownProfessions = new List<string>();

    public VillageData(VillageBuilder village)
    {
        VillageName = village.Name;
        Population = village.Population;
        Location = new float[3];
        Location[0] = village.location.x;
        Location[1] = village.location.y;
        Location[2] = village.location.z;
        TownProfessions = village.TownProfessions;
    }
}