using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashboard : MonoBehaviour
{
    [SerializeField] GameObject Inventory;

    bool InventoryStatus = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryStatus = !InventoryStatus;
            Inventory.SetActive(InventoryStatus);
        }
    }
}
