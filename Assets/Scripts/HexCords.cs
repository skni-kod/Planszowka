using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexCords
{
    public Vector3 hex_crds;
    public Vector3 crt_crds;

    public HexCords()
    {
        hex_crds = Vector3.zero;
        crt_crds = Vector3.zero;
    }

    public HexCords(Vector3 hex_coordinates)
    {
        hex_crds = hex_coordinates;
        recalculateCart();
    }

    public HexCords(int x, int y, int z)
    {
        hex_crds = new Vector3(x, y, z);
        recalculateCart();
    }

    private void recalculateCart()
    {
        crt_crds = HexMetrics.HexToCartesianCords((int)hex_crds.x, (int)hex_crds.y, (int)hex_crds.z);
    }

    private void recalculateHex()
    {
        hex_crds = HexMetrics.CartesianToHexCords(crt_crds);
    }

<<<<<<< Updated upstream
=======
    public static List<Vector3> getCorrectedHexCords(Vector3 hex_cords)
    {
        List<Vector3> list_of_correct_cords = new List<Vector3>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (checkIntegrity((int)hex_cords.x + x, (int)hex_cords.y + y, (int)hex_cords.z + z))
                        list_of_correct_cords.Add(new Vector3((int)hex_cords.x + x, (int)hex_cords.y + y, (int)hex_cords.z + z));
                        
                }
            }
        }
        return list_of_correct_cords;
    }

>>>>>>> Stashed changes
    public bool checkIntegrity()
    {
        if (hex_crds.x + hex_crds.y + hex_crds.z == 0)
            return true;
        return false;
    }

<<<<<<< Updated upstream
=======
    public static bool checkIntegrity(int x, int y, int z)
    {
        if (x + y + z == 0)
            return true;
        return false;
    }

>>>>>>> Stashed changes
    public HexCords(float x, float z)
    {
        crt_crds = new Vector3(x, 0, z);
        recalculateHex();
        recalculateCart();
        
    }

    public HexCords hex_offset(int x, int y, int z)
    {
        return new HexCords(new Vector3(hex_crds.x + x, hex_crds.y + y, hex_crds.z + z));
    }

    public HexCords hex_offset(Vector3 offset)
    {
        return new HexCords(hex_crds + offset);
    }

    public static HexCords FromChunkId(HexCords chunk_id, int ox=0, int oy=0, int oz=0)
    {
        HexCords h_x = new HexCords((chunk_id.hex_crds * 11) + new Vector3(ox, oy, oz));
        return h_x;
    }

    public static Vector3[] CornerHexIdsFromCenterId(HexCords center, bool mirror, int rotation = 0)
    {
        Vector3[] cordy = new Vector3[5];
        int g = 1;
        if (mirror)
            g = -1;

        int[] xx = { -6, -6, -6, -6, -7 };
        int[] yy = { 1, 2, 3, 4, 3 };
        int[] zz = { 5, 4, 3, 2, 4 };

        int[][] lb;

        if (rotation == 0)
            lb = new int[][] { xx, yy, zz };
        else if (rotation == 1)
            lb = new int[][] { zz, xx, yy };
        else
            lb = new int[][] { yy, zz, xx };


        cordy[0] = center.hex_offset(lb[0][0] * g, lb[1][0] * g, lb[2][0] * g).hex_crds;
        cordy[1] = center.hex_offset(lb[0][1] * g, lb[1][1] * g, lb[2][1] * g).hex_crds;
        cordy[2] = center.hex_offset(lb[0][2] * g, lb[1][2] * g, lb[2][2] * g).hex_crds;
        cordy[3] = center.hex_offset(lb[0][3] * g, lb[1][3] * g, lb[2][3] * g).hex_crds;
        cordy[4] = center.hex_offset(lb[0][4] * g, lb[1][4] * g, lb[2][4] * g).hex_crds;
        return cordy;
    }

    public static Vector3[] CornersIdsFromCenterId(HexCords center)
    {
        Vector3[] cordy_all = new Vector3[30];
        Vector3[] cords1 = CornerHexIdsFromCenterId(center, false, 0);
        Vector3[] cords2 = CornerHexIdsFromCenterId(center, false, 1);
        Vector3[] cords3 = CornerHexIdsFromCenterId(center, false, 2);
        Vector3[] cords4 = CornerHexIdsFromCenterId(center, true, 0);
        Vector3[] cords5 = CornerHexIdsFromCenterId(center, true, 1);
        Vector3[] cords6 = CornerHexIdsFromCenterId(center, true, 2);
        cords1.CopyTo(cordy_all, 0);
        cords2.CopyTo(cordy_all, 5);
        cords3.CopyTo(cordy_all, 10);
        cords4.CopyTo(cordy_all, 15);
        cords5.CopyTo(cordy_all, 20);
        cords6.CopyTo(cordy_all, 25);
        return cordy_all;

    }

