using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashboard : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject JournalPanel;
    [SerializeField] GameObject MapPanel;

    bool InventoryStatus = false;
    bool JournalStatus = false;
    bool MapStatus = false;

    bool Status = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryStatus = PanelState(InventoryPanel, InventoryStatus);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {

            JournalStatus = PanelState(JournalPanel, JournalStatus);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MapStatus = PanelState(MapPanel, MapStatus);
        }
    }

    bool PanelState(GameObject Panel, bool Status)
    {
        Status = !Status;
        Panel.SetActive(Status);
        return Status;
    }
}
