using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dashboard : MonoBehaviour
{
    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject Player;

    bool InventoryStatus = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryStatus = !InventoryStatus;
            Inventory.SetActive(InventoryStatus);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameData.playerPos = Player.gameObject.transform.position;
            StartBattle();
            GameData.currentDungeon = GameObject.Find("Dungeon");
            print(GameData.currentDungeon.name);
        }

        if(GameData.outOfCombat == true)
        {
            GameObject temp = GameObject.Find("Dungeon");
            temp = GameData.currentDungeon;
            Player.transform.position = GameData.playerPos;
            GameData.outOfCombat = false;
        }
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
