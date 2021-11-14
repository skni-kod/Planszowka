using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexChunkObject : MonoBehaviour
{
    public HexChunk data;
    public List<HexCell> cells = new List<HexCell>();
    public  HexGrid hex_grid_ref;




    public void GenerateCells()
    {
        foreach (Vector3 cell_id in data.cells_ids)
        {
            HexCell cell = hex_grid_ref.ReturnCreateCell(cell_id, transform);
            cells.Add(cell);
            cell.transform.SetParent(transform, false);  
        }
    }



}
