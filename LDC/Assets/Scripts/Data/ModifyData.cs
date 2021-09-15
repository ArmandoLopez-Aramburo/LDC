using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ModifyData : MonoBehaviour
{
    [Header("DATA SCRIPTS")]
    [SerializeField] private GameObject WorldData;
    private VillageBuilder village;
    private Pastime details;
    private WorldData data = new WorldData();

    // NEW SAVE SYSTEM VARIABLES
    [Header("SAVE OBJECTS")]
    [SerializeField] private GameObject BlockingPanel;
    private GameObject currentButtonClicked;


    [Header("SAVE/LOAD PANELS")]
    [SerializeField] private GameObject SavePanel;
    [SerializeField] private GameObject LoadPanel;
    public GameObject currentPanel;

    // Variables for Save Button
    [Header("OTHER")]
    [SerializeField] private GameObject OverwritePanel;
    [SerializeField] private GameObject SaveList;
    [SerializeField] private GameObject LoadList;
    [SerializeField] private GameObject saveButton;
    private GameObject temp;
    public int saveSlot = 0;
    public int x;

    // Awake Function that sets the data scripts and calls the UpdateUI function to populate the list of current saves
    public void Awake()
    {
        village = WorldData.GetComponent<VillageBuilder>();
        details = WorldData.GetComponent<Pastime>();

        UpdateUI(Directory.GetFiles(Application.persistentDataPath, "*.poop").Length, SaveList, "save");
        UpdateUI(Directory.GetFiles(Application.persistentDataPath, "*.poop").Length, LoadList, "load");
    }

    // Update Function that checks the escape key to close the current save/load screen
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentPanel == SavePanel)
            {
                BlockingPanel.SetActive(false);
                SavePanel.SetActive(false);
                currentPanel = null;
            }
            if (currentPanel == LoadPanel)
            {
                BlockingPanel.SetActive(false);
                LoadPanel.SetActive(false);
                currentPanel = null;
            }
        }
    }

    // Function used in OnClick to bring up the save screen
    public void SaveButton()
    {
        BlockingPanel.SetActive(true);
        SavePanel.SetActive(true);

        currentPanel = SavePanel;
    }

    // Function used in OnClick to bring up the loading screen
    public void LoadButton()
    {
        BlockingPanel.SetActive(true);
        LoadPanel.SetActive(true);
        currentPanel = LoadPanel;
    }

    // Function that starts the saving process dependent on the type passed into it
    public void SaveWorld(string type)
    {
        if(type == "NewSave")
        {
            if(saveSlot < 5)
            {
                StartSaveProcess(saveSlot, "NewSave");
                saveSlot++;
            }
        }
        if(type == "OldSave")
        {
            Debug.Log("Clicked SaveSlot is: " + EventSystem.current.currentSelectedGameObject.name);
            currentButtonClicked = EventSystem.current.currentSelectedGameObject;
            Debug.Log("OVERWRITE YAY OR NAY?");
            BlockingPanel.SetActive(true);
            OverwritePanel.SetActive(true);
        }
        else
        {
            Debug.Log("NEED OVERWRITE PROMPT");
            // OVERWRITE PROMPT POP UP IF YOU WANT TO OVERWRITE THE SAVE
        }
    }

    // Function called to Start the Saving Process of the data
    public void StartSaveProcess(int saveSlot, string type)
    {
        if(type == "OldSave")
        {
            village.SaveVillage(data);
            details.SaveDetails(data);
            this.gameObject.GetComponent<SaveSystem>().SaveWorld(data, saveSlot);
        }
        else
        {
            village.SaveVillage(data);
            details.SaveDetails(data);
            this.gameObject.GetComponent<SaveSystem>().SaveWorld(data, saveSlot);
            temp = Instantiate(saveButton, SaveList.transform);
            temp.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += saveSlot;
        }
    }

    // Function used to start the loading of the data from the file
    // Set inside an OnClick action to be called
    public void LoadWorld()
    {
        data = this.gameObject.GetComponent<SaveSystem>().LoadWorld(Convert.ToInt32(EventSystem.current.currentSelectedGameObject.name));
        village.LoadVillage(data);
        details.LoadDetails(data);
    }

    // Function that handles the Overwrite feature, if user selects yes then we overwrite the previous save with the new one.
    // else it goes back to the panel.
    public void Overwrite(bool action)
    {
        if(action)
        {
            StartSaveProcess(Convert.ToInt32(currentButtonClicked.name), "OldSave");

            BlockingPanel.SetActive(false);
            OverwritePanel.SetActive(false);
        }
        else
        {
            BlockingPanel.SetActive(false);
            OverwritePanel.SetActive(false);
        }
    }

    // Updates the List of the saves stored
    public void UpdateUI(int x, GameObject list, string type)
    {
        if (x > 0)
        {
            for (int i = 0; i < x; i++)
            {
                temp = Instantiate(saveButton, list.transform);
                temp.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += " " + i;
                temp.name = i.ToString();
                if(type == "save")
                {
                    temp.GetComponent<Button>().onClick.AddListener(delegate { SaveWorld("OldSave"); });
                }
                else
                {
                    temp.GetComponent<Button>().onClick.AddListener(delegate { LoadWorld(); });
                }
            }
        }
    }
}