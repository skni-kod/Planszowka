using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManipulator : MonoBehaviour
{
    public Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        for (int i = 0; i < 15; i++)
        {
            Debug.Log(mesh.triangles[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector2[] FixUv3(Vector2[] arr)
    {
        Vector2[] new_arr = new Vector2[arr.Length];
        int counter = 0;
        for (int i = 0; i<arr.Length; i++)
        {
            if (counter == 5)
                new_arr[i] = new Vector2(4.0f, 0.0f);
            if (counter == 3)
                new_arr[i] = new Vector2(3.0f, 0.0f);
            if (counter == 4)
                new_arr[i] = new Vector2(5.0f, 0.0f);
            else
                new_arr[i] = new Vector2((float)counter, 0.0f);
            counter++;
            if (counter > 5)
                counter = 0;
        }

        return new_arr;
    }

    void AddSquare(int posX, int posZ)
    {
        int how_much = mesh.vertices.Length;
        Vector3[] arr_verts = new Vector3[how_much + 6];
        int[] arr_trngs = new int[how_much + 6];
        Vector2[] arr_uv3 = new Vector2[how_much + 6];
        mesh.vertices.CopyTo(arr_verts, 0);
        mesh.triangles.CopyTo(arr_trngs, 0);
        mesh.uv3.CopyTo(arr_uv3, 0);

        arr_trngs[how_much + 0] = how_much + 0;
        arr_trngs[how_much + 1] = how_much + 1;
        arr_trngs[how_much + 2] = how_much + 2;
        arr_trngs[how_much + 3] = how_much + 3;
        arr_trngs[how_much + 4] = how_much + 4;
        arr_trngs[how_much + 5] = how_much + 5;

        arr_verts[how_much + 0] = new Vector3(arr_verts[0].x + posX, arr_verts[0].y, arr_verts[0].z + posZ);
        arr_verts[how_much + 1] = new Vector3(arr_verts[1].x + posX, arr_verts[1].y, arr_verts[1].z + posZ);
        arr_verts[how_much + 2] = new Vector3(arr_verts[2].x + posX, arr_verts[2].y, arr_verts[2].z + posZ);
        arr_verts[how_much + 3] = new Vector3(arr_verts[3].x + posX, arr_verts[3].y, arr_verts[3].z + posZ);
        arr_verts[how_much + 4] = new Vector3(arr_verts[4].x + posX, arr_verts[4].y, arr_verts[4].z + posZ);
        arr_verts[how_much + 5] = new Vector3(arr_verts[5].x + posX, arr_verts[5].y, arr_verts[5].z + posZ);

        arr_uv3[how_much + 0] = mesh.uv3[0];
        arr_uv3[how_much + 1] = mesh.uv3[1];
        arr_uv3[how_much + 2] = mesh.uv3[2];
        arr_uv3[how_much + 3] = mesh.uv3[3];
        arr_uv3[how_much + 4] = mesh.uv3[4];
        arr_uv3[how_much + 5] = mesh.uv3[5];

        mesh.vertices = arr_verts;
        mesh.triangles = arr_trngs;
        mesh.uv3 = arr_uv3;
    }

    void Add()
    {
        Vector3[] arr_verts = new Vector3[6];
        int[] arr_trngs = new int[6];
        mesh.vertices.CopyTo(arr_verts, 0);
        mesh.triangles.CopyTo(arr_trngs, 0);

        arr_trngs[3] = 3;
        arr_trngs[4] = 4;
        arr_trngs[5] = 5;

        arr_verts[3] = new Vector3(-49.0f, 0.0f, -49.0f);
        arr_verts[4] = new Vector3(-49.0f, 0.0f, -50.0f);
        arr_verts[5] = new Vector3(-50.0f, 0.0f, -49.0f);

        mesh.vertices = arr_verts;
        mesh.triangles = arr_trngs;
    }
}
