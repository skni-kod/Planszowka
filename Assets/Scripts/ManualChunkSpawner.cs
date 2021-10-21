using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualChunkSpawner : MonoBehaviour
{
    Plane plane = new Plane(new Vector3(0, 1, 0).normalized, Vector3.zero);
    HexGrid grid_reference;
    public int BrushSize = 2;

    void Start()
    {
        grid_reference = GetComponent<HexGrid>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float enter = 0.0f;

            if (plane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                Vector3 hitPoint = ray.GetPoint(enter);
                //grid_reference.CreateCellsRadius(new HexCords(hitPoint.x, hitPoint.z), BrushSize);
                grid_reference.GenerateChunk(HexCords.ChunkIdFromHexId(new HexCords(hitPoint.x, hitPoint.z)));

            }
        }
    }
}
