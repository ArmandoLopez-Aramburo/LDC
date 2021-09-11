using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionBuilder : MonoBehaviour
{
    VillageBuilder village = new VillageBuilder();
    private void Start()
    {
        village.GenerateVillage();
    }
}
