using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    public class UpdateableData : ScriptableObject
    {
        public event System.Action OnValuesUpdate;
        public bool autoUpdate;
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (autoUpdate)
            {
                UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
            }
        }

        public void NotifyOfUpdatedValues()
        {
            UnityEditor.EditorApplication.update -= NotifyOfUpdatedValues;
            if (OnValuesUpdate != null)
            {
                OnValuesUpdate();
            }
        }
#endif
    }
}
