using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace CraftingSystem
{
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private Vector3 PlacedItemsArea;
        public SO_CraftingRecipe CurrentRecipe { get; private set; }
        public SO_ItemList ItemList;
        public Transform ItemSpawnPoint;
        public LayerMask ItemsMask;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider[] colliderArray = Physics.OverlapBox(ItemSpawnPoint.transform.position, PlacedItemsArea, ItemSpawnPoint.transform.rotation, ItemsMask);
                List<Collider> colliderList = colliderArray.ToList();
                StartCoroutine(Craft(colliderList));
            }
        }

        public IEnumerator Craft(List<Collider> craftingCollider)
        {
            yield return new WaitForSeconds(CurrentRecipe.TimeToCraft);
            if (CurrentRecipe != null)
            {
                List<SO_Items> inputItems = new List<SO_Items>(CurrentRecipe.ItemsNeeded);
                List<GameObject> usedItems = new List<GameObject>();
                foreach (var item in inputItems)
                {
                    Debug.Log($"Items needed: {item.name}");
                }
                foreach (Collider collider in craftingCollider)
                {
                    if (collider.TryGetComponent(out SO_ItemHolder item))
                    {
                        Debug.Log($"Item before use: {item.Item}");
                        if (CurrentRecipe.ItemsNeeded.Contains(item.Item) && inputItems.Count > 0)
                        {
                            Debug.Log($"Item to use: {item.Item}");
                            inputItems.Remove(item.Item);
                            usedItems.Add(collider.gameObject);
                        }
                    }
                }
                if (inputItems.Count == 0)
                {
                    foreach (var item in usedItems)
                    {
                        Destroy(item);
                    }
                    //Debug.Log("Craftable");
                    for (int i = 0; i < CurrentRecipe.NumberOfItemsProduced; i++)
                    {
                        Instantiate(CurrentRecipe.ItemToCraft.ItemPrefab, ItemSpawnPoint.position, ItemSpawnPoint.rotation);
                    }
                    usedItems.Clear();
                }
                else
                {

                }
            }

        }

        public void SetCurrentRecipe(SO_CraftingRecipe selectedRecipe)
        {
            CurrentRecipe = selectedRecipe;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(ItemSpawnPoint.transform.position, PlacedItemsArea);
        }
    }
}
