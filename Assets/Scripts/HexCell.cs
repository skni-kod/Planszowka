using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    List<Vector3> verts = new List<Vector3>();
    List<int> trings = new List<int>();
    public Material[] materials;
    public int type = 0;
    public HexGrid grid_reference;
    private int internal_timer = 0;
    //public float level = 0f;
    //public float UPlevel = 0.1f;
    //public float scaleUP = 1.0f;
    Mesh mesh;

    public GameObject[] neighbours;
    public Vector3 id;
    public bool settled = false;
    public string owner = "none";


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        ChangeType(0);
        CreateMesh();
        RecalculateMesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        HandleInternalTimer();
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
        ChangeType(1);
        CreateNeighbours();
    }

    private void CreateNeighbours()
    {
        grid_reference.CreateCellsAround(id);
    }

    private void ChangeType(int number)
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

    private void CreateMesh()
    {
        foreach (Vector3 vert in HexMetrics.corners)
            verts.Add(vert);
        foreach (int intt in HexMetrics.triangles)
            trings.Add(intt);
    }

    private void RecalculateMesh()
    {
        mesh.Clear();
        mesh.vertices = verts.ToArray();
        mesh.triangles = trings.ToArray();
        mesh.RecalculateNormals();
    }

    public void ShowModel(int model_number)
    {
        internal_timer = 3;
        if (!settled)
            ChangeHexModel(model_number);
    }

    private void HandleInternalTimer()
    {
        if (internal_timer <= 0)
        {
            internal_timer = 0;
            if (!settled)
            {
                DeleteHexModel();
            }
        }
        else
            internal_timer -= 1;
    }
}