using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun : Weapon, Reloadable
{
    public SO_Gun gun;

    public void Reload()
    {
        Debug.Log($"Weapon: {gun.itemName} reloading");
    }

    public override void Use()
    {
        Debug.Log($"Current Gun: {gun.itemName}, damage: {gun.damage}");
    }
}
