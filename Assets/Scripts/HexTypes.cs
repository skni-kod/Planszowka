using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTypes : MonoBehaviour
{
    public static Dictionary<int, HexTypeClass> types = new Dictionary<int, HexTypeClass>
    {

        { 0, new HexTypeClass(0, "Empty", "Empty") },
        { 1, new HexTypeClass(1, "Transparent", "Transparent") },
        { 2, new HexTypeClass(2, "Green", "Red") },
        { 3, new HexTypeClass(3, "Water", "Water") },
        { 4, new HexTypeClass(4, "Forest", "Red") },

    };

}
