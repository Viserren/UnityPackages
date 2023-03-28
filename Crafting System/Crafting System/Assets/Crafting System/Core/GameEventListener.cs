using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CraftingSystem
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent<SO_CraftingRecipe> onEventTriggered;

        private void OnEnable()
        {
            gameEvent.AddListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(this);
        }
        public void OnEventTriggered(SO_CraftingRecipe newCraftingRecipe)
        {
            onEventTriggered.Invoke(newCraftingRecipe);
        }
    }
}
