using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTypes : MonoBehaviour
{
    public static Dictionary<string, HexTypeClass> types = new Dictionary<string, HexTypeClass>
    {

        { "empty", new HexTypeClass("empty", "Invisible") },
        { "transparent", new HexTypeClass("transparent", "Transparent") },
        { "grass", new HexTypeClass("grass", "Red") },
        { "water0", new HexTypeClass("water", "Invisible") },
        { "water1", new HexTypeClass("water", "Invisible") },
        { "water2", new HexTypeClass("water", "Invisible") },
        { "water3", new HexTypeClass("water", "Invisible") },
        { "water4", new HexTypeClass("water", "Invisible") },
        { "water5", new HexTypeClass("water", "Invisible") },
        { "water6", new HexTypeClass("water", "Invisible") },
        { "waterBioHazard", new HexTypeClass("water", "Invisible") },
        { "waterHourGlass", new HexTypeClass("water", "Invisible") },
        { "waterRAntenna", new HexTypeClass("water", "Invisible") },
        { "waterEars", new HexTypeClass("water", "Invisible") },
        { "waterLAntenna", new HexTypeClass("water", "Invisible") },
        { "waterThickHourGlass", new HexTypeClass("water", "Invisible") },
        { "waterUmbrella", new HexTypeClass("water", "Invisible") },
        { "medium_forest", new HexTypeClass("forest", "Red") },
        { "small_forest", new HexTypeClass("forest", "Red") },
        { "large_forest", new HexTypeClass("forest", "Red") }

    };

}
