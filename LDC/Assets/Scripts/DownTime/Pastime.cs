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
        Debug.Log("Year: " + Year + " Month: " + Month + " Day: " + Day);
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
}