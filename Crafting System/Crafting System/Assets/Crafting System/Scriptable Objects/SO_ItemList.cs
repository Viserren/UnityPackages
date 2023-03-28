using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem
{
    [CreateAssetMenu(fileName = "New List", menuName = "Crafting System/Items/list")]
    public class SO_ItemList : ScriptableObject
    {
        public List<SO_Items> AllItems;
        public List<SO_CraftingRecipe> AllRecipes;
        public List<SO_CraftingRecipe> LearntRecipes;
    }
}
