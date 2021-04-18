using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;

public class HexGrid : MonoBehaviourPun
{
	[SerializeField]
	private int InitialGridSize = 10;
	public HexCell cellPrefab;
	public CameraScript camera_reference;

	public Dictionary<Vector3, HexCell> cells = new Dictionary<Vector3, HexCell>();

	void Awake()
	{
		//CreateCell(0, 0, 0, false);
		CreateCellsRadius(Vector3.zero, InitialGridSize);
		//CreateCellsRadius(Vector3.zero, 15, radius_from: 10);
		//CreateCellsRadius(Vector3.zero, 20, radius_from: 15);
	}
	
	#region GridCreation
	public void CreateCell(int x, int y, int z, bool place_ready=true)
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
			if (place_ready)
            {
				cell.PlaceHex(false);
            }
		}
	}

	public void CreateCellsRadius(Vector3 center_hex_id, int radius, int radius_from=0)
	{
		int x = (int)center_hex_id.x;
		int y = (int)center_hex_id.y;
		int z = (int)center_hex_id.z;
		CreateCell(x, y, z);
		for (int tier = radius_from; tier < radius; tier++)
        {
			for (int t = 0; t < tier; t++)
            {
				CreateCell(-tier, t, tier - t);
				CreateCell(tier, -t, -(tier - t));
				CreateCell(tier, -(tier - t), -t);
			}
			for (int t = 0; t < tier; t++)
			{
				CreateCell(t, -tier, tier - t);
				CreateCell(-t, tier, -(tier - t));
				CreateCell(-(tier - t), tier, -t);
			}
			for (int t = 0; t < tier; t++)
			{
				CreateCell(t, (tier - t), -tier);
				CreateCell(-t, -(tier - t), tier);
			}
		}
	}

		public void CreateCellsAround(Vector3 center_hex_id)
    {
		int x = (int)center_hex_id.x;
		int y = (int)center_hex_id.y;
		int z = (int)center_hex_id.z;
		CreateCell(x, y, z, false);
		CreateCell(x - 1, y + 1, z, false);
		CreateCell(x + 1, y - 1, z, false);
		CreateCell(x, y - 1, z + 1, false);
		CreateCell(x - 1, y, z + 1, false);
		CreateCell(x + 1, y, z - 1, false);
		CreateCell(x, y + 1, z - 1, false);
	}

	public void GenerateChunk(Vector3 center_hex_id)
    {
		//Generate 7 central chunks, and then CreateCellsAround() them
		int x = (int)center_hex_id.x;
		int y = (int)center_hex_id.y;
		int z = (int)center_hex_id.z;
		CreateCell(x, y, z);
		CreateCellsAround(new Vector3(x - 2, y + 2, z));
		CreateCellsAround(new Vector3(x + 2, y - 2, z));
		CreateCellsAround(new Vector3(x, y - 2, z + 2));
		CreateCellsAround(new Vector3(x - 2, y, z + 2));
		CreateCellsAround(new Vector3(x + 2, y, z - 2));
		CreateCellsAround(new Vector3(x, y + 2, z - 2));
	}


	#endregion

	#region CellManiplation

	[PunRPC]
	private void _placeHex(Vector3 hex_id, bool generate_neighbours)
    {
		cells[hex_id].PlaceHex(generate_neighbours);
    }

	[PunRPC]
	private void _placeBuilding(Vector3 hex_id)
    {
		cells[hex_id].PlaceBuilding(0);
    }

	[PunRPC]
	private void _claimHex(Vector3 hex_id, int owner_id)
	{
		cells[hex_id].SetOwner(owner_id);
	}

	[PunRPC]
	private void _changeType(Vector3 hex_id, string typee)
	{
		cells[hex_id].ChangeType(typee);
	}

	[PunRPC]
	private void _rotateBuilding(Vector3 hex_id)
	{
		cells[hex_id].RotateRight(1);
	}

	#endregion

	#region RpcCellHandler

	public void PlaceHexRPC(Vector3 hex_id, bool generate_neighbours)
    {
		this.photonView.RPC("_placeHex", RpcTarget.All, hex_id, generate_neighbours);
	}

	public void PlaceBuildingRPC(Vector3 hex_id)
    {
		this.photonView.RPC("_placeBuilding", RpcTarget.All, hex_id);
	}


	public void ClaimHexRPC(Vector3 hex_id, int owner_id)
    {
		this.photonView.RPC("_claimHex", RpcTarget.All, hex_id, owner_id);
	}

	public void ChangeTypeRPC(Vector3 hex_id, string typee)
	{
		this.photonView.RPC("_changeType", RpcTarget.All, hex_id, typee);
	}

	public void RotateBuildingRPC(Vector3 hex_id)
    {
		this.photonView.RPC("_rotateBuilding", RpcTarget.All, hex_id);
	}

	#endregion
}