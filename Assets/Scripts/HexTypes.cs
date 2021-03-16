using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTypes : MonoBehaviour
{
    public static Dictionary<int, HexTypeClass> types = new Dictionary<int, HexTypeClass>
    {

        { 0, new HexTypeClass(0, "Empty") },
        { 1, new HexTypeClass(1, "Transparent") },

    };

}
