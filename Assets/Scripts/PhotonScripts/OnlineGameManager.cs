using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineGameManager : MonoBehaviourPun
{
    public GameObject ui_reference;
    public HexGrid grid_reference;
    public CameraScript camera_reference;
    public HexCell left_clicked_hex;

    void Start()
    {
        //GameObject newobj = PhotonNetwork.Instantiate("CameraHolder", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }   

    void Update()
    {
        HandleHexSelecting();
        if (Input.GetMouseButtonDown(1))
        {
            ui_reference.transform.position = Input.mousePosition;
            ui_reference.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ui_reference.SetActive(false);
        }
    }

    public void HandleHexSelecting()
    {
        if (camera_reference.selected_object != null)
        {
            HexCell hex = camera_reference.selected_object.GetComponent<HexCell>();
            if (Input.GetMouseButtonDown(1))
            {
                left_clicked_hex = hex;
            }
            if (Input.GetMouseButtonUp(0))
            {
                left_clicked_hex = null;
            }
        }  
    }

    public void PlaceBuilding()
    {
        if (left_clicked_hex != null)
        {
            grid_reference.PlaceBuildingRPC(left_clicked_hex.id);
        }
    }

}
