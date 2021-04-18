using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMapGenerator
{
    public static float pineForestChance = 0.50f;
    public static float waterChance = 0.34f;
    public static float seed = 2137f;

    private static Vector3 HexToKartezjan(Vector3 id)
    {
        return new Vector3((id.x - id.y) * (HexMetrics.innerRadius), 0f, id.z * (HexMetrics.outerRadius * 1.5f));
    }


    public static KeyValuePair<int, string> GenerateHexType(Vector3 id)
    {
        string type = "grass";
        int rotation = -1;
        int[] neighbours = { 0, 0, 0, 0, 0, 0 };
        float x = (id.x - id.y) * (HexMetrics.innerRadius);
        float z = id.z * (HexMetrics.outerRadius * 1.5f);


        if (GeneratePineForestChance(x, z) < pineForestChance)
            type = "small_forest";
        if (GeneratePineForestChance(x, z) < pineForestChance * 0.76f)
            type = "medium_forest";
        if (GeneratePineForestChance(x, z) < pineForestChance * 0.4f)
            type = "large_forest";

        if (GenerateWaterChance(x, z) < waterChance)
        {
            
            type = "water0";

            if (GenerateWaterChance(HexToKartezjan(new Vector3(id.x - 1, id.y, id.z + 1)).x, HexToKartezjan(new Vector3(id.x - 1, id.y, id.z + 1)).z) < waterChance)
                neighbours[0] = 1;
            if (GenerateWaterChance(HexToKartezjan(new Vector3(id.x, id.y -1 , id.z + 1)).x, HexToKartezjan(new Vector3(id.x, id.y - 1, id.z + 1)).z) < waterChance)
                neighbours[1] = 1;
            if (GenerateWaterChance(HexToKartezjan(new Vector3(id.x + 1, id.y - 1, id.z)).x, HexToKartezjan(new Vector3(id.x + 1, id.y - 1, id.z)).z) < waterChance)
                neighbours[2] = 1;
            if (GenerateWaterChance(HexToKartezjan(new Vector3(id.x + 1, id.y, id.z - 1)).x, HexToKartezjan(new Vector3(id.x + 1, id.y, id.z - 1)).z) < waterChance)
                neighbours[3] = 1;
            if (GenerateWaterChance(HexToKartezjan(new Vector3(id.x, id.y + 1, id.z - 1)).x, HexToKartezjan(new Vector3(id.x, id.y + 1, id.z - 1)).z) < waterChance)
                neighbours[4] = 1;
            if (GenerateWaterChance(HexToKartezjan(new Vector3(id.x - 1, id.y + 1, id.z)).x, HexToKartezjan(new Vector3(id.x - 1, id.y + 1, id.z)).z) < waterChance)
                neighbours[5] = 1;

            string new_str = "";
            foreach (int num in neighbours)
                new_str += num.ToString();


            for (int i = 0; i < 6; i++)
            {
                if (WaterData.waterCodes.ContainsKey(new_str))
                {
                    type = WaterData.waterCodes[new_str];
                    break;
                }
                new_str = PushStringLeft(new_str);
                rotation += 1;
            }
            


        }


        return new KeyValuePair<int, string>(rotation, type);
    }


    public static string PushStringLeft(string strin)
    {
        string new_string = strin.Substring(1, 5);
        new_string += strin[0];
        return new_string;
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
