using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Data", menuName = "Data/Melee Data")]
public class SO_Melee : SO_Weapon
{
    [SerializeReference] public float hitSpeed;
}
