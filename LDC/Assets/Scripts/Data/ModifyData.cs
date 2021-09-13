using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ModifyData : MonoBehaviour
{
    [SerializeField] private GameObject WorldData;
    private VillageBuilder village;
    private Pastime details;
    private WorldData data;

    public void Awake()
    {
        village = WorldData.GetComponent<VillageBuilder>();
        details = WorldData.GetComponent<Pastime>();
    }

    // Function that starts the saving process
    // Set inside an OnClick action to be called
    public void SaveWorld()
    {
        village.SaveVillage(data);
        details.SaveDetails(data);
        this.gameObject.GetComponent<SaveSystem>().SaveWorld(data);
    }

    // Function used to start the loading of the data from the file
    // Set inside an OnClick action to be called
    public void LoadWorld()
    {
        data = this.gameObject.GetComponent<SaveSystem>().LoadWorld();
        village.LoadVillage(data);
        details.LoadDetails(data);
    }
}