using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] public GameObject QuestPanel;
    [SerializeField] public GameObject PlayerPanel;
    [SerializeField] public GameObject HistoryPanel;

    public void Quest()
    {
        SetPanels(QuestPanel);
    }

    public void PlayerInfo()
    {
        SetPanels(PlayerPanel);
    }

    public void History()
    {
        SetPanels(HistoryPanel);
    }

    void SetPanels(GameObject panel)
    {
        panel.SetActive(true);
        if (panel != QuestPanel) QuestPanel.SetActive(false);
        if (panel != PlayerPanel) PlayerPanel.SetActive(false);
        if (panel != HistoryPanel) HistoryPanel.SetActive(false);
    }
}
