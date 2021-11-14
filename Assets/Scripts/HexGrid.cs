using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;


public class HexGrid : MonoBehaviourPun
{
	[SerializeField]
	private int InitialGridSize = 10;
	public HexCell cellPrefab;
	public HexChunkObject chunkprefab;
	public CameraScript camera_reference;

	public Dictionary<Vector3, HexCell> cells = new Dictionary<Vector3, HexCell>();
	public Dictionary<Vector3, HexChunkObject> chunks = new Dictionary<Vector3, HexChunkObject>();


	void Awake()
	{
<<<<<<< Updated upstream
		//CreateCell(0, 0, 0, false);
		GenerateChunk(new HexCords(0, 0, 0));
		GenerateChunk(new HexCords(1, 0, -1));
		/*GenerateChunk(new HexCords(0, -1, 1));
		GenerateChunk(new HexCords(-1, 0, 1));
		GenerateChunk(new HexCords(0, 1, -1));
		GenerateChunk(new HexCords(-1, 1, 0));
		GenerateChunk(new HexCords(1, -1, 0));*/
		//CreateCellsRadius(Vector3.zero, 15, radius_from: 10);
		//CreateCellsRadius(Vector3.zero, 20, radius_from: 15);
=======
		GenerateChunk(new HexCords(0, 0, 0));
>>>>>>> Stashed changes
	}
	
	#region GridCreation
	public void CreateCell(int x, int y, int z, bool place_ready=true)
    {
		Vector3 id = new Vector3(x, y, z);
		//First we check if this cell isn't already created before, just in case
		if (!cells.ContainsKey(id))
        {
<<<<<<< Updated upstream
			/*Vector3 position;
			position.x = (x - y) * (HexMetrics.innerRadius);
			position.y = 0f;
			position.z = z * (HexMetrics.outerRadius * 1.5f);*/

=======
>>>>>>> Stashed changes
			HexCell cell = cells[id] = Instantiate<HexCell>(cellPrefab);
			cell.GetComponent<HexCell>().grid_reference = this;
			cell.GetComponent<HexCell>().id = id;
			cell.transform.SetParent(transform, false);
			cell.transform.localPosition = HexMetrics.HexToCartesianCords(x, y, z); ;
			if (place_ready)
            {
				cell.PlaceHex(false);
            }
		}
	}

<<<<<<< Updated upstream
	public void CreateCell(HexCords cords, bool place_ready = true)
	{
=======
	public HexCell ReturnCreateCell(int x, int y, int z, Transform _parent)
	{
		Vector3 id = new Vector3(x, y, z);
		return ReturnCreateCell(id, _parent);
	}

	public HexCell ReturnCreateCell(Vector3 id, Transform _parent)
	{
		//First we check if this cell isn't already created before, just in case

		HexCell cell = cells[id] = Instantiate<HexCell>(cellPrefab);
		cell.GetComponent<HexCell>().grid_reference = this;
		cell.GetComponent<HexCell>().id = id;
		cell.transform.SetParent(_parent, false);
		cell.transform.localPosition = HexMetrics.HexToCartesianCords((int)id.x, (int)id.y, (int)id.z); ;
		cell.PlaceHex(false);
		return cell;

	}

	public void CreateCell(HexCords cords, bool place_ready = true)
	{
>>>>>>> Stashed changes
		Vector3 id = cords.hex_crds;
		//First we check if this cell isn't already created before, just in case
		if (!cells.ContainsKey(id))
		{
			HexCell cell = cells[id] = Instantiate<HexCell>(cellPrefab);
			cell.GetComponent<HexCell>().grid_reference = this;
			cell.GetComponent<HexCell>().id = id;
			cell.transform.SetParent(transform, false);
			cell.transform.localPosition = cords.crt_crds; ;
			if (place_ready)
			{
				cell.PlaceHex(false);
			}
		}
	}

	public void CreateCellsRadius(HexCords center_hex_id, int radius, int radius_from = 0)
	{
		int x = (int)center_hex_id.hex_crds.x;
		int y = (int)center_hex_id.hex_crds.y;
		int z = (int)center_hex_id.hex_crds.z;
		CreateCell(x, y, z);
		for (int tier = radius_from; tier < radius; tier++)
		{
			for (int t = 0; t < tier; t++)
			{
				CreateCell(-tier + x, t + y, tier - t + z);
				CreateCell(tier + x, -t + y, -(tier - t) + z);
				CreateCell(tier + x, -(tier - t) + y, -t + z);
			}
			for (int t = 0; t < tier; t++)
			{
				CreateCell(t + x, -tier + y, tier - t + z);
				CreateCell(-t + x, tier + y, -(tier - t) + z);
				CreateCell(-(tier - t) + x, tier + y, -t + z);
			}
			for (int t = 0; t < tier; t++)
			{
				CreateCell(t + x, (tier - t) + y, -tier + z);
				CreateCell(-t + x, -(tier - t) + y, tier + z);
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

	public void GenerateChunk(HexCords chunk_id)
    {
<<<<<<< Updated upstream
		HexCords center = HexCords.FromChunkId(chunk_id);
		CreateCellsRadius(center, 6);
		foreach (Vector3 cord in HexCords.CornersIdsFromCenterId(center))
        {
			CreateCell((int)cord.x, (int)cord.y, (int)cord.z);
        }
=======
		if (!chunks.ContainsKey(chunk_id.hex_crds))
        {
			HexChunk chunk = new HexChunk(chunk_id);
			HexChunkObject chunk_obj = Instantiate(chunkprefab);
			chunks.Add(chunk_id.hex_crds, chunk_obj);
			chunk_obj.transform.SetParent(transform, false);
			chunk_obj.data = chunk;
			chunk_obj.hex_grid_ref = this;
			chunk_obj.GenerateCells();
		}
>>>>>>> Stashed changes
	}

	public void GenerateChunk(Vector3 chunk_id)
    {
		GenerateChunk(new HexCords(chunk_id));
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