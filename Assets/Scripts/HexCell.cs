using System.Collections.Generic;
using UnityEngine;


public class HexCell : MonoBehaviour
{
    List<Vector3> verts = new List<Vector3>();
    List<int> trings = new List<int>();
    public string type = "empty";
    public HexGrid grid_reference;
    public int[] neighbours;
    public Dictionary<Vector3, bool> places = new Dictionary<Vector3, bool>();
    Mesh mesh;

    public Vector3 id;
    public Vector3 kartezjan_pos = new Vector3(0, 0, 0);
    public bool settled = false;


    public bool highlighted = false;
    public bool perma_highlighted = false;
    public bool show_ownership;
    public GameObject model_reference;
    public GameObject highligth_model;
    public GameObject ownership_ring;
    public bool has_building;
    public int owner_id = 0;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        ChangeTypeInternal("empty");
        CreateMesh();
        RecalculateMesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        HandleHighlighting();
    }

    #region PlacingNature
    public void PlaceForest()
    {
        int forest_number = (int)Mathf.Floor((Mathf.PerlinNoise((HexMapGenerator.seed * 1.8f) + (kartezjan_pos.x * 10), kartezjan_pos.y * 10) * 10000) % TreesData.trees[type].Count);
        string forest_model_name = TreesData.trees[type][forest_number];
        RotateRight(Mathf.FloorToInt(Mathf.FloorToInt((Mathf.PerlinNoise(kartezjan_pos.x, kartezjan_pos.z) * 1000) % 5)));

        GameObject model = (GameObject)Instantiate(Resources.Load(string.Format("Trees/{0}", forest_model_name)));
        model.transform.SetParent(transform, false);
        model_reference = model;
    }

    public void PlaceWater()
    {
        int water_number = (int)Mathf.Floor((Mathf.PerlinNoise((HexMapGenerator.seed * 1.8f) + (kartezjan_pos.x * 10), kartezjan_pos.y * 10) * 10000) % WaterData.water[type].Count);
        string water_model_name = WaterData.water[type][water_number];

        GameObject model = (GameObject)Instantiate(Resources.Load(string.Format("Water/{0}", water_model_name)));
        model.transform.SetParent(transform, false);
        model_reference = model;
    }

    #endregion

    #region ExternalGridInterface

    public void SetOwner(int new_owner_id, bool show_ownership_ring=true)
    {
        SetOwnerInternal(new_owner_id, show_ownership_ring);
    }

    public void Highligth(bool permament=false)
    {
        if (permament)
            perma_highlighted = true;
        highlighted = true;
        highligth_model.SetActive(true);
    }

    public void UnPermaHighligth()
    {
        perma_highlighted = false;
        UnHighlight();
    }

    public void PlaceHex(bool generate_neighbours)
    {
        PlaceHexInternal(generate_neighbours);
    }

    public void CreateNeighbours()
    {
        CreateNeighboursInternal();
    }

    public void ChangeType(string typee)
    {
        ChangeTypeInternal(typee);
    }

    public void RotateRight(int number_of_times)
    {
        RotateRightInternal(number_of_times);
    }

    #endregion

    #region InternalGridMethods
    private void SetKartezjanPos()
    {
        kartezjan_pos.x = (id.x - id.y) * (HexMetrics.innerRadius);
        kartezjan_pos.z = id.z * (HexMetrics.outerRadius * 1.5f);
    }

    private void SetOwnerInternal(int new_owner_id, bool show_ownership_ring=true)
    {
        owner_id = new_owner_id;
        if (show_ownership_ring)
        {
            show_ownership = true;
            ownership_ring.SetActive(true);
        }

    }

    private void PlaceHexInternal(bool generate_neighbours)
    {
        SetKartezjanPos();
        settled = true;
        ChangeTypeInternal(HexMapGenerator.GenerateHexType(id).Value);
        RotateRight(HexMapGenerator.GenerateHexType(id).Key);



        if (HexTypes.types[type].type == "forest")
            PlaceForest();
        if (HexTypes.types[type].type == "water")
            PlaceWater();


        if (generate_neighbours)
            CreateNeighboursInternal();

    }

    private void HandleHighlighting()
    {
        if(highlighted && !perma_highlighted)
        {
            if (grid_reference.camera_reference.selected_object.id != id)
            {
                UnHighlight();
            }
        }
    }

    private void UnHighlight()
    {
        highlighted = false;
        highligth_model.SetActive(false);
    }

    private void CreateNeighboursInternal()
    {
        grid_reference.CreateCellsAround(id);
    }

    private void ChangeTypeInternal(string typee)
    {
        Destroy(model_reference);
        has_building = false;
        type = typee;
        ChangeMaterialInternal(HexTypes.types[typee].material_name);
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
        if (places.Count == 0 && type == "grass" && !has_building)
        {
            GameObject model = (GameObject)Instantiate(Resources.Load("Buildings/tartak"));
            model.transform.SetParent(transform, false);
            has_building = true;
            model_reference = model;
        }
    }

    #endregion

}