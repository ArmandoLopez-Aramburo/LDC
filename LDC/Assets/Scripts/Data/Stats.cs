using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string Name;
    public int Level;

    public int damage;

    public int maxHP;
    public float currentHP;

    private float Speed;

    public bool dead { get; set; } = false;

    public int health { get; set; } = 100;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0) return true;
        else return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
