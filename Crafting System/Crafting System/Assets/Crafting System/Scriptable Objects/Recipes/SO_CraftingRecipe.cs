using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem
{
    [CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting System/Recipes/Crafting Recipe")]
    public class SO_CraftingRecipe : ScriptableObject
    {
        public SO_Items ItemToCraft;
        public int NumberOfItemsProduced;
        public List<SO_Items> ItemsNeeded;
        public float TimeToCraft;
    }
}
