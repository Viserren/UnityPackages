using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Meelee : Weapon
{
    public SO_Melee melee;

    public override void Use()
    {
        Debug.Log($"Current Melee: {melee.itemName}, damage: {melee.damage}");
    }
}
