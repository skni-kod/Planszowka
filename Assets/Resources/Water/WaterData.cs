using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaterData
{

    public static Dictionary<string, string> waterCodes = new Dictionary<string, string>
    {
        { "000000", "water0" },
        { "100000", "water1" },
        { "110000", "water2" },
        { "111000", "water3" },
        { "111100", "water4" },
        { "111110", "water5" },
        { "111111", "water6" },
        { "101010", "waterBioHazard" },
        { "100100", "waterHourGlass" },
        { "110100", "waterRAntenna" },
        { "101000", "waterEars" },
        { "110010", "waterLAntenna" },
        { "110110", "waterThickHourGlass" },
        { "111010", "waterUmbrella" },
    };



    public static Dictionary<string, List<string>> water = new Dictionary<string, List<string>>
    {

        { "water0", new List<string>() { "water0", "water0" } },
        { "water1", new List<string>() { "water1" } },
        { "water2", new List<string>() { "water2" } },
        { "water3", new List<string>() { "water3" } },
        { "water4", new List<string>() { "water4" } },
        { "water5", new List<string>() { "water5" } },
        { "water6", new List<string>() { "water6" } },
        { "waterBioHazard", new List<string>() { "waterBioHazard" } },
        { "waterHourGlass", new List<string>() { "waterHourGlass" } },
        { "waterRAntenna", new List<string>() { "waterRAntenna" } },
        { "waterEars", new List<string>() { "waterEars" } },
        { "waterLAntenna", new List<string>() { "waterLAntenna" } },
        { "waterThickHourGlass", new List<string>() { "waterThickHourGlass" } },
        { "waterUmbrella", new List<string>() { "waterUmbrella" } },

    };

}