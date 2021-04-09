using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashboard : MonoBehaviour
{
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject JournalPanel;
    [SerializeField] GameObject MapPanel;

    bool InventoryStatus = false;
    bool JournalStatus = false;
    bool MapStatus = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryStatus = !InventoryStatus;
            InventoryPanel.SetActive(InventoryStatus);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            JournalStatus = !JournalStatus;
            JournalPanel.SetActive(JournalStatus);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MapStatus = !MapStatus;
            MapPanel.SetActive(MapStatus);
        }
    }
}
