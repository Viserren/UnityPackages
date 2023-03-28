using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    [CreateAssetMenu(fileName = "newHeightMapSettings", menuName = "Terrain Generation/Scriptable Objects/Height Map Settings")]
    public class HeightMapSettings : UpdateableData
    {
        public NoiseSettings noiseSettings;

        public bool useFalloff;
        public float heightMultiplier;
        public AnimationCurve heightCurve;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            noiseSettings.ValidateValues();

            base.OnValidate();
        }
#endif
    }
}
