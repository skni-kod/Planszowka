using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexChunk
{
    public Vector3 hex_id;
    public List<Vector3> cells_ids = new List<Vector3>();


    public HexChunk(HexCords heex_id)
    {
        hex_id = heex_id.hex_crds;
        GenerateCellsIDs();
    }

    public HexChunk(Vector3 heex_id)
    {
        hex_id = heex_id;
        GenerateCellsIDs();
    }

    public HexChunk(int id_x, int id_y, int id_z)
    {
        hex_id = new Vector3(id_x, id_y, id_z);
        GenerateCellsIDs();
    }
    
    private void GenerateCellsIDs()
    {
        //Fills cells_ids with hex coords of the cells which are supposed to be in the chunk

        HexCords center_cell = HexCords.FromChunkId(new HexCords(hex_id));

        foreach (Vector3 cell_id in HexMetrics.cells_in_chunk)
        {
            cells_ids.Add(cell_id + center_cell.hex_crds);
        }

        /*HexCords center_cell = HexCords.FromChunkId(new HexCords(hex_id));

        foreach (Vector3 cord in HexCords.CreateCellsRadiusIDsList(center_cell, 6))
        {
            cells_ids.Add(new Vector3((int)cord.x, (int)cord.y, (int)cord.z));
        }
        foreach (Vector3 cord in HexCords.CornersIdsFromCenterId(center_cell))
        {
            cells_ids.Add(new Vector3((int)cord.x, (int)cord.y, (int)cord.z));
        }*/
    }


}
