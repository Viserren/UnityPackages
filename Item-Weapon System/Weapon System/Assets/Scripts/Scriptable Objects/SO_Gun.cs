using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Data/Gun Data")]
public class SO_Gun : SO_Weapon
{
    [SerializeField] protected float reloadSpeed;
    [SerializeField] protected float maxAmmoInclip;
    [SerializeField] protected float fireRange;
    [SerializeField] protected float fireSpeed;
    [SerializeField] protected float recoilAmount;
}
