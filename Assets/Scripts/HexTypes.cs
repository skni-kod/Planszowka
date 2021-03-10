using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTypes : MonoBehaviour
{
    public static Dictionary<int, HexTypeClass> types = new Dictionary<int, HexTypeClass>();

    void Awake()
    {
        types.Add(0, new HexTypeClass(0, "Empty"));
        types.Add(1, new HexTypeClass(1, "Black"));
        types.Add(2, new HexTypeClass(2, "Picked"));
        types.Add(3, new HexTypeClass(3, "Settled"));
    }
}
