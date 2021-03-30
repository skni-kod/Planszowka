using System.Collections.Generic;
using UnityEngine;


public class HexCell : MonoBehaviour
{
    List<Vector3> verts = new List<Vector3>();
    List<int> trings = new List<int>();
    public int type = 0;
    public HexGrid grid_reference;
    public Dictionary<Vector3, bool> places = new Dictionary<Vector3, bool>();
    Mesh mesh;

    public Vector3 id;
    public bool settled = false;
    public bool has_building;
    public string owner = "none";


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        ChangeTypeInternal(0);
        CreateMesh();
        RecalculateMesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    #region PlacingTrees
    public void PlaceForest()
    {
        GameObject model = (GameObject)Instantiate(Resources.Load("Trees/forest1"));
        model.transform.SetParent(transform, false);
    }

    #endregion

    #region ExternalGridInterface

    public void PlaceHex(bool generate_neighbours)
    {
        PlaceHexInternal(generate_neighbours);
    }

    public void CreateNeighbours()
    {
        CreateNeighboursInternal();
    }

    public void ChangeType(int number)
    {
        ChangeTypeInternal(number);
    }

    public void ChangeMaterial(string name)
    {
        ChangeMaterialInternal(name);
    }

    public void RotateRight(int number_of_times)
    {
        RotateRightInternal(number_of_times);
    }

    #endregion

    #region InternalGridMethods
    private void PlaceHexInternal(bool generate_neighbours)
    {
        settled = true;
        ChangeTypeInternal(HexMapGenerator.GenerateHexType(id));
        
        if (type == 4)
            PlaceForest();


        if (generate_neighbours)
            CreateNeighboursInternal();

    }

    private void CreateNeighboursInternal()
    {
        grid_reference.CreateCellsAround(id);
    }

    private void ChangeTypeInternal(int number)
    {
        if (!(number == 0 && settled == true))
        {
            type = number;
            ChangeMaterialInternal(HexTypes.types[type].material_name);
        }
    }

    private void ChangeMaterialInternal(string name)
    {
        GetComponent<MeshRenderer>().material = Resources.Load<Material>(string.Format("Materials/{0}", name));
    }

    private void RotateRightInternal(int number_of_times)
    {
        GetComponent<Transform>().Rotate(0, 60 * number_of_times, 0);
    }
    #endregion

    #region MeshStuff

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

    #endregion

    #region PlacingBuildings

    public void PlaceBuilding(int building_id)
    {
        if (places.Count == 0 && type == 2 && !has_building)
        {
            GameObject model = (GameObject)Instantiate(Resources.Load("Buildings/tartak"));
            model.transform.SetParent(transform, false);
            has_building = true;
        }
    }

    #endregion

    #region ModelMethods
    /*
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
    */
    #endregion
}