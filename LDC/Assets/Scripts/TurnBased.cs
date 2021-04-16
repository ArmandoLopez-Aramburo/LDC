using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class TurnBased : MonoBehaviour
{
    public BattleState state;

    private List<GameObject> EnemyList = new List<GameObject>();

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Stats playerStats;
    Stats enemyStats;

    public TextMeshProUGUI dialogueText;

    private void Awake()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyList.Add(enemy);
        }
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator SetUpBattle()
    {
        GameObject player = Instantiate(PlayerPrefab, playerBattleStation);
        playerStats = player.GetComponent<Stats>();
        GameObject enemy = Instantiate(EnemyPrefab, enemyBattleStation);
        enemyStats = enemy.GetComponent<Stats>();

        dialogueText.SetText("A wild " + enemyStats.Name + " appeared!!!");
        //playerStats.currentHP = GameData.playerCurrentHP;
        print(GameData.playerCurrentHP);
        print(playerStats.currentHP);

        yield return new WaitForSeconds(1f);
        // PLAYERS TURN
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public void EndBattle()
    {
        GameData.playerCurrentHP = playerStats.currentHP;
        print("PLAYERUNIT: " + playerStats.currentHP);
        print("GAMEDATA: " + GameData.playerCurrentHP);
        GameData.outOfCombat = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void PlayerTurn()
    {
        dialogueText.SetText("Choose an action:");
    }

    public void AttackAction()
    {
        if (state == BattleState.PLAYERTURN)
        {
            // NEED TO INCORPORATE MORE THAN ONE ENEMY.
            StartCoroutine(playerAttack());
        }
    }

    IEnumerator playerAttack()
    {
        // Damage the enemy.
        // NEED TO INCORPORATE ATTACKS MISSING.
        bool isDead = enemyStats.TakeDamage(playerStats.damage);
        dialogueText.SetText("The attack was successful!");
        yield return new WaitForSeconds(2f);

        // if statement to check if the enemy is dead.
        if (isDead)
        {
            // End battle
            state = BattleState.WON;
            dialogueText.SetText("You've won the battle!!");
            yield return new WaitForSeconds(2f);
            BattleXP();
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        // Change state based on what happens.
    }

    IEnumerator EnemyTurn()
    {

        bool isDead = playerStats.TakeDamage(enemyStats.damage);
        dialogueText.SetText(enemyStats.Name + " attacks!");

        yield return new WaitForSeconds(2f);
        // Check if the player is dead.
        if (isDead)
        {
            // End battle
            state = BattleState.LOST;
            dialogueText.SetText("You've lost the battle...");
            yield return new WaitForSeconds(2f);
            print(GameData.playerPos);
            EndBattle();
        }

        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void HealAction()
    {
        if (state == BattleState.PLAYERTURN)
        {
            // NEED TO INCORPORATE MORE THAN ONE ENEMY.
            StartCoroutine(playerHeal());
        }
    }

    IEnumerator playerHeal()
    {
        playerStats.Heal(5);
        dialogueText.SetText("You feel renewed strength surging through your veins!");
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    public void RunAwayAction()
    {
        if (state == BattleState.PLAYERTURN)
        {
            EndBattle();
        }
    }

    public void BattleXP()
    {
        GameData.playerCurrentXP += 20;
    }
}
