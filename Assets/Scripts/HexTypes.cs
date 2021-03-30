using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTypes : MonoBehaviour
{
    public static Dictionary<string, HexTypeClass> types = new Dictionary<string, HexTypeClass>
    {

        { "empty", new HexTypeClass("empty", "Empty") },
        { "transparent", new HexTypeClass("transparent", "Transparent") },
        { "grass", new HexTypeClass("grass", "Red") },
        { "water", new HexTypeClass("water", "Water") },
        { "medium_forest", new HexTypeClass("medium_forest", "Red") },
        { "small_forest", new HexTypeClass("small_forest", "Red") },
        { "large_forest", new HexTypeClass("large_forest", "Red") }

    };

}
