using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TreesData
{

    public static Dictionary<string, List<string>> trees = new Dictionary<string, List<string>>
    {

        { "small_forest", new List<string>() { "forest1", "forest1", "forest1"} },
        { "medium_forest", new List<string>() { "medium_tree" } },
        { "large_forest", new List<string>() { "big_tree" } },

    };

}
