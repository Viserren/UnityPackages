using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Crafting System/Items/Item")]
    public class SO_Items : ScriptableObject
    {
        public string ItemName;
        public Sprite ItemPicture;
        public ItemType Type;
        public bool isCookable;
        public GameObject ItemPrefab;
    }
}
