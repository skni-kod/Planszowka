using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TreesData
{

    public static Dictionary<string, List<string>> trees = new Dictionary<string, List<string>>
    {

        { "small_forest", new List<string>() { "forest_medium1"} },
        { "medium_forest", new List<string>() { "forest_medium2" } },
        { "large_forest", new List<string>() { "forest_medium3" } },

    };

}
