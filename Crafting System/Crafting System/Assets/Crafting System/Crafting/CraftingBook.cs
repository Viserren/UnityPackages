using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CraftingSystem
{
    public class CraftingBook : MonoBehaviour
    {
        public UnityEvent<SO_CraftingRecipe> ChangeCraftingRecipe;
        public SO_ItemList ItemList;
        public GameObject ItemTemplate;
        public Transform BookPages;

        private void Start()
        {
            foreach (var item in ItemList.AllRecipes)
            {
                GameObject recipe = Instantiate(ItemTemplate, BookPages);
                if(recipe.TryGetComponent(out SetItemButtonInfo buttonInfo)){
                    buttonInfo.SetInfo(item);
                }
            }
        }
    }
}
