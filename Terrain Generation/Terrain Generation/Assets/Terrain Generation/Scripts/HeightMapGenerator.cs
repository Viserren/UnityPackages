using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    public static class HeightMapGenerator
    {
        public static HeightMap GenerateHightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCentre)
        {
            float[,] values = Noise.GenerateNoiseMap(width, height, settings.noiseSettings,sampleCentre);
            AnimationCurve heightCurve_ThreadSafe = new AnimationCurve(settings.heightCurve.keys);

            float minValue = float.MaxValue;
            float maxValue = float.MinValue;

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    values[w,h] *= heightCurve_ThreadSafe.Evaluate(values[w,h]) * settings.heightMultiplier;

                    if (values[w,h] > maxValue)
                    {
                        maxValue = values[w,h];
                    }

                    if (values[w,h] < minValue)
                    {
                        minValue = values[w, h];
                    }
                }
            }
            return new HeightMap(values,minValue,maxValue);
        }
    }


    public struct HeightMap
    {
        public readonly float[,] values;
        public readonly float minValues;
        public readonly float maxValues;

        public HeightMap(float[,] values, float minValue, float maxValue)
        {
            this.values = values;
            this.minValues = minValue;
            this.maxValues = maxValue;
        }
    }
}
