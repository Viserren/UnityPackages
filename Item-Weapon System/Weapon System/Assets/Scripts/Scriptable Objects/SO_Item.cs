using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Data/Item Data")]
public class SO_Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public string description;
    public string itemId = Guid.NewGuid().ToString().ToUpper();
}
