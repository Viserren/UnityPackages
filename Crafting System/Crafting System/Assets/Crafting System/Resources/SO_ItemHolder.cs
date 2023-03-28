using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem
{
    public class SO_ItemHolder : MonoBehaviour
    {
        [field: SerializeField] public SO_Items Item { get; private set; }
    }
}