<<<<<<< Updated upstream
    public static HexCords ChunkIdFromHexId(HexCords hex_id)
    {
=======
    public static HexChunk CheckWhichChunkPointBelongsTo(float x_cart, float z_cart)
    {
        Vector3 point = new Vector3(x_cart, 0 , z_cart);

        List<HexChunk> chunk_list_to_check = new List<HexChunk>();

        HexCords hex_id = new HexCords(x_cart, z_cart);


        int x = (int)hex_id.hex_crds.x % 11;
        int chunk_x = ((int)hex_id.hex_crds.x - x) / 11;
        int y = (int)hex_id.hex_crds.y % 11;
        int chunk_y = ((int)hex_id.hex_crds.y - y) / 11;
        int z = (int)hex_id.hex_crds.z % 11;
        int chunk_z = ((int)hex_id.hex_crds.z - z) / 11;


        HexCords chunk_id = new HexCords(chunk_x, chunk_y, chunk_z);
        
        if (checkIntegrity(chunk_x, chunk_y, chunk_z))
        {
            foreach (Vector3 neighbour_chunk_id in CreateCellsRadiusIDsList(chunk_id, 2))
            {
                chunk_list_to_check.Add(new HexChunk(neighbour_chunk_id));
            }

            foreach (HexChunk chunk in chunk_list_to_check)
            {
                if (chunk.cells_ids.Contains(hex_id.hex_crds))
                {
                    return chunk;
                }
            }
        }
        else
        {
            List<Vector3> chunkers = getCorrectedHexCords(chunk_id.hex_crds);
            foreach(Vector3 hex_chunk_id in chunkers)
            {
                foreach (Vector3 neighbour_chunk_id in CreateCellsRadiusIDsList(new HexCords(hex_chunk_id), 2))
                {
                    chunk_list_to_check.Add(new HexChunk(neighbour_chunk_id));
                }

                foreach (HexChunk chunk in chunk_list_to_check)
                {
                    if (chunk.cells_ids.Contains(hex_id.hex_crds))
                    {
                        return chunk;
                    }
                }
            }

        }




        return new HexChunk(99, 99, 99);
    }

    public static List<Vector3> GenerateChunkIDsList(HexCords chunk_id)
    {
        List<Vector3> id_list = new List<Vector3>();

        HexCords center = HexCords.FromChunkId(chunk_id);

        id_list.AddRange(CreateCellsRadiusIDsList(center, 6));
        id_list.AddRange(HexCords.CornersIdsFromCenterId(center));
        return id_list;
    }

    private static void SafeListAdd(List<Vector3> list, Vector3 item)
    {
        if (!list.Contains(item))
            list.Add(item);
    }

    public static List<Vector3> CreateCellsRadiusIDsList(HexCords center_hex_id, int radius, int radius_from = 0)
    {
        List<Vector3> id_list = new List<Vector3>();

        int x = (int)center_hex_id.hex_crds.x;
        int y = (int)center_hex_id.hex_crds.y;
        int z = (int)center_hex_id.hex_crds.z;
        id_list.Add(new Vector3(x, y, z));

        for (int tier = radius_from; tier < radius; tier++)
        {
            for (int t = 0; t < tier; t++)
            {
                SafeListAdd(id_list, new Vector3(-tier + x, t + y, tier - t + z));
                SafeListAdd(id_list, new Vector3(tier + x, -t + y, -(tier - t) + z));
                SafeListAdd(id_list, new Vector3(tier + x, -(tier - t) + y, -t + z));
            }
            for (int t = 0; t < tier; t++)
            {
                SafeListAdd(id_list, new Vector3(t + x, -tier + y, tier - t + z));
                SafeListAdd(id_list, new Vector3(-t + x, tier + y, -(tier - t) + z));
                SafeListAdd(id_list, new Vector3(-(tier - t) + x, tier + y, -t + z));
            }
            for (int t = 0; t < tier; t++)
            {
                SafeListAdd(id_list, new Vector3(t + x, (tier - t) + y, -tier + z));
                SafeListAdd(id_list, new Vector3(-t + x, -(tier - t) + y, tier + z));
            }
        }
        return id_list;
    }

    public static HexCords ChunkIdFromHexId(HexCords hex_id)
    {

>>>>>>> Stashed changes
        int x = (int)hex_id.hex_crds.x % 11;
        int chunk_x = ((int)hex_id.hex_crds.x - x) / 11;
        int y = (int)hex_id.hex_crds.y % 11;
        int chunk_y = ((int)hex_id.hex_crds.y - y) / 11;
        int z = (int)hex_id.hex_crds.z % 11;
        int chunk_z = ((int)hex_id.hex_crds.z - z) / 11;

        HexCords chunk_id = new HexCords(chunk_x, chunk_y, chunk_z);
        if (!chunk_id.checkIntegrity())
<<<<<<< Updated upstream
            return new HexCords();
=======
        {
            return new HexCords();
        }

>>>>>>> Stashed changes

        int x2 = 0;
        int y2 = 0;
        int z2 = 0;

        if (x <= 5 && x >= -5)
            x2 = 1;
        if (y <= 5 && y >= -5)
            y2 = 1;
        if (z <= 5 && z >= -5)
            z2 = 1;

        if (x2 + y2 + z2 == 3)
        {
            return chunk_id;
        }
   
        else if (x2 + y2 + z2 == 2)
        {
            if (x2 == 1)
            {
                if (z > 0)
                    return chunk_id.hex_offset(0, -1, 1);
                else
                    return chunk_id.hex_offset(0, 1, -1);
            }
            if (y2 == 1)
            {
                if (z > 0)
                    return chunk_id.hex_offset(-1, 0, 1);
                else
                    return chunk_id.hex_offset(1, 0, -1);
            }
            if (z2 == 1)
            {
                if (y > 0)
                    return chunk_id.hex_offset(-1, 1, 0);
                else
                    return chunk_id.hex_offset(1, -1, 0);  
            }
        }

        else
        {
            HexCords hex = new HexCords(x, y, z);
            Debug.Log(hex.hex_crds);
            if (HexCords.CornersIdsFromCenterId(chunk_id).Contains(hex.hex_crds))
                return chunk_id;
            if (HexCords.CornersIdsFromCenterId(chunk_id.hex_offset(11, -11, 0)).Contains(hex.hex_crds))
                return chunk_id.hex_offset(1, -1, 0);
            if (HexCords.CornersIdsFromCenterId(chunk_id.hex_offset(-11, 11, 0)).Contains(hex.hex_crds))
                return chunk_id.hex_offset(-1, 1, 0);
            if (HexCords.CornersIdsFromCenterId(chunk_id.hex_offset(11, 0, -11)).Contains(hex.hex_crds))
                return chunk_id.hex_offset(1, 0, -1);
            if (HexCords.CornersIdsFromCenterId(chunk_id.hex_offset(-11, 0, 11)).Contains(hex.hex_crds))
                return chunk_id.hex_offset(-1, 0, 1);
            if (HexCords.CornersIdsFromCenterId(chunk_id.hex_offset(0, -11, 11)).Contains(hex.hex_crds))
                return chunk_id.hex_offset(0, -1, 1);
            if (HexCords.CornersIdsFromCenterId(chunk_id.hex_offset(0, 11, -11)).Contains(hex.hex_crds))
                return chunk_id.hex_offset(0, 1, -1);
        }


        return new HexCords();
    }

    

}
