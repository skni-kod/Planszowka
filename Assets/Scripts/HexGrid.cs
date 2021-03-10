using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{

	public int width = 6;
	public int height = 6;

	public GameObject[] models;
	public HexCell cellPrefab;

	public Dictionary<Vector3, HexCell> cells = new Dictionary<Vector3, HexCell>();
	//HexCell[] cells;
	
	void Awake()
	{
		CreateCell(0, 0, 0);
		CreateCellsAround(new Vector3(0, 0, 0));
	}

	void Start()
	{
	}

	public void CreateCell(int x, int y, int z)
    {
		Vector3 id = new Vector3(x, y, z);
		if (!cells.ContainsKey(id))
        {
			Vector3 position;
			position.x = (x - y) * (HexMetrics.innerRadius);
			position.y = 0f;
			position.z = z * (HexMetrics.outerRadius * 1.5f);

			HexCell cell = cells[id] = Instantiate<HexCell>(cellPrefab);
			cell.GetComponent<HexCell>().grid_reference = this;
			cell.GetComponent<HexCell>().id = id;
			cell.transform.SetParent(transform, false);
			cell.transform.localPosition = position;
		}
	}

	public void CreateCellsAround(Vector3 center)
    {
		int x = (int)center.x;
		int y = (int)center.y;
		int z = (int)center.z;
		CreateCell(x - 1, y + 1, z);
		CreateCell(x + 1, y - 1, z);
		CreateCell(x, y - 1, z + 1);
		CreateCell(x - 1, y, z + 1);
		CreateCell(x + 1, y, z - 1);
		CreateCell(x, y + 1, z - 1);
	}

	/*public void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		position.x = (x + (z * 0.5f) - (z / 2)) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.GetComponent<HexCell>().grid_reference = this;
		cell.transform.SetParent(transform, false);
	/cell.transform.localPosition = position;
	}

	public void CreateField()
    {
		for (int z = 0, i = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, z, i++);
			}
		}
	}*/
}