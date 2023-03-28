using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CraftingSystem
{
    public class SetItemButtonInfo : MonoBehaviour
    {
        public TextMeshProUGUI Title;
        public Image ItemImage;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI RequiredResources;
        public Button RecipeButton;
        public Dictionary<string, int> RequiredItems = new Dictionary<string, int>();
        public void SetInfo(SO_CraftingRecipe currentItem)
        {
            Title.text = currentItem.ItemToCraft.ItemName;
            RecipeButton.onClick.AddListener(() => GetComponentInParent<CraftingTable>().SetCurrentRecipe(currentItem));
            ItemImage.sprite = currentItem.ItemToCraft.ItemPicture;
            Description.text = "Null";
            RequiredResources.text = "";
            foreach (var item in currentItem.ItemsNeeded)
            {
                if (!RequiredItems.ContainsKey(item.ItemName))
                {
                    RequiredItems.Add(item.ItemName, 1);
                }
                else
                {
                    RequiredItems[item.ItemName]++;
                }
            }
            foreach (var item in RequiredItems)
            {
                RequiredResources.text += $"{item.Value}x {item.Key}\n";
            }
        }
    }
}
