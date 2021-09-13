using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pastime : MonoBehaviour
{
    public int Day;
    public int Month;
    public int Year;

    public int gp;

    public void SetYear()
    {
        Year = 812;
        Month = 5;
        Day = 15;
    }

    // Function that advances time by one week
    public void AdvanceTime()
    {
        Day += 7;
        if(Day >= 30)
        {
            Month++;
            Day -= 30;
        }
        if(Month >= 12)
        {
            Year++;
            Month -= 12;
        }
    }

    public void Work()
    {
        gp+=5;
        Debug.Log("MONEEEEEEE: " + gp);
    }

    public void Train()
    {
        Debug.Log("TRAINING.....");
    }

    // Function that updates the WorldData detail portion with the newest variables
    public void SaveDetails(WorldData data)
    {
        Debug.Log("Year: " + Year + " Month: " + Month + " Day: " + Day);
        Debug.Log("Player: " + gp);
        data.DetailData(this);

    }

    // Function that updates the variables with the newest variables on WorldData
    public void LoadDetails(WorldData data)
    {
        Day = data.Day;
        Month = data.Month;
        Year = data.Year = Year;
        gp = data.gp;
        Debug.Log("Year: " + Year + " Month: " + Month + " Day: " + Day);
        Debug.Log("Player gp: " + gp);
    }
}