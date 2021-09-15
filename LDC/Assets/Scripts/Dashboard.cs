using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dashboard : MonoBehaviour
{
    [Header("MiniScreen")]
    [SerializeField] public GameObject Inventory;
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject QuitPanel;
    [SerializeField] public GameObject Journal;

    bool InventoryStatus = false;
    bool QuitStatus = false;
    bool JournalStatus = false;

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
        if(Input.GetKeyDown(KeyCode.Tab) && Inventory != null)
        {
            InventoryStatus = !InventoryStatus;
            Inventory.SetActive(InventoryStatus);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameData.playerPos = Player.gameObject.transform.position;
            StartBattle();
        }

        if(Input.GetKeyDown(KeyCode.J) && Journal != null)
        {
            JournalStatus = !JournalStatus;
            Journal.SetActive(JournalStatus);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && QuitPanel != null)
        {
            if(this.transform.childCount > 0)
            {
                if (this.transform.GetChild(1).GetComponent<ModifyData>().currentPanel == null)
                {
                    QuitStatus = !QuitStatus;
                    QuitPanel.SetActive(QuitStatus);
                }
            }
            else
            {
                QuitStatus = !QuitStatus;
                QuitPanel.SetActive(QuitStatus);
            }
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

    public void ExitDungeon()
    {
        // display the Analysis Report
        // then goes to the scene where we'll handle outside dungeon shit.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void EnterDungeon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}