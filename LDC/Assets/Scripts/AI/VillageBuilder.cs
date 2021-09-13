using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageBuilder : MonoBehaviour
{

    [SerializeField] public string Name;
    [SerializeField] public int Population;
    [SerializeField] public Vector3 location;

    private int AvailableProfessions;

    private string temp = "";

    public List<string> TownProfessions = new List<string>();

    private string[] UnskilledProfessions = new string[]
    {
        "Town Crier", "Merchant", "Baker", "Butcher",
        "Stonemason", "Winemaker", "Mason", "Farmer",
        "Tanner", "Carpenter", "Blacksmith",
        "Herbalist", "Hunter",
    };

    private string[] SkilledProfessions = new string[]
{
        "Artisan", "Town Crier", "Executioner", "Merchant", "Plague Doctor", "Baker",
        "Butcher", "Stonemason", "Weaver", "Winemaker", "Mason", "Farmer", "Cobbler",
        "Tanner", "Tax Collector", "Armour", "Carpenter", "Blacksmith"
};

    public void GenerateVillage()
    {
        temp = null;
        SetName();
        TownProfessions.Clear();
        TownProfessions.Add("Elder");
        SetUniqueBuildings();
        setPopulation();

        SetLocation();

        PrintVillageStats();
    }

    // NEED TO IMPLEMENT A JSON FILE WITH ALL THE POSSIBLE TOWN/VILLAGE NAMES
    public void SetName()
    {
        Name = "Some Random Name";
    }

    // Function used to set the population of the town
    public void setPopulation()
    {
        Population = Random.Range(50, 100);
    }

    // Function to select the Unique Professions in the town
    public void SetUniqueBuildings()
    {
        AvailableProfessions = Random.Range(4, UnskilledProfessions.Length-2);

        for (int i = 0; i < AvailableProfessions; i++)
        {
            int x = Random.Range(0, UnskilledProfessions.Length - 1);

            CheckListForDuplicates(UnskilledProfessions[x]);
        }
    }

    // Function Selects a random location for the Village 
    public void SetLocation()
    {
        int x = Random.Range(0, 100);
        int y = Random.Range(0, 100);
        location = new Vector3(x, y);
    }

    // Function used to check if a Profession already exists in the list
    private void CheckListForDuplicates(string Profession)
    {
        if (!TownProfessions.Contains(Profession))
        {
            TownProfessions.Add(Profession);
        }
    }

    // Function to print out a list
    private void PrintList()
    {
        foreach (var x in TownProfessions)
        {
            temp += x + ", ";
        }
        temp = temp.Remove(temp.Length - 2);
        Debug.Log(temp);
    }

    // Function that updates the WorldData village portion with the newest variables
    public void SaveVillage(WorldData data)
    {
        Debug.Log(this.Population);

        data.VillageInfo(this);
        //VillageData.instance.VillageInfo(this);
    }

    // Function that updates the variables with the newest variables on WorldData
    public void LoadVillage(WorldData data)
    {
        if (TownProfessions.Count != 0)
        {
            TownProfessions.Clear();
            temp = null;
        }
        Population = data.Population;
        Name = data.VillageName;
        Population = data.Population;
        location.x = data.Location[0];
        location.y = data.Location[1];
        location.z = data.Location[2];
        TownProfessions = data.TownProfessions;
        PrintVillageStats();
    }

    private void PrintVillageStats()
    {
        Debug.Log("VILLAGE NAME: " + Name);
        Debug.Log("VILLAGE POPULATION: " + Population);
        Debug.Log("VILLAGE LOCATION: " + location);

        PrintList();
    }
}