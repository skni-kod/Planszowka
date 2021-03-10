using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    List<Vector3> verts = new List<Vector3>();
    List<int> trings = new List<int>();
    public Material[] materials;
    public int type = 0;
    public HexGrid grid_reference;
    public float level = 0f;
    public float UPlevel = 0.1f;
    public float scaleUP = 1.0f;
    Mesh mesh;

    public GameObject[] neighbours;
    public Vector3 id;
    public bool settled = false;
    public string owner = "none";


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        ChangeType(1);
        CreateMesh();
        RecalculateMesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void DeleteHexModel()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    public void ChangeHexModel(int model_number)
    {
        DeleteHexModel();
        GameObject model = Instantiate<GameObject>(grid_reference.models[model_number]);
        model.transform.SetParent(transform, false);
        model.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z), transform.rotation);
    }

    public void PlaceHex()
    {
        settled = true;
        CreateNeighbours();
    }

    public void CreateNeighbours()
    {
        grid_reference.CreateCellsAround(id);
    }

    public void ChangeType(int number)
    {
        type = number;
        ChangeMaterial(HexTypes.types[type].material);
    }

    public void ChangeMaterial(int number)
    {
        GetComponent<MeshRenderer>().material = materials[number];
    }

    public void RotateRight(int number_of_times)
    {
        GetComponent<Transform>().Rotate(0, 60 * number_of_times, 0);
    }

    public void CreateMesh()
    {
        foreach (Vector3 vert in HexMetrics.corners)
            verts.Add(vert);
        foreach (int intt in HexMetrics.triangles)
            trings.Add(intt);
    }

    void RecalculateMesh()
    {
        mesh.Clear();
        mesh.vertices = verts.ToArray();
        mesh.triangles = trings.ToArray();
        mesh.RecalculateNormals();
    }

    public void PickUp(int model_number)
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x, UPlevel, transform.position.z), transform.rotation);
        transform.localScale = new Vector3(scaleUP, 1f, scaleUP);
        if (!settled)
            ChangeHexModel(model_number);
        ChangeType(0);
    }

    public void PutDown()
    {
        if (transform.position.y < UPlevel)
        {
            if (!settled)
            {
                ChangeType(0);
                DeleteHexModel();
            }
        }
        if (transform.position.y > level)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), transform.rotation);
        }
        else
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, level, transform.position.z), transform.rotation);
        }


        if (transform.localScale.x > 1f)
            transform.localScale = new Vector3(transform.localScale.x - 0.1f, 1f, transform.localScale.z - 0.1f);
    }


    void Update()
    {
        PutDown();
    }
}