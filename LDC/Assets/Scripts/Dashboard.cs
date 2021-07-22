﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dashboard : MonoBehaviour
{
    [SerializeField] public GameObject Inventory;
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject QuitPanel;

    bool InventoryStatus = false;
    bool QuitStatus = false;

    private void Start()
    {
        if (GameData.outOfCombat == true)
        {
            
            Player.transform.position = GameData.playerPos;
            GameData.outOfCombat = false;
        }
    }

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
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitStatus = !QuitStatus;
            QuitPanel.SetActive(QuitStatus);
        }
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
