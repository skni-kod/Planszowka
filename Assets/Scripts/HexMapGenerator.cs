using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMapGenerator
{
    public static float pineForestChance = 0.5f;
    public static float waterChance = 0.34f;
    public static float seed = 2137f;


    public static string GenerateHexType(Vector3 id)
    {
        string type = "grass";

        float x = (id.x - id.y) * (HexMetrics.innerRadius);
        float z = id.z * (HexMetrics.outerRadius * 1.5f);


        if (GeneratePineForestChance(x, z) < pineForestChance)
            type = "small_forest";
        if (GeneratePineForestChance(x, z) < pineForestChance * 0.7f)
            type = "medium_forest";
        if (GeneratePineForestChance(x, z) < pineForestChance * 0.4f)
            type = "large_forest";

        if (GenerateWaterChance(x, z) < waterChance)
            type = "water";


        return type;
    }


    #region PerlinChances
    public static float GeneratePineForestChance(float x, float z)
    {
        float p = Mathf.PerlinNoise(seed + x / 90, seed + z / 90) * 0.6f;
        float drobnyP = Mathf.PerlinNoise(seed + x / 9, seed + z / 9) * 0.3f;
        float duzyP = Mathf.PerlinNoise(seed + x / 900, seed + z / 900) * 0.1f;
        return p + drobnyP + duzyP;
    }

    public static float GenerateWaterChance(float x, float z)
    {
        float p2 = Mathf.PerlinNoise(x * 2f, -seed + z * 1.5f) * 0.2f;
        float p3 = Mathf.PerlinNoise(seed * 4 + x / 140f, -seed + z / 200f) * 0.8f;
        return p2 + p3;
    }
    #endregion

}
