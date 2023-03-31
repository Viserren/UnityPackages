using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Data/Gun Data")]
public class SO_Gun : SO_Weapon
{
    [SerializeReference] public float reloadSpeed;
    [SerializeReference] public int maxAmmoInclip;
    [SerializeReference] public float fireRange;
    [SerializeReference] public float fireSpeed;
    [SerializeReference] public float recoilAmount;
}
